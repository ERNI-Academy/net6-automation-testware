using Microsoft.Extensions.DependencyInjection;
using TestWare.Core.Configuration;
using System.Reflection;
using TestWare.Core.Interfaces;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using System;

namespace TestWare.Core;

static public class TestWareProvider
{
    static private IServiceProvider _testWarePovider;
    private static readonly IServiceCollection _testWareServices;

    static TestWareProvider()
    {
        _testWareServices = new ServiceCollection();
        _testWarePovider = _testWareServices.BuildServiceProvider();
    }

    public static T GetTestWareComponent<T>() where T : notnull => _testWarePovider.GetRequiredService<T>();
    public static object GetTestWareComponent(Type T) => _testWarePovider.GetRequiredService(T);

    public static object GetTestWareComponent(Type T, string key) => _testWarePovider.GetRequiredKeyedService(T, key);

    static public void CreateScope(string[] scopes)
    {
        _testWarePovider.CreateScope();
        var s = (TestWareScopes)_testWarePovider.GetRequiredService(typeof(TestWareScopes));
        s.CurrentScopes = scopes;
    }

    static public object GetTestWareComponentFromTags(Type T, string[] tags, out string consumedTag)
    {
        foreach(var key in tags)
        {
            try
            {
                
                var foundComponent = _testWarePovider.GetRequiredKeyedService(T, key);
                consumedTag = key;
                return foundComponent;
            }
            catch
            {
                continue;
            }

        }
        consumedTag = string.Empty;
        return _testWarePovider.GetRequiredService(T);
    }

    static public T GetTestWareComponent<T>(string key) where T : notnull => _testWarePovider.GetRequiredKeyedService<T>(key);

    static public void RegisterTestWareComponents(IEnumerable<Assembly> extraAssemblies) => RegisterTestWareComponents(new TestWareConfiguration(), extraAssemblies);
    static public void RegisterTestWareComponents() => RegisterTestWareComponents(new TestWareConfiguration(), []);

    static public void RegisterTestWareComponents(string config_file) => RegisterTestWareComponents(ConfigurationManager.ReadConfigurationFile(config_file), []);
    static public void RegisterTestWareComponents(string config_file, IEnumerable<Assembly> extraAssemblies) => RegisterTestWareComponents(ConfigurationManager.ReadConfigurationFile(config_file), extraAssemblies);

    static private void RegisterTestWareComponents(ITestWareConfiguration config, IEnumerable<Assembly> extraAssemblies)
    {
        var assemblies = GetDomainAndReferencedAssemblies(extraAssemblies);

        _testWareServices.AddSingleton(typeof(TestWareScopes));
        //Register configuration
        _testWareServices.AddSingleton(config);

        //Register Test Cockpits by reflection
        var cockpitImplementedInterfaces = GetInterfacesAndImplementations<ITestWareCockpit>(assemblies);
        RegisterImplementations(_testWareServices, cockpitImplementedInterfaces);
        RegisterConfiguredImplementations(_testWareServices, cockpitImplementedInterfaces, config.CockpitScopes);

        //Register Test engines by reflection
        var testEngineImplementedInterfaces = GetInterfacesAndImplementations<ITestWareEngine>(assemblies);
        RegisterImplementations(_testWareServices, testEngineImplementedInterfaces);
        var keyedImplementations = RegisterConfiguredImplementations(_testWareServices, testEngineImplementedInterfaces, config.Scopes);

        //Register Test components
        var componentImplementedInterfaces = GetInterfacesAndImplementations<ITestwareComponent>(assemblies);
        foreach(var componentRegistration in componentImplementedInterfaces)
        {
            
            _testWareServices.AddScoped(componentRegistration.Key.AsType(), componentRegistration.Key);
            foreach (var keyedImplementation in keyedImplementations)
            {
                if (!componentRegistration.Key.GetConstructors().Any(c => c.GetParameters().Any(p => p.ParameterType.IsAssignableFrom(keyedImplementation.Value))))
                    continue;

                _testWareServices.AddKeyedScoped(
                    componentRegistration.Key,
                    keyedImplementation.Key,
                    (provider, key) =>
                    {
                        var parameters = Array.Empty<object>();
                        var keyedService = provider.GetRequiredKeyedService(keyedImplementation.Value, key);
                        return ActivatorUtilities.CreateInstance(provider, componentRegistration.Key.AsType(), [keyedService]);
                    });

                foreach (var interface_ in componentRegistration.Value)
                {
                    _testWareServices.AddScoped(interface_, componentRegistration.Key);

                  
                    _testWareServices.AddKeyedScoped(
                        interface_,
                        keyedImplementation.Key,
                        (provider, key) => {
                            return provider.GetRequiredKeyedService(componentRegistration.Key, key);
                        });

                }
            }
        }
         _testWarePovider = _testWareServices.BuildServiceProvider();
    }
    static private List<Assembly> GetDomainAndReferencedAssemblies(IEnumerable<Assembly> extraAssemblies)
    {
        var assembliesCount = -1;
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        extraAssemblies.
            Distinct()
            .Where(e => assemblies.Any(a => a.FullName == e.FullName) == false)
            .ToList()
            .ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(x.GetName())));

        do
        {
            assembliesCount = assemblies.Count;
            assemblies.SelectMany(x => x.GetReferencedAssemblies())
                .Distinct()
                .Where(y => assemblies.Any((a) => a.FullName == y.FullName) == false)
                .ToList()
                .ForEach(x => AppDomain.CurrentDomain.Load(x));

            assemblies = [.. AppDomain.CurrentDomain.GetAssemblies()];
        }
        while (assembliesCount != assemblies.Count);

        return assemblies;
    }
    private static Dictionary<TypeInfo, TypeInfo[]> GetInterfacesAndImplementations<T>(IEnumerable<Assembly> assemblies)
    {
        var implementedInterfaces = new Dictionary<TypeInfo, TypeInfo[]>();

        var definedTypes = assemblies.Where(a => !a.IsDynamic)
                            .Distinct()
                            .SelectMany(a => a.DefinedTypes);

        var interfaces = definedTypes.Where(
                                    t => t.IsInterface
                                    && t.ImplementedInterfaces.Any(i => i.FullName == typeof(T).FullName)
                                   ).ToList();

        interfaces.Insert(0, typeof(T).GetTypeInfo());

        foreach ( var interface_ in interfaces)
        {
            var implementations = definedTypes.Where(
                            t => t.IsClass
                            && !t.IsAbstract
                            && t.ImplementedInterfaces.Any(i => i.FullName == interface_.FullName)
                           ).ToList();
            implementations.ForEach(impl => implementedInterfaces[impl]  = implementedInterfaces.GetValueOrDefault(impl)?.Append(interface_)?.ToArray() ?? [interface_]);
        }
        
        return implementedInterfaces;
    }


    static private List<TypeInfo> RegisterImplementationWithInterfaces(IServiceCollection serviceCollection, TypeInfo implementation, IEnumerable<TypeInfo> interfaces)
    {
        var matchInterfaces = new List<TypeInfo>();
        foreach (var interface_ in interfaces)
        {
            if (implementation.ImplementedInterfaces.Any(i => i.FullName == (interface_.AsType().FullName)))
            {
                serviceCollection.AddTransient(interface_.AsType(), provider => { return Activator.CreateInstance(implementation.AsType())!; });
                matchInterfaces.Add(interface_);
            }
        }
        return matchInterfaces;
    }

    static private void RegisterImplementations(IServiceCollection serviceCollection, IDictionary<TypeInfo, TypeInfo[]> implementedInterfaces)
    {

        foreach (var registration in implementedInterfaces)
        {
            foreach (var interface_ in registration.Value)
            {
                serviceCollection.AddTransient(interface_.AsType(), provider => { return ActivatorUtilities.CreateInstance(provider, registration.Key.AsType()); });
            }
        }
    }

    static private void RegisterInterfacesConfiguredAndKeyed(IServiceCollection serviceCollection, TypeInfo implementation, TypeInfo[] interfaces, string key, JsonObject config)
    {
        serviceCollection.AddSingleton(implementation.AsType());

        serviceCollection.AddKeyedSingleton(
            implementation.AsType(),
            key,
            (provider, key) => { return ActivatorUtilities.CreateInstance(provider, implementation.AsType(),config); }
        );
        foreach (var interface_ in interfaces)
        {
            serviceCollection.AddSingleton(
                interface_.AsType(),
                provider => { return provider.GetRequiredKeyedService(implementation.AsType(), key); }
            );


            serviceCollection.AddKeyedSingleton(
                interface_.AsType(),
                key,
                (provider, key) => { return provider.GetRequiredKeyedService(implementation.AsType(), key); }
            );
        }
    }
    static private Dictionary<string, Type> RegisterConfiguredImplementations(IServiceCollection serviceCollection, IDictionary<TypeInfo, TypeInfo[]> targets, IEnumerable<ConfigurationScope> scopes)
    {
        var keyedImplementations = new Dictionary<string, Type>();
        if (scopes?.Count() > 0)
        {
            foreach (var target in targets)
            {
                var coreName = target.Key.GetField("Name")?.GetValue(null)?.ToString();
                foreach (var scope in scopes.Where(scope => scope.CoreName == coreName))
                {
                    RegisterInterfacesConfiguredAndKeyed(serviceCollection, target.Key, target.Value, scope.ScopeName, scope.Config);
                    keyedImplementations[scope.ScopeName] = target.Key;
                };
            }
        }
        return keyedImplementations;
    }
}

// TODO: MOVE ELSEWhere
internal class TestWareScopes
{
    internal string[] CurrentScopes { get; set; } = [];
}


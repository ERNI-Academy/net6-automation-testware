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

//public interface ITestwareComponent
//{
//    public Guid Id { get; set; }
//    public ITestWareEngine Engine { get; }
//}
//public class SeleniumPage: ITestwareComponent
//{
//    public Guid Id = Guid.NewGuid();
//    public ITestWareEngine Engine { get;}
//    Guid ITestwareComponent.Id { get => Id; set => throw new NotImplementedException(); }

//    public SeleniumPage(ITestWareEngine engine) 
//    {
//        Engine = engine;
//    }
//}

static public class TestWareProvider
{
    static private IServiceProvider TestWarepPovider;
    static private IServiceCollection TestWareServices;
    //static private IServiceProvider TestProvider;
    //static private IServiceCollection TestServices;

    static TestWareProvider()
    {
        TestWareServices = new ServiceCollection();
    }

    //static public T GetTestComponent<T>()
    //{
    //    return TestProvider.GetRequiredService<T>();
    //}
    
    //static public T GetTestComponent<T>(string key)
    //{
    //    return TestProvider.GetRequiredKeyedService<T>(key);
    //}

    static public T GetTestWareComponent<T>()
    {
        return TestWarepPovider.GetRequiredService<T>();
    }
    static public object GetTestWareComponent(Type T)
    {
        return TestWarepPovider.GetRequiredService(T);
    }

    static public object GetTestWareComponent(Type T, string key)
    {
        return TestWarepPovider.GetRequiredKeyedService(T, key);
    }

    static public object GetTestWareComponentFromTags(Type T, string[] tags, out string consumedTag)
    {
        foreach(var key in tags)
        {
            try
            {
                var foundComponent = TestWarepPovider.GetRequiredKeyedService(T, key);
                consumedTag = key;
                return foundComponent;
            }
            catch
            {
                continue;
            }

        }
        consumedTag = string.Empty;
        //var a = TestWarepPovider.GetRequiredKeyedService(T, "swagLabs");
        return TestWarepPovider.GetRequiredService(T);
    }

    static public T GetTestWareComponent<T>(string key)
    {
        return TestWarepPovider.GetRequiredKeyedService<T>(key);
    }

    static public void RegisterTestWareComponents(IEnumerable<Assembly> extraAssemblies) => RegisterTestWareComponents(new TestWareConfiguration(), extraAssemblies);
    static public void RegisterTestWareComponents() => RegisterTestWareComponents(new TestWareConfiguration(), []);

    static public void RegisterTestWareComponents(string config_file) => RegisterTestWareComponents(ConfigurationManager.ReadConfigurationFile(config_file), []);
    static public void RegisterTestWareComponents(string config_file, IEnumerable<Assembly> extraAssemblies) => RegisterTestWareComponents(ConfigurationManager.ReadConfigurationFile(config_file), extraAssemblies);

    static private void RegisterTestWareComponents(ITestWareConfiguration config, IEnumerable<Assembly> extraAssemblies)
    {
        var assemblies = GetDomainAndReferencedAssemblies(extraAssemblies);

        //Register configuration
        TestWareServices.AddSingleton(config);

        //Register Test Cockpits by reflection
        //var (cockpitInterfaces, cockpitImplementations) = GetInterfacesAndImplementations<ITestWareCockpit>(assemblies);
        //var cockpitRegistrations = RegisterImplementations(TestWareServices, cockpitImplementations, cockpitInterfaces);
        //RegisterConfiguredImplementations(TestWareServices, cockpitRegistrations, config.CockpitScopes);
        var cockpitImplementedInterfaces = GetInterfacesAndImplementations<ITestWareCockpit>(assemblies);
        RegisterImplementations(TestWareServices, cockpitImplementedInterfaces);
        RegisterConfiguredImplementations(TestWareServices, cockpitImplementedInterfaces, config.CockpitScopes);

        //Register Test engines by reflection
        var testEngineImplementedInterfaces = GetInterfacesAndImplementations<ITestWareEngine>(assemblies);
        RegisterImplementations(TestWareServices, testEngineImplementedInterfaces);
        RegisterConfiguredImplementations(TestWareServices, testEngineImplementedInterfaces, config.Scopes);

        //Register Test components
        var componentImplementedInterfaces = GetInterfacesAndImplementations<ITestwareComponent>(assemblies);
        foreach(var componentRegistration in componentImplementedInterfaces)
        {

            TestWareServices.AddScoped(componentRegistration.Value.First(), componentRegistration.Key);

            foreach (var scope in config.Scopes)
            {
                TestWareServices.AddKeyedScoped(
                    componentRegistration.Key,
                    scope.ScopeName,
                    (provider, key) =>
                    {
                        var parameters = new object[0];
                        var keyedService = provider.GetKeyedService<ITestWareEngine>(key);
                        if (keyedService is not null) parameters = parameters.Append(keyedService).ToArray();
                        return ActivatorUtilities.CreateInstance(provider, componentRegistration.Key.AsType(), parameters);
                    });
            }

            foreach (var interface_ in componentRegistration.Value)
            {
                TestWareServices.AddScoped(interface_, componentRegistration.Key);

                foreach (var scope in config.Scopes)
                {
                    TestWareServices.AddKeyedScoped(
                        interface_,
                        scope.ScopeName,
                        (provider, key) => provider.GetRequiredKeyedService(componentRegistration.Key.AsType(), key));
                }
            }


        }
        //var componentRegistrations = RegisterImplementations(TestWareServices, componentImplementations, componentInterfaces);
        //RegisterConfiguredImplementations(TestWareServices, componentRegistrations, config.Scopes);
        TestWarepPovider = TestWareServices.BuildServiceProvider();
    }

    //public static void RegisterTestComponents()
    //{
    //    TestServices = new ServiceCollection();

    //    var instance = TestWarepPovider.GetRequiredKeyedService<ITestWareEngine>("scenario01");
    //    TestServices.AddScoped<ITestWareEngine>(provider => { return instance; });
    //    TestServices.AddKeyedScoped<ITestWareEngine>("scenario01", (provider, key) => { return instance; });

    //    var instance2 = TestWarepPovider.GetRequiredKeyedService<ITestWareEngine>("scenario02");
    //    TestServices.AddScoped<ITestWareEngine>(provider => { return instance2; });
    //    TestServices.AddKeyedScoped<ITestWareEngine>("scenario02", (provider, key) => { return instance; });

    //    //TestServices.AddScoped<ITestWareEngine, PlaywrightEngine>();

    //    TestServices.AddSingleton<ITestwareComponent, SeleniumPage>();

    //    TestServices.AddKeyedSingleton<ITestwareComponent, SeleniumPage>("scenario01", 
    //        (provider, key) => {
    //            var parameters = new object[0];
    //            var keyedService = provider.GetKeyedService<ITestWareEngine>(key);
    //            if (keyedService is not null) parameters = parameters.Append(keyedService).ToArray();
    //            return ActivatorUtilities.CreateInstance<SeleniumPage>(provider,parameters);
    //        }
    //    );

    //    TestProvider = TestServices.BuildServiceProvider();
    //}


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

            assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
        while (assembliesCount != assemblies.Count);

        return assemblies;
    }

    //static private (IEnumerable<TypeInfo> Interfaces, IEnumerable<TypeInfo> Implementations) GetInterfacesAndImplementations<T>(IEnumerable<Assembly> assemblies)
    //{
    //    var definedTypes = assemblies.Where(a => !a.IsDynamic)
    //                        .Distinct()
    //                        .SelectMany(a => a.DefinedTypes);

    //    var Interfaces = definedTypes.Where(
    //                                t => t.IsInterface
    //                                && t.ImplementedInterfaces.Any(i => i.FullName == typeof(T).FullName)
    //                               ).ToList();

    //    var Implementations = definedTypes.Where(
    //                                t => t.IsClass
    //                                && t.ImplementedInterfaces.Any(i => i.FullName == typeof(T).FullName)
    //                               ).ToList();
    //    Interfaces.Insert(0, typeof(T).GetTypeInfo());

    //    var a = definedTypes.Where(x => x.FullName.StartsWith("TestWare")).ToList();
    //    var b = assemblies.Where(x => x.FullName.StartsWith("TestWare")).ToList();

    //    return (Interfaces, Implementations);
    //}
    static private IDictionary<TypeInfo, TypeInfo[]> GetInterfacesAndImplementations<T>(IEnumerable<Assembly> assemblies)
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
                            && t.ImplementedInterfaces.Any(i => i.FullName == interface_.FullName)
                           ).ToList();
            implementations.ForEach(impl => implementedInterfaces[impl]  = implementedInterfaces.GetValueOrDefault(impl)?.Append(interface_)?.ToArray() ?? [interface_]);
        }
        
        return implementedInterfaces;
    }


    static private IEnumerable<TypeInfo> RegisterImplementationWithInterfaces(IServiceCollection serviceCollection, TypeInfo implementation, IEnumerable<TypeInfo> interfaces)
    {
        var matchInterfaces = new List<TypeInfo>();
        foreach (var interface_ in interfaces)
        {
            if (implementation.ImplementedInterfaces.Any(i => i.FullName == (interface_.AsType().FullName)))
            {
                serviceCollection.AddTransient(interface_.AsType(), provider => { return Activator.CreateInstance(implementation.AsType()); });
                matchInterfaces.Add(interface_);
            }
        }
        return matchInterfaces;
    }

    //static private IDictionary<TypeInfo, IEnumerable<TypeInfo>> RegisterImplementations(IServiceCollection serviceCollection, IEnumerable<TypeInfo> implementations, IEnumerable<TypeInfo> interfaces)
    //{
    //    var implementationRegistrations = new Dictionary<TypeInfo, IEnumerable<TypeInfo>>();
    //    foreach (var implementation in implementations)
    //    {
    //        var matchInterfaces = RegisterImplementationWithInterfaces(serviceCollection, implementation, interfaces);
    //        implementationRegistrations.Add(implementation, matchInterfaces);
    //    }
    //    return implementationRegistrations;
    //}
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

    //static private void RegisterInterfacesConfiguredAndKeyed(IServiceCollection serviceCollection, TypeInfo implementation, IEnumerable<TypeInfo> interfaces, string key, JsonObject config)
    //{
    //    //var instance = ActivatorUtilities.CreateInstance(provider, implementation.GetType(), parameters);
    //    var instance = Activator.CreateInstance(implementation.AsType(), config)!;
    //    foreach (var interface_ in interfaces)
    //    {
    //        serviceCollection.AddSingleton(
    //            interface_.AsType(),
    //            provider => { return instance; }
    //        );


    //        serviceCollection.AddKeyedSingleton(
    //            interface_.AsType(),
    //            key,
    //            (provider, key) => { return instance; }
    //        );
    //    }
    //}

    static private void RegisterInterfacesConfiguredAndKeyed(IServiceCollection serviceCollection, TypeInfo implementation, TypeInfo[] interfaces, string key, JsonObject config)
    {
        serviceCollection.AddSingleton(
            interfaces.First().AsType(),
            provider => { return provider.GetRequiredKeyedService(implementation.AsType(), key); }
        );

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

    //static private void RegisterConfiguredImplementations(IServiceCollection serviceCollection, IDictionary<TypeInfo, IEnumerable<TypeInfo>> targets, IEnumerable<ConfigurationScope> scopes)
    //{
    //    if (scopes?.Count() > 0)
    //    {
    //        foreach (var target in targets)
    //        {
    //            var coreName = target.Key.GetField("Name")?.GetValue(null)?.ToString();
    //            var targetScopes = scopes.Where(scope => scope.CoreName == coreName).ToList();
    //            targetScopes.ForEach(scope => RegisterInterfacesConfiguredAndKeyed(serviceCollection, target.Key, target.Value, scope.ScopeName, scope.Config));
    //        }
    //    }
    //}
    static private void RegisterConfiguredImplementations(IServiceCollection serviceCollection, IDictionary<TypeInfo, TypeInfo[]> targets, IEnumerable<ConfigurationScope> scopes)
    {
        if (scopes?.Count() > 0)
        {
            foreach (var target in targets)
            {
                var coreName = target.Key.GetField("Name")?.GetValue(null)?.ToString();
                var targetScopes = scopes.Where(scope => scope.CoreName == coreName).ToList();
                targetScopes.ForEach(scope => RegisterInterfacesConfiguredAndKeyed(serviceCollection, target.Key, target.Value, scope.ScopeName, scope.Config));
            }
        }
    }
}



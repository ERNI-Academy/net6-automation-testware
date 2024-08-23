using Microsoft.Extensions.DependencyInjection;
using TestWare.Core.Configuration;
using System.Reflection;
using TestWare.Core.Interfaces;
using System.Text.Json.Nodes;

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

    /// <summary>
    /// Retrieves a test ware component of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <returns>
    /// An instance of the specified type <typeparamref name="T"/>.
    /// </returns>
    /// <remarks>
    /// This method retrieves a required service of the specified type from the `_testWareProvider`.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required service of type <typeparamref name="T"/> cannot be retrieved from the provider.
    /// </exception>
    public static T GetTestWareComponent<T>() where T : notnull => _testWarePovider.GetRequiredService<T>();
    /// <summary>
    /// Retrieves a test ware component of the specified type.
    /// </summary>
    /// <param name="T">The <see cref="Type"/> of the component to retrieve.</param>
    /// <returns>
    /// An instance of the specified type <paramref name="T"/>.
    /// </returns>
    /// <remarks>
    /// This method retrieves a required service of the specified type from the `_testWareProvider`.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required service of type <paramref name="T"/> cannot be retrieved from the provider.
    /// </exception>
    public static object GetTestWareComponent(Type T) => _testWarePovider.GetRequiredService(T);
    /// <summary>
    /// Retrieves a test ware component of the specified type and key.
    /// </summary>
    /// <param name="T">The <see cref="Type"/> of the component to retrieve.</param>
    /// <param name="key">The key associated with the component to retrieve.</param>
    /// <returns>
    /// An instance of the specified type <paramref name="T"/> associated with the given key <paramref name="key"/>.
    /// </returns>
    /// <remarks>
    /// This method retrieves a required keyed service of the specified type from the `_testWareProvider` using the provided key.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required keyed service of type <paramref name="T"/> cannot be retrieved from the provider using the specified key.
    /// </exception>
    public static object GetTestWareComponent(Type T, string key) => _testWarePovider.GetRequiredKeyedService(T, key);

    /// <summary>
    /// Creates a new test ware scope and sets the current scopes.
    /// </summary>
    /// <param name="scopes">An array of strings representing the scopes to be set.</param>
    /// <remarks>
    /// This method creates a new scope using the `_testWareProvider` and sets the current scopes in the `TestWareScopes` service.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required service <see cref="TestWareScopes"/> cannot be retrieved from the provider.
    /// </exception>
    static public void CreateScope(string[] scopes)
    {
        _testWarePovider.CreateScope();
        var s = (TestWareScopes)_testWarePovider.GetRequiredService(typeof(TestWareScopes));
        s.CurrentScopes = scopes;
    }

    /// <summary>
    /// Retrieves a test ware component of the specified type using the provided tags.
    /// </summary>
    /// <param name="T">The <see cref="Type"/> of the component to retrieve.</param>
    /// <param name="tags">An array of strings representing the tags to search for the component.</param>
    /// <param name="consumedTag">Outputs the tag that was successfully used to retrieve the component, or an empty string if no tag was successful.</param>
    /// <returns>
    /// An object representing the found test ware component. If no component is found using the tags, a default component of the specified type is returned.
    /// </returns>
    /// <remarks>
    /// This method attempts to retrieve a component from the `_testWareProvider` using each tag in the provided array.
    /// If a component is found for a tag, it is returned and the tag is output via the <paramref name="consumedTag"/> parameter.
    /// If no component is found for any tag, a default component of the specified type is returned.
    /// </remarks>    
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="T"/> or <paramref name="tags"/> is null.
    /// </exception>
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

    /// <summary>
    /// Retrieves a test ware component of the specified type and key.
    /// </summary>
    /// <typeparam name="T">The type of the component to retrieve.</typeparam>
    /// <param name="key">The key associated with the component to retrieve.</param>
    /// <returns>
    /// An instance of the specified type <typeparamref name="T"/> associated with the given key <paramref name="key"/>.
    /// </returns>
    /// <remarks>
    /// This method retrieves a required keyed service of the specified type from the `_testWareProvider` using the provided key.
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown when the required keyed service of type <typeparamref name="T"/> cannot be retrieved from the provider using the specified key.
    /// </exception>
    static public T GetTestWareComponent<T>(string key) where T : notnull => _testWarePovider.GetRequiredKeyedService<T>(key);
    /// <summary>
    /// Registers test ware components using the provided extra assemblies.
    /// </summary>
    /// <param name="extraAssemblies">A collection of additional assemblies to include in the registration process.</param>
    /// <remarks>
    /// This method registers test ware components by calling the overloaded method with a new instance of <see cref="TestWareConfiguration"/> and the provided extra assemblies.
    /// </remarks>
    static public void RegisterTestWareComponents(IEnumerable<Assembly> extraAssemblies) => RegisterTestWareComponents(new TestWareConfiguration(), extraAssemblies);
    /// <summary>
    /// Registers test ware components using a default configuration and no extra assemblies.
    /// </summary>
    /// <remarks>
    /// This method registers test ware components using a default configuration and no additional assemblies.
    /// </remarks>
    static public void RegisterTestWareComponents() => RegisterTestWareComponents(new TestWareConfiguration(), []);
    /// <summary>
    /// Registers test ware components using the specified configuration file.
    /// </summary>
    /// <param name="config_file">The path to the configuration file.</param>
    /// <remarks>
    /// This method reads the configuration from the specified file and registers the test ware components using the configuration.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the specified configuration file cannot be found.
    /// </exception>
    /// <exception cref="ConfigurationException">
    /// Thrown when there is an error reading the configuration file.
    /// </exception>
    static public void RegisterTestWareComponents(string config_file) => RegisterTestWareComponents(ConfigurationManager.ReadConfigurationFile(config_file), []);
    /// <summary>
    /// Registers test ware components using the specified configuration file and additional assemblies.
    /// </summary>
    /// <param name="config_file">The path to the configuration file.</param>
    /// <param name="extraAssemblies">A collection of additional assemblies to include in the registration process.</param>
    /// <remarks>
    /// This method reads the configuration from the specified file and registers the test ware components using the configuration and the provided additional assemblies.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the specified configuration file cannot be found.
    /// </exception>
    /// <exception cref="ConfigurationException">
    /// Thrown when there is an error reading the configuration file.
    /// </exception>
    static public void RegisterTestWareComponents(string config_file, IEnumerable<Assembly> extraAssemblies) => RegisterTestWareComponents(ConfigurationManager.ReadConfigurationFile(config_file), extraAssemblies);

    /// <summary>
    /// Registers test ware components, cockpits, and engines into the service collection based on the provided configuration and assemblies.
    /// </summary>
    /// <param name="config">The <see cref="ITestWareConfiguration"/> containing the configuration for test ware components.</param>
    /// <param name="extraAssemblies">An enumerable collection of additional assemblies to include in the registration process.</param>
    /// <remarks>
    /// This method performs the following actions:
    /// - Retrieves all domain and referenced assemblies, including any extra assemblies provided.
    /// - Registers the <see cref="TestWareScopes"/> and configuration into the service collection.
    /// - Registers test cockpits, engines, and components by reflection based on the provided configuration.
    /// - Configures keyed and scoped services for components that have constructors accepting keyed services.
    /// </remarks>
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
    /// <summary>
    /// Retrieves a list of all assemblies in the current application domain, including referenced assemblies and additional assemblies provided.
    /// </summary>
    /// <param name="extraAssemblies">An enumerable collection of additional assemblies to include.</param>
    /// <returns>
    /// A list of <see cref="Assembly"/> objects representing all assemblies in the current application domain, including the provided extra assemblies and their references.
    /// </returns>
    /// <remarks>
    /// This method loads any additional assemblies provided and ensures all referenced assemblies are included in the returned list.
    /// </remarks>
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
    /// <summary>
    /// Retrieves a dictionary of interfaces and their corresponding implementations from the provided assemblies.
    /// </summary>
    /// <typeparam name="T">The type of the interface to search for.</typeparam>
    /// <param name="assemblies">A collection of assemblies to search for interfaces and implementations.</param>
    /// <returns>
    /// A dictionary where the keys are <see cref="TypeInfo"/> objects representing the implementations, and the values are arrays of <see cref="TypeInfo"/> objects representing the interfaces implemented by the keys.
    /// </returns>
    /// <remarks>
    /// This method searches the provided assemblies for interfaces that implement the specified type <typeparamref name="T"/> and their corresponding class implementations.
    /// </remarks>
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

    /// <summary>
    /// Registers an implementation with its matching interfaces in the service collection.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to which the implementation and interfaces will be registered.</param>
    /// <param name="implementation">The <see cref="TypeInfo"/> of the implementation to register.</param>
    /// <param name="interfaces">An <see cref="IEnumerable{TypeInfo}"/> of interfaces to be matched and registered with the implementation.</param>
    /// <returns>
    /// A list of <see cref="TypeInfo"/> representing the interfaces that were successfully matched and registered.
    /// </returns>
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

    /// <summary>
    /// Registers implementations and their corresponding interfaces in the service collection.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection"/> to which the services will be added.</param>
    /// <param name="implementedInterfaces">A dictionary where the keys are the implementations and the values are arrays of the interfaces they implement.</param>
    /// <remarks>
    /// This method registers each interface to be resolved to its corresponding implementation using transient lifetime.
    /// </remarks>
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

    /// <summary>
    /// Registers configured implementations and their keyed services in the service collection.
    /// </summary>
    /// <param name="serviceCollection">The service collection to which the services will be added.</param>
    /// <param name="targets">A dictionary where the key is the type of the implementation and the value is an array of interface types implemented by the key type.</param>
    /// <param name="scopes">A collection of configuration scopes used to configure the implementations.</param>
    /// <returns>
    /// A dictionary where the key is the scope name and the value is the type of the implementation registered for that scope.
    /// </returns>
    /// <remarks>
    /// This method registers implementations and their interfaces in the service collection based on the provided configuration scopes.
    /// </remarks>
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

    /// <summary>
    /// Registers configured implementations for the specified targets and scopes.
    /// </summary>
    /// <param name="serviceCollection">The service collection to register the implementations with.</param>
    /// <param name="targets">A dictionary where the keys are target <see cref="TypeInfo"/> and the values are arrays of <see cref="TypeInfo"/> representing the implementations.</param>
    /// <param name="scopes">An enumerable collection of <see cref="ConfigurationScope"/> representing the scopes to register.</param>
    /// <returns>
    /// A dictionary where the keys are scope names and the values are the corresponding target types.
    /// </returns>
    /// <remarks>
    /// This method registers implementations for the specified targets and scopes in the provided service collection.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="serviceCollection"/>, <paramref name="targets"/>, or <paramref name="scopes"/> is null.
    /// </exception>
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


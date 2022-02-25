using Autofac;
using Autofac.Core;
using System.Collections.Concurrent;
using System.Reflection;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;

namespace TestWare.Core;
public static class ContainerManager
{
    public static IContainer Container { get; private set; }
    private static ContainerBuilder _builder = new();
    private static readonly List<Assembly> _assemblies = new();
    private static List<DependencyInfo> _dependencies = new();
    private static readonly  Dictionary<string, ILifetimeScope> _scopes = new();
    
    internal static void RegisterTestwareComponents(IEnumerable<Assembly> assemblies)
    {
        if (assemblies != null && assemblies.Any())
        {
            _assemblies.AddRange(assemblies);
        }
    }

    /// <summary>
    /// This method resolves a TestWare Component. It creates a new instance first time is called
    /// and resolve to the same instance on the subsequent calls
    /// </summary>
    /// <typeparam name="T"> Type of the requested instance</typeparam>
    /// <returns> Current instance of the type T. New one in case the instance don't exists</returns>
    /// <exception cref="ArgumentNullException"> In case is not possible to resolve to the instance</exception>
    public static T GetTestWareComponent<T>()
    {
        return Container.Resolve<T>() ?? throw new DependencyResolutionException(nameof(T));
    }

    /// <summary>
    /// It returns an instance of the TestWare Component based on the config Name. First time it is
    /// called a new instance is created for this specific name. Same instance is returned on subsequent
    /// calls.
    /// </summary>
    /// <typeparam name="T"> type of the instance to return</typeparam>
    /// <param name="name"> Config name for the instance dependency injection</param>
    /// <returns> Current instance with the dependency name injected. New one if not exists</returns>
    /// <exception cref="ArgumentNullException"> In case is not possible to resolve or named don't exists</exception>
    public static T GetTestWareComponent<T>(string name)
    {
        var dependency = _dependencies.FirstOrDefault(x => x.Name.ToUpperInvariant() == name.ToUpperInvariant()) ?? throw new DependencyResolutionException(nameof(T));
        if (!_scopes.TryGetValue(name, out ILifetimeScope scope))
        {
            scope = Container.BeginLifetimeScope(name);
            _scopes[name] = scope;
        }
        var parameter = new ResolvedParameter(
                (pi, ctx) => pi.ParameterType == dependency.InstanceType,
                (pi, ctx) => scope.ResolveNamed(dependency.Name, pi.ParameterType)
            );
        var testwareComponent = scope.Resolve<T>(parameter) ?? throw new DependencyResolutionException(nameof(T));
        return testwareComponent;
    }
    
    /// <summary>
    /// Resolve to multiple Instances that correspond with the valids names provided. In case a name is provided
    /// and is not possible to resolve with it; it si ignored.
    /// </summary>
    /// <typeparam name="T">type of the instance to resolve</typeparam>
    /// <param name="names">list of names used for the dependency injection</param>
    /// <returns>IEnumerable of T. With the instances of all the resolved objects</returns>
    /// <exception cref="ArgumentNullException"> In case no object has been resolved</exception>
    public static IEnumerable<T> GetTestWareComponents<T>(IEnumerable<string> names)
    {
        IEnumerable<T> testwareComponents = Enumerable.Empty<T>();
        foreach (string name in names)
        {
            try
            {
                var testwarecomponent = GetTestWareComponent<T>(name);
                testwareComponents = testwareComponents.Append(testwarecomponent);
            }
            catch (DependencyResolutionException)
            {
                // Do nothing, keep the loop.
            }
        }
        if (!testwareComponents.Any())
        {
            throw new DependencyResolutionException(nameof(T));
        }
        return testwareComponents;
    }

    private static void RegisterTestwareComponents()
    {
        var interfaces = _assemblies.Where(a => !a.IsDynamic)
                                    .Distinct()
                                    .SelectMany(a => a.DefinedTypes)
                                    .Where(t => t.IsInterface && t.ImplementedInterfaces.Any(i => i.FullName == typeof(ITestWareComponent).FullName)).ToList();

        _builder.RegisterAssemblyTypes(_assemblies.ToArray())
               .Where(t => t.IsClass &&
                          !t.IsAbstract &&
                           (interfaces.Any(i => t.GetTypeInfo().ImplementedInterfaces.Any(ii => ii.FullName == i.FullName)) ||
                           t.GetTypeInfo().ImplementedInterfaces.Any(ii => ii.FullName == typeof(ITestWareComponent).FullName)))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
    }
    public static bool ExistsType(Type type)
    {
        return _dependencies.Any(x => x.GetType() == type);
    }

    public static void RegisterType<T>(string name, T instance) where T : class
    {
        var info = new DependencyInfo(name, instance, typeof(T));
        _dependencies.Add(info);
    }

    public static void BuildContainer()
    {
        _builder = new ContainerBuilder();
        RegisterTestwareComponents();
        RegisterInstances();
        Container = _builder.Build();
    }

    public static void DisposeContainer()
    {
        foreach (KeyValuePair<string, ILifetimeScope> scope in _scopes)
        {
            scope.Value.Dispose();
        }
        _scopes.Clear();
        _dependencies = new();
        Container.Dispose();
    }

    private static void RegisterInstances()
    { 
        _dependencies.ForEach(dep => _builder.RegisterInstance(dep.Instance).AsImplementedInterfaces().Named(dep.Name, dep.InstanceType));
    }

    public static string GetNameFromInstance<T>(T instance)
    {
        return _dependencies.FirstOrDefault(x => x.Instance.Equals(instance)).Name;
    }
}


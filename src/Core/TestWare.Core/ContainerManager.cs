using Autofac;
using System.Collections.Concurrent;
using System.Reflection;
using TestWare.Core.Interfaces;

namespace TestWare.Core
{
    public static class ContainerManager
    {
        public static IContainer Container { get; private set; }
        private static ContainerBuilder _builder = new ContainerBuilder();
        private static List<Assembly> _assemblies= new();
        private static ConcurrentDictionary<Type,object> _instances = new();
        internal static void RegisterTestwareComponents(IEnumerable<Assembly> assemblies)
        {
            if (assemblies != null && assemblies.Any())
            {
                _assemblies.AddRange(assemblies);
                RegisterTestwareComponents();

            }
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
                               (interfaces.Any(i => t.GetTypeInfo().ImplementedInterfaces.Any(ii=> ii.FullName == i.FullName)) || 
                               t.GetTypeInfo().ImplementedInterfaces.Any(ii=> ii.FullName == typeof(ITestWareComponent).FullName)))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }

        public static bool ExistsType(Type type)
        {
            return _instances.ContainsKey(type);
        }

        public static void RegisterType<T>(T instance) where T : class
        {
            if(!_instances.ContainsKey(instance.GetType()))
            {
                _instances.TryAdd(instance.GetType(), instance);
            }
        }

        public static void BuildContainer()
        {
            _builder = new ContainerBuilder();
            RegisterTestwareComponents();
            RegisterInstances();
            Container = _builder.Build();
            _instances = new();
        }

        private static void RegisterInstances()
        {
            _instances.Select(item => item.Value).ToList().ForEach(i => _builder.RegisterInstance(i).AsImplementedInterfaces());
        }
    }
}

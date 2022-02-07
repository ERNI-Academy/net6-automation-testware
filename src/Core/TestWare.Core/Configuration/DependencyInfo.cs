namespace TestWare.Core.Configuration;

internal class DependencyInfo
{
    internal object Instance { get; set; }
    internal Type InstanceType { get; set; }
    internal string Name { get; set; }

    internal DependencyInfo(string name, object instance, Type instanceType)
    {
        Instance = instance;
        InstanceType = instanceType;
        Name = name;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Core.Attributes;

public static class TestWareAttributes
{

    public static Dictionary<string, IEnumerable<string>> GetTestWareDocDict(string className, string methodName)
    {
        Type? class_ = AppDomain.CurrentDomain.GetAssemblies().AsParallel()
            .Select(a => a.GetType(className)).FirstOrDefault(t => t != null);

        MethodInfo? method = class_?.GetMethod(methodName);
        return GetTestWareDocDict(class_, method);
    }
    public static Dictionary<string, IEnumerable<string>> GetTestWareDocDict(Type? class_, MethodInfo? method)
    {
        var data = new Dictionary<string, IEnumerable<string>>();

        class_?.GetCustomAttributes(typeof(TestWareDoc), true)
            .Cast<TestWareDoc>().ToList().ForEach(
            attr =>
            {
                if (data.ContainsKey(attr.Key))
                {
                    data[attr.Key] = data[attr.Key].Concat(attr.Values);
                }
                else
                {
                    data[attr.Key] = attr.Values;
                }
            });

        method?.GetCustomAttributes(typeof(TestWareDoc), true)
            .Cast<TestWareDoc>().ToList().ForEach(
            attr =>
            {
                if (data.ContainsKey(attr.Key))
                {
                    data[attr.Key] = data[attr.Key].Concat(attr.Values).ToArray();
                }
                else
                {
                    data[attr.Key] = attr.Values;
                }
            });
        return data;
    }

    public static string[] GetTestWareScopes(string className, string methodName)
    {
        Type? class_ = AppDomain.CurrentDomain.GetAssemblies().AsParallel()
            .Select(a => a.GetType(className)).FirstOrDefault(t => t != null);

        MethodInfo? method = class_?.GetMethod(methodName);
        return GetTestWareScopes(class_, method).ToArray();
    }
    public static IEnumerable<string> GetTestWareScopes(Type? class_, MethodInfo? method)
    {
        IEnumerable<string> scopes = [];

        class_?.GetCustomAttributes(typeof(TestWareScopes), true)
            .Cast<TestWareScopes>().ToList().ForEach(
            attr =>
            {
                scopes = scopes.Concat(attr.Scopes);
            });

        method?.GetCustomAttributes(typeof(TestWareScopes), true)
            .Cast<TestWareScopes>().ToList().ForEach(
            attr =>
            {
                scopes = scopes.Concat(attr.Scopes);
            });

        return scopes.ToArray();
    }
}
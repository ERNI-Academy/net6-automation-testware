using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core;

namespace TestWare.Samples.Tests.MsTest;


//MSTEST RUNNER
public class TestWareMethod : DataTestMethodAttribute
{
    public override TestResult[] Execute(ITestMethod testMethod)
    {
        TestResult[] results;
        if ((testMethod.Arguments?.Count() ?? 0) < (testMethod.ParameterTypes?.Count() ?? 0))
        {
            var scopes = TestWareAttributeAssistant.GetTestWareScopes(testMethod.TestClassName, testMethod.TestMethodName);
            object[] injectedArgs = new object[testMethod.ParameterTypes.Count()];
            var argsCount = 0;
            for (var i = 0; i<testMethod.ParameterTypes.Count(); i++)
            {
                var paramType = testMethod.ParameterTypes[i].ParameterType;
                if (paramType == testMethod.Arguments?.ElementAtOrDefault(argsCount)?.GetType())
                {
                    injectedArgs[i] = testMethod.Arguments[argsCount];
                    argsCount++;
                }
                else
                {
                    string consumedScope;
                    injectedArgs[i] = TestWareProvider.GetTestWareComponentFromTags(testMethod.ParameterTypes[i].ParameterType, scopes, out consumedScope);
                    scopes = scopes.Where(x=> x != consumedScope).ToArray();
                }
            }
            results = [testMethod.Invoke(injectedArgs)];
        }
        else
        {
            results = base.Execute(testMethod);
        }

        foreach (TestResult result in results)
        {
            if (result.Outcome == UnitTestOutcome.Failed)
            {
                string message = result.TestFailureException.Message;
            }
        }

        return results;
    }
}


//ASSISTANCE
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
internal class TestWareDoc : Attribute
{
    public string Key;
    public IEnumerable<string> Values;
    public TestWareDoc(string key, string value)
    {
        Key = key;
        Values = [value];
    }
    public TestWareDoc(string key, params string[] values)
    {
        Key = key;
        Values = values;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
internal class TestWareScopes : Attribute
{
    public IEnumerable<string> Scopes;
    public TestWareScopes(params string[] scopes)
    {
        Scopes = scopes;
    }
}


public static class TestWareAttributeAssistant
{

    public static Dictionary<string, IEnumerable<string>> GetTestWareDocDict(string className, string methodName)
    {
        Type class_ = Type.GetType(className);
        MethodInfo method = class_.GetMethod(methodName);
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
        Type class_ = Type.GetType(className);
        MethodInfo method = class_.GetMethod(methodName);
        return GetTestWareScopes(class_, method).ToArray();
    }
    public static IEnumerable<string> GetTestWareScopes(Type class_, MethodInfo method)
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


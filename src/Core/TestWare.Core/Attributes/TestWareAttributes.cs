using System.Reflection;

namespace TestWare.Core.Attributes;

public static class TestWareAttributes
{

    /// <summary>
    /// Retrieves a dictionary of test ware documentation for a specified class and method.
    /// </summary>
    /// <param name="className">The fully qualified name of the class.</param>
    /// <param name="methodName">The name of the method within the class.</param>
    /// <returns>
    /// A dictionary where the keys are strings representing documentation categories, and the values are collections of strings representing the documentation details for each category.
    /// </returns>
    /// <remarks>
    /// This method searches all loaded assemblies in the current application domain to find the specified class and method.
    /// If the class or method is not found, it returns an empty dictionary.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="className"/> or <paramref name="methodName"/> is null.
    /// </exception>
    public static Dictionary<string, IEnumerable<string>> GetTestWareDocDict(string className, string methodName)
    {
        Type? class_ = AppDomain.CurrentDomain.GetAssemblies().AsParallel()
            .Select(a => a.GetType(className)).FirstOrDefault(t => t != null);

        MethodInfo? method = class_?.GetMethod(methodName);
        return GetTestWareDocDict(class_, method);
    }

    /// <summary>
    /// Retrieves a dictionary of test ware documentation for a specified class and method.
    /// </summary>
    /// <param name="class_">The <see cref="Type"/> of the class.</param>
    /// <param name="method">The <see cref="MethodInfo"/> of the method within the class.</param>
    /// <returns>
    /// A dictionary where the keys are strings representing documentation categories, and the values are collections of strings representing the documentation details for each category.
    /// </returns>
    /// <remarks>
    /// This method extracts custom attributes of type <see cref="TestWareDoc"/> from the specified class and method.
    /// If the class or method is not found, it returns an empty dictionary.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="class_"/> or <paramref name="method"/> is null.
    /// </exception>
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

    /// <summary>
    /// Retrieves the test ware scopes for a specified class and method.
    /// </summary>
    /// <param name="className">The fully qualified name of the class.</param>
    /// <param name="methodName">The name of the method within the class.</param>
    /// <returns>
    /// An array of strings representing the test ware scopes associated with the specified class and method.
    /// </returns>
    /// <remarks>
    /// This method searches all loaded assemblies in the current application domain to find the specified class and method.
    /// If the class or method is not found, it returns an empty array.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="className"/> or <paramref name="methodName"/> is null.
    /// </exception>
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
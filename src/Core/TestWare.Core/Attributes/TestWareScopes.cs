namespace TestWare.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class TestWareScopes : Attribute
{
    public IEnumerable<string> Scopes;
    public TestWareScopes(params string[] scopes)
    {
        Scopes = scopes;
    }
}
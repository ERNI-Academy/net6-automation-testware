using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class TestWareDoc : Attribute
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

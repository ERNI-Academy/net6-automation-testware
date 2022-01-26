using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TestWare.Core;

public abstract class EngineManagerBase
{
    protected TEnum GetValidConfiguration<TEnum>(IEnumerable<string> tags) where TEnum : struct
    {
        foreach (var tag in tags)
        {
            if (Enum.TryParse<TEnum>(tag, true, out var foundConfiguration))
                return foundConfiguration;
        }
        return default;
    }

}

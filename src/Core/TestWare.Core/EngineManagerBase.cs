using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
using TestWare.Core.Configuration;

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

    protected IEnumerable<T> GetCapabilities<T>(TestConfiguration testConfiguration, string configName)
    {
        var configuration = testConfiguration.Configurations.FirstOrDefault(item => item.Tag.ToUpperInvariant() == configName);
        if (configuration?.Capabilities == null)
        {
            throw new ArgumentException("null configuration");
        }
        var capabilities = configuration.Capabilities.Select(x => x.Deserialize<T>());
        return capabilities;
    }

}

using System.Text.Json;
using TestWare.Core.Configuration;

namespace TestWare.Core;

public abstract class EngineManagerBase
{
    protected static TEnum GetValidConfiguration<TEnum>(IEnumerable<string> tags) where TEnum : struct
    {
        foreach (var tag in tags)
        {
            if (Enum.TryParse<TEnum>(tag, true, out var foundConfiguration))
                return foundConfiguration;
        }
        return default;
    }

    protected static IEnumerable<T> GetCapabilities<T>(TestConfiguration testConfiguration, string configName)
    {
        var configuration = testConfiguration.Configurations.FirstOrDefault(item => item.Tag.ToUpperInvariant() == configName.ToUpperInvariant());
        if (configuration?.Capabilities == null)
        {
            throw new ArgumentException($"Capabilities for configuration {configName} are null.");
        }
        var capabilities = configuration.Capabilities.Select(x => x.Deserialize<T>());
        return capabilities;
    }

}

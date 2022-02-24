using System.Text.Json;

namespace TestWare.Core.Configuration;

public static class ConfigurationManager
{
    public static TestConfiguration ReadConfigurationFile(string filePath)
    {
        var configurationFile = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<TestConfiguration>(configurationFile);
    }


    public static TEnum GetValidConfiguration<TEnum>(IEnumerable<string> tags) where TEnum : struct
    {
        foreach (var tag in tags)
        {
            if (Enum.TryParse<TEnum>(tag, true, out var foundConfiguration))
                return foundConfiguration;
        }
        return default;
    }

    public static IEnumerable<T> GetCapabilities<T>(TestConfiguration testConfiguration, string configName)
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
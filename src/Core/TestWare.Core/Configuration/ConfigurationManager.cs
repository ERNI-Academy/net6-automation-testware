using System.Text.Json;

namespace TestWare.Core.Configuration;

public class ConfigurationManager
{
    public TestConfiguration ReadConfigurationFile(string filePath)
    {
        var configurationFile = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<TestConfiguration>(configurationFile);
    }
}
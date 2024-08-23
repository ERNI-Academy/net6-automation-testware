using System.Text.Json;
using TestWare.Core.Configuration;

namespace TestWare.Core;

public static  class ConfigurationManager
{
    public static ITestWareConfiguration ReadConfigurationFile(string filePath)
    {
        var configurationFile = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<TestWareConfiguration>(configurationFile);
    }
}

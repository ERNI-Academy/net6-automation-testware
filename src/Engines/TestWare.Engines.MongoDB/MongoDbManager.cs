using Autofac;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.MongoDB.Configuration;
using TestWare.Engines.MongoDB.Factory;

namespace TestWare.Engines.MongoDB;

public class MongoDbManager : EngineManagerBase, IEngineManager
{
    private const string _name = "MongoDb";

    private static void RegisterSingle(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var configName = Enum.GetName(ConfigurationTags.mongodb).ToUpperInvariant();
        var capabilities = ConfigurationManager.GetCapabilities<Capabilities>(testConfiguration, configName);
        var singleCapability = capabilities.FirstOrDefault(x => tags.Contains(x.Name.ToUpperInvariant()));
        if (!ContainerManager.ExistsType(singleCapability.GetType()))
        {
            var driver = ClientFactory.Create(singleCapability);
            ContainerManager.RegisterType(singleCapability.Name, driver);
        }
    }

    public string CollectEvidence(string destinationPath, string evidenceName)
    {        
        var mongoDbClients = ContainerManager.Container.Resolve<IEnumerable<IMongoDbClient>>();

        foreach (var mongoDbClient in mongoDbClients)
        {
            var responses = mongoDbClient.GetClientLogs();
            var instanceName = ContainerManager.GetNameFromInstance(mongoDbClient);
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var evidenceData = JsonSerializer.Serialize(responses, serializeOptions);
            var evidencePath = Path.Combine(destinationPath, $"{evidenceName} - {instanceName}.json");
            File.WriteAllText(evidencePath, evidenceData, Encoding.UTF8);
            mongoDbClient.ClearClientLogs();
        }

        return destinationPath;
    }

    public void Destroy()
    {
        //Do nothing, the client handles it automatically
    }

    public string GetEngineName()
    {
        return _name;
    }

    public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var normalizedTags = tags.Select(x => x.ToUpperInvariant()).ToArray();
        var foundConfiguration = ConfigurationManager.GetValidConfiguration<ConfigurationTags>(normalizedTags);

        switch (foundConfiguration)
        {
            case ConfigurationTags.mongodb:
                RegisterSingle(normalizedTags, testConfiguration);
                break;
        }
    }
}

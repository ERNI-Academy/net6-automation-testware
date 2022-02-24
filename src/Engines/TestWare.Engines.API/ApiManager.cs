using Autofac;
using System.Text.Json;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Restsharp.Configuration;
using TestWare.Engines.Restsharp.Factory;

namespace TestWare.Engines.Restsharp;

public class RestSharpManager : EngineManagerBase, IEngineManager
{
    private const string _name = "Restsharp";

    public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var foundConfiguration = GetValidConfiguration<ConfigurationTags>(tags);
        switch (foundConfiguration)
        {
            case ConfigurationTags.api:
                var configName = Enum.GetName(ConfigurationTags.api).ToUpperInvariant();
                var configuration = testConfiguration.Configurations.FirstOrDefault(item => item.Tag.ToUpperInvariant() == configName);
                if (configuration?.Capabilities == null)
                {
                    throw new ArgumentException("API null configuration");
                }

                var capabilities = configuration.Capabilities.Select(x => x.Deserialize<Capabilities>());
                var capability = capabilities.FirstOrDefault(x => tags.Contains(x.Name));

                if (!ContainerManager.ExistsType(typeof(ClientFactory)))
                {
                    var client = ClientFactory.Create(capability);

                    ContainerManager.RegisterType(capability.Name, client);
                }
                break;
        }
    }

    public void Destroy() 
    {
        // Do nothing, not applicable.
    }

    public string CollectEvidence(string destinationPath, string evidenceName) 
    {
        IEnumerable<IApiClient> apiClients;
        apiClients = ContainerManager.Container.Resolve<IEnumerable<IApiClient>>();

        foreach (var apiClient in apiClients)
        {
            var responses = apiClient.GetRestResponses();
            var instanceName = ContainerManager.GetNameFromInstance(apiClient);
            var evidenceData = JsonSerializer.Serialize(responses);
            var evidencePath = Path.Combine(destinationPath, $"{evidenceName} - {instanceName}.json");
            File.WriteAllText(evidencePath, evidenceData);
            apiClient.ClearResponseQueue();
        }

        return destinationPath;
    }

    public string GetEngineName()
    {
        return _name;
    }
}

using Autofac;
using System.Linq;
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

    private static void RegisterSingle(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var configName = Enum.GetName(ConfigurationTags.api).ToUpperInvariant();
        var capabilities = ConfigurationManager.GetCapabilities<Capabilities>(testConfiguration, configName);
        var singleCapability = capabilities.FirstOrDefault(x => tags.Contains(x.Name.ToUpperInvariant()));
        if (!ContainerManager.ExistsType(singleCapability.GetType()))
        {
            var driver = ClientFactory.Create(singleCapability);
            ContainerManager.RegisterType(singleCapability.Name, driver);
        }
    }

    private static void RegisterMultiple(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var configName = Enum.GetName(ConfigurationTags.multiapi).ToUpperInvariant();
        var capabilities = ConfigurationManager.GetCapabilities<Capabilities>(testConfiguration, configName);
        
        var multipleCapabilities = capabilities.Where(x => tags.Contains(x.Name.ToUpperInvariant()));

        foreach (var capability in multipleCapabilities)
        {
            var driver = ClientFactory.Create(capability);
            ContainerManager.RegisterType(capability.Name, driver);
        }
    }

    public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var normalizedTags = tags.Select(x => x.ToUpperInvariant()).ToArray();
        var foundConfiguration = ConfigurationManager.GetValidConfiguration<ConfigurationTags>(normalizedTags);
        
        switch (foundConfiguration)
        {
            case ConfigurationTags.api:
                RegisterSingle(normalizedTags, testConfiguration);
                break;

            case ConfigurationTags.multiapi:
                RegisterMultiple(normalizedTags, testConfiguration);
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

using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Selenium.Configuration;
using TestWare.Engines.Selenoid.Factory;

namespace TestWare.Engines.Selenoid
{
    public class SelenoidManager : EngineManagerBase, IEngineManager
    {
        private const string _name = "Selenoid";

        private static void RegisterSingle(IEnumerable<string> tags, TestConfiguration testConfiguration)
        {
            var configName = Enum.GetName(ConfigurationTags.remotedriver).ToUpperInvariant();
            var capabilities = ConfigurationManager.GetCapabilities<Capabilities>(testConfiguration, configName);
            var singleCapability = capabilities.FirstOrDefault(x => tags.Contains(x.Name.ToUpperInvariant()));
            if (!ContainerManager.ExistsType(singleCapability.GetType()))
            {
                var driver = BrowserFactory.Create(singleCapability);
                ContainerManager.RegisterType(singleCapability.Name, driver);
            }
        }

        private static void RegisterMultiple(IEnumerable<string> tags, TestConfiguration testConfiguration)
        {
            var configName = Enum.GetName(ConfigurationTags.multiwebdriver).ToUpperInvariant();
            var capabilities = ConfigurationManager.GetCapabilities<Capabilities>(testConfiguration, configName);
            var multipleCapabilities = capabilities.Where(x => tags.Contains(x.Name.ToUpperInvariant()));

            foreach (var capability in multipleCapabilities)
            {
                var driver = BrowserFactory.Create(capability);
                ContainerManager.RegisterType(capability.Name, driver);
            }
        }

        public string CollectEvidence(string destinationPath, string evidenceName)
        {
            throw new NotImplementedException();
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public string GetEngineName()
        {
            return _name;
        }

        public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
        {
            var normalizedTags = tags.Select(x => x.ToUpperInvariant()).ToArray();
            var foundConfiguration = ConfigurationManager.GetValidConfiguration<ConfigurationTags>(tags);

            switch (foundConfiguration)
            {
                case ConfigurationTags.remotedriver:
                    RegisterSingle(normalizedTags, testConfiguration);
                    break;

                case ConfigurationTags.multiwebdriver:
                    RegisterMultiple(normalizedTags, testConfiguration);
                    break;
            }
        }
    }
}
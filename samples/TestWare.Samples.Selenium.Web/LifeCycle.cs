using System.Reflection;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Selenium;

namespace TestWare.Samples.Selenium.Web
{
    internal class LifeCycle : AutomationLifeCycleBase
    {
        protected override IEnumerable<Assembly> GetTestWareComponentAssemblies()
        {
            IEnumerable<Assembly> assemblies = new[]
            {
                typeof(Hook).Assembly
            };

            return assemblies;
        }

        protected override IEnumerable<IEngineManager> GetTestWareEngines()
        {
            IEnumerable<IEngineManager> engines = new[]
            {
                new SeleniumManager()
            };

            return engines;
        }

        protected override TestConfiguration GetConfiguration()
        {
            var configManager = new ConfigurationManager();
            return configManager.ReadConfigurationFile("TestConfiguration.Web.json");
        }
    }
}

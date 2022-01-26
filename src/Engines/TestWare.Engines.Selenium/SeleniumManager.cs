using Autofac;
using OpenQA.Selenium;
using System.Text.Json;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Selenium.Configuration;
using TestWare.Engines.Selenium.Factory;

namespace TestWare.Engines.Selenium
{
    public class SeleniumManager : EngineManagerBase, IEngineManager
    {
        private const string _name = "Selenium";

        public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
        {
            var foundConfiguration = GetValidConfiguration<ConfigurationTags>(tags);
            switch (foundConfiguration)
            {
                case ConfigurationTags.webdriver:
                    var configName = Enum.GetName<ConfigurationTags>(ConfigurationTags.webdriver).ToUpperInvariant();
                    var configuration = testConfiguration.Configurations.FirstOrDefault(item => item.Tag.ToUpperInvariant() == configName);
                    if (configuration?.Capabilities == null)
                    {
                        throw new ArgumentException("WebDriver null configuration");
                    }

                    var capabilities = configuration.Capabilities.Select(x => x.Deserialize<Capabilities>());
                    var capability = capabilities.FirstOrDefault(x => tags.Contains(x.Name));
                    if (!ContainerManager.ExistsType(BrowserFactory.GetBrowserType(capability)))
                    {
                        var driver = BrowserFactory.Create(capability);

                        ContainerManager.RegisterType(driver);
                    }
                    break;
            }
        }

        public void Destroy()
        {
            IWebDriver webDriver;
            using (var scope = ContainerManager.Container.BeginLifetimeScope())
            {
                webDriver = scope.Resolve<IWebDriver>();
            }
            webDriver.Close();
            webDriver.Dispose();
        }

        public string CollectEvidence(string destinationPath, string name)
        {
            var screenshotPath = string.Empty;

            IWebDriver webDriver;
            using (var scope = ContainerManager.Container.BeginLifetimeScope())
            {
                webDriver = scope.Resolve<IWebDriver>();
            }

            try
            {
                webDriver.SwitchTo().Alert();
                // No screenshot because an Alert is present
            }
            catch (NoAlertPresentException)
            {
                var ss = ((ITakesScreenshot)webDriver).GetScreenshot();
                ss.SaveAsFile(Path.Combine(destinationPath, $"{name}.png"), ScreenshotImageFormat.Png);
            }
                
            return screenshotPath;
        }

        public string GetEngineName()
        {
            return _name;
        }
    }
}

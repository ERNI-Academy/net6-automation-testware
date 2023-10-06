﻿using Autofac;
using Autofac.Core.Registration;
using OpenQA.Selenium;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Selenoid.Configuration;
using TestWare.Engines.Selenoid.Factory;

namespace TestWare.Engines.Selenoid
{
    public class SelenoidManager : EngineManagerBase, IEngineManager
    {
        private const string _name = "Selenoid";

        private static void RegisterSingle(IEnumerable<string> tags, TestConfiguration testConfiguration)
        {
            var configName = Enum.GetName(ConfigurationTags.remotedriver)?.ToUpperInvariant();
            var capabilities = ConfigurationManager.GetCapabilities<Capabilities>(testConfiguration, configName);
            var singleCapability = capabilities.FirstOrDefault(x => tags.Contains(x?.Name?.ToUpperInvariant()));
            if (singleCapability != null && !ContainerManager.ExistsType(singleCapability.GetType()))
            {
                var driver = BrowserFactory.Create(singleCapability);
                ContainerManager.RegisterType(singleCapability.Name, driver);
            }
            else {
                throw new NullReferenceException("No suitable capability was found.");
            }
        }

        private static void RegisterMultiple(IEnumerable<string> tags, TestConfiguration testConfiguration)
        {
            var configName = Enum.GetName(ConfigurationTags.multiwebdriver)?.ToUpperInvariant();
            var capabilities = ConfigurationManager.GetCapabilities<Capabilities>(testConfiguration, configName);
            var multipleCapabilities = capabilities.Where(x => tags.Contains(x?.Name?.ToUpperInvariant()));

            if (!multipleCapabilities.Any())
            {
                throw new NullReferenceException("No suitable capability was found.");
            }

            foreach (var capability in multipleCapabilities)
            {
                var driver = BrowserFactory.Create(capability);
                ContainerManager.RegisterType(capability.Name, driver);
            }
        }

        public string CollectEvidence(string destinationPath, string evidenceName)
        {
            var screenshotPath = string.Empty;

            IEnumerable<IWebDriver> webDrivers;
            using (var scope = ContainerManager.Container.BeginLifetimeScope())
            {
                webDrivers = scope.Resolve<IEnumerable<IWebDriver>>();
            }
            foreach (var webDriver in webDrivers)
            {
                try
                {
                    webDriver.SwitchTo().Alert();
                    // No screenshot because an Alert is present
                }
                catch (NoAlertPresentException)
                {
                    var instanceName = ContainerManager.GetNameFromInstance(webDriver);
                    var ss = ((ITakesScreenshot)webDriver).GetScreenshot();
                    ss.SaveAsFile(Path.Combine(destinationPath, $"{evidenceName} - {instanceName}.png"), ScreenshotImageFormat.Png);
                }
                catch (WebDriverException) { }

            }

            return screenshotPath;
        }

        public void Destroy()
        {
            try
            {
                IEnumerable<IWebDriver> webDrivers;
                using (var scope = ContainerManager.Container.BeginLifetimeScope())
                {
                    webDrivers = scope.Resolve<IEnumerable<IWebDriver>>();
                }

                foreach (var webDriver in webDrivers)
                {
                    webDriver.Close();
                    webDriver.Dispose();
                }
            }
            catch (ComponentNotRegisteredException) { }
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
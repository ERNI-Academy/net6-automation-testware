﻿using Autofac;
using OpenQA.Selenium;
using System.Text.Json;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Selenium.Configuration;
using TestWare.Engines.Selenium.Factory;

namespace TestWare.Engines.Selenium;

public class SeleniumManager : EngineManagerBase, IEngineManager
{
    private const string _name = "Selenium";

    private void RegisterSingle(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var configName = Enum.GetName(ConfigurationTags.webdriver).ToUpperInvariant();
        var capabilities = GetCapabilities<Capabilities>(testConfiguration, configName);
        var singleCapability = capabilities.FirstOrDefault(x => tags.Contains(x.Name));
        if (!ContainerManager.ExistsType(singleCapability.GetType()))
        {
            var driver = BrowserFactory.Create(singleCapability);
            ContainerManager.RegisterType(singleCapability.Name, driver);
        }
    }

    private void RegisterMultiple(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var configName = Enum.GetName(ConfigurationTags.multiwebdriver).ToUpperInvariant();
        var capabilities = GetCapabilities<Capabilities>(testConfiguration, configName);
        var multipleCapabilities = capabilities.Where(x => tags.Contains(x.Name));

        foreach (var capability in multipleCapabilities)
        {
            var driver = BrowserFactory.Create(capability);
            ContainerManager.RegisterType(capability.Name, driver);
        }
    }

    public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var foundConfiguration = GetValidConfiguration<ConfigurationTags>(tags);

        switch (foundConfiguration)
        {
            case ConfigurationTags.webdriver:
                RegisterSingle(tags, testConfiguration); 
                break;
            case ConfigurationTags.multiwebdriver:
                RegisterMultiple(tags, testConfiguration);
                break;
        }
    }

    public void Destroy()
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

    public string CollectEvidence(string destinationPath, string name)
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
                ss.SaveAsFile(Path.Combine(destinationPath, $"{name} - {instanceName}.png"), ScreenshotImageFormat.Png);
            }

        }
            
        return screenshotPath;
    }

    public string GetEngineName()
    {
        return _name;
    }
}

using Autofac;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Text.Json;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Appium.Configuration;
using TestWare.Engines.Appium.Factory;

namespace TestWare.Engines.Appium;

public class AppiumManager : EngineManagerBase, IEngineManager
{
    private const string _name = "Appium";
    public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var foundConfiguration = GetValidConfiguration<ConfigurationTags>(tags);
        switch (foundConfiguration)
        {
            case ConfigurationTags.appiumdriver:
                var configName = Enum.GetName(ConfigurationTags.appiumdriver).ToUpperInvariant();
                var configuration = testConfiguration.Configurations.FirstOrDefault(item => item.Tag.ToUpperInvariant() == configName);
                if (configuration?.Capabilities == null)
                {
                    throw new ArgumentException("AppiumDriver null configuration");
                }
                var capabilities = configuration.Capabilities.Select(x => JsonConvert.DeserializeObject<Capabilities>(x.ToJsonString()));
                var capability = capabilities.FirstOrDefault(x => tags.Contains(x.Name));

                if (!ContainerManager.ExistsType(typeof(AppiumDriverFactory)))
                {
                    var driver = AppiumDriverFactory.Create(capability);

                    ContainerManager.RegisterType(driver);
                }
                break;
        }
    }

    public void Destroy()
    {
        IAppiumDriver driver;
        using (var scope = ContainerManager.Container.BeginLifetimeScope())
        {
            driver = scope.Resolve<IAppiumDriver>();
        }
        ((AppiumDriver)driver).CloseApp();
        ((AppiumDriver)driver).Dispose();
    }

    public string CollectEvidence(string destinationPath, string name)
    {
        var screenshotPath = string.Empty;

        try
        {
            IAppiumDriver windowsDriver;
            using (var scope = ContainerManager.Container.BeginLifetimeScope())
            {
                windowsDriver = scope.Resolve<IAppiumDriver>();
            }

            var screenshot = ((ITakesScreenshot)windowsDriver).GetScreenshot();
            screenshot.SaveAsFile(Path.Combine(destinationPath, $"{name}.png"), ScreenshotImageFormat.Png);
        }
        catch { }

        return screenshotPath;
    }

    public string GetEngineName()
    {
        return _name;
    }
}

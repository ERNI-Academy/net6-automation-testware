using Autofac;
using OpenQA.Selenium;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.Appium.WinAppDriver.Configuration;
using TestWare.Engines.Appium.WinAppDriver.Factory;

namespace TestWare.Engines.Appium.WinAppDriver;

public class WinAppDriverManager : EngineManagerBase, IEngineManager
{
    private const string _name = "WinAppDriver";
    public void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration)
    {
        var foundConfiguration = ConfigurationManager.GetValidConfiguration<ConfigurationTags>(tags);
        switch (foundConfiguration)
        {
            case ConfigurationTags.winappdriver:
                var configName = Enum.GetName(ConfigurationTags.winappdriver).ToUpperInvariant();
                var configuration = testConfiguration.Configurations.FirstOrDefault(item => item.Tag.ToUpperInvariant() == configName);
                if (configuration?.Capabilities == null)
                {
                    throw new ArgumentException("WinAppDriver null configuration");
                }
                var capabilities = configuration.Capabilities.Select(x => x.Deserialize<Capabilities>());
                var capability = capabilities.FirstOrDefault(x => tags.Contains(x.Name));
                if (!ContainerManager.ExistsType(typeof(WindowsDriverFactory)))
                {
                    var driver = WindowsDriverFactory.CreateRootWinAppDriverSession(capability);
                    ContainerManager.RegisterType(capability.Name, driver);
                }
                break;
        }
    }

    public string CollectEvidence(string destinationPath, string evidenceName)
    {
        var screenshotPath = string.Empty;

        try
        {
            IWebDriver windowsDriver;
            using (var scope = ContainerManager.Container.BeginLifetimeScope())
            {
                windowsDriver = scope.Resolve<IWindowsDriver>();
            }

            var screenshot = ((ITakesScreenshot)windowsDriver).GetScreenshot();
            var screenshotBitmap = new Bitmap(new MemoryStream(screenshot.AsByteArray));
            screenshotBitmap.Save(Path.Combine(destinationPath, $"{evidenceName}.png"), ImageFormat.Png);
        }
        catch 
        {
            // Do nothing, not applicable.
        }

        return screenshotPath;
    }

    public void Destroy()
    {
        IWindowsDriver driver;
        using (var scope = ContainerManager.Container.BeginLifetimeScope())
        {
            driver = scope.Resolve<IWindowsDriver>();
        }
        driver.Close();
        driver.Dispose();
    }

    public string GetEngineName()
    {
        return _name;
    }
}

using OpenQA.Selenium.Appium;
using TestWare.Engines.Appium.Configuration;

namespace TestWare.Engines.Appium.Factory;

internal static class AppiumDriverFactory
{
    public static IAppiumDriver Create(Capabilities capabilities)
    {
        var appiumOptions = new AppiumOptions()
        { 
            App = capabilities.ApkPath,
            DeviceName = capabilities.DeviceName,
            PlatformName = capabilities.PlatformName
        };

        foreach(var capabilityOption in capabilities.Options)
        {
            appiumOptions.AddAdditionalAppiumOption(capabilityOption.Name, capabilityOption.Value);
        }

        return new AndroidDriver(new Uri(capabilities.AppiumUrl), appiumOptions, TimeSpan.FromSeconds(120));
    }
}

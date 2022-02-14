using OpenQA.Selenium.Appium;
using TestWare.Engines.Appium.Configuration;

namespace TestWare.Engines.Appium.Factory;

internal static class AppiumDriverFactory
{
    public static IAppiumDriver Create(Capabilities capabilities)
    {

        IAppiumDriver result = capabilities.GetPlatform() switch
        {
            SupportedPlatforms.Android => CreateAndroidDriver(capabilities),
            SupportedPlatforms.IOS => CreateIOSDriver(capabilities),
            SupportedPlatforms.Invalid => throw new NotImplementedException(),
            _ => throw new NotSupportedException($"Browser type is invalid."),
        };
        return result;
    }

    private static AndroidDriver CreateAndroidDriver(Capabilities capabilities)
    {
        var appiumOptions = new AppiumOptions()
        {
            App = capabilities.AppPath,
            DeviceName = capabilities.DeviceName,
            PlatformName = capabilities.PlatformName
        };

        foreach (var capabilityOption in capabilities.Options)
        {
            appiumOptions.AddAdditionalAppiumOption(capabilityOption.Name, capabilityOption.Value);
        }

        return new AndroidDriver(new Uri(capabilities.AppiumUrl), appiumOptions, TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }

    private static IOSDriver CreateIOSDriver(Capabilities capabilities)
    {
        var appiumOptions = new AppiumOptions()
        {
            App = capabilities.AppPath,
            DeviceName = capabilities.DeviceName,
            PlatformName = capabilities.PlatformName,
            PlatformVersion = capabilities.PlatformVersion,
            AutomationName = "XCUITest"
        };

        foreach (var capabilityOption in capabilities.Options)
        {
            appiumOptions.AddAdditionalAppiumOption(capabilityOption.Name, capabilityOption.Value);
        }
        
        return new IOSDriver(new Uri(capabilities.AppiumUrl), appiumOptions, TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }
}

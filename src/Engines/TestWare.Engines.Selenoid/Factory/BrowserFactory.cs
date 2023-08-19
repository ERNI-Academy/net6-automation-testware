using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using TestWare.Engines.Selenoid.Configuration;

namespace TestWare.Engines.Selenoid.Factory;

internal static class BrowserFactory
{
    private readonly static string SELENOID_OPTIONS_KEY = "selenoid:options";

    public static IWebDriver Create(Capabilities capabilities)
    {
        IWebDriver result = capabilities.GetDriver() switch
        {
            SupportedBrowsers.Chrome => CreateChromeDriver(capabilities),
            SupportedBrowsers.Firefox => CreateFirefoxDriver(capabilities),
            SupportedBrowsers.Edge => CreateEdgeDriver(capabilities),
            SupportedBrowsers.Android => CreateAndroidDriver(capabilities),
            SupportedBrowsers.Invalid => throw new NotImplementedException(),
            _ => throw new NotSupportedException($"Browser type is invalid."),
        };
        return result;
    }
    private static IWebDriver CreateChromeDriver(Capabilities capabilities)
    {
        ChromeOptions options = new();
        options.AddArguments(capabilities.Arguments);
        options.AddAdditionalOption(SELENOID_OPTIONS_KEY, GenerateSelenoidWebCapabilities(capabilities));

        return new RemoteWebDriver(new Uri(capabilities.Uri), options.ToCapabilities());
    }
    
    private static IWebDriver CreateFirefoxDriver(Capabilities capabilities)
    {
        FirefoxOptions options = new();
        options.AddArguments(capabilities.Arguments);
        options.AddAdditionalOption(SELENOID_OPTIONS_KEY, GenerateSelenoidWebCapabilities(capabilities));

        return new RemoteWebDriver(new Uri(capabilities.Uri), options.ToCapabilities());
    }

    private static IWebDriver CreateEdgeDriver(Capabilities capabilities)
    {
        EdgeOptions options = new();
        options.AddArguments(capabilities.Arguments);
        options.AddAdditionalOption(SELENOID_OPTIONS_KEY, GenerateSelenoidWebCapabilities(capabilities));

        return new RemoteWebDriver(new Uri(capabilities.Uri), options.ToCapabilities());
    }

    private static IWebDriver CreateAndroidDriver(Capabilities capabilities)
    {
        var options = new AppiumOptions();
        options.AddAdditionalAppiumOption(SELENOID_OPTIONS_KEY, GenerateSelenoidMobileCapabilities(capabilities));
       // return new AndroidDriver(new Uri(capabilities.Uri), appiumOptions);
        /*
        var appiumOptions = new AppiumOptions
        {
            DeviceName = "android",
            App = "https://github.com/saucelabs/sample-app-mobile/releases/download/2.7.1/Android.SauceLabs.Mobile.Sample.app.2.7.1.apk",
        };

        options.AddAdditionalAppiumOption("version", "10.0");
        options.AddAdditionalAppiumOption("appPackage", "com.swaglabsmobileapp");
        options.AddAdditionalAppiumOption("appActivity", "com.swaglabsmobileapp.MainActivity");
        options.AddAdditionalAppiumOption("enableVNC", true);
        // Change with your Selenoid hub instance URL.
        return new AndroidDriver(new Uri("http://localhost:4444/wd/hub"), options); 

        ChromeOptions options = new();
        options.AddArguments(capabilities.Arguments);
        options.AddAdditionalOption(SELENOID_OPTIONS_KEY, GenerateSelenoidMobileCapabilities(capabilities));*/

        return new RemoteWebDriver(new Uri(capabilities.Uri), options.ToCapabilities());
    }

    private static Dictionary<string, object> GenerateSelenoidWebCapabilities(Capabilities capabilities) 
    {
        var browser = (SupportedBrowsers)Enum.Parse(typeof(SupportedBrowsers), capabilities.BrowserName);
        return new Dictionary<string, object>
        {
            ["browserName"] = SupportedBrowsersHelper.GetBrowserName(browser),
            ["browserVersion"] = capabilities.BrowserVersion,
            ["screenResolution"] = capabilities.ScreenResolution,
            ["name"] = capabilities.BrowserName,
            ["sessionTimeout"] = capabilities.CommandTimeOutInMinutes + "m",
            ["enableLog"] = capabilities.EnableLog,
            ["enableVnc"] = capabilities.EnableVnc,
            ["enableVideo"] = capabilities.EnableVideo
        };
    }

    private static Dictionary<string, object> GenerateSelenoidMobileCapabilities(Capabilities capabilities)
    {
        var browser = (SupportedBrowsers)Enum.Parse(typeof(SupportedBrowsers), capabilities.BrowserName);
        return new Dictionary<string, object>
        {
            ["deviceName"] = "android",
            ["version"] = "10.0",
            ["app"] = "https://github.com/saucelabs/sample-app-mobile/releases/download/2.7.1/Android.SauceLabs.Mobile.Sample.app.2.7.1.apk",
            ["appPackage"] = "com.swaglabsmobileapp",
            ["appActivity"] = "com.swaglabsmobileapp.MainActivity",
            ["enableLog"] = capabilities.EnableLog,
            ["enableVnc"] = capabilities.EnableVnc,
        };
    }
}

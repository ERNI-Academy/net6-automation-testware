using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using TestWare.Engines.Selenoid.Configuration;

namespace TestWare.Engines.Selenoid.Factory;

internal static class BrowserFactory
{
    private static string SELENOID_OPTIONS_KEY = "selenoid:options";

    public static IWebDriver Create(Capabilities capabilities)
    {
        IWebDriver result = capabilities.GetDriver() switch
        {
            SupportedBrowsers.Chrome => CreateChromeDriver(capabilities),
            SupportedBrowsers.Firefox => CreateFirefoxDriver(capabilities),
            SupportedBrowsers.Edge => CreateEdgeDriver(capabilities),
            SupportedBrowsers.Invalid => throw new NotImplementedException(),
            _ => throw new NotSupportedException($"Browser type is invalid."),
        };
        return result;
    }
    private static IWebDriver CreateChromeDriver(Capabilities capabilities)
    {
        ChromeOptions options = new();
        options.AddArguments(capabilities.Arguments);
        options.AddAdditionalOption(SELENOID_OPTIONS_KEY, GenerateSelenoidCapabilities(capabilities));

        return new RemoteWebDriver(new Uri(capabilities.Uri), options.ToCapabilities());
    }
    
    private static IWebDriver CreateFirefoxDriver(Capabilities capabilities)
    {
        FirefoxOptions options = new();
        options.AddArguments(capabilities.Arguments);
        options.AddAdditionalOption(SELENOID_OPTIONS_KEY, GenerateSelenoidCapabilities(capabilities));

        return new RemoteWebDriver(new Uri(capabilities.Uri), options.ToCapabilities());
    }

    private static IWebDriver CreateEdgeDriver(Capabilities capabilities)
    {
        EdgeOptions options = new();
        options.AddArguments(capabilities.Arguments);
        options.AddAdditionalOption(SELENOID_OPTIONS_KEY, GenerateSelenoidCapabilities(capabilities));

        return new RemoteWebDriver(new Uri(capabilities.Uri), options.ToCapabilities());
    }

    private static Dictionary<string, object> GenerateSelenoidCapabilities(Capabilities capabilities) 
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
}

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TestWare.Engines.Selenium.Configuration;

namespace TestWare.Engines.Selenoid.Factory;

internal static class BrowserFactory
{
    public static IBrowserDriver Create(Capabilities capabilities)
    {
        IBrowserDriver result = capabilities.GetDriver() switch
        {
            SupportedBrowsers.Chrome => CreateChromeDriver(capabilities),
            SupportedBrowsers.Firefox => CreateFirefoxDriver(capabilities),
            SupportedBrowsers.InternetExplorer => CreateInternetExplorerDriver(capabilities),
            SupportedBrowsers.Edge => CreateEdgeDriver(capabilities),
            SupportedBrowsers.Invalid => throw new NotImplementedException(),
            _ => throw new NotSupportedException($"Browser type is invalid."),
        };
        return result;
    }
    private static IBrowserDriver CreateChromeDriver(Capabilities capabilities)
    {
        ChromeOptions options = new();
        options.AddAdditionalOption("selenoid:options", GenerateSelenoidCapabilities(capabilities));

        return (IBrowserDriver) new RemoteWebDriver(new Uri(capabilities.Uri), options);
    }

    private static IBrowserDriver CreateFirefoxDriver(Capabilities capabilities)
    {
        FirefoxOptions options = new();
        options.AddAdditionalOption("selenoid:options", GenerateSelenoidCapabilities(capabilities));

        return (IBrowserDriver)new RemoteWebDriver(new Uri(capabilities.Uri), options);
    }

    private static IBrowserDriver CreateInternetExplorerDriver(Capabilities capabilities)
    {
        InternetExplorerOptions options = new();
        options.AddAdditionalOption("selenoid:options", GenerateSelenoidCapabilities(capabilities));

        return (IBrowserDriver)new RemoteWebDriver(new Uri(capabilities.Uri), options);
    }

    private static IBrowserDriver CreateEdgeDriver(Capabilities capabilities)
    {
        EdgeOptions options = new();
        options.AddAdditionalOption("selenoid:options", GenerateSelenoidCapabilities(capabilities));

        return (IBrowserDriver)new RemoteWebDriver(new Uri(capabilities.Uri), options);
    }

    private static Dictionary<string, object> GenerateSelenoidCapabilities(Capabilities capabilities) 
    {
        return new Dictionary<string, object>
        {
            ["enableLog"] = capabilities.EnableLog,
            ["enableVnc"] = capabilities.EnableVnc,
            ["enableVideo"] = capabilities.EnableVideo
        };
    }
}

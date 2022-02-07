using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TestWare.Engines.Selenium.Configuration;

namespace TestWare.Engines.Selenium.Factory;

internal static class BrowserFactory
{
    public static IWebDriver Create(Capabilities capabilities)
    {
        IWebDriver result = capabilities.GetDriver() switch
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

    private static ChromeDriver CreateChromeDriver(Capabilities capabilities)
    {
        ChromeOptions options = new();
        options.AddArguments(capabilities.Arguments);

        return new ChromeDriver(ChromeDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }

    private static FirefoxDriver CreateFirefoxDriver(Capabilities capabilities)
    {
        FirefoxOptions options = new();
        options.AddArguments(capabilities.Arguments);

        return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }

    private static InternetExplorerDriver CreateInternetExplorerDriver(Capabilities capabilities)
    {
        InternetExplorerOptions options = new();

        return new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }

    private static EdgeDriver CreateEdgeDriver(Capabilities capabilities)
    {
        EdgeOptions options = new();
        options.AddArguments(capabilities.Arguments);

        return new EdgeDriver(EdgeDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }
}

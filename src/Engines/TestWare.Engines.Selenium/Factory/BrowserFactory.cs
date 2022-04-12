using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TestWare.Engines.Selenium.Configuration;

namespace TestWare.Engines.Selenium.Factory;

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
        options.AddArguments(capabilities.Arguments);

        return  new ChromeDriver(ChromeDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }

    private static IBrowserDriver CreateFirefoxDriver(Capabilities capabilities)
    {
        FirefoxOptions options = new();
        options.AddArguments(capabilities.Arguments);

        return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }

    private static IBrowserDriver CreateInternetExplorerDriver(Capabilities capabilities)
    {
        InternetExplorerOptions options = new();

        return new InternetExplorerDriver(InternetExplorerDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }

    private static IBrowserDriver CreateEdgeDriver(Capabilities capabilities)
    {
        EdgeOptions options = new();
        options.AddArguments(capabilities.Arguments);

        return new EdgeDriver(EdgeDriverService.CreateDefaultService(capabilities.Path),
                                options,
                                TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
    }
}
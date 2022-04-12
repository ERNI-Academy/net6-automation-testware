using OpenQA.Selenium.Chrome;

namespace TestWare.Engines.Selenium.Factory;

public class ChromeDriver : OpenQA.Selenium.Chrome.ChromeDriver, IBrowserDriver
{
    public ChromeDriver(ChromeOptions options) : base(options)
    {
    }

    public ChromeDriver(ChromeDriverService service) : base(service)
    {
    }

    public ChromeDriver(string chromeDriverDirectory) : base(chromeDriverDirectory)
    {
    }

    public ChromeDriver(string chromeDriverDirectory, ChromeOptions options) : base(chromeDriverDirectory, options)
    {
    }

    public ChromeDriver(ChromeDriverService service, ChromeOptions options) : base(service, options)
    {
    }

    public ChromeDriver(string chromeDriverDirectory, ChromeOptions options, TimeSpan commandTimeout) : base(chromeDriverDirectory, options, commandTimeout)
    {
    }

    public ChromeDriver(ChromeDriverService service, ChromeOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
    {
    }
}

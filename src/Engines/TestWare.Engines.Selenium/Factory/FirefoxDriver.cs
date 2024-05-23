using OpenQA.Selenium.Firefox;

namespace TestWare.Engines.Selenium.Factory;

public class FirefoxDriver : OpenQA.Selenium.Firefox.FirefoxDriver, IBrowserDriver
{
    public FirefoxDriver() : base()
    {
    }

    public FirefoxDriver(FirefoxOptions options) : base(options)
    {
    }

    public FirefoxDriver(FirefoxDriverService service) : base(service)
    {
    }

    public FirefoxDriver(string geckoDriverDirectory) : base(geckoDriverDirectory)
    {
    }

    public FirefoxDriver(string geckoDriverDirectory, FirefoxOptions options) : base(geckoDriverDirectory, options)
    {
    }

    public FirefoxDriver(FirefoxDriverService service, FirefoxOptions options) : base(service, options)
    {
    }

    public FirefoxDriver(string geckoDriverDirectory, FirefoxOptions options, TimeSpan commandTimeout) : base(geckoDriverDirectory, options, commandTimeout)
    {
    }

    public FirefoxDriver(FirefoxDriverService service, FirefoxOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
    {
    }
}

using OpenQA.Selenium.Edge;

namespace TestWare.Engines.Selenium.Factory;

public class EdgeDriver : OpenQA.Selenium.Edge.EdgeDriver, IBrowserDriver
{
    public EdgeDriver()
    {
    }

    public EdgeDriver(EdgeOptions options) : base(options)
    {
    }

    public EdgeDriver(EdgeDriverService service) : base(service)
    {
    }

    public EdgeDriver(string edgeDriverDirectory) : base(edgeDriverDirectory)
    {
    }

    public EdgeDriver(string edgeDriverDirectory, EdgeOptions options) : base(edgeDriverDirectory, options)
    {
    }

    public EdgeDriver(EdgeDriverService service, EdgeOptions options) : base(service, options)
    {
    }

    public EdgeDriver(string edgeDriverDirectory, EdgeOptions options, TimeSpan commandTimeout) : base(edgeDriverDirectory, options, commandTimeout)
    {
    }

    public EdgeDriver(EdgeDriverService service, EdgeOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
    {
    }
}

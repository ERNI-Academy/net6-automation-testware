

using OpenQA.Selenium.IE;

namespace TestWare.Engines.Selenium.Factory;

public class InternetExplorerDriver : OpenQA.Selenium.IE.InternetExplorerDriver, IBrowserDriver
{
    public InternetExplorerDriver()
    {
    }

    public InternetExplorerDriver(InternetExplorerOptions options) : base(options)
    {
    }

    public InternetExplorerDriver(InternetExplorerDriverService service) : base(service)
    {
    }

    public InternetExplorerDriver(string internetExplorerDriverServerDirectory) : base(internetExplorerDriverServerDirectory)
    {
    }

    public InternetExplorerDriver(string internetExplorerDriverServerDirectory, InternetExplorerOptions options) : base(internetExplorerDriverServerDirectory, options)
    {
    }

    public InternetExplorerDriver(InternetExplorerDriverService service, InternetExplorerOptions options) : base(service, options)
    {
    }

    public InternetExplorerDriver(string internetExplorerDriverServerDirectory, InternetExplorerOptions options, TimeSpan commandTimeout) : base(internetExplorerDriverServerDirectory, options, commandTimeout)
    {
    }

    public InternetExplorerDriver(InternetExplorerDriverService service, InternetExplorerOptions options, TimeSpan commandTimeout) : base(service, options, commandTimeout)
    {
    }
}

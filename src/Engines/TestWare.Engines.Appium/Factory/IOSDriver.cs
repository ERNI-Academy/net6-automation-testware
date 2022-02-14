using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;

namespace TestWare.Engines.Appium.Factory;

public class IOSDriver : OpenQA.Selenium.Appium.iOS.IOSDriver, IAppiumDriver
{
    public IOSDriver(DriverOptions driverOptions) : base(driverOptions)
    {
    }

    public IOSDriver(ICommandExecutor commandExecutor, DriverOptions driverOptions) : base(commandExecutor, driverOptions)
    {
    }

    public IOSDriver(DriverOptions driverOptions, TimeSpan commandTimeout) : base(driverOptions, commandTimeout)
    {
    }

    public IOSDriver(AppiumServiceBuilder builder, DriverOptions driverOptions) : base(builder, driverOptions)
    {
    }

    public IOSDriver(Uri remoteAddress, DriverOptions driverOptions) : base(remoteAddress, driverOptions)
    {
    }

    public IOSDriver(AppiumLocalService service, DriverOptions driverOptions) : base(service, driverOptions)
    {
    }

    public IOSDriver(AppiumServiceBuilder builder, DriverOptions driverOptions, TimeSpan commandTimeout) : base(builder, driverOptions, commandTimeout)
    {
    }

    public IOSDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout) : base(remoteAddress, driverOptions, commandTimeout)
    {
    }

    public IOSDriver(AppiumLocalService service, DriverOptions driverOptions, TimeSpan commandTimeout) : base(service, driverOptions, commandTimeout)
    {
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;

namespace TestWare.Engines.Appium.Factory;

public class AndroidDriver : OpenQA.Selenium.Appium.Android.AndroidDriver, IAppiumDriver
{
    public AndroidDriver(DriverOptions driverOptions) : base(driverOptions)
    {
    }

    public AndroidDriver(ICommandExecutor commandExecutor, DriverOptions driverOptions) : base(commandExecutor, driverOptions)
    {
    }

    public AndroidDriver(DriverOptions driverOptions, TimeSpan commandTimeout) : base(driverOptions, commandTimeout)
    {
    }

    public AndroidDriver(AppiumServiceBuilder builder, DriverOptions driverOptions) : base(builder, driverOptions)
    {
    }

    public AndroidDriver(Uri remoteAddress, DriverOptions driverOptions) : base(remoteAddress, driverOptions)
    {
    }

    public AndroidDriver(AppiumLocalService service, DriverOptions driverOptions) : base(service, driverOptions)
    {
    }

    public AndroidDriver(AppiumServiceBuilder builder, DriverOptions driverOptions, TimeSpan commandTimeout) : base(builder, driverOptions, commandTimeout)
    {
    }

    public AndroidDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout) : base(remoteAddress, driverOptions, commandTimeout)
    {
    }

    public AndroidDriver(AppiumLocalService service, DriverOptions driverOptions, TimeSpan commandTimeout) : base(service, driverOptions, commandTimeout)
    {
    }
}

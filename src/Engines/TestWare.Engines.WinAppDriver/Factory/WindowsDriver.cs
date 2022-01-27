using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;

namespace TestWare.Engines.Appium.WinAppDriver.Factory;

public class WindowsDriver : OpenQA.Selenium.Appium.Windows.WindowsDriver, IWindowsDriver
{
    public WindowsDriver(AppiumOptions AppiumOptions) : base(AppiumOptions)
    {
    }

    public WindowsDriver(AppiumOptions AppiumOptions, TimeSpan commandTimeout) : base(AppiumOptions, commandTimeout)
    {
    }

    public WindowsDriver(AppiumServiceBuilder builder, AppiumOptions AppiumOptions) : base(builder, AppiumOptions)
    {
    }

    public WindowsDriver(Uri remoteAddress, AppiumOptions AppiumOptions) : base(remoteAddress, AppiumOptions)
    {
    }

    public WindowsDriver(AppiumLocalService service, AppiumOptions AppiumOptions) : base(service, AppiumOptions)
    {
    }

    public WindowsDriver(AppiumServiceBuilder builder, AppiumOptions AppiumOptions, TimeSpan commandTimeout) : base(builder, AppiumOptions, commandTimeout)
    {
    }

    public WindowsDriver(Uri remoteAddress, AppiumOptions AppiumOptions, TimeSpan commandTimeout) : base(remoteAddress, AppiumOptions, commandTimeout)
    {
    }

    public WindowsDriver(AppiumLocalService service, AppiumOptions AppiumOptions, TimeSpan commandTimeout) : base(service, AppiumOptions, commandTimeout)
    {
    }
}

using TestWare.Engines.Selenium.Pages;
using TestWare.Engines.Common.Extras;
using TestWare.Engines.Appium.WinAppDriver.Factory;

namespace TestWare.Engines.Appium.WinAppDriver.Pages;

public abstract class WinAppDriverPage : PageBase
{

    protected WinAppDriverPage(IWindowsDriver driver)
    {
        Driver = driver;
        PageFactory.InitElements(Driver, this);
    }
}

using OpenQA.Selenium;
using TestWare.Engines.Common.Extras;
using TestWare.Engines.Selenium.Factory;

namespace TestWare.Engines.Selenium.Pages;

public abstract class WebPage : PageBase
{
    protected WebPage(IBrowserDriver driver)
    {
        Driver = driver;
        PageFactory.InitElements(Driver, this);
    }

    public void NavigateToUrl(string Url)
    {
        Driver.Navigate().GoToUrl(new Uri(Url));
    }

    protected string AcceptDialog()
        => this.AcceptDialog(TimeToWait);

    protected string AcceptDialog(int timeToWait)
    {
        IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(Driver);
        var content = alert.Text;
        alert.Accept();
        return content;
    }
}

using OpenQA.Selenium;
using TestWare.Engines.Selenoid.Extras;
using TestWare.Engines.Selenoid.Factory;

namespace TestWare.Engines.Selenoid.Pages;

public abstract class WebPage : PageBase
{
    protected string Url { get; set; }

    protected WebPage(IWebDriver driver)
    {
        Driver = driver;
        PageFactory.InitElements(Driver, this);
    }

    public void NavigateToUrl()
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

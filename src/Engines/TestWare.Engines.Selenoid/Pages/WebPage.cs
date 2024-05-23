using OpenQA.Selenium;
using TestWare.Engines.Common.Extras;

namespace TestWare.Engines.Selenoid.Pages;

public abstract class WebPage : PageBase
{
    protected string? Url { get; set; }

    protected WebPage(IWebDriver driver)
    {
        Driver = driver;
        PageFactory.InitElements(Driver, this);
    }

    public void NavigateToUrl()
    {
        if (Url == null) throw new NullReferenceException("Url variable was null");
        Driver?.Navigate().GoToUrl(new Uri(Url));            
    }

    protected string AcceptDialog()
        => this.AcceptDialog(TimeToWait);

    protected string AcceptDialog(int timeToWait)
    {
        if (Driver == null) throw new NullReferenceException("Driver variable was null");
        IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(Driver);
        var content = alert.Text;
        alert.Accept();
        return content;
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestWare.Engines.Selenium.Extras;

namespace TestWare.Engines.Selenium.Pages;

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
        try
        {
            var webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeToWait));
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(Driver);
            var content = alert.Text;
            alert.Accept();
            return content;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

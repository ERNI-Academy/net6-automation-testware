using OpenQA.Selenium;
using TestWare.Engines.Selenium.Extras;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.Stinto;

public class HomePage : WebPage, IHomePage
{
    private const string HomeUrl = "https://stin.to/en";

    [FindsBy(How = How.XPath, Using = "//*[@id='navbarToggler']/a")]
    private IWebElement CreateChatButton { get; set; }

    public HomePage(IBrowserDriver driver) : base(driver)
    {
        Url = HomeUrl;
        NavigateToUrl();
    }

    public void ClickCreateChat()
        => ClickElement(this.CreateChatButton);

    public void NavigateTo(string url)
    {
        Url = url;
        NavigateToUrl();
    }
}


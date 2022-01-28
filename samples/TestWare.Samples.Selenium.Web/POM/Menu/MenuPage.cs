using OpenQA.Selenium;
using TestWare.Engines.Selenium.Extras;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.Menu;

/// <summary>
/// Initializes a new instance of the <see cref="MenuPage"/> class that contains
/// all the elements and the interaction methods that will be exposed at it's Interface class.
/// </summary>
public class MenuPage : WebPage, IMenuPage
{
    [FindsBy(How = How.PartialLinkText, Using = "Log out")]
    private IWebElement LogoutButton { get; set; }

    [FindsBy(How = How.LinkText, Using = "New Customer")]
    private IWebElement NewCustomerMenuButton { get; set; }

    public MenuPage(IWebDriver driver)
        : base(driver)
    {
    }

    public void ClickLogoutButton()
        => ClickElement(LogoutButton);

    public void NavigateToNewCustomerTab()
        => ClickElement(NewCustomerMenuButton);
}

using OpenQA.Selenium;
using TestWare.Engines.Common.Extras;
using TestWare.Engines.Selenoid.Factory;
using TestWare.Engines.Selenoid.Pages;

namespace TestWare.Samples.Selenoid.Web.POM.Menu;

/// <summary>
/// Initializes a new instance of the <see cref="MenuPage"/> class that contains
/// all the elements and the interaction methods that will be exposed at it's Interface class.
/// </summary>
public class MenuPage : WebPage, IMenuPage
{
    [FindsBy(How = How.Id, Using = "react-burger-menu-btn")]
    private IWebElement OpenMenuButton { get; set; }

    [FindsBy(How = How.Id, Using = "logout_sidebar_link")]
    private IWebElement LogoutButton { get; set; }

    public MenuPage(IWebDriver driver)
        : base(driver)
    {
    }

    public void ClickOpenMenuButton()
        => ClickElement(OpenMenuButton);

    public void ClickLogoutButton()
        => ClickElement(LogoutButton);
}

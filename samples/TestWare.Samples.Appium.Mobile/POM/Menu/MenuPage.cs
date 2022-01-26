using OpenQA.Selenium;
using TestWare.Engines.Appium.Pages;
using TestWare.Engines.Appium.Extras;
using TestWare.Engines.Appium.Factory;

namespace TestWare.Samples.Appium.Mobile.POM.Menu
{
    public class MenuPage : MobilePage, IMenuPage
    {
        #nullable enable

        [FindsBy(How = How.AccessibilityId, Using = "test-Menu")]
        private IWebElement OpenMenuIcon { get; set; }

        [FindsBy(How = How.AccessibilityId, Using = "test-LOGOUT")]
        private IWebElement LogoutButton { get; set; }

        #nullable disable

        public MenuPage(IAppiumDriver driver) : base(driver)
        {
        }

        public void CheckMenuIsVisible()
        {
            WaitUntilElementIsClickable(this.OpenMenuIcon);
            this.OpenMenuIcon.Displayed.Should().BeTrue();
        }

        public void OpenMenu()
            => ClickElement(this.OpenMenuIcon);

        public void ClickLogoutButton()
            => ClickElement(this.LogoutButton);
    }
}

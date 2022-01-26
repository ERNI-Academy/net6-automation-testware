using OpenQA.Selenium;
using TestWare.Engines.Appium.Extras;
using TestWare.Engines.Appium.Factory;
using TestWare.Engines.Appium.Pages;

namespace TestWare.Samples.Appium.Mobile.POM.Login
{
    public class LoginPage : MobilePage, ILoginPage
    {
        #nullable enable
        [FindsBy(How = How.AccessibilityId, Using = "test-Username")]
        private IWebElement UsernameTextBox { get; set; }

        [FindsBy(How = How.AccessibilityId, Using = "test-Password")]
        private IWebElement PasswordTextBox { get; set; }

        [FindsBy(How = How.AccessibilityId, Using = "test-LOGIN")]
        private IWebElement LoginButton { get; set; }
        #nullable disable

        public LoginPage(IAppiumDriver driver) : base(driver)
        {
        }

        public void SendUsername(string userName)
            => SendKeysElement(this.UsernameTextBox, userName);

        public void SendPassword(string password)
            => SendKeysElement(this.PasswordTextBox, password);

        public void ClickLoginButton()
            => ClickElement(this.LoginButton);
    }
}

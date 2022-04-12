using OpenQA.Selenium;
using TestWare.Core.Libraries;
using TestWare.Engines.Selenium.Extras;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.Login;

public class LoginPage : WebPage, ILoginPage
{
    private const string LoginUrl = "https://www.saucedemo.com/";

    [FindsBy(How = How.Name, Using = "user-name")]
    private IWebElement UserIdTextBox { get; set; }

    [FindsBy(How = How.Name, Using = "password")]
    private IWebElement UserPasswordTextBox { get; set; }

    [FindsBy(How = How.Name, Using = "login-button")]
    private IWebElement LoginButton { get; set; }

    public LoginPage(IBrowserDriver driver) : base(driver)
    {
        Url = LoginUrl;
        NavigateToUrl();
    }

    /// <inheritdoc cref="ILoginPage" />
    public void EnterUserName(string name)
        => SendKeysElement(UserIdTextBox, name);

    /// <inheritdoc cref="ILoginPage" />
    public void ConfirmLogoutPopup()
        => this.Driver.SwitchTo().Alert().Accept();

    /// <inheritdoc cref="ILoginPage" />
    public void EnterUserPassword(string password)
        => SendKeysElement(UserPasswordTextBox, password);

    /// <inheritdoc cref="ILoginPage" />

    public void ClickLoginButton()
        => ClickElement(LoginButton);

    /// <inheritdoc cref="ILoginPage" />
    public void CheckUserIsAtLoginpage()
    {
        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.Driver.Url.Should().Be(LoginUrl);
            });
    }
}

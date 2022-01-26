
using Autofac;
using TestWare.Core;
using TestWare.Samples.Selenium.Web.POM.CookieManager;
using TestWare.Samples.Selenium.Web.POM.Login;

namespace TestWare.Samples.Selenium.Web.StepDefinitions;

/// <summary>
/// Initializes a new instance of the <see cref="LoginSteps"/> class that contains
/// all the step action methods that can be used at Feature level.
/// </summary>
[Binding]
public sealed class LoginSteps
{
    private readonly ILoginPage loginPage;
    private readonly ICookiePage cookiePage;

    public LoginSteps()
    {
        using (var scope = ContainerManager.Container.BeginLifetimeScope())
        {
            loginPage = scope.Resolve<ILoginPage>() ?? throw new ArgumentNullException(nameof(ILoginPage));
            cookiePage = scope.Resolve<ICookiePage>() ?? throw new ArgumentException(nameof(ICookiePage));
        }
    }

    [Given(@"Cookies have been accepted")]
    public void GivenCookiesHaveBeenAccpted()
    {
        cookiePage.ClickAcceptAllCookiesButton();   
    }

    [Given(@"the user enters username '([^']*)'")]
    public void GivenTheUserEntersUsername(string userName)
    {
        loginPage.EnterUserName(userName);
    }

    [When(@"the user confirms the logout popup")]
    public void ConfirmLogoutPopup()
    {
        loginPage.ConfirmLogoutPopup();
    }

    [Given(@"the user enters password '([^']*)'")]
    public void GivenTheUserEntersValidPassword(string password)
    {
        loginPage.EnterUserPassword(password);
    }

    [Given(@"user '([^']*)' is logged with '([^']*)' into Guru99")]
    public void GivenUserIsLogedWithIntoGuru(string userName, string password)
    {
        GivenTheUserEntersUsername(userName);
        GivenTheUserEntersValidPassword(password);
        WhenTheUserClicksSubmit();
    }


    [Given(@"the user clicks submit")]
    [When(@"the user clicks submit")]
    public void WhenTheUserClicksSubmit()
    {
        loginPage.ClickLoginButton();
    }

    [Then(@"the user can login")]
    public void ThenTheUserCanLogin()
    {
        loginPage.CheckUserIsAtHomepage();
    }

    [Then(@"the user is at Login page")]
    public void UserIsAtLoginPage()
    {
        loginPage.CheckUserIsAtLoginpage();
    }

    [When(@"user exit notification is accepted")]
    public void WhenUserExitNotificationIsAccepted()
    {
        loginPage.AcceptLogoutAlert();
    }

}

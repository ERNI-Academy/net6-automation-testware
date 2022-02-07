using TestWare.Core;
using TestWare.Samples.Appium.Mobile.POM.Login;
using TestWare.Samples.Appium.Mobile.POM.Menu;

namespace TestWare.Samples.Appium.Mobile.StepDefinitions;

[Binding]
public sealed class LoginSteps
{
    private readonly ILoginPage loginPage;
    private readonly IMenuPage menuPage;

    public LoginSteps()
    {
        loginPage = ContainerManager.GetTestWareComponent<ILoginPage>();
        menuPage = ContainerManager.GetTestWareComponent<IMenuPage>();
    }


    [Given(@"login on SwagLabs with username '(.*)' and password '(.*)'")]
    [When(@"login on SwagLabs with username '(.*)' and password '(.*)'")]
    public void LoginOnSwagLabs(string username, string password)
    {
        username = username ?? throw new ArgumentNullException(nameof(username));
        password = password ?? throw new ArgumentNullException(nameof(password));

        this.loginPage.SendUsername(username);
        this.loginPage.SendPassword(password);
        this.loginPage.ClickLoginButton();
    }

    [Given(@"the user can login - SwagLabs")]
    [When(@"the user can login - SwagLabs")]
    [Then(@"the user can login - SwagLabs")]
    public void UserCanLogin()
    {
        this.menuPage.CheckMenuIsVisible();
    }
}

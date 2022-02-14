using TestWare.Core;
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

    public LoginSteps()
    {
        loginPage = ContainerManager.GetTestWareComponent<ILoginPage>();
    }

    [Given(@"the user enters username '([^']*)'")]
    public void GivenTheUserEntersUsername(string userName)
    {
        loginPage.EnterUserName(userName);
    }

    [Given(@"the user enters password '([^']*)'")]
    public void GivenTheUserEntersValidPassword(string password)
    {
        loginPage.EnterUserPassword(password);
    }

    [Given(@"user '([^']*)' is logged with '([^']*)' into SwagLabs")]
    public void GivenUserIsLogedWithIntoSwagLabs(string userName, string password)
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

    [Then(@"the user is at Login page")]
    public void UserIsAtLoginPage()
    {
        loginPage.CheckUserIsAtLoginpage();
    }
}

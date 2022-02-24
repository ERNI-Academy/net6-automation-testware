using TestWare.Core;
using TestWare.Samples.Selenium.Web.POM.Inventory;
using TestWare.Samples.Selenium.Web.POM.Login;
using TestWare.Samples.Selenium.Web.POM.Menu;

namespace TestWare.Samples.Selenium.Web.StepDefinitions;

/// <summary>
/// Initializes a new instance of the <see cref="LoginSteps"/> class that contains
/// all the step action methods that can be used at Feature level.
/// </summary>
[Binding]
public sealed class MultiplePageSteps
{
    private readonly IEnumerable<ILoginPage> loginPages;
    private readonly IEnumerable<IInventoryPage> inventoryPages;

    public MultiplePageSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var tags = featureContext.FeatureInfo.Tags.Concat(scenarioContext.ScenarioInfo.Tags);
        loginPages = ContainerManager.GetTestWareComponents<ILoginPage>(tags);
        inventoryPages = ContainerManager.GetTestWareComponents<IInventoryPage>(tags);
    }

    [Given(@"the user enters username '([^']*)' on all")]
    public void GivenTheUserEntersUsernameOnAll(string userName)
    {
        loginPages.ToList().ForEach(x => x.EnterUserName(userName));
    }

    [Given(@"the user enters password '([^']*)' on all")]
    public void GivenTheUserEntersValidPasswordOnAll(string password)
    {
        loginPages.ToList().ForEach(x => x.EnterUserPassword(password));
    }

    [Given(@"the user clicks submit on all")]
    [When(@"the user clicks submit on all")]
    public void WhenTheUserClicksSubmitOnAll()
    {
        loginPages.ToList().ForEach(x => x.ClickLoginButton());
    }

    [Given(@"the user can login on all")]
    [Then(@"the user can login on all")]
    public void ThenTheUserCanLoginOnAll()
    {
        inventoryPages.ToList().ForEach(x => x.CheckUserIsAtInventory());
    }

    [When(@"the user clicks Logout button on '([^']*)'")]
    public static void WhenTheUserClicksLogoutButtonOn(string browser)
    {
        var menuPage = ContainerManager.GetTestWareComponent<IMenuPage>(browser);
        menuPage.ClickOpenMenuButton();
        menuPage.ClickLogoutButton();
    }

    [Then(@"the user is at Login page on '([^']*)'")]
    public static void ThenTheUserIsAtLoginPageOn(string browser)
    {
        var loginPage = ContainerManager.GetTestWareComponent<ILoginPage>(browser);
        loginPage.CheckUserIsAtLoginpage();
    }
}

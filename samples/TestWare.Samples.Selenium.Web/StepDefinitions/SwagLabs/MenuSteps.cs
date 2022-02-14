using TestWare.Core;
using TestWare.Samples.Selenium.Web.POM.Menu;

namespace TestWare.Samples.Selenium.Web.StepDefinitions;

/// <summary>
/// Initializes a new instance of the <see cref="MenuSteps"/> class that contains
/// all the step action methods that can be used at Feature level.
/// </summary>
[Binding]
public sealed class MenuSteps
{
    private readonly IMenuPage menuPage;

    public MenuSteps()
    {
        menuPage = ContainerManager.GetTestWareComponent<IMenuPage>();
    }

    [When(@"the user clicks Logout button")]
    public void WhenTheUserClicksLogoutButton()
    {
        menuPage.ClickOpenMenuButton();
        menuPage.ClickLogoutButton();
    }
}

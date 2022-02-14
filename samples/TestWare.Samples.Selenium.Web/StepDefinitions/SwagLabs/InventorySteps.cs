using TestWare.Core;
using TestWare.Samples.Selenium.Web.POM.Inventory;

namespace TestWare.Samples.Selenium.Web.StepDefinitions;

/// <summary>
/// Initializes a new instance of the <see cref="InventorySteps"/> class that contains
/// all the step action methods that can be used at Feature level.
/// </summary>
[Binding]
public sealed class InventorySteps
{
    private readonly IInventoryPage inventoryPage;

    public InventorySteps()
    {
        inventoryPage = ContainerManager.GetTestWareComponent<IInventoryPage>();
    }

    [Then(@"the user can login")]
    public void ThenTheUserCanLogin()
    {
        inventoryPage.CheckUserIsAtInventory();
    }
}

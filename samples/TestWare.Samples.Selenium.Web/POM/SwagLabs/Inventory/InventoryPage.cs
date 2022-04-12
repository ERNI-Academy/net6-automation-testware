using OpenQA.Selenium;
using TestWare.Core.Libraries;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.Inventory;

public class InventoryPage : WebPage, IInventoryPage
{
    private const string InventoryUrl = "https://www.saucedemo.com/inventory.html";

    public InventoryPage(IBrowserDriver driver) : base(driver)
    { 
    }

    /// <inheritdoc cref="IInventoryPage" />
    public void CheckUserIsAtInventory()
    {
        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.Driver.Url.Should().Be(InventoryUrl);
            });
    }

}


using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Inventory;

public interface IInventoryPage : ITestWareComponent
{
    void CheckUserIsAtInventory();
}


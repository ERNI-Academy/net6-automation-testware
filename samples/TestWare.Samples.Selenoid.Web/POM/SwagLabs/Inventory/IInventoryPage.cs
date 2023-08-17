using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenoid.Web.POM.Inventory;

public interface IInventoryPage : ITestWareComponent
{
    void CheckUserIsAtInventory();
}


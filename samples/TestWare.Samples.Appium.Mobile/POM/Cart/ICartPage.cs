using TestWare.Core.Interfaces;

namespace TestWare.Samples.Appium.Mobile.POM.Cart;

public interface ICartPage : ITestWareComponent
{
    void OpenCart();
    void ClickContinueShoppingButton();
    void RemoveItemFromCartByButton(string productName);
    void RemoveItemFromCartByDragging(string productName);
    void CheckItemExistsAtCart(string productName);
    void CheckItemDoesNotExistAtCart(string productName);
}

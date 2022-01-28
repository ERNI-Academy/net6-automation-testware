using TestWare.Core.Interfaces;

namespace TestWare.Samples.Appium.Mobile.POM.Product;

public interface IProductPage : ITestWareComponent
{
    void ClickViewToggle();
    void AddProductToCartByButton(string productName);
    void AddProductToCartByDragging(string productName);
}

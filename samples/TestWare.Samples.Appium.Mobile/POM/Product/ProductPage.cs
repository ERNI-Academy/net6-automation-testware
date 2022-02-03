using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using TestWare.Engines.Appium.Extras;
using TestWare.Engines.Appium.Factory;
using TestWare.Engines.Appium.Pages;

namespace TestWare.Samples.Appium.Mobile.POM.Product;

internal class ProductPage : MobilePage, IProductPage
{
    #nullable enable

    [FindsBy(How = How.AccessibilityId, Using = "test-Toggle")]
    private IWebElement ViewToggle { get; set; }

    [FindsBy(How = How.AccessibilityId, Using = "test-ADD TO CART")]
    private IList<IWebElement> AddToCartButtonList { get; set; }

    [FindsBy(How = How.AccessibilityId, Using = "test-Drag Handle")]
    private IList<IWebElement> DragToCartButtonList { get; set; }
    #nullable disable

    public ProductPage(IAppiumDriver driver) : base(driver)
    {
    }

    public void ClickViewToggle()
        => ClickElement(this.ViewToggle);

    public void AddProductToCartByButton(string productName)
        => ClickElement(AddToCartButtonList[GetProductListIndex(productName)]);

    public void AddProductToCartByDragging(string productName)
        => DragFromElementAToElementB(DragToCartButtonList[GetProductListIndex(productName)], ViewToggle);

    private int GetProductListIndex(string productName)
    {
        var productTitleList = Driver.FindElements(MobileBy.AccessibilityId("test-Item title"));
        var productListTextElements = productTitleList.Select(x => x.Text.ToLowerInvariant()).ToList();
        return productListTextElements.IndexOf(productName.ToLowerInvariant());
    }
}

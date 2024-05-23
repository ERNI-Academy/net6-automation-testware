using OpenQA.Selenium;
using TestWare.Engines.Appium.Pages;
using TestWare.Engines.Common.Extras;
using TestWare.Engines.Appium.Factory;
using TestWare.Core.Libraries;

namespace TestWare.Samples.Appium.Mobile.POM.Cart;

public class CartPage : MobilePage, ICartPage
{
    #nullable enable

    [FindsBy(How = How.AccessibilityId, Using = "test-Cart")]
    private IWebElement OpenCartIcon { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@content-desc='test-Description']/android.widget.TextView")]
    private IList<IWebElement> CartItemList { get; set; }

    [FindsBy(How = How.AccessibilityId, Using = "test-REMOVE")]
    private IList<IWebElement> RemoveItemButtonList { get; set; }

    [FindsBy(How = How.AccessibilityId, Using = "test-Delete")]
    private IWebElement DeleteItemButton { get; set; }

    [FindsBy(How = How.AccessibilityId, Using = "test-CONTINUE SHOPPING")]
    private IWebElement ContinueShoppingButton { get; set; }
    #nullable disable


    public CartPage(IAppiumDriver driver) : base(driver)
    {
    }

    public void OpenCart()
        => ClickElement(this.OpenCartIcon);

    public void ClickContinueShoppingButton()
        => ClickElement(this.ContinueShoppingButton);

    public void RemoveItemFromCartByButton(string productName)
        => ClickElement(RemoveItemButtonList[GetCartItemIndex(productName)]);

    public void RemoveItemFromCartByDragging(string productName)
    {
        var elementToRemove = CartItemList[GetCartItemIndex(productName)];

        SwipeElementToLeft(elementToRemove);
        ClickElement(DeleteItemButton);
    }

    public void CheckItemExistsAtCart(string productName)
    {
        WaitUntilElementIsClickable(this.ContinueShoppingButton);
        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.CartItemList.Any(x => x.Text.ToLowerInvariant() == productName.ToLowerInvariant()).Should().BeTrue();
            },
            numberOfRetries: 5);
    }

    public void CheckItemDoesNotExistAtCart(string productName)
    {
        WaitUntilElementIsClickable(this.ContinueShoppingButton);
        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.CartItemList.Any(x => x.Text.ToLowerInvariant() == productName.ToLowerInvariant()).Should().BeFalse();
            },
            numberOfRetries: 5);        
    }

    private int GetCartItemIndex(string productName)
    {
        var itemsList = CartItemList.Select(x => x.Text.ToLowerInvariant()).ToList();
        return itemsList.IndexOf(productName.ToLowerInvariant());
    }
}

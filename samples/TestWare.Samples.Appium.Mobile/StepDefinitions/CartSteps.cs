using Autofac;
using OpenQA.Selenium;
using TestWare.Core;
using TestWare.Samples.Appium.Mobile.POM.Cart;

namespace TestWare.Samples.Appium.Mobile.StepDefinitions;

[Binding]
public sealed class CartSteps
{
    private readonly ICartPage cartPage;

    public CartSteps()
    {
        using (var scope = ContainerManager.Container.BeginLifetimeScope())
        {
            cartPage = scope.Resolve<ICartPage>() ?? throw new ArgumentNullException(nameof(ICartPage));
        }
    }

    [When(@"the user removes product '(.*)' from cart by dragging")]
    public void WhenTheUserRemovesProductFromCartByDragging(string productName)
    {
        productName = productName ?? throw new ArgumentNullException(nameof(productName));
        this.cartPage.RemoveItemFromCartByDragging(productName);
    }

    [Then(@"cart contains '(.*)' product")]
    public void ThenCartContainsProduct(string productName)
    {
        productName = productName ?? throw new ArgumentNullException(nameof(productName));
        this.cartPage.CheckItemExistsAtCart(productName);
    }

    [Then(@"cart does not contain '(.*)' product")]
    public void ThenCartDoesNotContainProduct(string productName)
    {
        productName = productName ?? throw new ArgumentNullException(nameof(productName));
        this.cartPage.CheckItemDoesNotExistAtCart(productName);
    }

    [Given(@"the user opens the Cart")]
    [When(@"the user opens the Cart")]
    [Then(@"the user opens the Cart")]
    public void UserOpensCart()
    {
        this.cartPage.OpenCart();
    }

    [When(@"the user removes product '(.*)' from cart clicking Remove button")]
    public void WhenTheUserRemovesProductFromCartClickingAddButton(string productName)
    {
        productName = productName ?? throw new ArgumentNullException(nameof(productName));
        this.cartPage.RemoveItemFromCartByButton(productName);
    }
}

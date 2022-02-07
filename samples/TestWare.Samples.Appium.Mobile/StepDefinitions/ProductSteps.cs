using TestWare.Core;
using TestWare.Samples.Appium.Mobile.POM.Product;

namespace TestWare.Samples.Appium.Mobile.StepDefinitions;

[Binding]
public sealed class ProductSteps
{
    private readonly IProductPage productPage;

    public ProductSteps()
    {
        productPage = ContainerManager.GetTestWareComponent<IProductPage>();
    }

    [Given(@"the user click on toggle view - Products")]
    [When(@"the user click on toggle view - Products")]
    [Then(@"the user click on toggle view - Products")]
    public void GivenTheUserClickOnToggleView_Products()
    {
        this.productPage.ClickViewToggle();
    }

    [When(@"the user adds product '(.*)' to cart clicking Add button")]
    public void WhenTheUserAddProductToCartClickingAddButton(string productName)
    {
        productName = productName ?? throw new ArgumentNullException(nameof(productName));
        this.productPage.AddProductToCartByButton(productName);
    }

    [When(@"the user adds product '(.*)' to cart by dragging")]
    public void WhenTheUserAddProductToCartByDragging(string productName)
    {
        productName = productName ?? throw new ArgumentNullException(nameof(productName));
        this.productPage.AddProductToCartByDragging(productName);
    }
}

using TestWare.Samples.Suts.SwagLabs.Playwright.Screenplay;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;
using TestWare.Samples.Tests.MsTest.Screenplay.Interactions;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Behaviours;

public class AddToCart : Behaviour
{
    private string _productName;
    private bool _uniqueAddToCart;

    public static AddToCart TheProduct(string productName)
    {
        return new AddToCart() { _productName = productName, _uniqueAddToCart = false };
    }

    public AddToCart FromDetails()
    {
        _uniqueAddToCart = true;
        return this;
    }

    public override AddToCart PerformedAs(IActor actor)
    {
        actor.Performs(Log.TheMessage($"Product {_productName} is added to the cart"));
        if (_uniqueAddToCart)
        {
            actor.Performs(Click.On(Locators.ActionBtn));
        }
        else
        {
            actor.Performs(Click.On(Locators.ActionBtn).Inside(Locators.Card).WithSibilingText(_productName));
        }
        
        return this;
    }

}

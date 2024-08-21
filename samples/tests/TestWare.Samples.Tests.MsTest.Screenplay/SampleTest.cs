using TestWare.Core.Attributes;
using TestWare.Samples.Suts.SwagLabs.Playwright.Screenplay;
using TestWare.Samples.Tests.MsTest.Screenplay.Actors;
using TestWare.Samples.Tests.MsTest.Screenplay.Behaviours;
using TestWare.Samples.Tests.MsTest.Screenplay.Interactions;
using TestWare.Samples.Tests.MsTest.Screenplay.Questions;
using TestWare.Wheels.MsTestWheel;

namespace TestWare.Samples.Tests.MsTest.Screenplay;

[TestClass]
[TestWareScopes("PlaySwagLabs")]
[TestWareDoc("Description", "Test suite that runs a minimal test over Swaglabs scope using selenium as engine and Linear script as test implementation")]
[TestWareDoc("Scope", "swagLabs")]
public class SampleTest : TestSuiteBase
{

    [TestWareMethod]
    public void SimpleLoginTestWithInteractions(StandardUser standardUser)
    {
        var url = "https://www.saucedemo.com/v1/";
        var expectedProducts = 6;

        standardUser
            .Performs(Log.TheMessage($"{standardUser.Name} Navigates to url {url}"))
            .Performs(Navigate.To(url));

        Assert.IsTrue(standardUser.Answers(Presence.Of(Locators.LoginForm)));
        standardUser
            .Performs(Log.TheMessage($"{standardUser.Name} Introduces credentials"))
            .Performs(Enter.TheValue(standardUser.Name).Into(Locators.UserField))
            .Performs(Enter.TheValue(standardUser.Password).Into(Locators.PasswordField))
            .Performs(Log.TheMessage($"{standardUser.Name} submits login form"))
            .Performs(Click.On(Locators.SubmitBtn))
            .Answers(Presence.Of(Locators.Header), out var isPageLoaded)
            .Answers(Count.Of(Locators.Card), out var numberOfProducts);
       

        Assert.IsTrue(isPageLoaded);
        Assert.AreEqual(expectedProducts, numberOfProducts);
    }

    [TestWareMethod]
    public void SimpleLoginTestWithTasks(StandardUser standardUser)
    {
        var url = "https://www.saucedemo.com/v1/";
        var expectedProducts = 6;

        standardUser
            .Performs(Log.TheMessage($"{standardUser.Name} Navigates to url {url}"))
            .Performs(Navigate.To(url));
        Assert.IsTrue(standardUser.Answers(Presence.Of(Locators.LoginForm)));
        standardUser
            .Performs(Login.WithUser(standardUser.Name).WithPassword(standardUser.Password))
            .Answers(Presence.Of(Locators.Header), out var isPageLoaded)
            .Answers(Count.Of(Locators.Card), out var numberOfProducts);


        Assert.IsTrue(isPageLoaded);
        Assert.AreEqual(expectedProducts, numberOfProducts);
    }

    [TestWareMethod]
    public void BuyProductsFromList(StandardUser buyer)
    {
        var url = "https://www.saucedemo.com/v1/";

        buyer
            .Performs(Log.TheMessage($"{buyer.Name} Navigates to url {url}"))
            .Performs(Navigate.To(url));
        Assert.IsTrue(buyer.Answers(Presence.Of(Locators.LoginForm)));

        buyer
            .Performs(Login.WithUser(buyer.Name).WithPassword(buyer.Password))
            .Answers(Text.Of(Locators.Title), out var availableItems)
            .Performs(AddToCart.TheProduct(availableItems.ElementAt(4)))
            .Performs(Click.On(Locators.Title).WithInsideText(availableItems.ElementAt(2)))
            .Performs(AddToCart.TheProduct(availableItems.ElementAt(2)).FromDetails());
    }
}



using Microsoft.Playwright;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;
using TestWare.Engines.SeleniumEngine;
using TestWare.Engines.SeleniumEngine.Extensions;
using TestWare.Samples.Suts.SwagLabs.Interfaces.POM;

namespace TestWare.Samples.Tests.MsTest;


[TestClass]
[TestWareScopes("swagLabs")]
[TestWareDoc("Description", "Test suite that runs a minimal test over Swaglabs scope using selenium as engine and Linear script as test implementation")]
[TestWareDoc("Scope", "swagLabs")]
public class ScreenplaySeleniumSwagLabs : TestSuiteBase
{
    [TestWareMethod]
    [TestWareDoc("Description", "Test case for valid Login in the platform handling. Reporting with steps")]
    [TestWareDoc("Tags", ["SwagLabsTesting", "Selenium"])]
    [TestWareDoc("Devices", ["my device"])]
    [DataRow("standard_user", "secret_sauce")]
    public void NumberOfItemsInCatalog(string user, string password, IActor actor)
    {
        var expectedProducts = 6;
     
        Assert.IsTrue(actor.Asks(Presence.Of(SwagLabsLoginPage.LoginForm)));

        actor
            .Performs(
                Enter.TheValue(user).Into(SwagLabsLoginPage.UserField),
                Enter.TheValue(password).Into(SwagLabsLoginPage.PasswordField),
                Click.On(SwagLabsLoginPage.LoginButton)
             )
            .Asks(Presence.Of(SwagLabsProductPage.Header), out var pageLoaded)
            .Asks(Count.Of(SwagLabsProductPage.ItemCard), out var products);
        Assert.IsTrue(pageLoaded);
        Assert.AreEqual(expectedProducts, products) ;
    }
}

//TODO MOVE to Swaglabs
public interface IActor : ITestwareComponent
{
    public IActor HasAbility(ITestWareEngine ability);
    public IActor Performs(params Action<ITestWareEngine>[] tasks);
    public T Asks<T>(Func<ITestWareEngine, T> question);
    public IActor Asks<T>(Func<ITestWareEngine, T> question, out T returnValue);
}

public class Actor : IActor
{
    private ITestWareEngine _ability;
    public Actor(ITestWareEngine engine) 
    {
        _ability = engine;
    }

    public T Asks<T>(Func<ITestWareEngine, T> question)
    {
        return question(_ability);
    }

    public IActor Asks<T>(Func<ITestWareEngine, T> question, out T returnValue)
    {
        returnValue = Asks(question);
        return this;
    }

    public IActor HasAbility(ITestWareEngine ability)
    {
        _ability = ability;
        return this;
    }

    public IActor Performs(params Action<ITestWareEngine>[] tasks)
    {
        foreach (var task in tasks)
        {
            task(_ability);

        }
        return this;
    }
}

public class Presence
{
    public static Func<ITestWareEngine,bool> Of(By locator)
    {
        return engine => ((ISeleniumEngine)engine).Driver.ElementIsPresent(locator);
    }

}

public class Enter
{
    private string Value { get; set; }

    public static Enter TheValue(string value)
    {
        return new Enter() { Value = value };
    }

    public Action<ITestWareEngine> Into(By locator)
    {
        return engine => ((ISeleniumEngine)engine).Driver.SafeSendKeys(locator, Value);
    }
}
public class Click
{
    public static Action<ITestWareEngine> On(By locator)
    {
        return engine => ((ISeleniumEngine)engine).Driver.SafeClick(locator);
    }
}

public class Count
{
    public static Func<ITestWareEngine, int> Of(By locator)
    {
        return engine => ((ISeleniumEngine)engine).Driver.FindElements(locator).Count();
    }
}


public class SwagLabsLoginPage
{
    public static By LoginForm = By.ClassName("login_wrapper");
    public static By UserField = By.Id("user-name");
    public static By PasswordField = By.Name("password");
    public static By LoginButton = By.XPath("//*[@type='submit']");
}
public class SwagLabsProductPage
{
    public static By Header = By.ClassName("product_label");
    public static By ItemCard = By.ClassName("inventory_item");
}

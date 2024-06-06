using AventStack.ExtentReports.Model;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.EventHandlers;
using OpenQA.Selenium;
using RazorEngine;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using TestWare.Cockpits.ExtentReportsCockpit;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.SeleniumEngine;
using TestWare.Engines.SeleniumEngine.Extensions;

namespace TestWare.Samples.Tests.MsTest;

[TestClass]
[TestWareScopes("swagLabs")]
[TestWareDoc("Description", "Test suite that runs a minimal test over Swaglabs scope using selenium as engine and Linear script as test implementation")]
[TestWareDoc("Scope", "swagLabs")]
public class LinearScriptSeleniumSwagLabs : TestSuiteBase
{
    [TestWareMethod]
    [TestWareDoc("Description", "Test case for valid Login in the platform handling. Reporting with steps managed manually from the test. Engine and Cockpit injected")]
    [TestWareDoc("Tags", ["SwagLabsTesting", "Selenium"])]
    [TestWareDoc("Devices", ["my device"])]
    public void ValidLogin(ISeleniumEngine Engine, ITestWareCockpit Reporter)
    {
        var userNameInput = Engine.Driver.FindElement(By.Id("user-name"));
        var passwordInput = Engine.Driver.FindElement(By.Name("password"));
        var submitBtn = Engine.Driver.FindElement(By.XPath("//*[@type='submit']"));
        Reporter.StartTestStep("User enters credentials");
        userNameInput.SendKeys("standard_user");
        passwordInput.SendKeys("secret_sauce");
        var evidence = Engine.CollectEvidence(EvidencePath, "1.Credentials introduced");
        Reporter.AddTestStepActivity(evidence);

        Reporter.StartTestStep("User submits login action");
        submitBtn.Click();
        evidence = Engine.CollectEvidence(EvidencePath, "2.Logged in");
        Reporter.AddTestStepActivity(evidence);

        Assert.IsTrue(Engine.Driver.UrlPathEndsWith("inventory.html"));
    }

    [TestWareMethod]
    [TestWareDoc("Description", "Test case for invalid Login using dataRows for bad credential injection. No Reporting; No Evidences")]
    [DataRow("standard_user", "Bad_password")]
    [DataRow("Bad_user", "secret_sauce")]
    [DataRow("Bad_user", "bad_password")]
    public void InvalidLogin(ISeleniumEngine Engine, string user, string password)
    {
        var loginUrl = Engine.Driver.Url;
        var userNameInput = Engine.Driver.FindElement(By.Id("user-name"));
        var passwordInput = Engine.Driver.FindElement(By.Name("password"));
        var submitBtn = Engine.Driver.FindElement(By.XPath("//*[@type='submit']"));

        userNameInput.SendKeys(user);
        passwordInput.SendKeys(password);

        submitBtn.Click();

        var errorMessage = Engine.Driver.FindElement(By.XPath("//*[@data-test='error']"));

        Assert.AreEqual(loginUrl, Engine.Driver.Url);
        Assert.AreEqual("Epic sadface: Username and password do not match any user in this service", errorMessage.Text);
    }

    [TestWareMethod]
    [TestWareDoc("Description", "Test case for slow Login. Assistance StepChain used for Reporting and evidences")]
    public void SlowLogin(ISeleniumEngine Engine, ITestWareCockpit Reporter)
    {
        var userNameInput = Engine.Driver.FindElement(By.Id("user-name"));
        var passwordInput = Engine.Driver.FindElement(By.Name("password"));
        var submitBtn = Engine.Driver.FindElement(By.XPath("//*[@type='submit']"));

        Steps!.Step(args => { userNameInput.SendKeys(args[0]); }, "performance_glitch_user")
            .Step(args => { passwordInput.SendKeys(args[0]); }, "secret_sauce")
            .Step(submitBtn.Click)
            .Step(args => { Assert.IsTrue(Engine.Driver.UrlPathEndsWith("inventory.html")); });

    }

    [TestWareMethod]
    [TestWareDoc("Description", "Test case for checking amount of products displayed. Assistance StepChain used retreiving information from the function result")]
    public void CheckAmountOfProducts(ISeleniumEngine Engine, ITestWareCockpit Reporter)
    {
        var userNameInput = Engine.Driver.FindElement(By.Id("user-name"));
        var passwordInput = Engine.Driver.FindElement(By.Name("password"));
        var submitBtn = Engine.Driver.FindElement(By.XPath("//*[@type='submit']"));

        int products;
        Steps!.Step(args => { userNameInput.SendKeys(args[0]); }, "standard_user")
            .Step(args => { passwordInput.SendKeys(args[0]); }, "secret_sauce")
            .Step(submitBtn.Click)
            .Step<int>(args => { return Engine.Driver.FindElements(By.XPath(args[0])).Count; }, out products, "//img[@class='inventory_item_img']");
      
        Assert.AreEqual(6, products);
    }

    [TestWareMethod]
    [TestWareDoc("Description", "Test case for buy a product. Complete example using multiple features")]
    [DataRow("Sauce Labs Fleece Jacket")]
    [DataRow("Test.allTheThings() T-Shirt (Red)")]
    public void BuyAProduct(string productName, ISeleniumEngine Engine, ITestWareCockpit Reporter)
    {
        var expectedQuantity = 1;

        Steps!.Step("Login",
            () =>
            {
                var userNameInput = Engine.Driver.FindElement(By.Id("user-name"));
                var passwordInput = Engine.Driver.FindElement(By.Name("password"));
                var submitBtn = Engine.Driver.FindElement(By.XPath("//*[@type='submit']"));
                userNameInput.SendKeys("standard_user");
                passwordInput.SendKeys("secret_sauce");
                submitBtn.Click();
            })
            .Step("Select product",
            args =>
            {
                var products = Engine.Driver.FindElements(By.ClassName("inventory_item_name"));
                var productIndex = products.IndexOf(products.FirstOrDefault(x => x.Text == args[0]));
                var addToCartBtnList = Engine.Driver.FindElements(By.ClassName("btn_inventory"));
                addToCartBtnList[productIndex].Click();
            }, productName)
            .Step("Navigate to cart and check product",
            args =>
            {
                Engine.Driver.FindElement(By.XPath("//*[@data-icon='shopping-cart']")).Click();
                var productsAtCart = Engine.Driver.FindElements(By.ClassName("inventory_item_name"));
                var cartProductIndex = productsAtCart.IndexOf(productsAtCart.FirstOrDefault(x => x.Text == args[0]));
                var productQuantityList = Engine.Driver.FindElements(By.ClassName("cart_quantity"));
                Assert.AreEqual(args[1].ToString(), productQuantityList[cartProductIndex].Text);
                Engine.Driver.FindElement(By.ClassName("checkout_button")).Click();
            }, productName, expectedQuantity)
            .Step("Checkout: Personal Info",
            () =>
            {
                Engine.Driver.FindElement(By.Id("first-name")).SendKeys("Super");
                Engine.Driver.FindElement(By.Id("last-name")).SendKeys("User");
                Engine.Driver.FindElement(By.Id("postal-code")).SendKeys("1234");
                Engine.Driver.FindElement(By.ClassName("cart_button")).Click();
            })
            .Step("Checkout: Overview",
            args =>
            {
                var overviewProducts = Engine.Driver.FindElements(By.ClassName("inventory_item_name"));
                var overviewProductIndex = overviewProducts.IndexOf(overviewProducts.FirstOrDefault(x => x.Text == args[0]));
                var overviewProductQuantityList = Engine.Driver.FindElements(By.ClassName("summary_quantity"));
                Assert.AreEqual(args[1].ToString(), overviewProductQuantityList[overviewProductIndex].Text);
                Engine.Driver.FindElement(By.ClassName("cart_button")).Click();
            }, productName, expectedQuantity)
            .Step("Order finished",
            () =>
            {
                Assert.AreEqual(
                    "THANK YOU FOR YOUR ORDER",
                     Engine.Driver.FindElement(By.ClassName("complete-header")).Text
                );
                Assert.AreEqual(
                    "Your order has been dispatched, and will arrive just as fast as the pony can get there!",
                     Engine.Driver.FindElement(By.ClassName("complete-text")).Text
                );
            }
            );
    }
}

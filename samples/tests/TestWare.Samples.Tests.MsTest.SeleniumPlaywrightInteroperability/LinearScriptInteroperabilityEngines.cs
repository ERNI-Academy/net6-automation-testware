using OpenQA.Selenium;
using TestWare.Core.Attributes;
using TestWare.Core.Interfaces;
using TestWare.Engines.PlaywrightEngine;
using TestWare.Engines.SeleniumEngine;
using TestWare.Wheels.MsTestWheel;

namespace TestWare.Samples.Tests.MsTest.SeleniumPlaywrightInteroperability;

[TestClass]
[TestWareDoc("Description", "Test suite to demonstrate interoperability of selenium and playwright at different levels")]
public class LinearScriptInteroperabilityEngines
{
    [TestWareMethod]
    [TestWareDoc("Description", "Test case demonstrating raw interoperability playwright with selenium")]
    [TestWareScopes("PlaySwagLabs", "swagLabs")]
    public async Task LinearPlaywrightSelenium(ITestWareEngine playwrightEngine, ITestWareEngine seleniumEngine)
    {

        playwrightEngine.Initialize();
        seleniumEngine.Initialize();

        var page = ((PlaywrightEngine)playwrightEngine).Page;
        var driver = ((SeleniumEngine)seleniumEngine).Driver;
        await page.GotoAsync("https://www.saucedemo.com/v1/");
        driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
        await page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        driver.FindElement(By.XPath("//*[@type='submit']")).Click();

        playwrightEngine.Dispose();
        seleniumEngine.Dispose();
    }
}

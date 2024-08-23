using Microsoft.Playwright;
using TestWare.Core.Interfaces;
using TestWare.Core.Attributes;
using TestWare.Engines.PlaywrightEngine;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RazorEngine;
using TestWare.Wheels.MsTestWheel;

namespace TestWare.Samples.Tests.MsTest;

[TestClass]
[TestWareScopes("PlaySwagLabs")]
[TestWareDoc("Description", "Test suite that runs a minimal test over Swaglabs scope using playwright as engine and Linear script as test implementation")]
[TestWareDoc("Scope", "swagLabs")]
public class LinearScriptPlaywrightSwagLabs : TestSuiteBase
{
    [TestWareMethod]
    [TestWareDoc("Description", "Test case for valid Login in the platform handling. Reporting with steps")]
    public async Task ValidLogin(ITestWareEngine Engine, ITestWareCockpit Reporter)
    {
        var page = ((PlaywrightEngine)Engine).Page;
        var userNameInput = page.Locator("[data-test=\"username\"]");
        var passwordInput = page.Locator("[data-test=\"password\"]");
        var submitBtn = page.GetByRole(AriaRole.Button, new() { Name = "LOGIN" });
        
        Reporter.StartTestStep("User enters credentials");
        await userNameInput.FillAsync("standard_user");
        await passwordInput.FillAsync("secret_sauce");
        var evidence = Engine.CollectEvidence(EvidencePath!, "1.Credentials introduced");
        Reporter.AddTestStepActivity(evidence);

        Reporter.StartTestStep("User submits login action");
        await submitBtn.ClickAsync();
        evidence = Engine.CollectEvidence(EvidencePath!, "2.Logged in");
        Reporter.AddTestStepActivity(evidence);

        await Assertions.Expect(page.Locator("#inventory_filter_container")).ToContainTextAsync("Products");
        await Assertions.Expect(page.GetByText("Products")).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Combobox)).ToHaveValueAsync("az");

        await Assertions.Expect(page).ToHaveURLAsync(new Regex(".*/inventory.html"));
    }


    //TODO MOVE TO OWN MIGRATION PROJECT
    [TestWareMethod]
    [TestWareScopes("TheInternet-tables")]
    [TestWareDoc("Description", "Test case for valid Login in the platform handling. Reporting with steps")]
    public async Task TestFast()
    {
        var address = "http://localhost:5959";
        var options = new ChromeOptions();
        //options.DebuggerAddress = address;

        options.AddArgument("--remote-debugging-port=5959");
        options.AddArgument("--disable-search-engine-choice-screen");
        var selenium = new ChromeDriver(options);


        //selenium.Navigate().GoToUrl("https://www.saucedemo.com/v1/");
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.ConnectOverCDPAsync(address);
        var page = browser.Contexts[0].Pages[0];

        
        await page.GotoAsync("https://www.saucedemo.com/v1/");
        selenium.FindElement(By.Id("user-name")).SendKeys("standard_user");
        await page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        selenium.FindElement(By.XPath("//*[@type='submit']")).Click();

        await browser.DisposeAsync();
        selenium.Dispose();
        selenium.Quit();


    }

    [TestWareMethod]
    [TestWareScopes("TheInternet-tables")]
    [TestWareDoc("Description", "Test case for valid Login in the platform handling. Reporting with steps")]
    public async Task TestFast2()
    {

        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new() { Headless = false, Args = ["--remote-debugging-port=5959"], Channel = "chrome" });
        var page = await browser.NewPageAsync();

        var address = "localhost:5959";
        var options = new ChromeOptions();
        options.DebuggerAddress = address;

        var selenium = new ChromeDriver(options);


        await page.GotoAsync("https://www.saucedemo.com/v1/");
        selenium.FindElement(By.Id("user-name")).SendKeys("standard_user");
        await page.Locator("[data-test=\"password\"]").FillAsync("secret_sauce");
        selenium.FindElement(By.XPath("//*[@type='submit']")).Click();

        selenium.Dispose();
        await browser.CloseAsync();
    }
}

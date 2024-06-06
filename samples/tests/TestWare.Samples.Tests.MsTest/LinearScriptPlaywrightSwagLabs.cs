using Microsoft.Playwright;
using TestWare.Core.Interfaces;
using TestWare.Engines.PlaywrightEngine;
using System.Text.RegularExpressions;

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
        var evidence = Engine.CollectEvidence(EvidencePath, "1.Credentials introduced");
        Reporter.AddTestStepActivity(evidence);

        Reporter.StartTestStep("User submits login action");
        await submitBtn.ClickAsync();
        evidence = Engine.CollectEvidence(EvidencePath, "2.Logged in");
        Reporter.AddTestStepActivity(evidence);

        await Assertions.Expect(page.Locator("#inventory_filter_container")).ToContainTextAsync("Products");
        await Assertions.Expect(page.GetByText("Products")).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Combobox)).ToHaveValueAsync("az");

        await Assertions.Expect(page).ToHaveURLAsync(new Regex(".*/inventory.html"));
    }
}

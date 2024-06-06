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
using TestWare.Samples.Suts.SwagLabs.Interfaces.POM;

namespace TestWare.Samples.Tests.MsTest;

[TestClass]
[TestWareScopes("swagLabs")]
[TestWareDoc("Description", "Test suite that runs a minimal test over Swaglabs scope using selenium as engine and Linear script as test implementation")]
[TestWareDoc("Scope", "swagLabs")]
public class POMSeleniumSwagLabs : TestSuiteBase
{
    [TestWareMethod]
    [TestWareDoc("Description", "Test case for valid Login in the platform handling. Reporting with steps")]
    [TestWareDoc("Tags", ["SwagLabsTesting", "Selenium"] )]
    [TestWareDoc("Devices", ["my device"])]
    [DataRow("standard_user", "secret_sauce")]
    public void ValidLogin(string user, string password, ILoginPage loginPage)
    {
        Assert.IsTrue(loginPage.IsLoaded());
        loginPage.SetUserName(user);
        loginPage.SetPassword(password);
        loginPage.SubmitCredentials();
        Assert.IsTrue(loginPage.IsNotLoaded());
    }
}

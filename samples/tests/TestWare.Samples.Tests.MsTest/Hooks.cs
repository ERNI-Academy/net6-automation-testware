
using AventStack.ExtentReports.Model;
using RazorEngine;
using System.Reflection;
using TestWare.Cockpits.ExtentReportsCockpit;
using TestWare.Cockpits.ReportPortalCockpit;
using TestWare.Core;
using TestWare.Core.Interfaces;
using TestWare.Samples.Suts.SwagLabs.Interfaces.POM;


namespace TestWare.Samples.Tests.MsTest;

[TestClass]
public class Hooks
{
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {
        
        IEnumerable<Assembly> extraAssemblies = [
            typeof(ExtentCockpit).Assembly, 
            typeof(ILoginPage).Assembly,
            typeof(Suts.SwagLabs.Selenium.POM.LoginPage).Assembly,
            typeof(Suts.SwagLabs.Playwright.POM.LoginPage).Assembly,
            ];
        TestWareProvider.RegisterTestWareComponents("TestConfig.json", extraAssemblies);
        var Reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        Reporter.Initialize();
        //var a = TestWareProvider.GetTestWareComponent<ILoginPage>("swagLabs");
        //var b = TestWareProvider.GetTestWareComponent<ILoginPage>("PlaySwagLabs");
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        var Reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        Reporter.Dispose();
    }
}

using TestWare.Core.Attributes;
using TestWare.Samples.Suts.SwagLabs.Interfaces.POM;
using TestWare.Wheels.MsTestWheel;

namespace TestWare.Samples.Test.MsTest.ExtendingEngines;


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

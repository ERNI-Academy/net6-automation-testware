
using TestWare.Core.Interfaces;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Attributes;
using TestWare.Wheels.MsTestWheel;
using System.Reflection;
using TestWare.Samples.Suts.SwagLabs.Interfaces.POM;


namespace TestWare.Samples.Test.MsTest.ExtendingEngines;


[TestClass]
public abstract class TestSuiteBase
{
    public TestContext? TestContext { get; set; }
    protected string? EvidencePath { get; set; }


    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {

        IEnumerable<Assembly> extraAssemblies = [
            typeof(ILoginPage).Assembly,
            typeof(Suts.SwagLabs.Selenium.POM.LoginPage).Assembly,
        ];
        TestWareProvider.RegisterTestWareComponents("TestConfig.json", extraAssemblies);
    }

    [TestInitialize]
    public void Setup()
    {
        var config = TestWareProvider.GetTestWareComponent<ITestWareConfiguration>();
        EvidencePath = Path.Combine(config.EvidenceBasePath, DateTime.UtcNow.Ticks.ToString());

        var scopes = TestWareAttributes.GetTestWareScopes(TestContext!.ManagedType!, TestContext.TestName!);
        var engine = TestWareProvider.GetTestWareComponent<ITestWareEngine>(scopes.First());
        engine.Initialize();
        engine.StartRecordingEvidences();

    }

    [TestCleanup]
    public void TearDown()
    {
        var scopes = TestWareAttributes.GetTestWareScopes(TestContext!.ManagedType!, TestContext.TestName!);
        var Engine = TestWareProvider.GetTestWareComponent<ITestWareEngine>(scopes.First());
        var evidence = Engine.StopRecordingEvidences(EvidencePath!, "network");
        Engine.Dispose();
    }
}

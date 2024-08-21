
using TestWare.Core.Interfaces;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Attributes;
using TestWare.Wheels.MsTestWheel;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;
using TestWare.Samples.Tests.MsTest.Screenplay.Actors;
using TestWare.Samples.Tests.MsTest.Screenplay.Abilities;


namespace TestWare.Samples.Tests.MsTest.Screenplay;


[TestClass]
public abstract class TestSuiteBase
{
    public TestContext? TestContext { get; set; }
    protected string? EvidencePath { get; set; }


    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {
        TestWareProvider.RegisterTestWareComponents("TestConfig.json");
    }

    [TestInitialize]
    public void Setup()
    {
        var scopes = TestWareAttributes.GetTestWareScopes(TestContext!.ManagedType!, TestContext.TestName!);
        var actor = TestWareProvider.GetTestWareComponent<StandardUser>();
        actor.HasAbility(TestWareProvider.GetTestWareComponent<BrowseTheWeb>(scopes.First()))
            .HasAbility(LogTheActivity.To(Console.Out));
    }

    [TestCleanup]
    public void TearDown()
    {
        //var scopes = TestWareAttributes.GetTestWareScopes(TestContext!.ManagedType!, TestContext.TestName!);
        //var Engine = TestWareProvider.GetTestWareComponent<ITestWareEngine>(scopes.First());
        //Engine.Dispose();
    }
}

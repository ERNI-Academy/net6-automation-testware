
using TestWare.Core.Interfaces;
using TestWare.Core;
using TestWare.Core.Configuration;
using TestWare.Core.Attributes;
using TestWare.Wheels.MsTestWheel;

namespace TestWare.Samples.Tests.MsTest;


[TestClass]
public abstract class TestSuiteBase
{
    public TestContext? TestContext { get; set; }
    protected string? EvidencePath { get; set; }

    protected StepWithEvidenceChain? Steps { get; set; }

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void ClassInitialize(TestContext context)
    {
        var reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        var data = TestWareAttributes.GetTestWareDocDict(Type.GetType(context.ManagedType!),null);
        reporter.StartTestSuite(context.ManagedType!, data);
    }

    [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void Cleanup()
    {
        var reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        reporter.StopTestSuite();
    }

    [TestInitialize]
    public void Setup()
    {
        var reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        var config = TestWareProvider.GetTestWareComponent<ITestWareConfiguration>();
        EvidencePath = Path.Combine(config.EvidenceBasePath, DateTime.UtcNow.Ticks.ToString());
        var data = TestWareAttributes.GetTestWareDocDict(null, Type.GetType(TestContext!.ManagedType!)?.GetMethod(TestContext.TestName!));
        reporter.StartTestCase(TestContext.TestName!, data);

        var scopes = TestWareAttributes.GetTestWareScopes(TestContext.ManagedType!, TestContext.TestName!);
        var engine = TestWareProvider.GetTestWareComponent<ITestWareEngine>(scopes.First());
        engine.Initialize();
        engine.StartRecordingEvidences();

        Steps = new StepWithEvidenceChain(EvidencePath!, engine, reporter);
        Steps.Step("Initial Page Loaded");
    }

    [TestCleanup]
    public void TearDown()
    {
        var reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        var scopes = TestWareAttributes.GetTestWareScopes(TestContext!.ManagedType!, TestContext.TestName!);
        var Engine = TestWareProvider.GetTestWareComponent<ITestWareEngine>(scopes.First());
        var evidence = Engine.StopRecordingEvidences(EvidencePath!, "network");
        reporter.AddTestCaseActivity(evidence);
        Engine.Dispose();
        reporter.StopTestCase(ResultMapper.Translate(TestContext.CurrentTestOutcome));
    }
}

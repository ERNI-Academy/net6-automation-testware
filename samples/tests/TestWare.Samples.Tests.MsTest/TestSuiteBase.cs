
using TestWare.Core.Interfaces;
using TestWare.Core;
using TestWare.Engines.SeleniumEngine;
using TestWare.Core.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AventStack.ExtentReports.Model;
using RazorEngine;
using ReportPortal.Client.Abstractions.Responses;

namespace TestWare.Samples.Tests.MsTest;


[TestClass]
public abstract class TestSuiteBase
{
    public TestContext? TestContext { get; set; }
    protected string? EvidencePath { get; set; }

    protected StepChain? Steps { get; set; }

    [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    public static void ClassInitialize(TestContext context)
    {
        var reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        var data = TestWareAttributeAssistant.GetTestWareDocDict(Type.GetType(context.ManagedType!),null);
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
        //TODO MOVE TO ATTRIBUTE
        var reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        var config = TestWareProvider.GetTestWareComponent<ITestWareConfiguration>();
        EvidencePath = Path.Combine(config.EvidenceBasePath, DateTime.UtcNow.Ticks.ToString());
        var data = TestWareAttributeAssistant.GetTestWareDocDict(null, Type.GetType(TestContext.ManagedType!)?.GetMethod(TestContext.TestName!));
        reporter.StartTestCase(TestContext.TestName!, data);

        var scopes = TestWareAttributeAssistant.GetTestWareScopes(TestContext.ManagedType!, TestContext.TestName!);
        var engine = TestWareProvider.GetTestWareComponent<ITestWareEngine>(scopes.First());
        engine.Initialize();
        engine.StartRecordingEvidences();

        Steps = new StepChain(EvidencePath!, engine, reporter);
        Steps.Step("Initial Page Loaded");
    }

    [TestCleanup]
    public void TearDown()
    {
        //TODO MOVE TO ATTRIBUTE
        var reporter = TestWareProvider.GetTestWareComponent<ITestWareCockpit>();
        var scopes = TestWareAttributeAssistant.GetTestWareScopes(TestContext.ManagedType!, TestContext.TestName!);
        var Engine = TestWareProvider.GetTestWareComponent<ITestWareEngine>(scopes.First());
        var config = TestWareProvider.GetTestWareComponent<ITestWareConfiguration>();
        var evidence = Engine.StopRecordingEvidences(EvidencePath, "network");
        reporter.AddTestCaseActivity(evidence);
        Engine.Dispose();
        reporter.StopTestCase(ResultMapper(TestContext.CurrentTestOutcome));
    }

    //TODO MOve to MSTestRunner
    protected TestWareResult ResultMapper(UnitTestOutcome result) =>
    result switch
    {
        UnitTestOutcome.Failed => TestWareResult.Fail,
        UnitTestOutcome.Passed => TestWareResult.Pass,
        UnitTestOutcome.Error => TestWareResult.Fail,
        UnitTestOutcome.Timeout => TestWareResult.Fail,
        UnitTestOutcome.Aborted => TestWareResult.Skip,
        UnitTestOutcome.NotRunnable => TestWareResult.Skip,
        _ => TestWareResult.Unkown,
    };

   
}

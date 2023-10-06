using System.Globalization;
using TestWare.Reporting.ExtentReport;
using TestWare.Reporting.AllureReport;

namespace TestWare.Samples.Selenium.Web;

[Binding]
public sealed class Hook
{
    private readonly TestContext _testContext;
    private int _stepCounter;
    private static readonly LifeCycle _lifeCycle = new();
    private static ExtentReport _extentReport;
    private static AllureReport _allureReport;

    public Hook(TestContext testContext)
    {
        _testContext = testContext;
    }

    [BeforeFeature]
    public static void BeforeFeature(FeatureContext featureContext)
    {
        var name = featureContext.FeatureInfo.Title;
        var tags = featureContext.FeatureInfo.Tags;

        _lifeCycle.BeginTestSuite(name);
        _extentReport.CreateFeature(name, tags);
    }

    [AfterFeature]
    public static void AfterFeature(FeatureContext featureContext)
    {
        _lifeCycle.EndTestSuite();
    }

    [BeforeScenario]
    public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var name = scenarioContext.ScenarioInfo.Arguments.Count > 0
            ? $"{DateTime.UtcNow.ToString("yyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture)} - {scenarioContext.ScenarioInfo.Title}"
            : scenarioContext.ScenarioInfo.Title;

        var description = scenarioContext.ScenarioInfo.Description ?? "";
        var scenarioTags = scenarioContext.ScenarioInfo.Tags;
        _extentReport.CreateTestCase(name, description, scenarioTags);

        _testContext.WriteLine("----------------------------------------- \r\n");
        _testContext.WriteLine($"Feature: {featureContext.FeatureInfo.Title}");
        _testContext.WriteLine($"   Scenario: {scenarioContext.ScenarioInfo.Title} \r\n");

        _stepCounter = 1;
        var tags = GetTags(featureContext, scenarioContext);
        _lifeCycle.BeginTestCase(name, tags);
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _extentReport.SetTestcaseOutcome(_testContext.CurrentTestOutcome);
        _lifeCycle.EndTestCase();
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        _lifeCycle.BeginTestExecution();
        _extentReport = new ExtentReport(_lifeCycle.GetCurrentResultsDirectory());
        _allureReport = new AllureReport();
        _allureReport.CleanResultsFolder();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _lifeCycle.EndTestExecution();
        _extentReport.CreateTestReportFile();
    }

    [BeforeStep]
    public void BeforeStep(ScenarioContext scenarioContext)
    {
        var name = scenarioContext.CurrentScenarioBlock.ToString();
        var description = scenarioContext.StepContext.StepInfo.Text;
        _extentReport.CreateStep(name, description);

        var stepId = $"{_stepCounter:00} {description}";
        _stepCounter++;
        _lifeCycle.BeginTestStep(stepId);
    }

    [AfterStep]
    public void AfterStep(ScenarioContext scenarioContext)
    {            
        _lifeCycle.EndTestStep();
        var evidencesPath = _lifeCycle.GetStepEvidences();

        foreach (var evidence in evidencesPath)
        {
            _extentReport.AddScreenshotToStep(evidence);
            _allureReport.AddAttachment(evidence, Path.GetFileNameWithoutExtension(evidence));
            _testContext.AddResultFile(evidence);
        }
    }

    private static List<string> GetTags(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var tags = featureContext.FeatureInfo.Tags.ToList();
        tags.AddRange(scenarioContext.ScenarioInfo.Tags.ToList());
        return tags;
    }
}

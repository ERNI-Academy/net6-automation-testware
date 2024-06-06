using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Net.NetworkInformation;
using System.Text.Json.Nodes;
using TestWare.Core;
using TestWare.Core.Interfaces;

namespace TestWare.Cockpits.ExtentReportsCockpit;

public class ExtentCockpit : ITestWareCockpit
{
    public const string Name = "ExtentReports";
    public ExtentReports Reporter { get; set; }
    private ExtentElementDefinition CurrentSuiteInfo;
    private Guid CurrentTestCase;
    private Guid CurrentTestStep;
    private Dictionary<Guid, ExtentTest> TestElements;
    private string ReportFile;
    public ExtentCockpit() { }

    public ExtentCockpit(JsonObject config)
    {
        ReportFile = Path.Combine(config["ReportPath"].ToString(), config["ReportName"].ToString());
    }
    public void Dispose()
    {
        Reporter.Flush();
    }

    public void Initialize()
    {
        Reporter = new ExtentReports();
        var SparkReporter = new ExtentSparkReporter(ReportFile);
        Reporter.AttachReporter(SparkReporter);
        //Reporter.AddSystemInfo();
        TestElements = new Dictionary<Guid, ExtentTest>();
    }

    public Guid StartTestSuite() => StartTestSuite("DefaultTestSuite", new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestSuite(string id) => StartTestSuite(id, new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestSuite(string id, Dictionary<string, IEnumerable<string>> attributes)
    {
        var info = new ExtentElementDefinition(attributes);
        info.Name = id;
        CurrentSuiteInfo = info;
        return Guid.Empty;
    }

    public Guid StartTestCase() => StartTestCase("DefaultTestCase", new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestCase(string id) => StartTestCase(id, new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestCase(string id, Dictionary<string, IEnumerable<string>> attributes)
    {
        var guid = Guid.NewGuid();
        var info = new ExtentElementDefinition(attributes);

        var testCase = Reporter.CreateTest(id, info.Description);
        testCase.AssignAuthor((CurrentSuiteInfo.Authors.Concat(info.Authors)).ToArray());
        testCase.AssignDevice(CurrentSuiteInfo.Devices.Concat(info.Devices).ToArray());
        testCase.AssignCategory(CurrentSuiteInfo.Tags.Concat(info.Tags).ToArray());

        TestElements.Add(guid, testCase);
        CurrentTestCase = guid;
        return guid;
    }

    public Guid StartTestStep() => StartTestStep("DefaultStep", new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestStep(string id) => StartTestStep(id, new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestStep(string id, Dictionary<string, IEnumerable<string>> attributes)
    {
        var guid = Guid.NewGuid();
        var info = new ExtentElementDefinition(attributes);

        var testStep = TestElements[CurrentTestCase].CreateNode(id, info.Description);

        testStep.Log(Status.Info, info.Description);
        testStep.AssignAuthor(info.Authors.ToArray());
        testStep.AssignDevice(info.Devices.ToArray());
        testStep.AssignCategory(info.Tags.ToArray());

        TestElements.Add(guid, testStep);
        CurrentTestStep = guid;
        return guid;
    }


    public Guid AddTestSuiteActivity(Guid id, params string[] activity) => Guid.Empty;

    public Guid AddTestSuiteActivity(params string[] activity) => Guid.Empty;

    public Guid AddTestCaseActivity(params string[] activity) => AddTestCaseActivity(CurrentTestCase, activity);
    public Guid AddTestCaseActivity(Guid id, params string[] activity) => AddActivity(id, activity);

    public Guid AddTestStepActivity(params string[] activity) => AddActivity(CurrentTestStep, activity);

    public Guid AddTestStepActivity(Guid id, params string[] activity) => AddActivity(id, activity);

    private Guid AddActivity(Guid id, params string[] activity)
    {
        var element = TestElements[id];
        foreach (var act in activity)
        {
            if (Path.Exists(act))
            {
                if (Path.GetExtension(act) == ".png")
                    element.Log(Status.Info, MediaEntityBuilder.CreateScreenCaptureFromPath(act).Build());
                else //TODO: Add files as evidences
                    element.Log(Status.Info, MediaEntityBuilder.CreateScreenCaptureFromPath(act).Build());
            }
            else
                element.Log(Status.Info, act);
        }
        return id;
    }

    public Guid StopTestSuite()
    {
        CurrentSuiteInfo = null;
        return Guid.Empty;
    }
    public Guid StopTestSuite(Guid id) => StopTestSuite();
    public Guid StopTestSuite(TestWareResult result) => StopTestSuite();
    public Guid StopTestSuite(Guid id, TestWareResult result) => StopTestSuite();

    public Guid StopTestCase() => CurrentTestCase;
    public Guid StopTestCase(TestWareResult result, string details = "") => SetTestElementResult(CurrentTestCase, result, details);
    public Guid StopTestCase(Guid id) => id;
    public Guid StopTestCase(Guid id, TestWareResult result, string details = "") => SetTestElementResult(id, result, details);

    public Guid StopTestStep() => CurrentTestStep;
    public Guid StopTestStep(TestWareResult result, string details = "") => SetTestElementResult(CurrentTestStep, result, details);
    public Guid StopTestStep(Guid id) => id;
    public Guid StopTestStep(Guid id, TestWareResult result, string details = "") => SetTestElementResult(id, result, details);

    private Guid SetTestElementResult(Guid id, TestWareResult result, string details)
    {
        var element = TestElements[id];
        switch (result)
        {
            case TestWareResult.Pass: element.Pass(details); break;
            case TestWareResult.Fail: element.Fail(details); break;
            case TestWareResult.Skip: element.Skip(details); break;
            default: element.Warning(); break;
        };
        return id;
    }

}

public class ExtentElementDefinition
{
    public string Name = "";
    public string Description = "";
    public IEnumerable<string> Tags = new List<string>();
    public IEnumerable<string> Devices = new List<string>();
    public IEnumerable<string> Authors = new List<string>();

    public ExtentElementDefinition() { }
    public ExtentElementDefinition(Dictionary<string, IEnumerable<string>> data)
    {
        if (data.ContainsKey("Name")) Name = data["Name"].First();
        if (data.ContainsKey("Description")) Description = data["Description"].First();
        if (data.ContainsKey("Tags")) Tags = data["Tags"];
        if (data.ContainsKey("Devices")) Devices = data["Devices"];
        if (data.ContainsKey("Authors")) Authors = data["Authors"];
    }
}


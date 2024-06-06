using ReportPortal.Client.Abstractions.Requests;
using ReportPortal.Client;
using ReportPortal.Client.Abstractions.Responses;
using ReportPortal.Client.Abstractions.Models;
using TestWare.Core;
using TestWare.Core.Interfaces;
using System.Text.Json.Nodes;

namespace TestWare.Cockpits.ReportPortalCockpit;

public class ReportPortalCockpit : ITestWareCockpit
{
    public const string Name = "ReportPortal";
    public Service Reporter { get; set; }
    private LaunchCreatedResponse Launch;
    private Guid CurrentTestSuite;
    private Guid CurrentTestCase;
    private Guid CurrentTestStep;
    private Dictionary<Guid, string> TestElements;
    private string Url;
    private string Project;
    private string ApiKey;

    public ReportPortalCockpit() { }

    public ReportPortalCockpit(JsonObject config)
    {
        Url = config["Url"].ToString();
        Project = config["ProjectName"].ToString();
        ApiKey = config["ApiKey"].ToString();
    }

    public async void Dispose()
    {
        await Reporter.Launch.FinishAsync(Launch.Uuid, new FinishLaunchRequest());
    }

    public void Initialize()
    {
        Reporter = new ReportPortal.Client.Service(new Uri(Url), Project, ApiKey);
        var request = new StartLaunchRequest
        {
            Name = "LaunchName",
            Description = "LaunchDescription"
        };
        Launch = Reporter.Launch.StartAsync(request).Result;

        TestElements = new Dictionary<Guid, string>();
    }

    public Guid StartTestSuite() => StartTestSuite("DefaultTestSuite", new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestSuite(string id) => StartTestSuite(id, new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestSuite(string id, Dictionary<string, IEnumerable<string>> attributes)
    {
        CurrentTestSuite = StartTestElement(TestItemType.Suite, id, attributes);
        return CurrentTestSuite;
    }

    public Guid StartTestCase() => StartTestCase("DefaultTestCase", new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestCase(string id) => StartTestCase(id, new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestCase(string id, Dictionary<string, IEnumerable<string>> attributes)
    {
        CurrentTestCase = StartTestElement(TestItemType.Test, id, attributes);
        return CurrentTestCase;
    }

    public Guid StartTestStep() => StartTestStep("DefaultStep", new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestStep(string id) => StartTestStep(id, new Dictionary<string, IEnumerable<string>>());
    public Guid StartTestStep(string id, Dictionary<string, IEnumerable<string>> attributes)
    {
        CurrentTestStep = StartTestElement(TestItemType.Step, id, attributes);
        return CurrentTestStep;
    }

    private Guid StartTestElement(TestItemType type, string id, Dictionary<string, IEnumerable<string>> attributes)
    {
        var parent = type switch
        {
            TestItemType.Test => TestElements[CurrentTestSuite],
            TestItemType.Step => TestElements[CurrentTestCase],
            _ => null
        };

        var request = new StartTestItemRequest
        {
            LaunchUuid = Launch.Uuid,
            Name = id,
            Type = type,
        };

        TestItemCreatedResponse element;

        if (parent == null)
            element = Reporter.TestItem.StartAsync(request).Result;
        else
            element = Reporter.TestItem.StartAsync(parent, request).Result;

        var guid = Guid.NewGuid();
        TestElements.Add(guid, element.Uuid);
        return guid;
    }

    public Guid AddTestSuiteActivity(Guid id, params string[] activity) => AddActivity(id, activity);

    public Guid AddTestSuiteActivity(params string[] activity) => AddTestSuiteActivity(CurrentTestSuite, activity);

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
                var mimeType = MimeTypes.MimeTypeMap.GetMimeType(act);
                var response = Reporter.LogItem.CreateAsync(new CreateLogItemRequest
                {
                    TestItemUuid = element,
                    Text = act,
                    Attach = new LogItemAttach(mimeType, File.ReadAllBytes(act)),
                }).Result;


            }
            else
            {
                var response = Reporter.LogItem.CreateAsync(new CreateLogItemRequest
                {
                    TestItemUuid = element,
                    Text = act,
                }).Result;
            }

        }
        return id;
    }


    public Guid StopTestSuite() => CurrentTestSuite;
    public Guid StopTestSuite(Guid id) => id;
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

        var status = result switch
        {
            TestWareResult.Pass => Status.Passed,
            TestWareResult.Fail => Status.Failed,
            TestWareResult.Skip => Status.Skipped,
            _ => Status.Warn
        };

        var response = Reporter.TestItem.FinishAsync(element, new FinishTestItemRequest
        {
            Status = status,
            Description = details
        }).Result;

        return id;
    }

}
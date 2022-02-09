namespace TestWare.Core.AutomationEntities;

public class AutomationSummary
{
    private readonly ExecutableItem _testRun;
    public ExecutableItem TestRun { get { return _testRun; } }

    public AutomationSummary(string id)
    {
        _testRun = new ExecutableItem(id);
        _testRun.StartExecution();
    }

    public void StopExecution()
    {
        _testRun.StopExecution();
    }
    public void StartTestSuite(string id)
    {
        var currentSuite = _testRun.AddItem(id); 
        currentSuite.StartExecution();
    }

    public void StopTestSuite()
    {
        var currentSuite = _testRun.GetCurrentItem();
        currentSuite.StopExecution();
    }

    public string BuildSuitePath()
    {
        var currentSuite = _testRun.GetCurrentItem();
        var path = Path.Combine(_testRun.Id, currentSuite.Id);
        return path;

    }
    public void StartTestCase(string id)
    {
        var currentSuite = _testRun.GetCurrentItem();
        var testcase = currentSuite.AddItem(id);
        testcase.StartExecution();
    }

    public void StopTestCase()
    {
        var currentSuite = _testRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        testcase.StopExecution();
    }

    public string BuildTestPath()
    {
        var currentSuite = _testRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var path = Path.Combine(_testRun.Id, currentSuite.Id, testcase.Id);
        return path;

    }
    public void StartTestStep(string id)
    {
        var currentSuite = _testRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.AddItem(id);
        teststep.StartExecution();
    }

    public void StopTestStep()
    {
        var currentSuite = _testRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.GetCurrentItem();
        teststep.StopExecution();
    }

    public string BuildStepPath()
    {
        var currentSuite = _testRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.GetCurrentItem();
        var path = Path.Combine(_testRun.Id, currentSuite.Id,testcase.Id, teststep.Id);
        return path;
    }
    public string GetCurrentStepId()
    {
        var currentSuite = _testRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.GetCurrentItem();
        return teststep.Id;
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Core.AutomationEntities;

public class AutomationSummary
{
    public ExecutableItem TestRun;

    public AutomationSummary(string id)
    {
        TestRun = new ExecutableItem(id);
        TestRun.StartExecution();
    }

    public void StopExecution()
    {
        TestRun.StopExecution();
    }
    public void StartTestSuite(string id)
    {
        var currentSuite = TestRun.AddItem(id); 
        currentSuite.StartExecution();
    }

    public void StopTestSuite()
    {
        var currentSuite = TestRun.GetCurrentItem();
        currentSuite.StopExecution();
    }

    public string BuildSuitePath()
    {
        var currentSuite = TestRun.GetCurrentItem();
        var path = Path.Combine(TestRun.Id, currentSuite.Id);
        return path;

    }
    public void StartTestCase(string id)
    {
        var currentSuite = TestRun.GetCurrentItem();
        var testcase = currentSuite.AddItem(id);
        testcase.StartExecution();
    }

    public void StopTestCase()
    {
        var currentSuite = TestRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        testcase.StopExecution();
    }

    public string BuildTestPath()
    {
        var currentSuite = TestRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var path = Path.Combine(TestRun.Id, currentSuite.Id, testcase.Id);
        return path;

    }
    public void StartTestStep(string id)
    {
        var currentSuite = TestRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.AddItem(id);
        teststep.StartExecution();
    }

    public void StopTestStep()
    {
        var currentSuite = TestRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.GetCurrentItem();
        teststep.StopExecution();
    }

    public string BuildStepPath()
    {
        var currentSuite = TestRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.GetCurrentItem();
        var path = Path.Combine(TestRun.Id, currentSuite.Id,testcase.Id, teststep.Id);
        return path;
    }
    public string GetCurrentStepId()
    {
        var currentSuite = TestRun.GetCurrentItem();
        var testcase = currentSuite.GetCurrentItem();
        var teststep = testcase.GetCurrentItem();
        return teststep.Id;
    }

}

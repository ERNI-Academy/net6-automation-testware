using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Core.Interfaces;

public interface ITestWareCockpit
{
    Guid AddTestCaseActivity(Guid id, params string[] activity);
    Guid AddTestCaseActivity(params string[] activity);
    Guid AddTestStepActivity(Guid id, params string[] activity);
    Guid AddTestStepActivity(params string[] activity);
    Guid AddTestSuiteActivity(Guid id, params string[] activity);
    Guid AddTestSuiteActivity(params string[] activity);
    void Dispose();
    void Initialize();
    Guid StartTestCase();
    Guid StartTestCase(string id);
    Guid StartTestCase(string id, Dictionary<string, IEnumerable<string>> attributes);
    Guid StartTestStep();
    Guid StartTestStep(string id);
    Guid StartTestStep(string id, Dictionary<string, IEnumerable<string>> attributes);
    Guid StartTestSuite();
    Guid StartTestSuite(string id);
    Guid StartTestSuite(string id, Dictionary<string, IEnumerable<string>> attributes);
    Guid StopTestCase();
    Guid StopTestCase(Guid id);
    Guid StopTestCase(Guid id, TestWareResult result, string details = "");
    Guid StopTestCase(TestWareResult result, string details = "");
    Guid StopTestStep();
    Guid StopTestStep(Guid id);
    Guid StopTestStep(Guid id, TestWareResult result, string details = "");
    Guid StopTestStep(TestWareResult result, string details = "");
    Guid StopTestSuite();
    Guid StopTestSuite(Guid id);
    Guid StopTestSuite(Guid id, TestWareResult result);
    Guid StopTestSuite(TestWareResult result);
}
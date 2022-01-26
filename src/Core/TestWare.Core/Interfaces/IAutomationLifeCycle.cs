using System.Reflection;
using TestWare.Core.Configuration;

namespace TestWare.Core.Interfaces;

public interface IAutomationLifeCycle
{
    public void BeginTestExecution(string id);
    public void EndTestExecution();
    public void BeginTestSuite(string id);
    public void EndTestSuite();
    public void BeginTestCase(string id, IEnumerable<string> tags);
    public void EndTestCase();
    public void BeginTestStep(string id);
    public void EndTestStep();

}


using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using TestWare.Core.AutomationEntities;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;

namespace TestWare.Core;

public abstract class AutomationLifeCycleBase : IAutomationLifeCycle
{
    public AutomationSummary Summary;

    public IEnumerable<IEngineManager> Engines { get; private set; }
    public TestConfiguration TestConfiguration;


    protected abstract IEnumerable<Assembly> GetTestWareComponentAssemblies();
    protected abstract IEnumerable<IEngineManager> GetTestWareEngines();
    protected abstract TestConfiguration GetConfiguration();

    public void BeginTestExecution(string id)
    {
        var assemblies = GetTestWareComponentAssemblies();
        ContainerManager.RegisterTestwareComponents(assemblies);
        Engines = GetTestWareEngines();
        TestConfiguration = GetConfiguration();
        Summary = new(id);
        CreateTestResultsDirectory(TestConfiguration.TestResultPath, id);
    }

    public void BeginTestExecution()
    {
        var id = $"{DateTime.UtcNow.ToString("yyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture)}";
        BeginTestExecution(id);
    }

    public void BeginTestSuite(string id)
    {
        Summary.StartTestSuite(id);
        var path = Summary.BuildSuitePath();
        CreateTestResultsDirectory(TestConfiguration.TestResultPath, path);
    }

    public void BeginTestCase(string id, IEnumerable<string> tags)
    {
        foreach(var engine in Engines)
        {
            engine.Initialize(tags, TestConfiguration);
        }
        ContainerManager.BuildContainer();
        Summary.StartTestCase(id);
        var path = Summary.BuildTestPath();
        CreateTestResultsDirectory(TestConfiguration.TestResultPath, path);
    }

    public void BeginTestStep(string name)
    {
        var id = ResultPathValidation(name);
        Summary.StartTestStep(id);
    }

    public void EndTestStep()
    {
        var path = Path.Combine(TestConfiguration.TestResultPath,Summary.BuildTestPath());
        path = ResultPathValidation(path);
        var stepName = Summary.GetCurrentStepId();

        foreach (var engine in Engines)
        {
            var name = $"{stepName} - {engine.GetEngineName()}";
            engine.CollectEvidence(path, name);
        }
        Summary.StopTestStep();
    }

    public void EndTestCase()
    {
        foreach (var engine in Engines)
        {
            engine.Destroy();
        }
        Summary.StopTestCase();
        ContainerManager.DisposeContainer();
    }

    public void EndTestSuite()
    {
        Summary.StopTestSuite();
    }

    public void EndTestExecution()
    {
        Summary.StopExecution();
        //TODO Create summary report
    }

    private string CreateTestResultsDirectory(string path, string name)
    {
        var testResultsDirectory = Path.Combine(path, name);
        testResultsDirectory = ResultPathValidation(testResultsDirectory);

        if (Directory.Exists(testResultsDirectory))
        {
            Directory.Delete(testResultsDirectory);
        }

        Directory.CreateDirectory(testResultsDirectory);

        return testResultsDirectory;
    }

    public string GetCurrentTestResultsDirectory()
    {
        return GetResultsDirectory(Summary.BuildTestPath());
    }
    public IEnumerable<string> GetStepEvidences()
    {
        var path = Path.Combine(TestConfiguration.TestResultPath, Summary.BuildTestPath());
        path = ResultPathValidation(path);
        var stepName = Summary.GetCurrentStepId();

        DirectoryInfo evidenceFolder = new DirectoryInfo(path);
        var evidences = evidenceFolder.GetFiles().Where<FileInfo>(f => f.Name.StartsWith(stepName)).Select(f => f.FullName);

        return evidences;
    }
    public string GetCurrentSuitetResultsDirectory()
    {
        return GetResultsDirectory(Summary.BuildSuitePath());
    }

    public string GetCurrentResultsDirectory()
    {
        return GetResultsDirectory(Summary.TestRun.Id);
    }

    private string GetResultsDirectory(string relativePath)
    {
        var path = Path.Combine(TestConfiguration.TestResultPath, relativePath);
        path = ResultPathValidation(path);
        return path;
    }

    protected virtual string ResultPathValidation(string path)
    {
        path = Regex.Replace(path, @"[^a-zA-Z0-9- \\$:_]+", string.Empty, RegexOptions.Compiled);
        path = path.Length >= 247 ? path.Substring(0, 244) : path;
        return path;
    }



}

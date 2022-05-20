using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using TestWare.Core.AutomationEntities;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;

namespace TestWare.Core;

public abstract class AutomationLifeCycleBase : IAutomationLifeCycle
{
    private AutomationSummary _summary;
    private TestConfiguration _configuration;
    public IEnumerable<IEngineManager> Engines { get; private set; }
    public TestConfiguration TestConfiguration { get { return _configuration; } }
    public AutomationSummary Summary { get { return _summary; } }


    protected abstract IEnumerable<Assembly> GetTestWareComponentAssemblies();
    protected abstract IEnumerable<IEngineManager> GetTestWareEngines();
    protected abstract TestConfiguration GetConfiguration();

    public void BeginTestExecution(string id)
    {
        var assemblies = GetTestWareComponentAssemblies();
        ContainerManager.RegisterTestwareComponents(assemblies);
        Engines = GetTestWareEngines();
        _configuration = GetConfiguration();
        _summary = new(id);
        CreateTestResultsDirectory(_configuration.TestResultPath, id);
    }

    public void BeginTestExecution()
    {
        var id = $"{DateTime.UtcNow.ToString("yyy-MM-dd HH-mm-ss", CultureInfo.InvariantCulture)}";
        BeginTestExecution(id);
    }

    public void BeginTestSuite(string id)
    {
        _summary.StartTestSuite(id);
        var path = _summary.BuildSuitePath();
        CreateTestResultsDirectory(_configuration.TestResultPath, path);
    }

    public void BeginTestCase(string id, IEnumerable<string> tags)
    {
        foreach(var engine in Engines)
        {
            engine.Initialize(tags, _configuration);
        }
        ContainerManager.BuildContainer();
        _summary.StartTestCase(id);
        var path = _summary.BuildTestPath();
        CreateTestResultsDirectory(_configuration.TestResultPath, path);
    }

    public void BeginTestStep(string id)
    {
        var name = ResultPathValidation(id);
        _summary.StartTestStep(name);
    }

    public void EndTestStep()
    {
        var path = Path.Combine(_configuration.TestResultPath,Summary.BuildTestPath());
        path = ResultPathValidation(path);
        var stepName = _summary.GetCurrentStepId();

        foreach (var engine in Engines)
        {
            var name = $"{stepName} - {engine.GetEngineName()}";
            engine.CollectEvidence(path, name);
        }
        _summary.StopTestStep();
    }

    public void EndTestCase()
    {
        foreach (var engine in Engines)
        {
            engine.Destroy();
        }
        _summary.StopTestCase();
        ContainerManager.DisposeContainer();
    }

    public void EndTestSuite()
    {
        _summary.StopTestSuite();
    }

    public void EndTestExecution()
    {
        _summary.StopExecution();
    }

    private void CreateTestResultsDirectory(string path, string name)
    {
        var testResultsDirectory = Path.Combine(path, name);
        testResultsDirectory = ResultPathValidation(testResultsDirectory);

        if (Directory.Exists(testResultsDirectory))
        {
            Directory.Delete(testResultsDirectory, true);
        }

        Directory.CreateDirectory(testResultsDirectory);
    }

    public string GetCurrentTestResultsDirectory()
    {
        return GetResultsDirectory(_summary.BuildTestPath());
    }
    public IEnumerable<string> GetStepEvidences()
    {
        var path = Path.Combine(_configuration.TestResultPath, _summary.BuildTestPath());
        path = ResultPathValidation(path);
        var stepName = _summary.GetCurrentStepId();

        var evidenceFolder = new DirectoryInfo(path);
        var evidences = evidenceFolder.GetFiles().Where<FileInfo>(f => f.Name.StartsWith(stepName)).Select(f => f.FullName);

        return evidences;
    }
    public string GetCurrentSuitetResultsDirectory()
    {
        return GetResultsDirectory(_summary.BuildSuitePath());
    }

    public string GetCurrentResultsDirectory()
    {
        return GetResultsDirectory(_summary.TestRun.Id);
    }

    private string GetResultsDirectory(string relativePath)
    {
        var path = Path.Combine(_configuration.TestResultPath, relativePath);
        path = ResultPathValidation(path);
        return path;
    }

    protected virtual string ResultPathValidation(string path)
    {
        path = Regex.Replace(path, @"[^a-zA-Z0-9- \\$:_]+", string.Empty, RegexOptions.Compiled);
        path = path.Length < 247 ? path : path[..244];
        return path;
    }



}

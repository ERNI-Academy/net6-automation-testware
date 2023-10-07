using Allure.Commons;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TestWare.Reporting.AllureReport;

public class AllureReport
{
    private const string AllureConfigFile = "allureConfig.json";
    private const string AllureEnvironmentFile = "environment.properties";
    private const string AllureResultsFolder = "allure-results";

    private readonly Capabilities AllureCapabilities;

    public AllureReport(JsonObject capabilities)
    {
        if (capabilities == null) throw new ArgumentNullException(nameof(capabilities));
        AllureCapabilities = JsonConvert.DeserializeObject<Capabilities>(capabilities.ToString());

        if (AllureCapabilities == null) throw new ArgumentNullException(nameof(AllureCapabilities));
        SetIssueTrackerBaseUrl(AllureCapabilities.IssueTrackerBaseUrl);
        SetTestManagementSystemBaseUrl(AllureCapabilities.TestManagementSystemBaseUrl);
    }

    public void AddAttachment(string path, string attachmentTitle)
    {
        AllureLifecycle.Instance.AddAttachment(path, attachmentTitle);
    }

    public void CleanResultsFolder()
    {
        AllureLifecycle.Instance.CleanupResultDirectory();
    }

    public void GenerateAllureEnvironmentFile()
    {
        using var sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AllureResultsFolder, AllureEnvironmentFile), true);
        foreach (var keyValuePair in AllureCapabilities.EnvironmentValues)
        {
            sw.WriteLine("{0}={1}", keyValuePair.Key, keyValuePair.Value);
        }
    }

    private void SetIssueTrackerBaseUrl(string issueTrackerBaseUrl)
    {
        string text = ReadAllureConfigFile();
        text = Regex.Replace(text, @".*{issue}.*", '"' + issueTrackerBaseUrl + "{issue}");
        WriteAllureConfigFile(text);
    }

    private void SetTestManagementSystemBaseUrl(string testManagementSystemBaseUrl)
    {
        string text = ReadAllureConfigFile();
        text = Regex.Replace(text, @".*{tms}.*", '"' + testManagementSystemBaseUrl + "{tms}");
        WriteAllureConfigFile(text);
    }

    private string ReadAllureConfigFile()
    {
        var allureConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AllureConfigFile);
        return File.ReadAllText(allureConfigPath, Encoding.UTF8);
    }

    private void WriteAllureConfigFile(string text)
    {
        var allureConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AllureConfigFile);
        File.WriteAllText(allureConfigPath, text);
    }
}

using Allure.Commons;
using Allure.Commons.Configuration;

namespace TestWare.Reporting.AllureReport;

public class AllureReport : AllureConfiguration
{
    private AllureConfiguration _allureConfiguration;

    public AllureReport(string title, HashSet<string> links)
    {
        _allureConfiguration = new AllureConfiguration(title, null, links);
    }

    public void AddAttachment(string path, string attachmentTitle)
    {
        AllureLifecycle.Instance.AddAttachment(path, attachmentTitle);
    }

    public void CleanResultsFolder()
    {
        AllureLifecycle.Instance.CleanupResultDirectory();
    }

    public void SetIssueTrackerUrl(string issueTrackerBaseUrl)
    { 
       
    }
}

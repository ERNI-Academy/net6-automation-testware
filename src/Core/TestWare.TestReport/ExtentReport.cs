using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestWare.Reporting.ExtentReport
{
    public class ExtentReport
    {
        private static ExtentHtmlReporter _htmlReporter;
        private static ExtentReports _extentReport;
        private static ExtentTest _extentStep;
        private static ExtentTest _extentTest;
        private static ExtentTest _extentFeature;

        public ExtentReport(string reportFolderPath)
        {
            _htmlReporter = new ExtentHtmlReporter(reportFolderPath + "\\");
            _extentReport = new ExtentReports();
            _extentReport.AttachReporter(_htmlReporter);
        }

        public void CreateTestReportFile()
        {
            _extentReport.Flush();
        }

        public void CreateFeature(string name, IEnumerable<string> tags)
        {
            _extentFeature = _extentReport.CreateTest(new GherkinKeyword("Feature"), $"Feature: {name}");

            foreach (var tag in tags)
            {
                _extentFeature.AssignCategory(tag);
            }
        }

        public void CreateTestCase(string name, string description, IEnumerable<string> tags)
        {
            _extentTest = _extentFeature.CreateNode(new GherkinKeyword("Scenario"), name, description);

            foreach (var tag in tags)
            {
                _extentTest.AssignCategory(tag);
            }
        }

        public void CreateStep(string name, string description )
        {
            _extentStep = _extentTest.CreateNode(new GherkinKeyword(name), description);
        }

        public void AddScreenshotToStep(string screenshotPath)
        {
            _extentStep.AddScreenCaptureFromPath(screenshotPath);
        }

        public void SetTestcaseOutcome(UnitTestOutcome testOutcome)
        {
            switch (testOutcome)
            {
                case UnitTestOutcome.Passed:
                    _extentTest.Pass("");
                    break;
                case UnitTestOutcome.Failed:
                    _extentTest.Fail("");
                    break;
                case UnitTestOutcome.Inconclusive:
                case UnitTestOutcome.Aborted:
                default:
                    _extentTest.Skip("");
                    break;
            }
        }
    }
}
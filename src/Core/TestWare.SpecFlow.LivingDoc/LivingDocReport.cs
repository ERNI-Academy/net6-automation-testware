using System.Diagnostics;

namespace TestWare.SpecFlow.LivingDoc;

public class LivingDocReport
{
    private const string LIVING_DOC_TOOL_NAME = "SpecFlow.Plus.LivingDoc.CLI";
    private readonly string reportFolderPath;
    private readonly string executionAssemblyPath;

    public LivingDocReport(string reportFolderPath, string executionAssemblyPath) 
    {
        this.reportFolderPath = reportFolderPath;
        this.executionAssemblyPath = executionAssemblyPath;
        InstallSpecflowLivingDocTool();
    }
    
    public void GenerateLivingDocReport()
    {
        var executionAssemblyFolderPath = Path.GetDirectoryName(executionAssemblyPath);
        var command = $"livingdoc test-assembly \"{executionAssemblyPath}\" -t \"{executionAssemblyFolderPath}\\TestExecution.json\" --output \"{reportFolderPath}\\LivingDoc.html\"";
        Process.Start("CMD.exe", command);
        Process process = new();
        ProcessStartInfo startInfo = new()
        {
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = "cmd.exe",
            Arguments = command
        };
        process.StartInfo = startInfo;
        process.Start();
    }
        
    private static void InstallSpecflowLivingDocTool()
    {
        Process.Start("dotnet", $"tool install --global {LIVING_DOC_TOOL_NAME}");
    }
}

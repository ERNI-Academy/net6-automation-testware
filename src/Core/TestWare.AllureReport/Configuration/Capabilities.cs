using Newtonsoft.Json;

namespace TestWare.Reporting.AllureReport;

internal class Capabilities
{
    public string? IssueTrackerBaseUrl { get; set; }

    public string? IssueTagRegex { get; set; }

    public string? TestManagementSystemBaseUrl { get; set; }

    public string? TestManagementSystemTagRegex { get; set; }

    public IEnumerable<EnvironmentValue> EnvironmentValues { get; set; } = Enumerable.Empty<EnvironmentValue>();
}

public partial class EnvironmentValue
{
    public string? Key { get; set; }

    public string? Value { get; set; }
}


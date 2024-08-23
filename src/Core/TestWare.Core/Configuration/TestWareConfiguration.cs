namespace TestWare.Core.Configuration;

public class TestWareConfiguration : ITestWareConfiguration
{
    public IEnumerable<ConfigurationScope> Scopes { get; set; }
    public IEnumerable<ConfigurationScope> CockpitScopes { get; set; }
    public string EvidenceBasePath { get; set; }
}

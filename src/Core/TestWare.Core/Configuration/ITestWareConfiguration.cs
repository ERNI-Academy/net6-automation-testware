
namespace TestWare.Core.Configuration;

public interface ITestWareConfiguration
{
    string EvidenceBasePath { get; set; }
    IEnumerable<ConfigurationScope> Scopes { get; set; }
    IEnumerable<ConfigurationScope> CockpitScopes { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Core.Configuration;

public class TestWareConfiguration : ITestWareConfiguration
{
    public IEnumerable<ConfigurationScope> Scopes { get; set; }
    public IEnumerable<ConfigurationScope> CockpitScopes { get; set; }
    public string EvidenceBasePath { get; set; }
}

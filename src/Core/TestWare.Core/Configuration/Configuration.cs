

using System.Text.Json.Nodes;

namespace TestWare.Core.Configuration;

public class Configuration
{
    public string Tag { get; set; }
    public IEnumerable<JsonObject> Capabilities { get; set; }
}

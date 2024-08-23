using System.Text.Json.Nodes;

namespace TestWare.Core.Configuration;


public class ConfigurationScope
{
    public string ScopeName { get; set; }

    public string CoreName { get; set; }

    public JsonObject Config { get; set; }
}

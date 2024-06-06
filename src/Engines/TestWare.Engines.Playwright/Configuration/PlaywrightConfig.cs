using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TestWare.Engines.PlaywrightEngine.Configuration;

internal class PlaywrightConfig
{
    public string Browser { get; set; } = default!;
    public string BaseUrl { get; set; } = default!;

    public JsonObject LaunchOptions { get; set; } = [];
    public JsonObject PageOptions { get; set;} = [];
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TestWare.Engines.SeleniumEngine.Configuration;

public class SeleniumConfig
{
    public string Driver { get; set; } = default!;
    public string BaseUrl { get; set; } = default!;

    public float ImplicitWaitSeconds { get; set; } = default!;

    public bool CollectNetworkEvidences { get; set; } = false;

    public string Service { get; set; } = default!;

    public JsonObject Options { get; set; } = new JsonObject();
}

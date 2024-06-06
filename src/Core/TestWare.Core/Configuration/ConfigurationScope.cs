using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TestWare.Core.Configuration;


public class ConfigurationScope
{
    public string ScopeName { get; set; }

    public string CoreName { get; set; }

    public JsonObject Config { get; set; }
}

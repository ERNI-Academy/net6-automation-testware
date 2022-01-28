using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Engines.Restsharp.Factory;

namespace TestWare.Engines.Restsharp.Resources;

public class ApiResource : ResourceBase
{
    protected string ResourceName { get; set; }

    public ApiResource(IApiClient client) : base()
    {
        Client = (ApiClient)client;
    }
}

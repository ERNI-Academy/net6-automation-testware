using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;
using TestWare.Samples.API.Resources.Factor;

namespace TestWare.Samples.API.Resources.Integrate;

internal class IntegrateResource : ApiResource, IIntegrateResource
{
    private const string _resourceName = "integrate";

    public IntegrateResource(IApiClient client) : base(client)
    {
        ResourceName = _resourceName;
    }

    public RestResponse<IntegrateResponse> Integrate(string formula)
    {
        var encodedUrl = HttpUtility.UrlEncode(formula);
        var req = new RestRequest($"{ResourceName}/{encodedUrl}");
        var response = ExecuteRequest<IntegrateResponse>(req);
        return response;
    }
}




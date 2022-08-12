using RestSharp;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Integrate;

internal class IntegrateResource : ApiResource, IIntegrateResource
{
    private const string _resourceName = "integrate";

    public IntegrateResource(IApiClient client) : base(client)
    {
        ResourceName = _resourceName;
    }

    public async Task<RestResponse<IntegrateResponse>> Integrate(string formula)
    {
        var encodedUrl = HttpUtility.UrlEncode(formula);
        var req = new RestRequest($"{ResourceName}/{encodedUrl}");
        var response = await ExecuteRequest<IntegrateResponse>(req);
        return response;
    }
}




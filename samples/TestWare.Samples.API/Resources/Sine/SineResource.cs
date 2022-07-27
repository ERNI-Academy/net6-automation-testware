using RestSharp;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Sine;

internal class SineResource : ApiResource, ISineResource
{
    private const string _resourceName = "sin";

    public SineResource(IApiClient client) : base(client)
    {
        ResourceName = _resourceName;
    }

    async Task<RestResponse<SineResponse>> ISineResource.Sine(string operation)
    {
        var encodedUrl = HttpUtility.UrlEncode(operation);
        var req = new RestRequest($"{ResourceName}/{encodedUrl}");
        var response = await ExecuteRequest<SineResponse>(req);
        return response;
    }
}

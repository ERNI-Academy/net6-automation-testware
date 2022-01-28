using RestSharp;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Simplify;

public class SimplifyResource : ApiResource, ISimplifyResource
{
    private const string _resourceName = "simplify";
    
    public SimplifyResource(IApiClient client) : base(client)
    {
        ResourceName = _resourceName;
    }

    public RestResponse<SimplifyResponse> Simplify(string formula)
    {
        var encodedUrl = HttpUtility.UrlEncode(formula);
        var req = new RestRequest($"{ResourceName}/{encodedUrl}");
        var response = ExecuteRequest<SimplifyResponse>(req);
        return response;
    }
}




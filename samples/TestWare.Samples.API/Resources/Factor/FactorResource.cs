using RestSharp;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Factor;

public class FactorResource : ApiResource, IFactorResource
{
    private const string _resourceName = "factor";

    public FactorResource(IApiClient client) : base(client)
    {
        ResourceName = _resourceName;
    }

    public async Task<RestResponse<FactorResponse>> Factor(string formula)
    {
        var encodedUrl = HttpUtility.UrlEncode(formula);
        var req = new RestRequest($"{ResourceName}/{encodedUrl}");
        var response = await ExecuteRequest<FactorResponse>(req);
        return response;
    }
}




using RestSharp;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Age;

internal class AgeResource : ApiResource, IAgeResource
{
    private const string _resourceName = "";

    public AgeResource(IApiClient client) : base(client)
    {
        ResourceName = _resourceName;
    }

    public RestResponse<AgeResponse> GuessAge(string name)
    {
        var req = new RestRequest($"{ResourceName}");
        req.AddParameter("name", name);
        var response = ExecuteRequest<AgeResponse>(req);
        return response;
    }
}


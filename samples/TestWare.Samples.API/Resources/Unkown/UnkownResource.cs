using RestSharp;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Unkown;

internal class UnkownResource : ApiResource, IUnkownResource
{
    private const string _resourceName = "unkown/invalid";

    public UnkownResource(IApiClient client) : base(client)
    {
        ResourceName = _resourceName;
    }

    public async Task<RestResponse<UnkownResponse>> Operate()
    {
        var req = new RestRequest($"{ResourceName}");
        var response = await ExecuteRequest<UnkownResponse>(req);
        return response;
    }
}

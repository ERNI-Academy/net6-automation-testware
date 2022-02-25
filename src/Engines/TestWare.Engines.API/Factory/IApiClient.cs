

using RestSharp;

namespace TestWare.Engines.Restsharp.Factory;

public interface IApiClient
{
    public void EnqueueResponse(RestResponse response);

    public void ClearResponseQueue();
    public IEnumerable<RestResponse> GetRestResponses();
}

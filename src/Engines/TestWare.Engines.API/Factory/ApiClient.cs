using RestSharp;

namespace TestWare.Engines.Restsharp.Factory;

public class ApiClient : RestClient, IApiClient
{
    internal Queue<RestResponse> responseQueue = new();

    public ApiClient()
    {
    }

    public ApiClient(HttpMessageHandler handler) : base(handler)
    {
    }

    public ApiClient(RestClientOptions options) : base(options)
    {
    }

    public ApiClient(Uri baseUrl) : base(baseUrl)
    {
    }

    public ApiClient(string baseUrl) : base(baseUrl)
    {
    }

    public ApiClient(HttpClient httpClient, RestClientOptions options = null) : base(httpClient, options)
    {
    }

    public void EnqueueResponse(RestResponse response)
    {
        responseQueue.Enqueue(response);
    }

    public void ClearResponseQueue()
    {
        responseQueue.Clear();
    }

    public IEnumerable<RestResponse> GetRestResponses()
    {
        return responseQueue.ToArray();
    }

}

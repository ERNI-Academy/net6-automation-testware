using RestSharp;
using TestWare.Engines.Restsharp.Factory;

namespace TestWare.Engines.Restsharp.Resources;

public class ResourceBase
{
    public IApiClient Client { get; protected set; }

    public ResourceBase()
    {
    }

    public async Task<RestResponse<T>> ExecuteRequest<T>(RestRequest request)
    {
        CancellationToken cancellationToken = new();

        RestResponse<T> response = await ((RestClient)Client).ExecuteAsync<T>(request, cancellationToken);

        Client.EnqueueResponse(response);
        return response ?? throw new ApplicationException(nameof(response));
    }

    public async Task<RestResponse> ExecuteRequest(RestRequest request)
    {
        CancellationToken cancellationToken = new();

        RestResponse response = await ((RestClient)Client).ExecuteAsync(request, cancellationToken);

        Client.EnqueueResponse(response);
        return response ?? throw new ApplicationException(nameof(response));
    }
}


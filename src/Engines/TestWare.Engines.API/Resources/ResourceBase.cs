using RestSharp;
using TestWare.Engines.Restsharp.Factory;

namespace TestWare.Engines.Restsharp.Resources;

public class ResourceBase
{
    public IApiClient Client { get; protected set; }


    public ResourceBase()
    {
    }

    public RestResponse<T> ExecuteRequest<T>(RestRequest request)
    {
        CancellationToken cancellationToken = new();
        RestResponse<T> response = null;
        var task = Task.Run(async () => { 
            response = await ((RestClient)Client).ExecuteAsync<T>(request, cancellationToken); 
        });

        task.Wait();
        Client.EnqueueResponse(response);
        return response ?? throw new ApplicationException(nameof(response));
    }
}

using RestSharp;
using TestWare.Engines.Restsharp.Factory;

namespace TestWare.Engines.Restsharp.Resources
{
    public class ResourceBase
    {
        public ApiClient Client { get; protected set; }
        internal Queue<RestResponse> responseQueue;

        public ResourceBase()
        {
            responseQueue = new();
        }

        public RestResponse<T> ExecuteRequest<T>(RestRequest request)
        {
            CancellationToken cancellationToken = new();
            RestResponse<T> response = null;
            var task = Task.Run(async () => { response = await Client.ExecuteAsync<T>(request, cancellationToken); });
            task.Wait();
            responseQueue.Append(response);
            return response ?? throw new ApplicationException(nameof(response));
        }
    }
}

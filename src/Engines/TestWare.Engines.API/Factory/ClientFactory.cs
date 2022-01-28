using RestSharp;
using TestWare.Engines.Restsharp.Configuration;

namespace TestWare.Engines.Restsharp.Factory;

internal static class ClientFactory
{
    public static IApiClient Create(Capabilities capabilities)
    {
        var options = new RestClientOptions(capabilities.BaseUrl)
        {
            Timeout = capabilities.Timeout
        };

        var client = new ApiClient(options);

        return client;
    }

}

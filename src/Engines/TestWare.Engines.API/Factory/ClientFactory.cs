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

        foreach (var queryParam in capabilities.QueryParameters ?? Enumerable.Empty<CapabilityParameter>())
        {
            client.AddDefaultQueryParameter(queryParam.Name, queryParam.Value);
        }

        foreach (var headerParam in capabilities.HeaderParameters ?? Enumerable.Empty<CapabilityParameter>())
        {
            client.AddDefaultHeader(headerParam.Name, headerParam.Value);
        }

        return client;
    }

}

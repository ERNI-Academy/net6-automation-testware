using RestSharp;

namespace TestWare.Samples.API;

public class LastResponse
{
    private readonly Dictionary<Type, object> _lastResponseDict = new();

    public void AddResponse<T>(RestResponse<T> response)
    {
        _lastResponseDict[typeof(T)] = response;
    }

    public RestResponse<T> GetResponse<T>()
    {
        return (RestResponse<T>)_lastResponseDict[typeof(T)];
    }
}

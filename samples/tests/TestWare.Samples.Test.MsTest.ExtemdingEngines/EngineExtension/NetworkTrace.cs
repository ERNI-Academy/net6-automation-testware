namespace TestWare.Samples.Test.MsTest.ExtendingEngines.EngineExtension;


internal class NetworkTrace
{
    IDictionary<string, string>? RequestHeaders;
    string? RequestUrl;
    string? RequestMethod;
    string? RequestPostData;
    DateTime RequestUtcDateTime;
    string? ResponseUrl;
    string? ResponseContent;
    IDictionary<string, string>? ResponseHeaders;
    string? ResponseResourceType;
    string? ResponseStatusCode;
    DateTime ResponseUtcDateTime;

    internal void AddRequestData(string url, string method, IDictionary<string, string> headers, string postData)
    {
        RequestUrl = url;
        RequestMethod = method;
        RequestHeaders = headers;
        RequestPostData = postData;
        RequestUtcDateTime = DateTime.UtcNow;
    }

    internal void AddResponseData(string url, string content, IDictionary<string, string> headers, string resourceType, string statusCode)
    {
        ResponseUrl = url;
        ResponseContent = content;
        ResponseHeaders = headers;
        ResponseResourceType = resourceType;
        ResponseStatusCode = statusCode;
        ResponseUtcDateTime = DateTime.UtcNow;
    }

    internal string ToCsv()
    {
        var requestHeaders = HeadersToString(RequestHeaders!);
        var responseHeaders = HeadersToString(ResponseHeaders!);
        return $"{RequestUtcDateTime:yyyyMMdd-hh.mm.ss.ffffff},{RequestMethod},{RequestUrl},{requestHeaders},{RequestPostData},{ResponseUtcDateTime:yyyyMMdd-hh.mm.ss.ffffff},{ResponseStatusCode},{ResponseUrl},{responseHeaders},{ResponseResourceType},{ResponseContent}";
    }
    internal static string ToCsvHeaders()
    {
        return "{RequestUtcDateTime},{RequestMethod},{RequestUrl},{requestHeaders},{RequestPostData},{ResponseUtcDateTime},{ResponseStatusCode},{ResponseUrl},{responseHeaders},{ResponseResourceType},{ResponseContent}";
    }

    private static string HeadersToString(IDictionary<string, string> headers)
    {
        if (headers == null || headers.Count == 0) return "";
        else return string.Format("\"{0}\"", string.Join(",", headers.Select(x => $"{x.Key}={x.Value.Replace('"', '\'')}")));
    }
}
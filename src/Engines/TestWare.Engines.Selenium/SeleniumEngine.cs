using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.SeleniumEngine.Configuration;
using TestWare.Engines.SeleniumEngine.Factory;

namespace TestWare.Engines.SeleniumEngine;

public class SeleniumEngine : ISeleniumEngine
{
    private SeleniumConfig Configuration { get; set; }
    private OrderedDictionary NetworkTraces { get; set;}
    
    public const string Name = "Selenium";

    public IWebDriver Driver { get; set; }

    public SeleniumEngine() { }

    public SeleniumEngine(JsonObject keyValuePairs) {
        Configuration = JsonSerializer.Deserialize<SeleniumConfig>(keyValuePairs);
    }

    public string CollectEvidence(string destinationPath, string evidenceName)
    {
        var filePath = Path.Combine(destinationPath, $"{evidenceName}.png");
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        var ss = ((ITakesScreenshot)Driver).GetScreenshot();
        ss.SaveAsFile(Path.Combine(destinationPath, $"{evidenceName}.png"));
        return filePath;
    }

    public void Dispose()
    {
        Driver.Dispose();
        Driver.Quit();
    }

    public void Initialize()
    {

        Driver = new DriverFactory().Create(Configuration);
        Driver.Navigate().GoToUrl(Configuration.BaseUrl);
    }

    public async void StartRecordingEvidences()
    {
        NetworkTraces = new OrderedDictionary();
        Driver.Manage().Network.NetworkResponseReceived += ResponseHandler;
        Driver.Manage().Network.NetworkRequestSent += RequestHandler;
        try
        {
            await Driver.Manage().Network.StartMonitoring();
        }
        catch (Exception ex) when (ex is CommandResponseException || ex is WebDriverException)
        {
            
        }
       
    }
    private void ResponseHandler(object sender, NetworkResponseReceivedEventArgs e)
    {
        var trace =  (NetworkTrace)NetworkTraces[e.RequestId] ?? new NetworkTrace();
        trace.AddResponseData(e.ResponseUrl, e.ResponseContent.ToString(), e.ResponseHeaders.ToDictionary(), e.ResponseResourceType, e.ResponseStatusCode.ToString());
        NetworkTraces[e.RequestId] = trace;
    }

    private void RequestHandler(object sender, NetworkRequestSentEventArgs e)
    {
        var trace = new NetworkTrace();
        trace.AddRequestData(e.RequestUrl, e.RequestMethod, e.RequestHeaders.ToDictionary(), e.RequestPostData);
        NetworkTraces.Add(e.RequestId, trace);
    }

    private async void StopMonitoring()
    {
        try
        {
            await Driver.Manage().Network.StopMonitoring();
        }
        catch (Exception ex) when (ex is CommandResponseException || ex is WebDriverException)
        {

        }
        
    }
    public string StopRecordingEvidences(string destinationPath, string evidenceName)
    {
        StopMonitoring();

        var filePath = Path.Combine(destinationPath, $"{evidenceName}.csv");
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        using (StreamWriter sw = new StreamWriter(filePath, false))
        {
            sw.WriteLine(new NetworkTrace().ToCsvHeaders());
            foreach(NetworkTrace trace in NetworkTraces.Values.Cast<NetworkTrace>().ToList())
            {
                sw.WriteLine(trace.ToCsv());
            }
        }
        
            
       return filePath;
    }
}

internal class NetworkTrace 
{
    IDictionary<string, string> RequestHeaders;
    string RequestUrl;
    string RequestMethod;
    string RequestPostData;
    DateTime RequestUtcDateTime;
    string ResponseUrl;
    string ResponseContent;
    IDictionary<string,string> ResponseHeaders;
    string ResponseResourceType;
    string ResponseStatusCode;
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
        var requestHeaders = HeadersToString(RequestHeaders);
        var responseHeaders = HeadersToString(ResponseHeaders);
        return $"{RequestUtcDateTime.ToString("yyyyMMdd-hh.mm.ss.ffffff")},{RequestMethod},{RequestUrl},{requestHeaders},{RequestPostData},{ResponseUtcDateTime.ToString("yyyyMMdd-hh.mm.ss.ffffff")},{ResponseStatusCode},{ResponseUrl},{responseHeaders},{ResponseResourceType},{ResponseContent}";
    }
    internal string ToCsvHeaders() 
    { 
        return "{RequestUtcDateTime},{RequestMethod},{RequestUrl},{requestHeaders},{RequestPostData},{ResponseUtcDateTime},{ResponseStatusCode},{ResponseUrl},{responseHeaders},{ResponseResourceType},{ResponseContent}";
    }

    private string HeadersToString(IDictionary<string,string> headers)
    {
        if (headers == null || headers.Count == 0) return "";
        else return string.Format("\"{0}\"", string.Join(",", headers.Select(x => $"{x.Key}={x.Value.Replace('"', '\'')}")));
    }
}
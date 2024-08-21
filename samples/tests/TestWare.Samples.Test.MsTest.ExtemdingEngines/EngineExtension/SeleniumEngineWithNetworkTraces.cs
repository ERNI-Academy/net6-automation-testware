using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TestWare.Engines.SeleniumEngine;

namespace TestWare.Samples.Test.MsTest.ExtendingEngines.EngineExtension;

internal class SeleniumEngineWithNetworkTraces: SeleniumEngine, ISeleniumEngine
{
    private OrderedDictionary? _networkTraces;
    public new const string Name = "ExtendedSelenium";

    public SeleniumEngineWithNetworkTraces()
    {
    }

    public SeleniumEngineWithNetworkTraces(JsonObject keyValuePairs) : base(keyValuePairs)
    {
    }

    public new async void StartRecordingEvidences()
    {
        _networkTraces = new OrderedDictionary();
        Driver.Manage().Network.NetworkResponseReceived += ResponseHandler!;
        Driver.Manage().Network.NetworkRequestSent += RequestHandler!;
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
        var trace = _networkTraces![e.RequestId] as NetworkTrace ?? new NetworkTrace();
        trace.AddResponseData(e.ResponseUrl,
                              e.ResponseContent.ToString()!,
                              e.ResponseHeaders.ToDictionary(),
                              e.ResponseResourceType,
                              e.ResponseStatusCode.ToString());
        _networkTraces[e.RequestId] = trace;
    }

    private void RequestHandler(object sender, NetworkRequestSentEventArgs e)
    {
        var trace = new NetworkTrace();
        trace.AddRequestData(e.RequestUrl, e.RequestMethod, e.RequestHeaders.ToDictionary(), e.RequestPostData);
        _networkTraces!.Add(e.RequestId, trace);
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
    public new string StopRecordingEvidences(string destinationPath, string evidenceName)
    {
        StopMonitoring();

        var filePath = Path.Combine(destinationPath, $"{evidenceName}.csv");
        var path = Path.GetDirectoryName(filePath);
        if (path != null)
        {
            Directory.CreateDirectory(path);
            using StreamWriter sw = new(filePath, false);
            sw.WriteLine(NetworkTrace.ToCsvHeaders());
            foreach (NetworkTrace trace in _networkTraces!.Values.Cast<NetworkTrace>().ToList())
            {
                sw.WriteLine(trace.ToCsv());
            }
        }

        return filePath;
    }
}

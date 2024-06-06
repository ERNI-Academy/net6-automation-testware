using System.Text.Json.Nodes;
using System.Text.Json;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using Microsoft.Playwright;
using TestWare.Engines.PlaywrightEngine.Configuration;
using System.Collections.Generic;
using TestWare.Engines.PlaywrightEngine.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace TestWare.Engines.PlaywrightEngine;

public class PlaywrightEngine : ITestWareEngine
{
    public const string Name = "Playwright";
    private IPlaywright _Playwright;
    public IBrowser Browser;
    public IPage Page;

    private PlaywrightConfig Configuration { get; set; }
    public PlaywrightEngine() { }

    public PlaywrightEngine(JsonObject keyValuePairs)
    {
        Configuration = JsonSerializer.Deserialize<PlaywrightConfig>(keyValuePairs);
    }

    public string CollectEvidence(string destinationPath, string evidenceName)
    {
        var filePath = Path.Combine(destinationPath, $"{evidenceName}.png");
        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        Page.ScreenshotAsync(new() { Path = filePath }).Wait();
        return filePath;
    }
    

    public void Dispose()
    {
        Browser?.DisposeAsync();
        _Playwright?.Dispose();
    }

    public async Task InitializeAsync()
    {
        _Playwright = await Playwright.CreateAsync();
        var launchOptions = JsonSerializer.Deserialize<BrowserTypeLaunchOptions>(Configuration.LaunchOptions);
        Browser = await _Playwright.LaunchBrowser(Configuration.Browser, launchOptions!);

        var pageOptions = JsonSerializer.Deserialize<BrowserNewPageOptions>(Configuration.PageOptions);
        Page = await Browser.NewPageAsync();
        await Page.GotoAsync(Configuration.BaseUrl);
    }
    public void Initialize()
    {
        InitializeAsync().Wait();
    }

    public void StartRecordingEvidences()
    {
        //throw new NotImplementedException();
    }

    public string StopRecordingEvidences(string destinationPath, string evidenceName)
    {
        //throw new NotImplementedException();
        return "";
    }
}

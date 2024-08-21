using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using TestWare.Core.Configuration;
using TestWare.Core.Interfaces;
using TestWare.Engines.SeleniumEngine.Configuration;
using TestWare.Engines.SeleniumEngine.Factory;

namespace TestWare.Engines.SeleniumEngine;

public class SeleniumEngine : ISeleniumEngine
{
    protected SeleniumConfig? _configuration;
    protected IWebDriver? _driver;
    
    public const string Name = "Selenium";

    public IWebDriver Driver { get
        {
            if (_driver == null) Initialize();
            return _driver!;
        }
    }

    public SeleniumEngine() { }

    public SeleniumEngine(JsonObject keyValuePairs) {
        _configuration = JsonSerializer.Deserialize<SeleniumConfig>(keyValuePairs);
    }

    public string CollectEvidence(string destinationPath, string evidenceName)
    {
        var filePath = Path.Combine(destinationPath, $"{evidenceName}.png");
        var path = Path.GetDirectoryName(filePath);
        if (path != null)
        {
            Directory.CreateDirectory(path);
            var ss = ((ITakesScreenshot)Driver).GetScreenshot();
            ss.SaveAsFile(Path.Combine(destinationPath, $"{evidenceName}.png"));
        }

        return filePath;
    }

    public void Dispose()
    {
        Driver.Dispose();
        Driver.Quit();
    }

    public void Initialize()
    {
        var config = _configuration ?? throw new NullReferenceException("Configuration is mandatory");
        _driver = new DriverFactory().Create(config);
        if (_configuration.BaseUrl != default)
            _driver.Navigate().GoToUrl(_configuration.BaseUrl);
    }

    public void StartRecordingEvidences()
    {

    }

    public string StopRecordingEvidences(string destinationPath, string evidenceName)
    {
       return "";
    }
}


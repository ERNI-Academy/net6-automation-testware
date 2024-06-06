using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TestWare.Engines.SeleniumEngine.Configuration;

namespace TestWare.Engines.SeleniumEngine.Factory;

internal class DriverFactory
{

    internal IWebDriver Create(SeleniumConfig configuration) =>
        Enum.Parse<SupportedBrowsers>(configuration.Driver, true) switch
        {
            SupportedBrowsers.Chrome => CreateLocalService<ChromeDriver, ChromeOptions, ChromeDriverService>(configuration),
            SupportedBrowsers.Firefox => CreateLocalService<FirefoxDriver, FirefoxOptions, FirefoxDriverService>(configuration),
            SupportedBrowsers.InternetExplorer => CreateLocalService<InternetExplorerDriver, InternetExplorerOptions, InternetExplorerDriverService>(configuration),
            SupportedBrowsers.Edge => CreateLocalService<EdgeDriver, EdgeOptions, EdgeDriverService>(configuration),
            SupportedBrowsers.Invalid => throw new NotImplementedException(),
            _ => throw new NotSupportedException($"Browser type is invalid."),
        };
    
    private TDriver CreateLocalService<TDriver, TOptions, TService>(SeleniumConfig configuration) 
    {
        var options = (TOptions)Activator.CreateInstance(typeof(TOptions))!;
        foreach (var option in configuration.Options)
        {
            
            if (option.Key == "Arguments")
            {
                var args = JsonSerializer.Deserialize<string[]>(option.Value);
                options.GetType().GetMethod("AddArguments", [args.GetType()])?.Invoke(options, [args]);
            }
            else
            {
                var prop = options.GetType().GetProperty(option.Key);
                if (prop != null)
                {
                    var value = JsonSerializer.Deserialize(option.Value, prop.PropertyType);
                    prop.SetValue(options, value);
                }
            }
        }

        object service = configuration.Service != default! ?
            service = typeof(TService).GetMethod("CreateDefaultService", [typeof(string)])?.Invoke(null, [configuration.Service])! :
            service = typeof(TService).GetMethod("CreateDefaultService", [])?.Invoke(null,null)!;
       
        return configuration.ImplicitWaitSeconds != default ?
            (TDriver)Activator.CreateInstance(typeof(TDriver), service, options, TimeSpan.FromSeconds(configuration.ImplicitWaitSeconds))! :
            (TDriver)Activator.CreateInstance(typeof(TDriver), service, options)!;
    }
    private TDriver CreateRemoteService<TDriver, TOptions, TService>(SeleniumConfig configuration)
    {
        //RemoteWebDriver(Uri remoteAddress, ICapabilities capabilities, TimeSpan commandTimeout)
        throw new NotImplementedException();
    }


}

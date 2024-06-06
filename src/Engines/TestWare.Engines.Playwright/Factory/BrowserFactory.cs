using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Engines.PlaywrightEngine.Configuration;

namespace TestWare.Engines.PlaywrightEngine.Factory;

//internal class BrowserFactory
//{
//    internal IBrowser Create(PlaywrightConfig configuration, ) =>
//    Enum.Parse<SupportedBrowsers>(configuration.Browser, true) switch
//    {
//        SupportedBrowsers.Chrome => ,
//        SupportedBrowsers.Firefox => CreateLocalService<FirefoxDriver, FirefoxOptions, FirefoxDriverService>(configuration),
//        SupportedBrowsers.InternetExplorer => CreateLocalService<InternetExplorerDriver, InternetExplorerOptions, InternetExplorerDriverService>(configuration),
//        SupportedBrowsers.Edge => CreateLocalService<EdgeDriver, EdgeOptions, EdgeDriverService>(configuration),
//        SupportedBrowsers.Invalid => throw new NotImplementedException(),
//        _ => throw new NotSupportedException($"Browser type is invalid."),
//    };
//}


//internal enum SupportedBrowsers
//{
//    Invalid = 0,
//    Chrome = 1,
//    Firefox = 2,
//    InternetExplorer = 3,
//    Edge = 4
//}

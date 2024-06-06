using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Engines.PlaywrightEngine.Extensions;

static public class PlaywrightExtensions
{
    static async public Task<IBrowser> LaunchBrowser(this IPlaywright playwright, string browser, BrowserTypeLaunchOptions options) =>
        Enum.Parse<SupportedBrowsers>(browser, true) switch
        {
            SupportedBrowsers.Chromium => await playwright.Chromium.LaunchAsync(options),
            SupportedBrowsers.Firefox => await playwright.Firefox.LaunchAsync(options),
            SupportedBrowsers.Webkit => await playwright.Webkit.LaunchAsync(options),
            SupportedBrowsers.Invalid => throw new NotImplementedException(),
            _ => throw new NotSupportedException($"Browser type is invalid."),
        };

}
internal enum SupportedBrowsers
{
    Invalid = 0,
    Chromium = 1,
    Firefox = 2,
    Webkit = 3,
}

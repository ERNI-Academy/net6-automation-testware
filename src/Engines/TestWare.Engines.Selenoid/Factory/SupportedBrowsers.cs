using System.Reflection;

namespace TestWare.Engines.Selenoid.Factory;

public enum SupportedBrowsers
{    
    Invalid = 0,
    [BrowserName("chrome")]
    Chrome = 1,
    [BrowserName("firefox")]
    Firefox = 2,
    [BrowserName("MicrosoftEdge")]
    Edge = 3,
    Android = 4
}

static class SupportedBrowsersHelper
{
    public static string? GetBrowserName(this SupportedBrowsers enumValue)
    {
        return enumValue
                  .GetType()
                  .GetMember(enumValue.ToString())
                  .First()?
                  .GetCustomAttribute<BrowserNameAttribute>()?
                  .GetValue();
    }
}

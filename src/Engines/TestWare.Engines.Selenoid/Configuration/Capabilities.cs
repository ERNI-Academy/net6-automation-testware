using TestWare.Engines.Selenoid.Factory;

namespace TestWare.Engines.Selenoid.Configuration;

internal class Capabilities
{
    public string? Name { get; set; }

    public string? Uri { get; set; }

    public string? BrowserName { get; set; }

    public string? BrowserVersion { get; set; }

    public string? ScreenResolution { get; set; }

    public int CommandTimeOutInMinutes { get; set; }

    public bool EnableLog { get; set; }

    public bool EnableVnc { get; set; }

    public bool EnableVideo { get; set; }

    public IEnumerable<string> Arguments { get; set; } = Enumerable.Empty<string>();

    public SupportedBrowsers GetDriver()
    {
        if (BrowserName == null) {
            throw new NullReferenceException("BrowserName capability was null.");
        }
        return Enum.Parse<SupportedBrowsers>(BrowserName, true);
    }
}
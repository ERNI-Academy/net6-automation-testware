using TestWare.Engines.Selenoid.Factory;

namespace TestWare.Engines.Selenium.Configuration;

internal class Capabilities
{
    public string Name { get; set; }

    public string Uri { get; set; }

    public string BrowserType { get; set; }

    public string BrowserVersion { get; set; }

    public string Resolution { get; set; }

    public int CommandTimeOutInMinutes { get; set; }

    public bool EnableLog { get; set; }

    public bool EnableVnc { get; set; }

    public bool EnableVideo { get; set; }

    public IEnumerable<string> Arguments { get; set; } = Enumerable.Empty<string>();

    public SupportedBrowsers GetDriver()
    {
        return Enum.Parse<SupportedBrowsers>(BrowserType, true);
    }
}
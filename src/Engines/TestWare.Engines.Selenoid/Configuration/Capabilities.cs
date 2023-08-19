using TestWare.Engines.Selenoid.Factory;

namespace TestWare.Engines.Selenoid.Configuration;

internal class Capabilities
{
    public string Name { get; set; }

    public string Uri { get; set; }

    public string BrowserName { get; set; }

    public string BrowserVersion { get; set; }

    public string ScreenResolution { get; set; }

    public string DeviceName { get; set; }
    public string DeviceVersion { get; set; }
    public string ApkUrl { get; set; }
    public string AppPath { get; set; }

    public int CommandTimeOutInMinutes { get; set; }

    public bool EnableLog { get; set; }

    public bool EnableVnc { get; set; }

    public bool EnableVideo { get; set; }

    public IEnumerable<string> Arguments { get; set; } = Enumerable.Empty<string>();

    public SupportedBrowsers GetDriver()
    {
        return Enum.Parse<SupportedBrowsers>(BrowserName, true);
    }
}
namespace TestWare.Engines.Appium.Configuration;

internal class Capabilities
{
    public string Name { get; set; }
    public string AppiumUrl { get; set; }
    public string ApkUrl { get; set; }
    public string ApkPath { get; set; }

    public string DeviceName {get; set;}

    public string PlatformName {get; set;}

    public IEnumerable<CapabilityOption<object>> Options { get; set; }

}


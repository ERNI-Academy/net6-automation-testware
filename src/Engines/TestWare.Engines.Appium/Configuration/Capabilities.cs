namespace TestWare.Engines.Appium.Configuration
{
    internal class Capabilities
    {
        public string Name { get; set; }
        public string AppiumUrl { get; set; }
        public string ApkUrl { get; set; }
        public string ApkPath { get; set; }

        public string DeviceName {get; set;}

        public string PlatformName {get; set;}

        public CapabilityOptions Options { get; set; } = new CapabilityOptions();

    }

    internal class CapabilityOptions
    {
        public bool? fullReset { get; set; } = null;
        public bool? noReset { get; set; } = null;
        public string? appActivity { get; set; } = null;
        public bool? unicodeKeyboard { get; set; } = null;
        public bool? resetKeyboard { get; set; } = null;
        public bool? autoAcceptAlerts { get; set; } = null;
        public bool? autoGrantPermissions { get; set; } = null;
        public int? newCommandTimeout { get; set; } = null;

    }

}
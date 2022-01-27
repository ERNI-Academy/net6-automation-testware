namespace TestWare.Engines.Appium.Configuration;

internal class CapabilityOptions
{
    public bool? FullReset { get; set; } = null;
    public bool? NoReset { get; set; } = null;
    public string AppActivity { get; set; } = null;
    public bool? UnicodeKeyboard { get; set; } = null;
    public bool? ResetKeyboard { get; set; } = null;
    public bool? AutoAcceptAlerts { get; set; } = null;
    public bool? AutoGrantPermissions { get; set; } = null;
    public int? NewCommandTimeout { get; set; } = null;

}


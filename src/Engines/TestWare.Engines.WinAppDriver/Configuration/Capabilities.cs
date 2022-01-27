namespace TestWare.Engines.Appium.WinAppDriver.Configuration;

internal class Capabilities
{
    public string Name { get; set; }

    public string ApplicationPath { get; set; }

    public string ApplicationId { get; set; }

    public string ApplicationName { get; set; }

    public string ApplicationClassName { get; set; } = string.Empty;

    public string WinAppDriverUrl { get; set; }

    public int CommandTimeOutInMinutes { get; set; }
}

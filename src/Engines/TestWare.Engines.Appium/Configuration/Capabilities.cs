﻿using TestWare.Engines.Appium.Factory;

namespace TestWare.Engines.Appium.Configuration;

internal class Capabilities
{
    public string Name { get; set; }
    public string AppiumUrl { get; set; }
    public string ApkUrl { get; set; }
    public string AppPath { get; set; }

    public string DeviceName {get; set;}
    public int CommandTimeOutInMinutes { get; set; }

    public string PlatformName {get; set;}

    public string PlatformVersion {get; set;}

    public IEnumerable<CapabilityOption<object>> Options { get; set; } = Enumerable.Empty<CapabilityOption<object>>();


    public SupportedPlatforms GetPlatform()
    {
        return Enum.Parse<SupportedPlatforms>(PlatformName, true);
    }
}


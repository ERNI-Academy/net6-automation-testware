using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Diagnostics;
using TestWare.Core.Libraries;
using TestWare.Engines.Appium.WinAppDriver.Configuration;
using TestWare.Engines.Appium.WinAppDriver.UniversalWindowsPlatform;

namespace TestWare.Engines.Appium.WinAppDriver.Factory;

internal static class WindowsDriverFactory
{
    private const int _waitForApplicationToOpen = 30;
    private const int _retriesWaitForApplication = 16;

    private static IWindowsDriver _rootDriver;

    public static IWindowsDriver CreateRootWinAppDriverSession(Capabilities capabilities)
    {
        LaunchApplication(capabilities);

        var appCapabilities = new AppiumOptions()
        {
            App = "Root",
            DeviceName = "WindowsPC",
            PlatformName = "Windows",
            AutomationName = "Windows"
        };

        _rootDriver = new WindowsDriver(new Uri(capabilities.WinAppDriverUrl), appCapabilities, TimeSpan.FromSeconds(_waitForApplicationToOpen));
        _rootDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_waitForApplicationToOpen);

        return AttachToApplication(capabilities);

    }

    private static void LaunchApplication(Capabilities capabilities)
    {
        if (!string.IsNullOrEmpty(capabilities.ApplicationPath))
        {
            Process[] processes = Process.GetProcessesByName(capabilities.ApplicationName);
            foreach (Process process in processes)
            {
                process.Kill();
            }
            Process.Start(capabilities.ApplicationPath);
        }
        else if (!string.IsNullOrEmpty(capabilities.ApplicationId))
        {
            ApplicationActivationManager appActiveManager = new ApplicationActivationManager();
            appActiveManager.ActivateApplication(capabilities.ApplicationId, null, ActivateOptions.None, out uint pid);
        }
        else
        {
            throw new FileNotFoundException("capabilities.ApplicationPath and capabilities.ApplicationId are empty, please specify one of them to start the application to test.");
        }
    }

    private static IWindowsDriver AttachToApplication(Capabilities capabilities)
    {
        WindowsDriver driver = null;

        RetryPolicies.ExecuteActionWithRetries(
        () =>
        {
            IWebElement window = null;

            if (!string.IsNullOrEmpty(capabilities.ApplicationClassName))
            {
                window = _rootDriver.FindElement(MobileBy.ClassName(capabilities.ApplicationClassName));
            }
            else if (!string.IsNullOrEmpty(capabilities.ApplicationName))
            {
                window = _rootDriver.FindElement(MobileBy.Name(capabilities.ApplicationName));
            }

            var topLevelWindowHandle = window?.GetAttribute("NativeWindowHandle");
            topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("x"); // Convert to Hex

            var appCapabilities = new AppiumOptions()
            {
                AutomationName = "Windows"
            };
            appCapabilities.AddAdditionalAppiumOption("appTopLevelWindow", topLevelWindowHandle);
            driver = new WindowsDriver(new Uri(capabilities.WinAppDriverUrl), appCapabilities, TimeSpan.FromMinutes(capabilities.CommandTimeOutInMinutes));
        },
        numberOfRetries: _retriesWaitForApplication);

        if (driver == null)
        {
            throw new DriveNotFoundException();
        }

        return driver;
    }
}

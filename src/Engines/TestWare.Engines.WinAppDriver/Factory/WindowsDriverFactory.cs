using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System.Diagnostics;
using TestWare.Core.Libraries;
using TestWare.Engines.Appium.WinAppDriver.Configuration;

namespace TestWare.Engines.Appium.WinAppDriver.Factory
{
    internal static class WindowsDriverFactory
    {
        private const int _waitForApplicationToOpen = 5;
        private const int _retriesWaitForApplication = 16;

        private static IWindowsDriver _rootDriver;

        public static IWindowsDriver CreateRootWinAppDriverSession(Capabilities capabilities)
        {
            LaunchApplication(capabilities);

            var appCapabilities = new AppiumOptions()
            {
                App = "Root",
                DeviceName = "WindowsPC",
                PlatformName = "Windows"
            };

            _rootDriver = new WindowsDriver(new Uri(capabilities.WinAppDriverUrl), appCapabilities, TimeSpan.FromSeconds(_waitForApplicationToOpen));
            _rootDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_waitForApplicationToOpen);

            return AttachToApplication(capabilities);

        }

        private static void LaunchApplication(Capabilities capabilities)
        {
            /*
            var allProcess = Process.GetProcesses();
            //var a = allProcess.FirstOrDefault().Modules[0].FileName;
            var processes3 = allProcess.Select(x => x.MainModule?.FileName == capabilities.ApplicationPath);
            */

            Process[] processes = Process.GetProcessesByName(capabilities.ApplicationName);

            foreach (Process process in processes)
            {
                process.Kill();
            }
            Process.Start(capabilities.ApplicationPath);
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

                var topLevelWindowHandle = window.GetAttribute("NativeWindowHandle");
                topLevelWindowHandle = int.Parse(topLevelWindowHandle).ToString("x"); // Convert to Hex

                var appCapabilities = new AppiumOptions();
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
}

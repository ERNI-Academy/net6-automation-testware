using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Reflection;
using TestWare.Engines.Appium.Configuration;

namespace TestWare.Engines.Appium.Factory
{
    internal class AppiumDriverFactory
    {
        public static IAppiumDriver Create(Capabilities capabilities)
        {
            var appiumOptions = new AppiumOptions()
            { 
                App = capabilities.ApkPath,
                DeviceName = capabilities.DeviceName,
                PlatformName = capabilities.PlatformName
            };

            foreach(PropertyInfo propertyInfo in capabilities.Options.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(capabilities.Options);
                if (value != null)
                {
                    appiumOptions.AddAdditionalAppiumOption(propertyInfo.Name, value);
                }
            }

            return new AndroidDriver(new Uri(capabilities.AppiumUrl), appiumOptions, TimeSpan.FromSeconds(120));
        }
    }
}

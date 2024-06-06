using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestWare.Engines.SeleniumEngine.Extensions;

static public class WebDriverExtensions
{
    static public bool UrlPathEndsWith(this IWebDriver driver, string path)
    {
        return UrlPathEndsWith(driver, path, driver.Manage().Timeouts().ImplicitWait);
    }
    static public bool UrlPathEndsWith(this IWebDriver driver, string path, TimeSpan timeout)
    {
        var wait = new WebDriverWait(driver, timeout);
        var a = wait.Until(driver => new Uri(driver.Url).LocalPath.EndsWith(path));
        return true;
    }

    static public IWebElement SafeSendKeys(this IWebDriver driver, By locator, string value)
    {
        return SafeSendKeys(driver, locator, value, driver.Manage().Timeouts().ImplicitWait);
    }
    static public IWebElement SafeSendKeys(this IWebDriver driver, By locator, string value, TimeSpan timeout)
    {
        var wait = new WebDriverWait(driver, timeout);
        var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        element.SendKeys(value);
        return element;
    }

    static public IWebElement SafeClick(this IWebDriver driver, By locator)
    {
        return SafeClick(driver, locator, driver.Manage().Timeouts().ImplicitWait);
    }

    static public IWebElement SafeClick(this IWebDriver driver, By locator, TimeSpan timeout)
    {
        var wait = new WebDriverWait(driver, timeout);
        var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        element.Click();
        return element;
    }

    static public bool ElementIsPresent(this IWebDriver driver, By locator)
    {
        return ElementIsPresent(driver, locator, driver.Manage().Timeouts().ImplicitWait);
    }

    static public bool ElementIsPresent(this IWebDriver driver, By locator, TimeSpan timeout)
    {
        var wait = new WebDriverWait(driver, timeout);
        wait.Until(ExpectedConditions.ElementExists(locator));
        return true;
    }

    static public bool ElementIsNotPresent(this IWebDriver driver, By locator)
    {
        return ElementIsNotPresent(driver, locator, driver.Manage().Timeouts().ImplicitWait);
    }

    static public bool ElementIsNotPresent(this IWebDriver driver, By locator, TimeSpan timeout)
    {
        var wait = new WebDriverWait(driver, timeout);
        wait.Until(driver => driver.FindElements(locator).Count == 0);
        return true;
    }
}


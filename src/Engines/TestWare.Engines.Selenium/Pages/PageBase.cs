using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestWare.Core.Libraries;
using TestWare.Engines.Selenium.Extras;

namespace TestWare.Engines.Selenium.Pages;

public abstract class PageBase
{
    protected const int TimeToWait = 15;
    protected const int NumberOfRetries = 5;

    public IWebDriver Driver { get; protected set; }

    private TimeSpan RetryAttemp = TimeSpan.FromMilliseconds(200);

    protected void ClickElement(IWebElement element)
    {
        element = element ?? throw new ArgumentNullException("Element to be clicked was null", nameof(element));

        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.WaitUntilElementIsClickable(element);
                element.Click();
            },
            numberOfRetries: NumberOfRetries,
            retryAttemp: RetryAttemp);
    }

    protected void ClickInnerElement(IWebElement element)
    {
        element = element ?? throw new ArgumentNullException("Element to be clicked was null", nameof(element));

        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.WaitUntilElementIsClickable(element);
                Actions action = new Actions(Driver);
                action.MoveToElement(element).Click().Perform();
            },
            numberOfRetries: NumberOfRetries,
            retryAttemp: RetryAttemp);
    }

    protected void DoubleClickElement(IWebElement element)
    {
        element = element ?? throw new ArgumentNullException("Element to be double clicked was null", nameof(element));

        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.WaitUntilElementIsClickable(element);
                new Actions(Driver).DoubleClick(element).Perform();
            },
            numberOfRetries: NumberOfRetries,
            retryAttemp: RetryAttemp);
    }

    protected void SendKeysElement(IWebElement element, string text)
        => this.SendKeysElement(element, text, TimeToWait);

    protected void SendKeysElement(IWebElement element, string text, int timeToWait)
    {
        element = element ?? throw new ArgumentNullException("Element to send keys was null", nameof(element));

        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.WaitUntilElementIsClickable(element, timeToWait);
                element.SendKeys(text);
            },
            numberOfRetries: NumberOfRetries,
            retryAttemp: RetryAttemp);
    }

    protected void ClearElementText(IWebElement element)
        => this.ClearElementText(element, TimeToWait);

    protected void ClearElementText(IWebElement element, int timeToWait)
    {
        element = element ?? throw new ArgumentNullException("Element to clear was null", nameof(element));

        RetryPolicies.ExecuteActionWithRetries(
            () =>
            {
                this.WaitUntilElementIsClickable(element, timeToWait);
                element.Clear();
            },
            numberOfRetries: NumberOfRetries,
            retryAttemp: RetryAttemp);
    }

    protected void WaitUntilElementIsClickable(IWebElement element)
        => this.WaitUntilElementIsClickable(element, TimeToWait);

    protected void WaitUntilElementIsVisible(By locator)
        => this.WaitUntilElementIsVisible(locator, TimeToWait);

    protected void WaitUntilElementIsVisible(By locator, int timeToWait)
    {
        try
        {
            // TODO: La llamada al WebDriverWait falla si el driver es de tipo WindowsDriver.
            var webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeToWait));
            webDriverWait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void WaitUntilElementIsClickable(IWebElement element, int timeToWait)
    {
        try
        {
            var webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeToWait));
            webDriverWait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void WaitUntilElementNotVisible(By locator, int secondsToWait)
    {
        Thread.Sleep(1000);
        try
        {
            var webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(secondsToWait));
            webDriverWait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ExecuteActionWithDelay(Action action, int secondsToDelayAction)
    {
        Thread.Sleep(secondsToDelayAction * 1000);
        action.Invoke();
    }
}

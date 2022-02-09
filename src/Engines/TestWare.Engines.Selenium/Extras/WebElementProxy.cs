#nullable enable
using System.Collections.ObjectModel;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Internal;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Intercepts the request to a single <see cref="IWebElement"/>
/// </summary>
internal class WebElementProxy : WebDriverObjectProxy, IWrapsElement, IWebElement, ILocatable, IFindsElement
{
    private IWebElement? cachedElement;

    public WebElementProxy(IElementLocator locator, IEnumerable<By> bys, bool cache)
        : base(locator, bys, cache)
    {
    }

    /// <summary>
    /// Gets the IWebElement object this proxy represents, returning a cached one if requested.
    /// </summary>
    public IWebElement WrappedElement
    {
        get
        {
            if (!this.Cache || this.cachedElement == null)
            {
                this.cachedElement = this.Locator.LocateElement(this.Bys);
            }

            return this.cachedElement;
        }
    }

    #region Forwarded WrappedElement calls

    public string TagName => WrappedElement.TagName;

    public string Text => WrappedElement.Text;

    public bool Enabled => WrappedElement.Enabled;

    public bool Selected => WrappedElement.Selected;

    public Point Location => WrappedElement.Location;

    public Size Size => WrappedElement.Size;

    public bool Displayed => WrappedElement.Displayed;

    public void Clear() => WrappedElement.Clear();

    public void Click() => WrappedElement.Click();

    public IWebElement FindElement(By by) => WrappedElement.FindElement(by);

    public ReadOnlyCollection<IWebElement> FindElements(By by) => WrappedElement.FindElements(by);

    public string GetAttribute(string attributeName) => WrappedElement.GetAttribute(attributeName);

    public string GetCssValue(string propertyName) => WrappedElement.GetCssValue(propertyName);

    [Obsolete ("Deprecated on IWebElement")]
    public string GetProperty(string propertyName) => WrappedElement.GetProperty(propertyName);

    public void SendKeys(string text) => WrappedElement.SendKeys(text);

    public void Submit() => WrappedElement.Submit();

    public Point LocationOnScreenOnceScrolledIntoView
        => ((ILocatable)WrappedElement).LocationOnScreenOnceScrolledIntoView;

    public ICoordinates Coordinates
        => ((ILocatable)WrappedElement).Coordinates;

    public override int GetHashCode() => WrappedElement.GetHashCode();

    public override bool Equals(object? obj) => WrappedElement.Equals(obj);

    public IWebElement FindElement(string mechanism, string value)
        => ((IFindsElement)WrappedElement).FindElement(mechanism, value);

    public ReadOnlyCollection<IWebElement> FindElements(string mechanism, string value)
        => ((IFindsElement)WrappedElement).FindElements(mechanism, value);

    public string GetDomAttribute(string attributeName) => WrappedElement.GetDomAttribute(attributeName);

    public string GetDomProperty(string propertyName) => WrappedElement.GetDomProperty(propertyName);

    public ISearchContext GetShadowRoot() => WrappedElement.GetShadowRoot();

    #endregion Forwarded WrappedElement calls
}

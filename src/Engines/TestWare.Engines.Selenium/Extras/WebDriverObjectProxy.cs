using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Represents a base proxy class for objects used with the PageFactory.
/// </summary>
internal abstract class WebDriverObjectProxy
{
    /// <summary>
    /// Create WebDriverObjectProxy
    /// </summary>
    /// <param name="locator">The <see cref="IElementLocator"/> implementation that
    /// determines how elements are located.</param>
    /// <param name="bys">The list of methods by which to search for the elements.</param>
    /// <param name="cache"><see langword="true"/> to cache the lookup to the element; otherwise, <see langword="false"/>.</param>
    protected WebDriverObjectProxy(IElementLocator locator, IEnumerable<By> bys, bool cache)
    {
        this.Locator = locator;
        this.Bys = bys;
        this.Cache = cache;
    }

    /// <summary>
    /// Gets the <see cref="IElementLocator"/> implementation that determines how elements are located.
    /// </summary>
    protected IElementLocator Locator { get; }

    /// <summary>
    /// Gets the list of methods by which to search for the elements.
    /// </summary>
    protected IEnumerable<By> Bys { get; }

    /// <summary>
    /// Gets a value indicating whether element search results should be cached.
    /// </summary>
    protected bool Cache { get; }
}

#nullable enable

using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// A default locator for elements for use with the <see cref="PageFactory"/>. This locator
/// implements no retry logic for elements not being found, nor for elements being stale.
/// </summary>
public class DefaultElementLocator : IElementLocator
{

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultElementLocator"/> class.
    /// </summary>
    /// <param name="searchContext">The <see cref="ISearchContext"/> used by this locator
    /// to locate elements.</param>
    public DefaultElementLocator(ISearchContext searchContext)
    {
        this.SearchContext = searchContext;
    }

    /// <summary>
    /// Gets the <see cref="ISearchContext"/> to be used in locating elements.
    /// </summary>
    public ISearchContext SearchContext { get; }

    /// <summary>
    /// Locates an element using the given list of <see cref="By"/> criteria.
    /// </summary>
    /// <param name="bys">The list of methods by which to search for the element.</param>
    /// <returns>An <see cref="IWebElement"/> which is the first match under the desired criteria.</returns>
    public IWebElement LocateElement(IEnumerable<By> bys)
    {
        if (bys == null)
        {
            throw new ArgumentNullException(nameof(bys), "List of criteria may not be null");
        }

        string? errorString = null;
        foreach (var by in bys)
        {
            try
            {
                return this.SearchContext.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                errorString = (errorString == null ? "Could not find element by: " : errorString + ", or: ") + by;
            }
        }

        throw new NoSuchElementException(errorString);
    }

    /// <summary>
    /// Locates a list of elements using the given list of <see cref="By"/> criteria.
    /// </summary>
    /// <param name="bys">The list of methods by which to search for the elements.</param>
    /// <returns>A list of all elements which match the desired criteria.</returns>
    public ReadOnlyCollection<IWebElement> LocateElements(IEnumerable<By> bys)
    {
        if (bys == null)
        {
            throw new ArgumentNullException(nameof(bys), "List of criteria may not be null");
        }

        List<IWebElement> collection = new List<IWebElement>();
        foreach (var by in bys)
        {
            ReadOnlyCollection<IWebElement> list = this.SearchContext.FindElements(by);
            collection.AddRange(list);
        }

        return collection.AsReadOnly();
    }
}

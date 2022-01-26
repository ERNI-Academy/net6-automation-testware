
using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Interface describing how elements are to be located by a <see cref="PageFactory"/>.
/// </summary>
/// <remarks>
/// A locator must always contain a way to retrieve the <see cref="ISearchContext"/> to
/// use in locating elements. In practice, this will usually be implemented by passing
/// the context in via a constructor.
/// </remarks>
public interface IElementLocator
{
    /// <summary>
    /// Gets the <see cref="ISearchContext"/> to be used in locating elements.
    /// </summary>
    ISearchContext SearchContext { get; }

    /// <summary>
    /// Locates an element using the given list of <see cref="By"/> criteria.
    /// </summary>
    /// <param name="bys">The list of methods by which to search for the element.</param>
    /// <returns>An <see cref="IWebElement"/> which is the first match under the desired criteria.</returns>
    IWebElement LocateElement(IEnumerable<By> bys);

    /// <summary>
    /// Locates a list of elements using the given list of <see cref="By"/> criteria.
    /// </summary>
    /// <param name="bys">The list of methods by which to search for the elements.</param>
    /// <returns>A list of all elements which match the desired criteria.</returns>
    ReadOnlyCollection<IWebElement> LocateElements(IEnumerable<By> bys);
}

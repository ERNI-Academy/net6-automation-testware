
namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Provides the lookup methods for the FindsBy attribute (for using in PageObjects)
/// </summary>
public enum How
{
    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.Id" />
    /// </summary>
    Id,

    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.Name" />
    /// </summary>
    Name,

    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.TagName" />
    /// </summary>
    TagName,

    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.ClassName" />
    /// </summary>
    ClassName,

    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.CssSelector" />
    /// </summary>
    CssSelector,

    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.LinkText" />
    /// </summary>
    LinkText,

    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.PartialLinkText" />
    /// </summary>
    PartialLinkText,

    /// <summary>
    /// Finds by <see cref="OpenQA.Selenium.By.XPath" />
    /// </summary>
    XPath,

    /// <summary>
    /// Finds by a custom <see cref="By"/> implementation.
    /// </summary>
    Custom
}

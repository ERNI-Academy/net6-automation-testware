#nullable enable

using System.Reflection;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Interface describing how members of a class which represent elements in a Page Object
/// are detected.
/// </summary>
public interface IPageObjectMemberDecorator
{
    /// <summary>
    /// Locates an element or list of elements for a Page Object member, and returns a
    /// proxy object for the element or list of elements.
    /// </summary>
    /// <param name="member">The <see cref="MemberInfo"/> containing information about
    /// a class's member.</param>
    /// <param name="locator">The <see cref="IElementLocator"/> used to locate elements.</param>
    /// <returns>A transparent proxy to the WebDriver element object.</returns>
    object? Decorate(MemberInfo member, IElementLocator locator);
}

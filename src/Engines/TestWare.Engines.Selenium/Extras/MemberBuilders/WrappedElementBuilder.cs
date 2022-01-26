using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras.MemberBuilders;

internal class WrappedElementBuilder : IMemberBuilder
{
    public bool CreateObject(Type memberType, IElementLocator locator, IEnumerable<By> bys, bool cache, [NotNullWhen(true)] out object createdObject)
    {
        createdObject = null;

        if (typeof(IWrapsElement).IsAssignableFrom(memberType))
        {
            var webElement = new WebElementProxy(locator, bys, cache);
            createdObject = WrapsElementFactory.Wrap(memberType, webElement);
            return true;
        }

        return false;
    }
}

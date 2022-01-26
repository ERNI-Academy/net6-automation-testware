﻿using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras.MemberBuilders;

/// <summary>
/// Creates member of <see cref="IList{IWebElement}"/> type
/// </summary>
internal class WebElementListBuilder : IMemberBuilder
{
    public bool CreateObject(Type memberType, IElementLocator locator, IEnumerable<By> bys, bool cache, [NotNullWhen(true)] out object createdObject)
    {
        createdObject = null;

        if (memberType == typeof(IList<IWebElement>))
        {
            createdObject = new WebElementListProxy(locator, bys, cache);
            return true;
        }

        return false;
    }
}

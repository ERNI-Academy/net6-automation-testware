﻿using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras.MemberBuilders;

internal class WrappedElementListBuilder : IMemberBuilder
{
    public bool CreateObject(Type memberType, IElementLocator locator, IEnumerable<By> bys, bool cache, [NotNullWhen(true)] out object createdObject)
    {
        createdObject = null;

        if (memberType.IsGenericType && memberType.GetGenericTypeDefinition() == typeof(IList<>))
        {
            var elementType = memberType.GetGenericArguments()[0];

            if (typeof(IWrapsElement).IsAssignableFrom(elementType))
            {
                var listType = typeof(WrapsElementListProxy<>).MakeGenericType(memberType.GetGenericArguments()[0]);
                createdObject = Activator.CreateInstance(listType, new object[] { locator, bys, cache });
            }
        }

        return createdObject != null;
    }
}

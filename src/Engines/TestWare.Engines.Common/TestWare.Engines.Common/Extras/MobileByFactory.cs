#nullable enable
using System.Globalization;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace TestWare.Engines.Common.Extras;

/// <summary>
/// Provides instances of the <see cref="By"/> object to the attributes.
/// </summary>
internal static class MobileByFactory
{
    /// <summary>
    /// Gets an instance of the <see cref="By"/> class based on the specified attribute.
    /// </summary>
    /// <param name="attribute">The <see cref="MobileFindsByAttribute"/> describing how to find the element.</param>
    /// <returns>An instance of the <see cref="By"/> class.</returns>
    public static By From(MobileFindsByAttribute attribute)
    {
        var how = attribute.How;
        var usingValue = attribute.Using;
        switch (how)
        {
            case MobileHow.Id:
                return MobileBy.Id(usingValue);
            case MobileHow.Name:
                return MobileBy.Name(usingValue);
            case MobileHow.TagName:
                return MobileBy.TagName(usingValue);
            case MobileHow.ClassName:
                return MobileBy.ClassName(usingValue);
            case MobileHow.CssSelector:
                return By.CssSelector(usingValue);
            case MobileHow.LinkText:
                return By.LinkText(usingValue);
            case MobileHow.PartialLinkText:
                return By.PartialLinkText(usingValue);
            case MobileHow.XPath:
                return By.XPath(usingValue);
            case MobileHow.AccessibilityId:
                return MobileBy.AccessibilityId(usingValue);
            case MobileHow.Custom:
                if (attribute.CustomFinderType == null)
                {
                    throw new ArgumentException("Cannot use How.Custom without supplying a custom finder type");
                }

                if (!attribute.CustomFinderType.IsSubclassOf(typeof(By)))
                {
                    throw new ArgumentException("Custom finder type must be a descendent of the By class");
                }

                ConstructorInfo? ctor = attribute.CustomFinderType.GetConstructor(new Type[] { typeof(string) });
                if (ctor == null)
                {
                    throw new ArgumentException("Custom finder type must expose a public constructor with a string argument");
                }

                By finder = (By)ctor.Invoke(new object?[] { usingValue });

                return finder;
        }

        throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Did not know how to construct How from how {0}, using {1}", how, usingValue));
    }
}

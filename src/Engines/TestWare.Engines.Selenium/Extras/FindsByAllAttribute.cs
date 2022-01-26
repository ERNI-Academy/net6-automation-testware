
namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Marks elements to indicate that found elements should match the criteria of
/// all <see cref="FindsByAttribute"/> on the field or property.
/// </summary>
/// <remarks>
/// <para>
/// When used with a set of <see cref="FindsByAttribute"/>, all criteria must be
/// matched to be returned. The criteria are used in sequence according to the
/// Priority property. Note that the behavior when setting multiple
/// <see cref="FindsByAttribute"/> Priority properties to the same value, or not
/// specifying a Priority value, is undefined.
/// </para>
/// <para>
/// <code>
/// // Will find the element with the tag name "input" that also has an ID
/// // attribute matching "elementId".
/// [FindsByAll]
/// [FindsBy(How = How.TagName, Using = "input", Priority = 0)]
/// [FindsBy(How = How.Id, Using = "elementId", Priority = 1)]
/// public IWebElement thisElement;
/// </code>
/// </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class FindsByAllAttribute : Attribute
{
}

#nullable enable

using System.Collections;
using OpenQA.Selenium;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Represents a proxy class for a list of elements to be used with the PageFactory.
/// </summary>
internal class WebElementListProxy : WebDriverObjectProxy, IList<IWebElement>
{
    private IList<IWebElement>? _items;

    public WebElementListProxy(IElementLocator locator, IEnumerable<By> bys, bool cache)
        : base(locator, bys, cache)
    {
    }

    private IList<IWebElement> Items
    {
        get
        {
            if (_items == null || !Cache)
            {
                _items = Locator.LocateElements(Bys);
            }
            return _items;
        }
    }

    #region Forwarded Items calls

    public IWebElement this[int index]
    {
        get { return Items[index]; }
        set { Items[index] = value; }
    }

    public int Count => Items.Count;

    public bool IsReadOnly => Items.IsReadOnly;

    public void Add(IWebElement item) => Items.Add(item);

    public void Clear() => Items.Clear();

    public bool Contains(IWebElement item) => Items.Contains(item);

    public void CopyTo(IWebElement[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

    public IEnumerator<IWebElement> GetEnumerator() => Items.GetEnumerator();

    public int IndexOf(IWebElement item) => Items.IndexOf(item);

    public void Insert(int index, IWebElement item) => Items.Insert(index, item);

    public bool Remove(IWebElement item) => Items.Remove(item);

    public void RemoveAt(int index) => Items.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

    #endregion
}

#nullable enable

using System.Collections;
using OpenQA.Selenium;
using TestWare.Engines.Selenium.Extras;

namespace TestWare.Engines.Selenium.Extras;

/// <summary>
/// Represents a proxy class for a list of elements to be used with the PageFactory.
/// </summary>
internal class WrapsElementListProxy<T> : WebDriverObjectProxy, IList<T> where T : IWrapsElement
{
    private IList<T>? _items;

    public WrapsElementListProxy(IElementLocator locator, IEnumerable<By> bys, bool cache)
        : base(locator, bys, cache)
    {
    }

    private IList<T> Items
    {
        get
        {
            // Find elements, and wrap them in IWrapsElement instances.
            // If caching enabled - use previously found elements, if any.
            if (_items == null || !Cache)
            {
                _items = Locator
                    .LocateElements(Bys)
                    .Select(WrapsElementFactory.Wrap<T>)
                    .ToList();
            }
            return _items;
        }
    }

    #region Forwarded Items calls

    public T this[int index]
    {
        get { return Items[index]; }
        set { Items[index] = value; }
    }

    public int Count => Items.Count;

    public bool IsReadOnly => Items.IsReadOnly;

    public void Add(T item) => Items.Add(item);

    public void Clear() => Items.Clear();

    public bool Contains(T item) => Items.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => Items.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

    public int IndexOf(T item) => Items.IndexOf(item);

    public void Insert(int index, T item) => Items.Insert(index, item);

    public bool Remove(T item) => Items.Remove(item);

    public void RemoveAt(int index) => Items.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

    #endregion
}

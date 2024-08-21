using Microsoft.Playwright;

namespace TestWare.Samples.Suts.SwagLabs.Playwright;

public class Locator
{
    private Func<IPage, ILocator> _getter;
    public ILocator this[IPage page]
    {
        get => _getter(page);
    }

    public Locator(Func<IPage, ILocator> getter)
    {
        _getter = getter;
    }
}


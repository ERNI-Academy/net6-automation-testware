using Microsoft.Playwright;
using TestWare.Samples.Suts.SwagLabs.Playwright;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Interactions;

public class Click : Interaction<IPage>
{
    private Locator _locator;
    private Locator _container;
    private Func<ILocator, bool> _filterCriteria;
    private Func<ILocator, bool> _filterContainerCriteria;
    private int _index;

    public static Click On(Locator locator)
    {
        return new Click()
        {
            _locator = locator,
            _filterCriteria = _ => true,
            _filterContainerCriteria = _ => true,
            _index = 0
        };
    }

    public Click Inside(Locator locator)
    {
        _container = locator;
        return this;
    }

    public Click WithPosition(int index)
    {
        _index = index;
        return this;
    }

    public Click WithSibilingText(string text)
    {
        _filterContainerCriteria = loc => loc.TextContentAsync().Result.Contains(text);
        return this;
    }

    public Click WithInsideText(string text)
    { 
        _filterCriteria = loc => loc.TextContentAsync().Result.Contains(text);
        return this;
    }

    public override Click PerformedBy(IActor _)
    {
        var container = _container?[Enabler!].AllAsync().Result.First(el => _filterContainerCriteria(el)).Locator(_locator[Enabler!]) ?? _locator[Enabler!];
        Assertions.Expect(container.First).ToBeVisibleAsync().Wait();
        container.AllAsync().Result.Where(el => _filterCriteria(el)).ElementAt(_index).ClickAsync().Wait();
        return this;
    }

}
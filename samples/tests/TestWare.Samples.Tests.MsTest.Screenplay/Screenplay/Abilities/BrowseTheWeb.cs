
using Microsoft.Playwright;
using TestWare.Core.Interfaces;
using TestWare.Engines.PlaywrightEngine;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Abilities;

public class BrowseTheWeb : Ability<IPage>
{
    private ITestWareEngine _engine;

    public BrowseTheWeb(PlaywrightEngine engine)
    {
        engine.Initialize();
        _engine = engine;
        Enabler = engine.Page!; 
    }

    public BrowseTheWeb Dispose()
    {
        _engine.Dispose();
        return this;
    }
}


using Microsoft.Playwright;
using TestWare.Samples.Suts.SwagLabs.Playwright;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Questions;

public class Presence : Question<IPage, bool>
{
    private Locator _locator;

    public static Presence Of(Locator locator)
    {
        return new Presence() { _locator = locator };
    }

    public override Presence AnsweredTo(IActor _, out bool response)
    {
        try
        {
            Assertions.Expect(_locator[Enabler!]).ToBeVisibleAsync().Wait();
            response = true;
        }
        catch (Exception e)
        {
            response = false;
        }
        return this;
    }
}

using Microsoft.Playwright;
using TestWare.Samples.Suts.SwagLabs.Playwright;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Questions;
public class Text : Question<IPage, string[]>
{
    private Locator _locator;

    public static Text Of(Locator locator)
    {
        return new Text() { _locator = locator };
    }

    public override Text AnsweredTo(IActor _, out string[] response)
    {
        try
        {
            _locator[Enabler!].AllAsync().Result.ToList().ForEach( loc => Assertions.Expect(loc).ToBeVisibleAsync().Wait());
            response = _locator[Enabler].AllTextContentsAsync().Result.ToArray();
        }
        catch (Exception e)
        {
            response = [];
        }
        return this;
    }
}

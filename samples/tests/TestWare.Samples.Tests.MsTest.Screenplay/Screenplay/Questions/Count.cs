using Microsoft.Playwright;
using TestWare.Samples.Suts.SwagLabs.Playwright;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Questions;
public class Count : Question<IPage, int>
{
    private Locator? _locator;

    public static Count Of(Locator locator)
    {
        return new Count() { _locator = locator };
    }

    public override Count AnsweredTo(IActor _, out int response)
    {
        try
        {
            Assertions.Expect(_locator![Enabler!].First).ToBeVisibleAsync().Wait();
            response = _locator[Enabler!].CountAsync().Result;
        }
        catch (Exception e)
        {
            response = 0;
        }
        return this;
    }
}

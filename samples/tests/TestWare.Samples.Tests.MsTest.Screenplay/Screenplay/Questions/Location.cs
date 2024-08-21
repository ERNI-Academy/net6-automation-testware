using Microsoft.Playwright;
using TestWare.Samples.Suts.SwagLabs.Playwright;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Questions;

public class Location : Question<IPage, int>
{
    private Locator _locator;
    private string _text;

    public static Location Of(Locator locator)
    {
        return new Location() { _locator = locator };
    }

    public Location WithText(string text) 
    {
        _text = text;
        return this;
    }

    public override Location AnsweredTo(IActor _, out int response)
    {
        Assertions.Expect(_locator[Enabler!].First).ToBeVisibleAsync().Wait();
        var elements = _locator[Enabler!].AllTextContentsAsync().Result;
        response =  elements.ToList().IndexOf(_text);
        return this;
    }
}

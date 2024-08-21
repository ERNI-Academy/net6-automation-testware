using Microsoft.Playwright;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Interactions;




public class Navigate : Interaction<IPage>
{
    private string _url;

    public static Navigate To(string url)
    {
        return new Navigate() { _url = url };
    }
    public override Navigate PerformedBy(IActor _)
    {
        Enabler.GotoAsync(_url).Wait();
        return this;
    }
}

using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Interactions;


public class Log : Interaction<TextWriter>
{
    private string _message;

    public static Log TheMessage(string message)
    {
        return new Log() { _message = message };
    }

    public override Log PerformedBy(IActor _)
    {
        Enabler.WriteLine(_message);
        return this;
    }
}


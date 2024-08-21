using TestWare.Core.Interfaces;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Abilities;

public class LogTheActivity : Ability<TextWriter>
{
    private LogTheActivity(TextWriter logger) 
    {
        Enabler = logger;
    }
    public static LogTheActivity To(TextWriter logger)
    {
        return new LogTheActivity(logger);
    }
}



using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;

public abstract class Behaviour : IBehaviour
{
    public abstract IBehaviour PerformedAs(IActor actor);
}


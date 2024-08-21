using TestWare.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

public interface IBehaviour
{
    public IBehaviour PerformedAs(IActor actor);
}
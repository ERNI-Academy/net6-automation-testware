using TestWare.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

public interface IInteraction<T>
{
    public IInteraction<T> PerformedBy(IActor actor);
    public IInteraction<T> EnabledBy(T enabler);
}

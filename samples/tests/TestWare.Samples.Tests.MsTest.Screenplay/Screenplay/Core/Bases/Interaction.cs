using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;

public abstract class Interaction<T> : IInteraction<T>
{
    protected T? Enabler;

    public virtual IInteraction<T> EnabledBy(T enabler)
    {
        Enabler = enabler;
        return this;
    }
    public abstract IInteraction<T> PerformedBy(IActor actor);
}


using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
public abstract class Ability<T> : IAbility<T>
{
    protected T Enabler;
    public virtual T GetEnabler()
    {
        return Enabler;
    }
}


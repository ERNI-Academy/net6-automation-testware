using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;

public abstract class Question<T,R> : IQuestion<T,R>
{
    protected T? Enabler;
    public abstract IQuestion<T, R> AnsweredTo(IActor actor, out R response);
    public virtual IQuestion<T, R> EnabledBy(T enabler)
    {
        Enabler = enabler;
        return this;
    }
}



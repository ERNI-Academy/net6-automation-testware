using TestWare.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

public interface IQuestion<T,R>
{
    public IQuestion<T, R> EnabledBy(T enabler);
    public IQuestion<T, R> AnsweredTo(IActor actor, out R response);
}

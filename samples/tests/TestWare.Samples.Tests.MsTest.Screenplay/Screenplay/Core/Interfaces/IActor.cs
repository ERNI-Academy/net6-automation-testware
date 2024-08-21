using TestWare.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

public interface IActor: ITestwareComponent
{
    public IActor Performs(IBehaviour tasks);
    public IActor Performs<T>(IInteraction<T> interactions);
    public IActor HasAbility<T>(IAbility<T> ability);
    public IActor Answers<T,R>(IQuestion<T,R> question, out R response);
    public R Answers<T,R>(IQuestion<T,R> question);
}

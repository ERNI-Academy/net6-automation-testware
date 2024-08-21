using TestWare.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

public interface IAbility<T>: ITestwareComponent
{
    public T GetEnabler();
}

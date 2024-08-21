
namespace TestWare.Assistance.FluentSteps;


/// <summary>
/// Interface that defines the step chain
/// Step is over
/// </summary>
public interface IStepChain
{
    IStepChain Step(Action action);
    IStepChain Step(Action<dynamic[]> action, params dynamic[] args);
    IStepChain Step(string name, Action action);
    IStepChain Step(string name, Action<dynamic[]> action, params dynamic[] args);
    IStepChain Step<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IStepChain Step<T>(Func<T> function, out T returnValue, params dynamic[] args);
    IStepChain Step<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IStepChain Step<T>(string name, Func<T> function, out T returnValue, params dynamic[] args);

}
namespace TestWare.Assistance.FluentSteps.Flavours;

public interface IAAAChain: IStepChain
{
    IAAAChain Arrange(Action action);
    IAAAChain Arrange(Action<dynamic[]> action, params dynamic[] args);
    IAAAChain Arrange(string name);
    IAAAChain Arrange(string name, Action action);
    IAAAChain Arrange(string name, Action<dynamic[]> action, params dynamic[] args);
    IAAAChain Arrange<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IAAAChain Arrange<T>(Func<T> function, out T returnValue, params dynamic[] args);
    IAAAChain Arrange<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IAAAChain Arrange<T>(string name, Func<T> function, out T returnValue, params dynamic[] args);

    IAAAChain Act(Action action);
    IAAAChain Act(Action<dynamic[]> action, params dynamic[] args);
    IAAAChain Act(string name);
    IAAAChain Act(string name, Action action);
    IAAAChain Act(string name, Action<dynamic[]> action, params dynamic[] args);
    IAAAChain Act<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IAAAChain Act<T>(Func<T> function, out T returnValue, params dynamic[] args);
    IAAAChain Act<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IAAAChain Act<T>(string name, Func<T> function, out T returnValue, params dynamic[] args);

    IAAAChain Assert(Action action);
    IAAAChain Assert(Action<dynamic[]> action, params dynamic[] args);
    IAAAChain Assert(string name);
    IAAAChain Assert(string name, Action action);
    IAAAChain Assert(string name, Action<dynamic[]> action, params dynamic[] args);
    IAAAChain Assert<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IAAAChain Assert<T>(Func<T> function, out T returnValue, params dynamic[] args);
    IAAAChain Assert<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IAAAChain Assert<T>(string name, Func<T> function, out T returnValue, params dynamic[] args);
}

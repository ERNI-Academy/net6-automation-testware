namespace TestWare.Assistance.FluentSteps.Flavours;

public abstract class AAAChain : StepChain, IAAAChain
{
    public IAAAChain Act(string name) => (IAAAChain)Step(name, () => { });
    public IAAAChain Act(Action<dynamic[]> action, params dynamic[] args) => (IAAAChain)Step(action.ToString(), action, args);
    public IAAAChain Act(Action action) => (IAAAChain)Step(action.ToString(), action);
    public IAAAChain Act<T>(Func<T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(function.ToString(), args => { return function(); }, out returnValue, args);
    public IAAAChain Act<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(function.ToString(), args => { return function(args); }, out returnValue, args);

    public IAAAChain Act(string name, Action<dynamic[]> action, params dynamic[] args) => (IAAAChain)Step(name, args => { action(args); return true; }, out _, args);
    public IAAAChain Act(string name, Action action) => (IAAAChain)Step(name, args => { action(); return true; }, out _);
    public IAAAChain Act<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(name, args => { return function(); }, out returnValue);
    public virtual IAAAChain Act<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(name, function, out returnValue, args);


    public IAAAChain Arrange(string name) => (IAAAChain)Step(name, () => { });
    public IAAAChain Arrange(Action<dynamic[]> action, params dynamic[] args) => (IAAAChain)Step(action.ToString(), action, args);
    public IAAAChain Arrange(Action action) => (IAAAChain)Step(action.ToString(), action);
    public IAAAChain Arrange<T>(Func<T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(function.ToString(), args => { return function(); }, out returnValue, args);
    public IAAAChain Arrange<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(function.ToString(), args => { return function(args); }, out returnValue, args);

    public IAAAChain Arrange(string name, Action<dynamic[]> action, params dynamic[] args) => (IAAAChain)Step(name, args => { action(args); return true; }, out _, args);
    public IAAAChain Arrange(string name, Action action) => (IAAAChain)Step(name, args => { action(); return true; }, out _);
    public IAAAChain Arrange<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(name, args => { return function(); }, out returnValue);
    public virtual IAAAChain Arrange<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(name, function, out returnValue, args);


    public IAAAChain Assert(string name) => (IAAAChain)Step(name, () => { });
    public IAAAChain Assert(Action<dynamic[]> action, params dynamic[] args) => (IAAAChain)Step(action.ToString(), action, args);
    public IAAAChain Assert(Action action) => (IAAAChain)Step(action.ToString(), action);
    public IAAAChain Assert<T>(Func<T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(function.ToString(), args => { return function(); }, out returnValue, args);
    public IAAAChain Assert<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(function.ToString(), args => { return function(args); }, out returnValue, args);

    public IAAAChain Assert(string name, Action<dynamic[]> action, params dynamic[] args) => (IAAAChain)Step(name, args => { action(args); return true; }, out _, args);
    public IAAAChain Assert(string name, Action action) => (IAAAChain)Step(name, args => { action(); return true; }, out _);
    public IAAAChain Assert<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(name, args => { return function(); }, out returnValue);
    public virtual IAAAChain Assert<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IAAAChain)Step(name, function, out returnValue, args);


}


namespace TestWare.Assistance.FluentSteps.Flavours;

public abstract class GherkinChain : StepChain, IGherkinChain
{
    public IGherkinChain Given(string name) => (IGherkinChain)Step(name, () => { });
    public IGherkinChain Given(Action<dynamic[]> action, params dynamic[] args) => (IGherkinChain)Step(action.ToString(), action, args);
    public IGherkinChain Given(Action action) => (IGherkinChain)Step(action.ToString(), action);
    public IGherkinChain Given<T>(Func<T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(function.ToString(), args => { return function(); }, out returnValue, args);
    public IGherkinChain Given<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(function.ToString(), args => { return function(args); }, out returnValue, args);

    public IGherkinChain Given(string name, Action<dynamic[]> action, params dynamic[] args) => (IGherkinChain)Step(name, args => { action(args); return true; }, out _, args);
    public IGherkinChain Given(string name, Action action) => (IGherkinChain)Step(name, args => { action(); return true; }, out _);
    public IGherkinChain Given<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(name, args => { return function(); }, out returnValue);
    public virtual IGherkinChain Given<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(name, function, out returnValue, args);


    public IGherkinChain When(string name) => (IGherkinChain)Step(name, () => { });
    public IGherkinChain When(Action<dynamic[]> action, params dynamic[] args) => (IGherkinChain)Step(action.ToString(), action, args);
    public IGherkinChain When(Action action) => (IGherkinChain)Step(action.ToString(), action);
    public IGherkinChain When<T>(Func<T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(function.ToString(), args => { return function(); }, out returnValue, args);
    public IGherkinChain When<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(function.ToString(), args => { return function(args); }, out returnValue, args);

    public IGherkinChain When(string name, Action<dynamic[]> action, params dynamic[] args) => (IGherkinChain)Step(name, args => { action(args); return true; }, out _, args);
    public IGherkinChain When(string name, Action action) => (IGherkinChain)Step(name, args => { action(); return true; }, out _);
    public IGherkinChain When<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(name, args => { return function(); }, out returnValue);
    public virtual IGherkinChain When<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(name, function, out returnValue, args);


    public IGherkinChain Then(string name) => (IGherkinChain)Step(name, () => { });
    public IGherkinChain Then(Action<dynamic[]> action, params dynamic[] args) => (IGherkinChain)Step(action.ToString(), action, args);
    public IGherkinChain Then(Action action) => (IGherkinChain)Step(action.ToString(), action);
    public IGherkinChain Then<T>(Func<T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(function.ToString(), args => { return function(); }, out returnValue, args);
    public IGherkinChain Then<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(function.ToString(), args => { return function(args); }, out returnValue, args);

    public IGherkinChain Then(string name, Action<dynamic[]> action, params dynamic[] args) => (IGherkinChain)Step(name, args => { action(args); return true; }, out _, args);
    public IGherkinChain Then(string name, Action action) => (IGherkinChain)Step(name, args => { action(); return true; }, out _);
    public IGherkinChain Then<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(name, args => { return function(); }, out returnValue);
    public virtual IGherkinChain Then<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => (IGherkinChain)Step(name, function, out returnValue, args);


}

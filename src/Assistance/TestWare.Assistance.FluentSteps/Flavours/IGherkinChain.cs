namespace TestWare.Assistance.FluentSteps.Flavours;
public interface IGherkinChain : IStepChain
{
    IGherkinChain Given(Action action);
    IGherkinChain Given(Action<dynamic[]> action, params dynamic[] args);
    IGherkinChain Given(string name);
    IGherkinChain Given(string name, Action action);
    IGherkinChain Given(string name, Action<dynamic[]> action, params dynamic[] args);
    IGherkinChain Given<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IGherkinChain Given<T>(Func<T> function, out T returnValue, params dynamic[] args);
    IGherkinChain Given<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IGherkinChain Given<T>(string name, Func<T> function, out T returnValue, params dynamic[] args);

    IGherkinChain When(Action action);
    IGherkinChain When(Action<dynamic[]> action, params dynamic[] args);
    IGherkinChain When(string name);
    IGherkinChain When(string name, Action action);
    IGherkinChain When(string name, Action<dynamic[]> action, params dynamic[] args);
    IGherkinChain When<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IGherkinChain When<T>(Func<T> function, out T returnValue, params dynamic[] args);
    IGherkinChain When<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IGherkinChain When<T>(string name, Func<T> function, out T returnValue, params dynamic[] args);

    IGherkinChain Then(Action action);
    IGherkinChain Then(Action<dynamic[]> action, params dynamic[] args);
    IGherkinChain Then(string name);
    IGherkinChain Then(string name, Action action);
    IGherkinChain Then(string name, Action<dynamic[]> action, params dynamic[] args);
    IGherkinChain Then<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IGherkinChain Then<T>(Func<T> function, out T returnValue, params dynamic[] args);
    IGherkinChain Then<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args);
    IGherkinChain Then<T>(string name, Func<T> function, out T returnValue, params dynamic[] args);


}

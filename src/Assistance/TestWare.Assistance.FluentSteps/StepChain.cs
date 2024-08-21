namespace TestWare.Assistance.FluentSteps;

public abstract class StepChain : IStepChain
{
    protected int _stepN;

    public StepChain()
    {
        _stepN = 1;
    }

    public IStepChain Step(Action<dynamic[]> action, params dynamic[] args) => Step(action.ToString(), action, args);
    public IStepChain Step(Action action) => Step(action.ToString(), action);
    public IStepChain Step(string name) => Step(name, () => { });
    public IStepChain Step<T>(Func<T> function, out T returnValue, params dynamic[] args) => Step($"{function}", args => { return function(); }, out returnValue, args);
    public IStepChain Step<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => Step($"{function}", args => { return function(args); }, out returnValue, args);

    public IStepChain Step(string name, Action<dynamic[]> action, params dynamic[] args) => Step(name, args => { action(args); return true; }, out _, args);
    public IStepChain Step(string name, Action action) => Step(name, args => { action(); return true; }, out _);
    public IStepChain Step<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => Step(name, args => { return function(); }, out returnValue);
    public virtual IStepChain Step<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args)
    {
       
        try
        {
            BeforeStep(name, function, args);
            returnValue = function(args);
            AfterStep(name, function, returnValue, args);
            return this;
        }
        catch(Exception e)
        {
            ErrorStep(name, function, e, args);
            throw;
        }
        finally
        {
            _stepN++;
        }
    }
    public abstract void AfterStep<T>(string name, Func<dynamic[], T> function, T returnValue, params dynamic[] args);
    public abstract void BeforeStep<T>(string name, Func<dynamic[], T> function, params dynamic[] args);
    public abstract void ErrorStep<T>(string name, Func<dynamic[], T> function, Exception e, params dynamic[] args);
}

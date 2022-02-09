using Polly;
using Polly.Retry;
using System.Diagnostics;

namespace TestWare.Core.Libraries;

public static class RetryPolicies
{
    public static void ExecuteActionWithRetries(Action action)
        => ExecuteActionWithRetries(action, 3, TimeSpan.FromSeconds(1));

    public static void ExecuteActionWithRetries(Action action, int numberOfRetries)
        => ExecuteActionWithRetries(action, numberOfRetries, TimeSpan.FromSeconds(1));

    public static void ExecuteActionWithRetries(Action action, int numberOfRetries, TimeSpan retryAttemp)
    {
        var policy = Policy.Handle<Exception>()
                                .WaitAndRetry(numberOfRetries, retryAttempt => retryAttemp, (ex, time) => { });

        policy.Execute(() =>
        {
            action.Invoke();
        });
    }

    public static void ExecuteActionWithTimeout(Action action, int timeoutInMinutes)
        => ExecuteActionWithTimeout(action, timeoutInMinutes, null, null);

    public static void ExecuteActionWithTimeout(Action action, int timeoutInMinutes, Action actionIfException)
        => ExecuteActionWithTimeout(action, timeoutInMinutes, actionIfException, null);

    public static void ExecuteActionWithTimeout(Action action, int timeoutInMinutes, Action actionIfException, Action actionIfCatch)
    {
        var succeeded = false;
        var lastException = new Exception();
        var timer = new Stopwatch();
        timer.Start();

        while (!succeeded && timer.Elapsed.TotalMinutes < timeoutInMinutes)
        {
            try
            {
                action.Invoke();
                succeeded = true;
                lastException = new Exception();
            }
            catch (Exception ex)
            {
                actionIfCatch?.Invoke();
                lastException = ex;
            }
        }

        timer.Stop();

        if (lastException.Source != null)
        {
            actionIfException?.Invoke();
            throw lastException;
        }
    }

    public static void ExecuteActionDuringPeriod(Action action, int periodInMinutes)
    {
        var succeeded = true;

        var lastException = new Exception();
        var timer = new Stopwatch();
        timer.Start();

        while (succeeded && timer.Elapsed.TotalMinutes < periodInMinutes)
        {
            try
            {
                action.Invoke();
                succeeded = true;
            }
            catch (Exception ex)
            {
                lastException = ex;
                succeeded = false;
            }
        }

        timer.Stop();

        if (lastException.Source != null)
        {
            throw lastException;
        }
    }
}

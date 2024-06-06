using AventStack.ExtentReports.Model;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest;

//TODO move to assistance
public class StepChain
{
    private IEnumerable<ITestWareEngine> _engines;
    private IEnumerable<ITestWareCockpit> _cockpits;
    private string _evidencePath;
    private int _stepN;


    public StepChain(string evidencePath, IEnumerable<ITestWareEngine> engines, IEnumerable<ITestWareCockpit> cockpits)
    {
        _engines = engines;
        _cockpits = cockpits;
        _evidencePath = evidencePath;
        _stepN = 1;
    }
    public StepChain(string evidencePath, IEnumerable<ITestWareEngine> engines) : this(evidencePath, engines, [])
    {
    }


    public StepChain(string evidencePath, ITestWareEngine engine, ITestWareCockpit cockpit) : this(evidencePath, [engine], [cockpit])
    {
    }

    public StepChain(string evidencePath, ITestWareEngine engine) : this(evidencePath, [engine], [])
    {
    }

    public StepChain Step(string name) => Step(name, () => { });
    public StepChain Step(Action<dynamic[]> action, params dynamic[] args) => Step(action.ToString(), action, args);
    public StepChain Step(Action action) => Step(action.ToString(), action);
    public StepChain Step<T>(Func<T> function, out T returnValue, params dynamic[] args) => Step(function.ToString(), args => { return function(); }, out returnValue, args);
    public StepChain Step<T>(Func<dynamic[], T> function, out T returnValue, params dynamic[] args) => Step(function.ToString(), args => { return function(args); }, out returnValue, args);

    public StepChain Step(string name, Action<dynamic[]> action, params dynamic[] args) => Step(name, args => { action(args); return true; }, out _, args);
    public StepChain Step(string name, Action action) => Step(name, args => { action(); return true; }, out _);
    public StepChain Step<T>(string name, Func<T> function, out T returnValue, params dynamic[] args) => Step(name, args => { return function(); }, out returnValue);
    public StepChain Step<T>(string name, Func<dynamic[], T> function, out T returnValue, params dynamic[] args)
    {
        foreach (var cockpit in _cockpits)
        {
            cockpit.StartTestStep(name);
        }
        try
        {
            returnValue = function(args);
            return this;
        }
        finally
        {
            foreach (var engine in _engines)
            {
                var evidence = engine.CollectEvidence(_evidencePath, $"{_stepN:0000}.{name}");
                foreach (var cockpit in _cockpits)
                {
                    cockpit.AddTestStepActivity(evidence);
                }
            }
            _stepN++;
        }
    }
}

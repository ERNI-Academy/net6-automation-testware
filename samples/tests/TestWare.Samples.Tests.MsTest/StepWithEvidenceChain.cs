using TestWare.Assistance.FluentSteps;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest;

public class StepWithEvidenceChain(string evidencePath, IEnumerable<ITestWareEngine> engines, IEnumerable<ITestWareCockpit> cockpits) : StepChain
{
    public StepWithEvidenceChain(string evidencePath, IEnumerable<ITestWareEngine> engines) : this(evidencePath, engines, [])
    {
    }


    public StepWithEvidenceChain(string evidencePath, ITestWareEngine engine, ITestWareCockpit cockpit) : this(evidencePath, [engine], [cockpit])
    {
    }

    public StepWithEvidenceChain(string evidencePath, ITestWareEngine engine) : this(evidencePath, [engine], [])
    {
    }

    public override void AfterStep<T>(string name, Func<dynamic[], T> function, T returnValue, params dynamic[] args)
    {
        foreach (var engine in engines)
        {
            var evidence = engine.CollectEvidence(evidencePath, $"{_stepN:0000}.{name}");
            foreach (var cockpit in cockpits)
            {
                cockpit.AddTestStepActivity(evidence);
                cockpit.StopTestStep(Core.TestWareResult.Pass);
            }
        }
    }

    public override void BeforeStep<T>(string name, Func<dynamic[], T> function, params dynamic[] args)
    {
        foreach (var cockpit in cockpits)
        {
            cockpit.StartTestStep(name);
        }
    }

    public override void ErrorStep<T>(string name, Func<dynamic[], T> function, Exception e, params dynamic[] args)
    {
        foreach (var engine in engines)
        {
            var evidence = engine.CollectEvidence(evidencePath, $"{_stepN:0000}.{name}");
            foreach (var cockpit in cockpits)
            {
                cockpit.AddTestStepActivity(evidence);
                cockpit.StopTestStep(Core.TestWareResult.Fail);
            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestWare.Core;

namespace TestWare.Wheels.MsTestWheel;

public class ResultMapper {
    public static TestWareResult Translate(UnitTestOutcome result) =>
    result switch
    {
        UnitTestOutcome.Failed => TestWareResult.Fail,
        UnitTestOutcome.Passed => TestWareResult.Pass,
        UnitTestOutcome.Error => TestWareResult.Fail,
        UnitTestOutcome.Timeout => TestWareResult.Fail,
        UnitTestOutcome.Aborted => TestWareResult.Skip,
        UnitTestOutcome.NotRunnable => TestWareResult.Skip,
        _ => TestWareResult.Unknown,
    };
} 
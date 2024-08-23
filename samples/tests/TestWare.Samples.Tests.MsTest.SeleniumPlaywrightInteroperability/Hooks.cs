
using System.Reflection;
using TestWare.Core;
using TestWare.Engines.PlaywrightEngine;
using TestWare.Engines.SeleniumEngine;


namespace TestWare.Samples.Tests.MsTest.SeleniumPlaywrightInteroperability;
[TestClass]
public class Hooks
{
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {
        IEnumerable<Assembly> extraAssemblies = [
            typeof(SeleniumEngine).Assembly,
            typeof(PlaywrightEngine).Assembly,
        ];

        TestWareProvider.RegisterTestWareComponents("TestConfig.json", extraAssemblies);

    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {

    }
}

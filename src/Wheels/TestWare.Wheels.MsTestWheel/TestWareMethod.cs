using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core;
using TestWare.Core.Attributes;

namespace TestWare.Wheels.MsTestWheel;

public class TestWareMethod : DataTestMethodAttribute
{
    public override TestResult[] Execute(ITestMethod testMethod)
    {
        TestResult[] results;
        if ((testMethod.Arguments?.Count() ?? 0) < (testMethod.ParameterTypes?.Count() ?? 0))
        {
            var scopes = TestWareAttributes.GetTestWareScopes(testMethod.TestClassName, testMethod.TestMethodName);
            TestWareProvider.CreateScope(scopes);
            object[] injectedArgs = new object[testMethod.ParameterTypes.Count()];
            var argsCount = 0;
            for (var i = 0; i < testMethod.ParameterTypes.Count(); i++)
            {
                var paramType = testMethod.ParameterTypes[i].ParameterType;
                if (paramType == testMethod.Arguments?.ElementAtOrDefault(argsCount)?.GetType())
                {
                    injectedArgs[i] = testMethod.Arguments[argsCount];
                    argsCount++;
                }
                else
                {
                    string consumedScope;
                    injectedArgs[i] = TestWareProvider.GetTestWareComponentFromTags(testMethod.ParameterTypes[i].ParameterType, scopes, out consumedScope);
                    scopes = scopes.Where(x => x != consumedScope).ToArray();
                }
            }
            results = [testMethod.Invoke(injectedArgs)];
        }
        else
        {
            results = base.Execute(testMethod);
        }

        foreach (TestResult result in results)
        {
            if (result.Outcome == UnitTestOutcome.Failed)
            {
                string message = result.TestFailureException.Message;
            }
        }

        return results;
    }
}
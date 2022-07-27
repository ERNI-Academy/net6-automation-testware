
using TestWare.Core;
using TestWare.Samples.API.Resources.Age;
using TestWare.Samples.API.Resources.Simplify;

namespace TestWare.Samples.API.StepDefinitions;

[Binding]
internal class AgeSteps
{
    private readonly LastResponse _lastResponse;
    private readonly IAgeResource _ageResource;

    public AgeSteps(LastResponse lastResponse)
    {
        _lastResponse = lastResponse;
        _ageResource = ContainerManager.GetTestWareComponent<IAgeResource>("Api2");
    }

    [Given(@"A calculated age for name '([^']*)'")]
    public async Task GivenACalculatedAgeForName(string name)
    {
        var ageResponse = await _ageResource.GuessAge(name);
        _lastResponse.AddResponse(ageResponse);
    }

    [When(@"the formula '([^']*)' is simplified on '([^']*)'")]
    public async Task WhenTheFormulaIsSimplifiedOn(string formula, string api)
    {
        var _simplifyResource = ContainerManager.GetTestWareComponent<ISimplifyResource>(api);
        var ageResponse = await _simplifyResource.Simplify(formula);
        _lastResponse.AddResponse(ageResponse);
    }

    [Then(@"Age and expresion should be the same")]
    public void ThenAgeAndExpresionShouldBeTheSame()
    {
        var simplifyResponse = _lastResponse.GetResponse<SimplifyResponse>();
        var ageResponse = _lastResponse.GetResponse<AgeResponse>();
        simplifyResponse.Data.result.Should().Be(ageResponse.Data.age.ToString());
    }

}


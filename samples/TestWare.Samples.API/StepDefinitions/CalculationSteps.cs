using TestWare.Core;
using TestWare.Samples.API.Resources.Derive;
using TestWare.Samples.API.Resources.Factor;
using TestWare.Samples.API.Resources.Integrate;
using TestWare.Samples.API.Resources.Simplify;
using TestWare.Samples.API.Resources.Sine;
using TestWare.Samples.API.Resources.Unkown;

namespace TestWare.Samples.API.StepDefinitions;

[Binding]
public sealed class CalculationSteps
{
    private readonly ISimplifyResource _simplifyResource;
    private readonly IFactorResource _factorResource;
    private readonly IDeriveResource _deriveResource;
    private readonly IIntegrateResource _integrateResource;
    private readonly ISineResource _sineResource;
    private readonly IUnkownResource _unkownResource;
    private readonly LastResponse _lastResponse;

    public CalculationSteps(LastResponse lastResponse)
    {
        _simplifyResource = ContainerManager.GetTestWareComponent<ISimplifyResource>();
        _factorResource = ContainerManager.GetTestWareComponent<IFactorResource>();
        _deriveResource = ContainerManager.GetTestWareComponent<IDeriveResource>();
        _integrateResource = ContainerManager.GetTestWareComponent<IIntegrateResource>();
        _sineResource = ContainerManager.GetTestWareComponent<ISineResource>();
        _unkownResource = ContainerManager.GetTestWareComponent<IUnkownResource>();
        _lastResponse = lastResponse;
    }

    [When(@"the formula '([^']*)' is simplified")]
    public async Task WhenTheFormulaIsSimplified(string formula)
    {
        var simplifyResponse = await _simplifyResource.Simplify(formula);
        _lastResponse.AddResponse(simplifyResponse);
    }

    [Then(@"the simplified result is '([^']*)'")]
    public void ThenTheExpectedResultIs(string result)
    {
        var response = _lastResponse.GetResponse<SimplifyResponse>();
        response.Data.result.Should().Be(result);
    }

    [When(@"the formula '([^']*)' is factorized")]
    public async Task WhenTheFormulaIsFactorized(string formula)
    {
        var response = await _factorResource.Factor(formula);
        _lastResponse.AddResponse(response);
    }

    [Then(@"factor response result is ""([^""]*)""")]
    public void ThenFactorResponseResultIs(string result)
    {
        var response = _lastResponse.GetResponse<FactorResponse>();
        response.Data.result.Should().Be(result);
    }

    [When(@"the formula '([^']*)' is derived")]
    public async Task WhenTheFormulaIsDerived(string formula)
    {
        var response = await _deriveResource.Derive(formula);
        _lastResponse.AddResponse(response);
    }

    [Then(@"derive response result is ""([^""]*)""")]
    public void ThenDeriveResponseResultIs(string result)
    {
        var response = _lastResponse.GetResponse<DeriveResponse>();
        response.Data.result.Should().Be(result);
    }

    [When(@"the formula '([^']*)' is integrated")]
    public async Task WhenTheFormulaIsIntegrated(string formula)
    {
        var response = await _integrateResource.Integrate(formula);
        _lastResponse.AddResponse(response);
    }

    [Then(@"integrate response result is ""([^""]*)""")]
    public void ThenIntegrateResponseResultIs(string result)
    {
        var response = _lastResponse.GetResponse<IntegrateResponse>();
        response.Data.result.Should().Be(result);
    }

    [When(@"the operation sine is invoked with ""([^""]*)""")]
    public async Task WhenTheOperationSineIsInvokedWith(string operation)
    {
        var response = await _sineResource.Sine(operation);
        _lastResponse.AddResponse(response);
    }

    [Then(@"sine response result is ""([^""]*)""")]
    public void ThenSineResponseResultIs(string result)
    {
        var response = _lastResponse.GetResponse<SineResponse>();
        response.Data.result.Should().Be(result);
    }

    [Then(@"sine response error is ""([^""]*)""")]
    public void ThenSineResponseErrorIs(string error)
    {
        var response = _lastResponse.GetResponse<SineResponse>();
        response.Data.error.Should().Be(error);
    }

    [When(@"inexistent endpoint is called")]
    public async Task WhenInexistendEndpointIsCalled()
    {
        var response = await _unkownResource.Operate();
        _lastResponse.AddResponse(response);
    }

    [Then(@"unexpected response error is ""([^""]*)""")]
    public void ThenUnexpectedResponseErrorIs(string error)
    {
        var response = _lastResponse.GetResponse<UnkownResponse>();
        response.Data.error.Should().Be(error);
    }

}

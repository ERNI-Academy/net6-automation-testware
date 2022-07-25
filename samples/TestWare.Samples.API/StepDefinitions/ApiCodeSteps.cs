using RestSharp;
using TestWare.Samples.API.Resources.Derive;
using TestWare.Samples.API.Resources.Factor;
using TestWare.Samples.API.Resources.Integrate;
using TestWare.Samples.API.Resources.Simplify;
using TestWare.Samples.API.Resources.Sine;
using TestWare.Samples.API.Resources.Unkown;

namespace TestWare.Samples.API.StepDefinitions;

[Binding]
internal class ApiCodeSteps
{
    private readonly LastResponse _lastResponse;

    public ApiCodeSteps(LastResponse lastResponse)
    {
        _lastResponse = lastResponse;
    }

    [Then(@"the simplified response status code is '([^']*)'")]
    public void ThenTheSimplifiedResponseStatusCodeIs(int code)
    {
        var response = _lastResponse.GetResponse<SimplifyResponse>();
        ValidateReturnCode(response, code);
    }

    [Then(@"factor response status should be '([^']*)'")]
    public void ThenFactorResponseStatusShouldBe(int code)
    {
        var response = _lastResponse.GetResponse<FactorResponse>();
        ValidateReturnCode(response, code);
    }

    [Then(@"derive response status should be '([^']*)'")]
    public void ThenDeriveResponseStatusShouldBe(int code)
    {
        var response = _lastResponse.GetResponse<DeriveResponse>();
        ValidateReturnCode(response, code);
    }

    [Then(@"integrate response status should be '([^']*)'")]
    public void ThenIntegrateResponseStatusShouldBe(int code)
    {
        var response = _lastResponse.GetResponse<IntegrateResponse>();
        ValidateReturnCode(response, code);
    }

    [Then(@"sine response status should be '([^']*)'")]
    public void ThenSineResponseStatusShouldBe(int code)
    {
        var response = _lastResponse.GetResponse<SineResponse>();
        ValidateReturnCode(response, code);
    }

    [Then(@"unexpected response status should be '([^']*)'")]
    public void ThenUnexpectedResponseStatusShouldBe(int code)
    {
        var response = _lastResponse.GetResponse<UnkownResponse>();
        ValidateReturnCode(response, code);
    }

    private void ValidateReturnCode(RestResponse response, int expectedCode)
    {
        var code = (int)response.StatusCode;
        code.Should().Be(expectedCode);
    }
}

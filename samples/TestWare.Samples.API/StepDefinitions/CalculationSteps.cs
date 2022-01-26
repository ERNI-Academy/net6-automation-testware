using Autofac;
using TestWare.Core;

using TestWare.Samples.API.Resources.Derive;
using TestWare.Samples.API.Resources.Factor;
using TestWare.Samples.API.Resources.Integrate;
using TestWare.Samples.API.Resources.Simplify;
using TestWare.Samples.API.Resources.Sine;
using TestWare.Samples.API.Resources.Unkown;

namespace TestWare.Samples.API.StepDefinitions
{
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
            using (var scope = ContainerManager.Container.BeginLifetimeScope())
            {
                _simplifyResource = scope.Resolve<ISimplifyResource>() ?? throw new ArgumentNullException(nameof(ISimplifyResource));
                _factorResource = scope.Resolve<IFactorResource>() ?? throw new ArgumentNullException(nameof(IFactorResource));
                _deriveResource = scope.Resolve<IDeriveResource>() ?? throw new ArgumentNullException(nameof(IDeriveResource));
                _integrateResource = scope.Resolve<IIntegrateResource>() ?? throw new ArgumentNullException(nameof(IIntegrateResource));
                _sineResource = scope.Resolve<ISineResource>() ?? throw new ArgumentNullException(nameof(ISineResource));
                _unkownResource = scope.Resolve<IUnkownResource>() ?? throw new ArgumentException(nameof(IUnkownResource));
            }
            _lastResponse = lastResponse;
        }

        [When(@"the formula '([^']*)' is simplified")]
        public void WhenTheFormulaIsSimplified(string formula)
        {
            var simplifyResponse = _simplifyResource.Simplify(formula);
            _lastResponse.AddResponse(simplifyResponse);
        }

        [Then(@"the simplified result is '([^']*)'")]
        public void ThenTheExpectedResultIs(string result)
        {
            var response = _lastResponse.GetResponse<SimplifyResponse>();
            response.Data.result.Should().Be(result);
        }

        [When(@"the formula '([^']*)' is factorized")]
        public void WhenTheFormilaIsFactorized(string formula)
        {
            var response = _factorResource.Factor(formula);
            _lastResponse.AddResponse(response);
        }
        [Then(@"factor response result is ""([^""]*)""")]
        public void ThenFactorResponseResultIs(string result)
        {
            var response = _lastResponse.GetResponse<FactorResponse>();
            response.Data.result.Should().Be(result);
        }

        [When(@"the formula '([^']*)' is derived")]
        public void WhenTheFormilaIsDerived(string formula)
        {
            var response = _deriveResource.Derive(formula);
            _lastResponse.AddResponse(response);
        }

        [Then(@"derive response result is ""([^""]*)""")]
        public void ThenDeriveResponseResultIs(string result)
        {
            var response = _lastResponse.GetResponse<DeriveResponse>();
            response.Data.result.Should().Be(result);
        }

        [When(@"the formula '([^']*)' is integrated")]
        public void WhenTheFormilaIsIntegrated(string formula)
        {
            var response = _integrateResource.Integrate(formula);
            _lastResponse.AddResponse(response);
        }

        [Then(@"integrate response result is ""([^""]*)""")]
        public void ThenIntegrateResponseResultIs(string result)
        {
            var response = _lastResponse.GetResponse<IntegrateResponse>();
            response.Data.result.Should().Be(result);
        }

        [When(@"the operation sine is invoked with ""([^""]*)""")]
        public void WhenTheOperationSineIsInvokedWith(string operation)
        {
            var response = _sineResource.Sine(operation);
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
        public void WhenUnexistendEndpointIsCalled()
        {
            var response = _unkownResource.Operate();
            _lastResponse.AddResponse(response);
        }

        [Then(@"unexpected response error is ""([^""]*)""")]
        public void ThenUnexpectedResponseErrorIs(string error)
        {
            var response = _lastResponse.GetResponse<UnkownResponse>();
            response.Data.error.Should().Be(error);
        }

    }
}
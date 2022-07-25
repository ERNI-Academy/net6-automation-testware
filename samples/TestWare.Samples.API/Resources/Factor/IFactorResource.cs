using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Factor;

internal interface IFactorResource : ITestWareComponent
{
    Task<RestResponse<FactorResponse>> Factor(string formula);
}

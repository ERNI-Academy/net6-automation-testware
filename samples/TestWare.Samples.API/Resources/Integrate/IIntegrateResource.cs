using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Integrate;

internal interface IIntegrateResource : ITestWareComponent
{
    public Task<RestResponse<IntegrateResponse>> Integrate(string formula);
}

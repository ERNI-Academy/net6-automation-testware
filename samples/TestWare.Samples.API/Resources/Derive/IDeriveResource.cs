using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Derive;

internal interface IDeriveResource : ITestWareComponent
{
    public Task<RestResponse<DeriveResponse>> Derive(string formula);
}

using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Sine;

internal interface ISineResource : ITestWareComponent
{
    public Task<RestResponse<SineResponse>> Sine(string operation);
}

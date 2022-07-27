using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Age;

internal interface IAgeResource : ITestWareComponent
{
    public Task<RestResponse<AgeResponse>> GuessAge(string name);
}

using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Simplify;

internal interface ISimplifyResource : ITestWareComponent
{
    Task<RestResponse<SimplifyResponse>> Simplify(string formula);
}

using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Derive;

internal interface IDeriveResource : ITestWareComponent
{
    public RestResponse<DeriveResponse> Derive(string formula);
}

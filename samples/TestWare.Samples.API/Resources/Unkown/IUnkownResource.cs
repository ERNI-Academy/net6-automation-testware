using RestSharp;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Unkown;

internal interface IUnkownResource : ITestWareComponent
{
    public Task<RestResponse<UnkownResponse>> Operate();
}

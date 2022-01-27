using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Unkown;

internal interface IUnkownResource: ITestWareComponent
{
    public RestResponse<UnkownResponse> Operate();
}

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Sine;

internal interface ISineResource: ITestWareComponent
{
    public RestResponse<SineResponse> Sine(string operation);
}

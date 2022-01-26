using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Integrate
{
    internal interface IIntegrateResource: ITestWareComponent
    {
        public RestResponse<IntegrateResponse> Integrate(string formula);
    }
}

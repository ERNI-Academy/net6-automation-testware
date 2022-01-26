using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;

namespace TestWare.Samples.API.Resources.Derive
{
    internal interface IDeriveResource : ITestWareComponent
    {
        public RestResponse<DeriveResponse> Derive(string formula);
    }
}

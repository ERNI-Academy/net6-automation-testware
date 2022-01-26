using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Unkown
{
    internal class UnkownResource : ApiResource, IUnkownResource
    {
        private const string _resourceName = "unkown/invalid";

        public UnkownResource(IApiClient client) : base(client)
        {
            ResourceName = _resourceName;
        }
        public RestResponse<UnkownResponse> Operate()
        {
            var req = new RestRequest($"{ResourceName}");
            var response = ExecuteRequest<UnkownResponse>(req);
            return response;
        }
    }
}

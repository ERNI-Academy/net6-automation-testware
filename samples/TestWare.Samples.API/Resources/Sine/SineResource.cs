using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Sine
{
    internal class SineResource : ApiResource, ISineResource
    {
        private const string _resourceName = "sin";

        public SineResource(IApiClient client) : base(client)
        {
            ResourceName = _resourceName;
        }
        RestResponse<SineResponse> ISineResource.Sine(string operation)
        {
            var encodedUrl = HttpUtility.UrlEncode(operation);
            var req = new RestRequest($"{ResourceName}/{encodedUrl}");
            var response = ExecuteRequest<SineResponse>(req);
            return response;
        }
    }
}

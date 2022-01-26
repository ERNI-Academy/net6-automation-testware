using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TestWare.Engines.Restsharp.Factory;
using TestWare.Engines.Restsharp.Resources;

namespace TestWare.Samples.API.Resources.Derive
{
    internal class DeriveResource: ApiResource, IDeriveResource
    {
        private const string _resourceName = "derive";

        public DeriveResource(IApiClient client) : base(client)
        {
            ResourceName = _resourceName;
        }

        public RestResponse<DeriveResponse> Derive(string formula)
        {
            var encodedUrl = HttpUtility.UrlEncode(formula);
            var req = new RestRequest($"{ResourceName}/{encodedUrl}");
            var response = ExecuteRequest<DeriveResponse>(req);
            return response;
        }
    }
}

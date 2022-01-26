using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Samples.API
{
    public class LastResponse
    {
        private Dictionary<Type, object> _lastResponseDict = new();

        public void AddResponse<T>(RestResponse<T> response)
        {
            _lastResponseDict[typeof(T)] = response;
        }

        public RestResponse<T> GetResponse<T>()
        {
            return (RestResponse<T>)_lastResponseDict[typeof(T)];
        }
    }
}

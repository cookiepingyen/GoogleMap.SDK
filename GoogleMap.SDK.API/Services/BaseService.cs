using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using HttpUtility;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services
{
    public class BaseService
    {
        private string apiKey;
        protected IHttpRequest _httpRequest;

        public BaseService(IHttpRequest request, IConfiguration configuration)
        {
            apiKey = configuration.GetSection("apiKey").Value;
            _httpRequest = request;
        }

        public async Task<T> GetAsync<T>(BaseRequest request)
        {
            string url = request.ToUri(apiKey);
            var response = await _httpRequest.GetAsync<T>(url);
            return response;
        }
        public async Task<T> PostAsync<T>(BaseRequest request, object data, Dictionary<string, string> urlParam = null, JsonSerializerSettings settings = null)
        {
            string url = request.ToUri(apiKey);
            var response = await _httpRequest.PostAsync<T>(url, data, urlParam, settings);
            return response;
        }
    }
}

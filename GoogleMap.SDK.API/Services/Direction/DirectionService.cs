using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Direction;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Geocoding;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using HttpUtility;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Direction
{
    public class DirectionService : BaseService, IDirectionService
    {
        public DirectionService(IHttpRequest request, IConfiguration configuration) : base(request, configuration)
        {
        }

        public Task<DirectionResModel> GetDirections(DirectionRequest directionRequest, string urlInput = null)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new DirectionConvert() }
            };

            return base.GetAsync<DirectionResModel>(directionRequest, settings);
        }
    }
}

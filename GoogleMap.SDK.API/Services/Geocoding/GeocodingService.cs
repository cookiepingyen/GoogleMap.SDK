using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Geocoding;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.FindPlace;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using HttpUtility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Geocoding
{
    public class GeocodingService : BaseService, IGeocodingService
    {
        public GeocodingService(IHttpRequest request, IConfiguration configuration) : base(request, configuration)
        {
        }

        public Task<GeocodingResModel> GeocodingSearch(GeocodingRequest geocodingRequest, string urlInput = null)
        {
            return base.GetAsync<GeocodingResModel>(geocodingRequest);
        }
    }
}

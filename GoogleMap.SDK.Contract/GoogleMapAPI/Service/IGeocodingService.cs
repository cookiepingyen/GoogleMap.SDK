using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Service
{
    public interface IGeocodingService
    {
        Task<GeocodingResModel> GeocodingSearch(GeocodingRequest geocodingRequest, string urlInput = null);
    }
}

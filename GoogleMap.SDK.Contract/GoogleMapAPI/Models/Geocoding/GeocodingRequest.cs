using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Geocoding
{
    public class GeocodingRequest : BaseRequest
    {
        protected override string Endpoint => "geocode/json";

        public string Address { get; set; }
    }
}

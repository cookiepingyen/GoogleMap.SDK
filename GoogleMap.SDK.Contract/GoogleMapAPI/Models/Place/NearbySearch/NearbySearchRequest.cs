using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.NearbySearch
{
    public class NearbySearchRequest : BaseRequest
    {
        protected override string Endpoint => "place/nearbysearch/json";


        [Restructure("location", ',')]
        [Require]
        public Location location { get; set; }


        private int _radiue { get; set; }
        [Require]
        public int radius { get; set; }


        [Optional]
        public string keyword { get; set; }

        [Optional]
        public LocationType type { get; set; }

        public NearbySearchRequest() { }

        public NearbySearchRequest(Location location, int radius, string keyword, LocationType type)
        {
            this.location = location;
            this.radius = radius;
            this.keyword = keyword;
            this.type = type;
        }


    }
}

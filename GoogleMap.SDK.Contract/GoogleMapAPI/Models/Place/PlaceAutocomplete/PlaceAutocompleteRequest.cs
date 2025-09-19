using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceAutocomplete
{
    public class PlaceAutocompleteRequest : BaseRequest
    {
        protected override string Endpoint => "place/autocomplete/json";

        [Require]
        public string input { get; set; }

        [Restructure("location", ',')]
        [Optional]
        public Location location { get; set; }

        [Optional]
        public int radius { get; set; }

        [Optional]
        public LocationType types { get; set; }

        public PlaceAutocompleteRequest() { }

        public PlaceAutocompleteRequest(string input, Location location, LocationType type, int radius = 500)
        {
            this.input = input;
            this.location = location;
            this.radius = radius;
            this.types = type;
        }


    }
}

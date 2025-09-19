using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail
{
    public class PlaceDetailRequest : BaseRequest
    {
        protected override string Endpoint => "place/details/json";

        [Require]
        public string placeId { get; set; }
        [Optional]
        public PlaceDetailInputFields[] fields { get; set; }


        public PlaceDetailRequest()
        {

        }

        public PlaceDetailRequest(string placeId, PlaceDetailInputFields[] fields = null)
        {
            this.placeId = placeId;
            this.fields = fields;
        }
    }
}

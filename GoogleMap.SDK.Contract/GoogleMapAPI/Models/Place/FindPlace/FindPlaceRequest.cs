using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.FindPlace
{
    public class FindPlaceRequest : BaseRequest
    {
        protected override string Endpoint => "place/findplacefromtext/json";

        [Require]
        public FindPlaceInputFields[] fields { get; set; }
        public string inputType { get; set; }

        [Require]
        public string input { get; set; }


        public FindPlaceRequest(string inputType = "textquery")
        {

        }

        public FindPlaceRequest(FindPlaceInputFields[] fields, string inputType = "textquery")
        {
            this.inputType = inputType;
            this.fields = fields;
        }
    }
}

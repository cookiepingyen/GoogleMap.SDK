using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlacePhoto
{
    public class PlacePhotoRequest : BaseRequest
    {
        protected override string Endpoint => "place/photo";

        [Require]
        [Atleast(1)]
        public PhotoSpec photoSpec { get; set; }

        [Require]
        public string photo_reference { get; set; }

        public PlacePhotoRequest()
        {

        }

        public PlacePhotoRequest(PhotoSpec photoSpec, string photo_reference)
        {
            this.photoSpec = photoSpec;
            this.photo_reference = photo_reference;
        }
    }
}

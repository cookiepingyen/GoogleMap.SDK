using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes
{
    public class RoutesPostModel
    {
        public Address origin { get; set; }
        public Address destination { get; set; }
        public string travelMode { get; set; }
        public string routingPreference { get; set; }
        public bool computeAlternativeRoutes { get; set; }
        public Routemodifiers routeModifiers { get; set; }
        public string languageCode { get; set; } = "zh-TW";
        public string units { get; set; } = "METRIC";

        public class Address
        {
            public string placeId { get; set; }
            public string address { get; set; }
            public Address(string location, AddressType addressType)
            {
                if (addressType == AddressType.Address)
                {
                    this.address = location;
                }
                else
                {
                    this.placeId = location;
                }

            }
        }


        public class Routemodifiers
        {
            public bool avoidTolls { get; set; }
            public bool avoidHighways { get; set; }
            public bool avoidFerries { get; set; }
            public Routemodifiers(bool avoidTolls, bool avoidHighways, bool avoidFerries)
            {
                this.avoidTolls = avoidTolls;
                this.avoidHighways = avoidHighways;
                this.avoidFerries = avoidFerries;
            }
        }

    }
}

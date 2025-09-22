using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes
{
    public class RoutesRequest
    {
        public static readonly string HttpsUri = "https://routes.googleapis.com/directions/v2:computeRoutes";

        [Require]
        public string destination { get; set; }

        [Require]
        public string origin { get; set; }

        public TravelMode mode { get; set; } = TravelMode.DRIVE;

        public string routingPreference { get; set; }

        public bool computeAlternativeRoutes { get; set; }

        public bool avoidTolls { get; set; } = false;

        public bool avoidHighways { get; set; }
        public bool avoidFerries { get; set; }

        public AddressType addressType { get; set; }

        public RoutesRequest()
        {

        }


        public RoutesRequest(string origin, string destination, TravelMode mode = TravelMode.DRIVE, AddressType addressType = AddressType.Address)
        {
            this.destination = destination;
            this.origin = origin;
            this.mode = mode;
            this.addressType = addressType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="origin">起點位置</param>
        /// <param name="destination">終點位置</param>
        /// <param name="mode">交通模式</param>
        /// <param name="avoidTolls">是否避免收費</param>
        /// <param name="avoidHighways">是否避開高速公路</param>
        /// <param name="avoidFerries">是否避開渡輪</param>
        public RoutesRequest(string origin, string destination, TravelMode mode = TravelMode.DRIVE, bool avoidTolls = false, bool avoidHighways = false, bool avoidFerries = false)
        {
            this.destination = destination;
            this.origin = origin;
            this.avoidTolls = avoidTolls;
            this.avoidHighways = avoidHighways;
            this.avoidFerries = avoidFerries;
            this.mode = mode;
        }

    }
}

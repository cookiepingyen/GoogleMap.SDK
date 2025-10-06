using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Direction
{
    public class DirectionRequest : BaseRequest
    {
        protected override string Endpoint => "directions/json";

        [Require]
        public string destination { get; set; }

        [Require]
        public string origin { get; set; }

        [Separator('|')]
        public AvoidType[] avoid { get; set; }

        public Mode mode { get; set; }

        public bool alternatives { get; set; }

        [Separator('|')]
        public string[] waypoints { get; set; }


        public DirectionRequest()
        {

        }

        public DirectionRequest(string destination, string origin)
        {
            this.destination = destination;
            this.origin = origin;
        }

    }

}

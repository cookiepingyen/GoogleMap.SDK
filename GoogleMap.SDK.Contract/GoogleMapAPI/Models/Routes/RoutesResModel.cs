using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes
{
    public class RoutesResModel
    {
        public Route[] routes { get; set; }

        public class Route
        {
            public Leg[] legs { get; set; }
            public int distanceMeters { get; set; }
            public string duration { get; set; }
            public Overview_Polyline polyline { get; set; }
        }

        public class Overview_Polyline
        {
            public Location[] encodedPolyline { get; set; }
        }

        public class Leg
        {
            public int distanceMeters { get; set; }
            public string duration { get; set; }
            public string staticDuration { get; set; }
            public Polyline1 polyline { get; set; }
            public Startlocation startLocation { get; set; }
            public Endlocation endLocation { get; set; }
            public Step[] steps { get; set; }
            public Localizedvalues localizedValues { get; set; }
        }

        public class Polyline1
        {
            public string encodedPolyline { get; set; }
        }

        public class Startlocation
        {
            public Latlng latLng { get; set; }
        }

        public class Latlng
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class Endlocation
        {
            public Latlng1 latLng { get; set; }
        }

        public class Latlng1
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class Localizedvalues
        {
            public Distance distance { get; set; }
            public Duration duration { get; set; }
            public Staticduration staticDuration { get; set; }
        }

        public class Distance
        {
            public string text { get; set; }
        }

        public class Duration
        {
            public string text { get; set; }
        }

        public class Staticduration
        {
            public string text { get; set; }
        }

        public class Step
        {
            public int distanceMeters { get; set; }
            public string staticDuration { get; set; }
            public Polyline2 polyline { get; set; }
            public Startlocation1 startLocation { get; set; }
            public Endlocation1 endLocation { get; set; }
            public Navigationinstruction navigationInstruction { get; set; }
            public Localizedvalues1 localizedValues { get; set; }
            public string travelMode { get; set; }
        }

        public class Polyline2
        {
            public string encodedPolyline { get; set; }
        }

        public class Startlocation1
        {
            public Latlng2 latLng { get; set; }
        }

        public class Latlng2
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class Endlocation1
        {
            public Latlng3 latLng { get; set; }
        }

        public class Latlng3
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class Navigationinstruction
        {
            public string maneuver { get; set; }
            public string instructions { get; set; }
        }

        public class Localizedvalues1
        {
            public Distance1 distance { get; set; }
            public Staticduration1 staticDuration { get; set; }
        }

        public class Distance1
        {
            public string text { get; set; }
        }

        public class Staticduration1
        {
            public string text { get; set; }
        }


    }
}

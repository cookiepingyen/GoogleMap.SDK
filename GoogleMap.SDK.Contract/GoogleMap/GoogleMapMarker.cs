using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMap
{
    public class GoogleMapMarker
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public object Tag { get; set; }

        public GoogleMapMarker() { }
        public GoogleMapMarker(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Lat={0}, Lng={1}}}", Latitude, Longitude);
        }


    }
}

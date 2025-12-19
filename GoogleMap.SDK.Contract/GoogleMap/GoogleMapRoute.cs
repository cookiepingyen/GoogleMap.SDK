using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMap
{
    public class GoogleMapRoute
    {
        public List<Location> locations { get; set; }

        public string name { get; set; }
        public object Tag { get; set; }

        public GoogleMapRoute() { }
        public GoogleMapRoute(string name, List<Location> locations)
        {
            this.name = name;
            this.locations = locations;
        }

    }
}

using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMap
{
    public interface IOverlay
    {
        string Name { get; set; }
        void AddMarker(Location location);
        void AddMarkers(List<Location> locations);
        void RemoveMarker(Location location);
        void RemoveMarkers(List<Location> locations);
        void AddRoutes(List<Location> route);
        void RemoveRoutes(string name);
    }
}

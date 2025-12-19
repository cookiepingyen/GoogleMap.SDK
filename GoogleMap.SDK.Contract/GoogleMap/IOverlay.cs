using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contract.GoogleMap.IMapControl;

namespace GoogleMap.SDK.Contract.GoogleMap
{
    public interface IOverlay
    {
        string Name { get; set; }
        void AddMarker(Location location, GoogleMapEvent markerEvent = null, object tooltip = null);
        void AddMarkers(List<Location> locations);
        void RemoveMarker(Location location);
        void RemoveMarkers(List<Location> locations);
        void AddRoutes(List<Location> route, RouteEvent RouteClick = null);
        void RemoveRoutes(string name);
    }
}

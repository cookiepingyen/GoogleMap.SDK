using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMap
{
    public interface IMapControl
    {
        delegate void GoogleMapEvent(GoogleMapMarker marker);
        delegate void RouteEvent(List<Location> route);
        event GoogleMapEvent MarkerClick;
        event RouteEvent RouteClick;

        void ChangePosition(Location point);

        void AddMarker(string str, Location location);

        void AddRoute(string str, List<Location> locationList);

    }
}

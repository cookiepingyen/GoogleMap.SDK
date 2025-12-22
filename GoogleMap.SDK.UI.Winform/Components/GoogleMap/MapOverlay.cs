using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GoogleMap.SDK.Contract.GoogleMap;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.StaticMap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contract.GoogleMap.IMapControl;

namespace GoogleMap.SDK.UI.Winform.Components.GoogleMap
{
    public class MapOverlay : GMapOverlay, IOverlay
    {
        private string _name;
        public string Name { get => _name; set => _name = value; }

        public void AddMarker(Location location, GoogleMapEvent markerEvent = null, object tooltop = null)
        {
            PointLatLng point = new PointLatLng(location.Latitude, location.Longitude);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
            marker.Tag = this;
            marker.ToolTipText = tooltop.ToString();

            // tooltip
            GMapToolTip gMapToolTip = new GMapToolTip(marker);
            gMapToolTip.Fill = new SolidBrush(Color.FromArgb(100, Color.Black));
            gMapToolTip.Foreground = Brushes.White;
            gMapToolTip.TextPadding = new System.Drawing.Size(20, 20);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTip = gMapToolTip;
            Markers.Add(marker);
        }

        public void AddMarkers(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                PointLatLng point = new PointLatLng(location.Latitude, location.Longitude);
                GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_dot);
                marker.Tag = this;
                Markers.Add(marker);
            }
        }

        public void AddRoutes(List<Location> routes, RouteEvent RouteClick = null)
        {
            List<PointLatLng> pointLatLngs = routes.Select(
                location => new PointLatLng(location.Latitude, location.Longitude)
                ).ToList();

            GMapRoute route = new GMapRoute(pointLatLngs, this.Name);
            Routes.Add(route);
        }

        public void RemoveMarker(Location location)
        {
            GMapMarker marker = Markers.FirstOrDefault(x => x.Position.ToString() == location.ToString());
            Markers.Remove(marker);
        }

        public void RemoveMarkers(List<Location> locations)
        {
            throw new NotImplementedException();
        }

        public void RemoveRoutes(string name)
        {
            var route = Routes.First(x => x.Name == this.Name);
            Routes.Remove(route);
        }
    }
}

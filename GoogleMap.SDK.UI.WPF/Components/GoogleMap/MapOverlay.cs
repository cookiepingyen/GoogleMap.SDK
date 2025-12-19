using GMap.NET;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.GoogleMap;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.StaticMap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static GoogleMap.SDK.Contract.GoogleMap.IMapControl;

namespace GoogleMap.SDK.UI.WPF.Components.GoogleMap
{
    public class MapOverlay : IOverlay
    {
        public readonly ObservableCollection<GMapMarker> Markers = new ObservableCollection<GMapMarker>();
        public readonly ObservableCollection<GMapRoute> Routes = new ObservableCollection<GMapRoute>();

        public event GoogleMapEvent MarkerClick;


        private string _name;
        public string Name { get => _name; set => _name = value; }

        public void AddMarker(Location location, GoogleMapEvent markerEvent = null, object tooltip = null)
        {
            PointLatLng point = new PointLatLng(location.Latitude, location.Longitude);
            Image dynamicImage = new Image();
            dynamicImage.Width = 32;
            dynamicImage.Height = 32;
            dynamicImage.Tag = location;
            dynamicImage.ToolTip = tooltip;
            dynamicImage.MouseRightButtonDown += (sender, e) =>
            {
                markerEvent?.Invoke(new GoogleMapMarker(location.Latitude, location.Longitude));
            };


            // 設定圖片來源
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://github.com/judero01col/GMap.NET/blob/master/GMap.NET/GMap.NET.Core/Resources/marker.png?raw=true", UriKind.Absolute);
            bitmap.EndInit();

            dynamicImage.Source = bitmap;

            GMapMarker marker = new GMapMarker(point)
            {
                Shape = dynamicImage
            };
            marker.Tag = this;

            Markers.Add(marker);
        }

        public void AddMarkers(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                PointLatLng point = new PointLatLng(location.Latitude, location.Longitude);
                GMapMarker marker = new GMapMarker(point);
                Markers.Add(marker);
            }
        }

        public void AddRoutes(List<Location> routes, RouteEvent RouteClick = null)
        {
            List<PointLatLng> pointLatLngs = routes.Select(
                location => new PointLatLng(location.Latitude, location.Longitude)
                ).ToList();


            GoogleRoute route = new GoogleRoute(Name, pointLatLngs, RouteClick);
            Routes.Add(route);
        }

        private void Marker_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image markerImage = (Image)sender;
            Location location = (Location)markerImage.Tag;
            MarkerClick?.Invoke(new GoogleMapMarker(location.Latitude, location.Longitude));
        }

        public void RemoveMarker(Location location)
        {
            GMapMarker marker = Markers.FirstOrDefault(x => x.Position.ToString() == location.ToString());
            Markers.Remove(marker);
        }

        public void RemoveMarkers(List<Location> locations)
        {
            foreach (Location location in locations)
            {
                RemoveMarker(location);
            }
        }

        public void RemoveRoutes(string name = null)
        {
            var routes = Routes.ToList();

            foreach (GMapRoute gMapRoute in routes)
            {
                Routes.Remove(gMapRoute);
            }
        }
    }
}

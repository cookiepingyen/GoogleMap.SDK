using GMap.NET.WindowsPresentation;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using static GoogleMap.SDK.Contract.GoogleMap.IMapControl;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMap;

namespace GoogleMap.SDK.UI.WPF
{
    public class GoogleRoute : GMapRoute
    {
        private string _name;
        public string Name { get => _name; set => _name = value; }

        public Brush Stroke = Brushes.Navy;

        public RouteEvent Route_Click;
        public GoogleRoute(string name, IEnumerable<PointLatLng> points, RouteEvent mapRouteEvent = null) : base(points)
        {
            Route_Click = mapRouteEvent;
            this.Name = name;
        }


        public override Path CreatePath(List<Point> localPath, bool addBlurEffect)
        {
            Path path = base.CreatePath(localPath, addBlurEffect);
            path.Stroke = Stroke;
            path.IsHitTestVisible = true;

            List<Location> locations = localPath.Select(
                point => new Location(point.X, point.Y)
                ).ToList();
            path.Tag = locations;

            path.MouseLeftButtonDown += Path_MouseLeftButtonDown;
            return path;
        }

        private void Path_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Path path = (Path)sender;
            List<Location> locations = (List<Location>)path.Tag;
            GoogleMapRoute googleMapRoute = new GoogleMapRoute(this.Name, locations);

            Route_Click?.Invoke(googleMapRoute);
        }
    }
}

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GoogleMap.SDK.Contract.GoogleMap;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static GoogleMap.SDK.Contract.GoogleMap.IMapControl;
using Location = GoogleMap.SDK.Contract.GoogleMapAPI.Models.Location;

namespace GoogleMap.SDK.UI.Winform.Components.GoogleMap
{
    public partial class GoogleMapControl : UserControl, IMapControl
    {
        IOverlayService overlayService;
        Dictionary<string, IOverlay> overlays = new Dictionary<string, IOverlay>();

        public event GoogleMapEvent MarkerClick;
        public event RouteEvent RouteClick;



        public GoogleMapControl(IOverlayService overlayService)
        {
            InitializeComponent();
            this.overlayService = overlayService;

            mapControl.MapProvider = GMapProviders.GoogleMap;
            mapControl.MinZoom = 2;
            mapControl.MaxZoom = 25;
            mapControl.Zoom = 16;
            mapControl.ShowCenter = false; //不显示中心十字点
            mapControl.DragButton = MouseButtons.Left; //左键拖拽地图
            mapControl.Position = new PointLatLng(25.0090256, 121.5027398); //地图中心位置：南京
            mapControl.OnMarkerClick += MapControl_OnMarkerClick;
            mapControl.OnRouteDoubleClick += MapControl_OnRouteDoubleClick;
        }

        private void MapControl_OnRouteDoubleClick(GMapRoute item, MouseEventArgs e)
        {
            IOverlay overlay = (IOverlay)item.Tag;
            overlay.RemoveRoutes(overlay.Name);

            List<Location> route = item.Points.Select(x => new Location(x.Lat, x.Lng)).ToList();
            RouteClick?.Invoke(route);
        }

        private void MapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            IOverlay overlay = (IOverlay)item.Tag;
            overlay.RemoveMarker(new Location(item.Position.Lat, item.Position.Lng));
            MarkerClick?.Invoke(new GoogleMapMarker(item.Position.Lat, item.Position.Lng));
        }


        public void AddMarker(string name, Location location)
        {
            IOverlay overlay = TryGetOverLayer(name);
            overlay.AddMarker(location);
            ChangePosition(location);

        }

        public void AddRoute(string name, List<Location> route)
        {
            IOverlay overlay = TryGetOverLayer(name);
            overlay.AddRoutes(route);
        }

        public void ChangePosition(Location point)
        {
            mapControl.Position = new PointLatLng(point.Latitude, point.Longitude);
        }

        private IOverlay TryGetOverLayer(string overlayId)
        {
            IOverlay overlay = overlayService.GetLayer(overlayId);

            if (!overlays.ContainsKey(overlayId))
            {
                overlays.Add(overlayId, overlay);
                mapControl.Overlays.Add((GMapOverlay)overlay);
            }

            return overlay;
        }


    }
}

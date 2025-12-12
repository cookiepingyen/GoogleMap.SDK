using GMap.NET.MapProviders;
using GMap.NET;
using GoogleMap.SDK.Contract.GoogleMap;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Location = GoogleMap.SDK.Contract.GoogleMapAPI.Models.Location;
using static GoogleMap.SDK.Contract.GoogleMap.IMapControl;
using System.Xml.Linq;
using System.Collections.Specialized;
using GMap.NET.WindowsPresentation;

namespace GoogleMap.SDK.UI.WPF.Components.GoogleMap
{
    /// <summary>
    /// GoogleMapControl.xaml 的互動邏輯
    /// </summary>
    public partial class GoogleMapControl : UserControl, IMapControl
    {
        IOverlayService overlayService;
        List<IOverlay> overlays = new List<IOverlay>();

        public event GoogleMapEvent MarkerClick;
        public event RouteEvent RouteClick;

        public GoogleMapControl(IOverlayService overlayService)
        {
            InitializeComponent();
            this.overlayService = overlayService;

            mapControl.MapProvider = GMapProviders.GoogleMap; //google china 地图
            mapControl.MinZoom = 2;  //最小缩放
            mapControl.MaxZoom = 25; //最大缩放
            mapControl.Zoom = 15;     //当前缩放
            mapControl.ShowCenter = false; //不显示中心十字点
            mapControl.DragButton = MouseButton.Left; //左键拖拽地图
            mapControl.Position = new PointLatLng(25.0090256, 121.5027398); //地图中心位置：南京

        }


        public void AddMarker(string str, Location location)
        {
            IOverlay overlay = overlayService.GetLayer(str);
            AddOverlayListener(overlay);
            overlay.AddMarker(location);
            ChangePosition(location);
        }

        public void AddRoute(string str, List<Location> locationList)
        {
            IOverlay overlay = overlayService.GetLayer(str);
            AddOverlayListener(overlay);
            overlay.AddRoutes(locationList);
        }

        public void ChangePosition(Location point)
        {
            mapControl.Position = new PointLatLng(point.Latitude, point.Longitude);
        }

        private IOverlay AddOverlayListener(IOverlay overlay)
        {
            if (!overlays.Contains(overlay))
            {
                overlays.Add(overlay);

                MapOverlay mapOverlay = (MapOverlay)overlay;
                mapOverlay.Markers.CollectionChanged += Markers_CollectionChanged;
                mapOverlay.Routes.CollectionChanged += Markers_CollectionChanged;
            }

            return overlay;
        }

        private void Markers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (GMapMarker item in e.NewItems)
                {
                    mapControl.Markers.Add(item);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (GMapMarker item in e.NewItems)
                {
                    mapControl.Markers.Remove(item);
                }
            }
        }

    }
}

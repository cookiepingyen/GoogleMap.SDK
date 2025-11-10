using GMap.NET.MapProviders;
using GMap.NET;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete;
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
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;
using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Direction;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using Location = GoogleMap.SDK.Contract.GoogleMapAPI.Models.Location;
using static GMap.NET.Entity.OpenStreetMapGraphHopperGeocodeEntity;
using Microsoft.Extensions.DependencyInjection;
using IOCServiceCollection;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes;
using GoogleMap.SDK.Contract.GoogleMapAPI;

namespace GoogleMap.SDK.UI.WPF.Test
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public event EventHandler<MouseButtonEventArgs> onMarkerClick;
        PlaceAutoCompleteView originAutoCompleteView;
        PlaceAutoCompleteView destAutoCompleteView;
        IGoogleAPIContext googleAPIContext;

        public MainWindow(ServiceProvider provider)
        {
            InitializeComponent();

            // 加上兩個 autocomplete
            originAutoCompleteView = (PlaceAutoCompleteView)provider.GetService<IAutoCompleteView>();
            destAutoCompleteView = (PlaceAutoCompleteView)provider.GetService<IAutoCompleteView>();
            destAutoCompleteView.Margin = new Thickness(0, 50, 0, 0);

            autoCompletePanel.Children.Add(originAutoCompleteView);
            autoCompletePanel.Children.Add(destAutoCompleteView);

            originAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;
            destAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;

            googleAPIContext = provider.GetService<IGoogleAPIContext>();


            // 加上路線按鈕
            Button placeBtn = new Button();
            placeBtn.Content = "規劃路線";
            placeBtn.Margin = new Thickness(0, 50, 0, 0);
            placeBtn.Click += PlaceBtn_Click;

            autoCompletePanel.Children.Add(placeBtn);



            mapControl.MapProvider = GMapProviders.GoogleMap; //google china 地图
            mapControl.MinZoom = 2;  //最小缩放
            mapControl.MaxZoom = 25; //最大缩放
            mapControl.Zoom = 15;     //当前缩放
            mapControl.ShowCenter = false; //不显示中心十字点
            mapControl.DragButton = MouseButton.Left; //左键拖拽地图
            mapControl.Position = new PointLatLng(25.0090256, 121.5027398); //地图中心位置：南京

            mapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);

            GoogleMapRouteGenerate();
        }



        private void PlaceAutoCompleteView_selectChange(object sender, PlaceDetailResModel e)
        {
            Console.WriteLine($"name: {e.result.name}, place_id: {e.result.place_id}");

            MapToolTip toolTip = new MapToolTip();
            toolTip.DataContext = new PlaceCard()
            {
                PlaceName = e.result.name,
                Phone = e.result.formatted_phone_number,
                Address = e.result.formatted_address
            };

            Image dynamicImage = new Image();
            dynamicImage.Width = 32;
            dynamicImage.Height = 32;
            dynamicImage.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
            dynamicImage.MouseRightButtonDown += Marker_MouseRightButtonDown;
            dynamicImage.ToolTip = toolTip;  // 直接設定，不需要再包一層！

            // 設定圖片來源
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://github.com/judero01col/GMap.NET/blob/master/GMap.NET/GMap.NET.Core/Resources/marker.png?raw=true", UriKind.Absolute);
            bitmap.EndInit();

            dynamicImage.Source = bitmap;

            GMapMarker marker = new GMapMarker(new PointLatLng(e.result.geometry.location.lat, e.result.geometry.location.lng))
            {
                Shape = dynamicImage
            };

            dynamicImage.Tag = marker;
            mapControl.Markers.Add(marker);
        }

        void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Point clickPoint = e.GetPosition(mapControl);
            //PointLatLng point = mapControl.FromLocalToLatLng((int)clickPoint.X - 16, (int)clickPoint.Y - 16);

            //Image dynamicImage = new Image();
            //dynamicImage.Width = 32;  // 可自訂大小
            //dynamicImage.Height = 32;
            //dynamicImage.MouseLeftButtonDown += Marker_MouseLeftButtonDown;
            //dynamicImage.MouseRightButtonDown += Marker_MouseRightButtonDown;
            //dynamicImage.ToolTip = new MapToolTip();
            //// 設定圖片來源
            //BitmapImage bitmap = new BitmapImage();
            //bitmap.BeginInit();
            //bitmap.UriSource = new Uri("https://github.com/judero01col/GMap.NET/blob/master/GMap.NET/GMap.NET.Core/Resources/marker.png?raw=true", UriKind.Absolute);
            //bitmap.EndInit();

            //dynamicImage.Source = bitmap;

            //GMapMarker marker = new GMapMarker(point)
            //{
            //    Shape = dynamicImage
            //};

            //dynamicImage.Tag = marker;
            //mapControl.Markers.Add(marker);
        }

        private void Marker_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            GMapMarker marker = (GMapMarker)image.Tag;

            mapControl.Markers.Remove(marker);
        }

        private void Marker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            GMapMarker marker = (GMapMarker)image.Tag;
            onMarkerClick.Invoke(this, e);
        }

        void GoogleMapRouteGenerate()
        {

            string encodePath = "wwzwCgsrdVJs@ZqBlAT~A`@pBZrB^pDt@[jBO|@M|@[nB_@bC]~BK|@GPCJUd@w@bBc@|@a@|@]p@KJCJcArBg@bAWf@g@hAsDjIaAjBOXiAhCaCpF_@rAKVsEdKa@`Aw@fBMZKXaAhCIPINo@vA_BrDi@jAg@~@Yn@Wh@O^iBlEa@`Ae@r@OFi@jAYz@E`@B~AqCnA}At@|@nCFRlDuAvAo@JGv@]`@QZMb@Qf@SfBy@n@Y";

            List<Location> list = GooglePoints.Decode(encodePath).ToList();

            var points = list.Select((x) => new PointLatLng()
            {
                Lat = x.Latitude,
                Lng = x.Longitude,
            }).ToList();

            GMapRoute route = new GMapRoute(points);
            mapControl.Markers.Add(route);

            Console.WriteLine(list);
        }

        private async void PlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            string origin = originAutoCompleteView.SelectValue;
            string destination = destAutoCompleteView.SelectValue;

            DirectionRequest directionRequest = new DirectionRequest(destination, origin, true);

            AvoidType[] avoidTypes = { AvoidType.tolls, AvoidType.ferries };
            directionRequest.avoid = avoidTypes;
            directionRequest.mode = Mode.driving;
            directionRequest.alternatives = true;

            DirectionResModel directionResModel = await googleAPIContext.Direction.GetDirections(directionRequest);
            Console.WriteLine(directionResModel.geocoded_waypoints[0].place_id);
            Console.WriteLine(directionResModel.routes[0].summary);

            Location[] locationArray = directionResModel.routes[0].overview_polyline.points;


            var points = locationArray.Select((x) => new PointLatLng()
            {
                Lat = x.Latitude,
                Lng = x.Longitude,
            }).ToList();

            GMapRoute route = new GMapRoute(points);
            mapControl.Markers.Add(route);
        }
    }
}

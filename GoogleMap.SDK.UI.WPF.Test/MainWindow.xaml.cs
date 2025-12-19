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
using static GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes.RoutesResModel;
using System.Diagnostics;
using GoogleMap.SDK.Contract.GoogleMap;
using static GoogleMap.SDK.UI.WPF.GoogleRoute;
using static GoogleMap.SDK.Contract.GoogleMap.IMapControl;

namespace GoogleMap.SDK.UI.WPF.Test
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        PlaceAutoCompleteView originAutoCompleteView;
        PlaceAutoCompleteView destAutoCompleteView;
        IMapControl mapControl;
        ServiceProvider serviceProvider;
        IGoogleAPIContext googleAPIContext;
        GoogleMapMarker selectedMarker;

        public event EventHandler<MouseButtonEventArgs> onMarkerClick;


        public MainWindow(ServiceProvider provider, IGoogleAPIContext googleAPIContext)
        {
            this.serviceProvider = provider;
            this.googleAPIContext = googleAPIContext;
            InitializeComponent();

            // 加上兩個 autocomplete
            originAutoCompleteView = (PlaceAutoCompleteView)provider.GetService<IAutoCompleteView>();
            originAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;
            autoCompletePanel.Children.Add(originAutoCompleteView);

            destAutoCompleteView = (PlaceAutoCompleteView)provider.GetService<IAutoCompleteView>();
            destAutoCompleteView.Margin = new Thickness(0, 50, 0, 0);
            destAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;
            autoCompletePanel.Children.Add(destAutoCompleteView);

            // 加上 規劃路線按鈕
            Button placeBtn = new Button();
            placeBtn.Content = "規劃路線";
            placeBtn.Margin = new Thickness(0, 50, 0, 0);
            placeBtn.Click += PlaceBtn_Click;
            autoCompletePanel.Children.Add(placeBtn);


            // 加上 移除marker按鈕
            Button removeMarkerBtn = new Button();
            removeMarkerBtn.Content = "移除marker";
            removeMarkerBtn.Margin = new Thickness(0, 50, 0, 0);
            removeMarkerBtn.Click += RemoveMarkerBtn_Click;
            autoCompletePanel.Children.Add(removeMarkerBtn);


            mapControl = serviceProvider.GetService<IMapControl>();
            mapControl.RouteClick += MapControl_RouteClick;
            mapControl.MarkerClick += MapControl_MarkerClick;
            Control control = (Control)mapControl;
            container.Children.Add(control);
            Grid.SetRow(control, 0);
            Grid.SetColumn(control, 1);
        }

        private void RemoveMarkerBtn_Click(object sender, RoutedEventArgs e)
        {
            mapControl.RemoveMarker("選擇的地點", new Location(selectedMarker.Latitude, selectedMarker.Longitude));
        }

        private void MapControl_MarkerClick(GoogleMapMarker marker)
        {
            selectedMarker = marker;
        }

        private void MapControl_RouteClick(Contract.GoogleMap.GoogleMapRoute route)
        {
            mapControl.RemoveRoute(route.name);
        }

        private void PlaceAutoCompleteView_selectChange(object sender, PlaceDetailResModel e)
        {
            Console.WriteLine($"name: {e.result.name}, place_id: {e.result.place_id}");

            MapToolTip toolTip = new MapToolTip();
            toolTip.DataContext = new PlaceCard()
            {
                PlaceName = e.result.name,
                Phone = e.result.formatted_phone_number,
                Address = e.result.formatted_address,
                Rating = e.result.rating,
                UserRatingsTotal = $"({e.result.user_ratings_total.ToString()})",
                BusinessStatus = (e.result.current_opening_hours.open_now ? "營業中" : "已打烊")
            };

            var location = e.result.geometry.location;

            mapControl.AddMarker("選擇的地點", new Location(location.lat, location.lng), toolTip);
        }


        private void Marker_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Image image = (Image)sender;
            //GMapMarker marker = (GMapMarker)image.Tag;

            //mapControl.Markers.Remove(marker);
        }

        private void Marker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            GMapMarker marker = (GMapMarker)image.Tag;
            onMarkerClick.Invoke(this, e);
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

            List<Location> locationList = directionResModel.routes[0].overview_polyline.points.ToList();
            mapControl.AddRoute("路線1", locationList);
        }



    }
}

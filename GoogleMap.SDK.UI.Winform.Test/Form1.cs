using GMap.NET.WindowsForms;
using GMap.NET;
using GoogleMap.SDK.API.Services.Routes;
using GoogleMap.SDK.Contract.GoogleMap;
using GoogleMap.SDK.Contract.GoogleMapAPI;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Direction;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes;
using GoogleMap.SDK.Core.Component.AutoComplete;
using GoogleMap.SDK.UI.Winform.Components.AutoComplete;
using IOCServiceCollection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.UI.Winform.Test
{
    public partial class Form1 : Form
    {
        PlaceAutoCompleteView originCompleteView;
        PlaceAutoCompleteView destCompleteView;
        IMapControl mapControl;
        ServiceProvider serviceProvider;
        IGoogleAPIContext googleAPIContext;

        public Form1(ServiceProvider serviceProvider, IGoogleAPIContext googleAPIContext)
        {
            this.serviceProvider = serviceProvider;
            this.googleAPIContext = googleAPIContext;
            InitializeComponent();
            this.originCompleteView = (PlaceAutoCompleteView)serviceProvider.GetService<IAutoCompleteView>();
            PlaceAutoCompleteView originAutoCompleteView = this.originCompleteView;
            originAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;
            flowLayoutPanel1.Controls.Add(originAutoCompleteView);

            this.destCompleteView = (PlaceAutoCompleteView)serviceProvider.GetService<IAutoCompleteView>();
            PlaceAutoCompleteView destAutoCompleteView = this.destCompleteView;
            destAutoCompleteView.selectChange += PlaceAutoCompleteView_selectChange;
            flowLayoutPanel1.Controls.Add(destAutoCompleteView);

            Button placeBtn = new Button();
            placeBtn.Text = "規劃路線";
            placeBtn.Click += PlaceBtn_Click;
            flowLayoutPanel1.Controls.Add(placeBtn);

            this.mapControl = serviceProvider.GetService<IMapControl>();
            this.mapControl.MarkerClick += MapControl_MarkerClick;


            Control map = (Control)mapControl;
            map.Dock = DockStyle.Fill;
            panel2.Controls.Add(map);
        }

        private void MapControl_MarkerClick(GoogleMapMarker marker)
        {
            Console.WriteLine("Click");
            Console.WriteLine(marker.ToString());
        }

        private async void PlaceBtn_Click(object sender, EventArgs e)
        {
            string origin = originCompleteView.SelectValue;
            string destination = destCompleteView.SelectValue;


            DirectionRequest directionRequest = new DirectionRequest(destination, origin, true);

            AvoidType[] avoidTypes = { AvoidType.tolls, AvoidType.ferries };
            directionRequest.avoid = avoidTypes;
            directionRequest.mode = Mode.driving;
            directionRequest.alternatives = true;


            DirectionResModel directionResModel = await googleAPIContext.Direction.GetDirections(directionRequest);
            Console.WriteLine(directionResModel.geocoded_waypoints[0].place_id);
            Console.WriteLine(directionResModel.routes[0].summary);

            Location[] locationArray = directionResModel.routes[0].overview_polyline.points;

            this.mapControl.AddRoute("路線1", locationArray.ToList());
            this.mapControl.RouteClick += MapControl_RouteClick;
        }

        private void MapControl_RouteClick(List<Location> route)
        {
            Console.WriteLine("route click");
        }

        private void PlaceAutoCompleteView_selectChange(object sender, PlaceDetailResModel e)
        {
            var location = e.result.geometry.location;
            this.mapControl.AddMarker("好吃的地點", new Location(location.lat, location.lng));
            Console.WriteLine($"name: {e.result.name}, place_id: {e.result.place_id}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

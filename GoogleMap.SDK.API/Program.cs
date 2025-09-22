using GoogleMap.SDK.API.Services.Place;
using GoogleMap.SDK.Contract.GoogleMapAPI;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.FindPlace;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.NearbySearch;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlacePhoto;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using IOCServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCollection = IOCServiceCollection.ServiceCollection;

namespace GoogleMap.SDK.API
{
    internal class Program
    {
        public static IServiceProvider provider = null;
        static async Task Main(string[] args)
        {

            var collection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            collection.AddGoogleMapAPIRegistration(configuration);
            collection.Add(new ServiceDescriptor(typeof(IConfiguration), configuration));

            provider = collection.BuildServiceProvider();
            IGoogleAPIContext googleAPIContext = provider.GetService<IGoogleAPIContext>();

            #region FindPlace
            //FindPlaceInputFields[] fields = { FindPlaceInputFields.name, FindPlaceInputFields.geometry, FindPlaceInputFields.type };
            //FindPlaceRequest findPlaceRequest = new FindPlaceRequest(fields);
            //findPlaceRequest.input = "永和豆漿大王";
            //FindPlaceResModel findPlaceResModel = await googleAPIContext.Place.NearbySearch(findPlaceRequest);
            //Console.WriteLine(findPlaceResModel.candidates[0].name);
            #endregion

            #region NearbySearch
            //Location location = new Location();
            //location.Latitude = -33.8587323;
            //location.Longitude = 151.2100055;

            //NearbySearchRequest nearbySearchRequest = new NearbySearchRequest();
            //nearbySearchRequest.keyword = "cruise";
            //nearbySearchRequest.location = location;
            //nearbySearchRequest.type = LocationType.restaurant;
            //nearbySearchRequest.radius = 150;

            //NearbySearchResModel nearbySearchResModel = await googleAPIContext.Place.NearbySearch(nearbySearchRequest);
            //Console.WriteLine(nearbySearchResModel.results[0].name);
            #endregion

            #region PlaceDetail
            //PhotoSpec photoSpec = new PhotoSpec();
            //photoSpec.maxwidth = 400;
            //photoSpec.maxheight = 400;

            //PlacePhotoRequest placePhotoRequest = new PlacePhotoRequest();
            //placePhotoRequest.photo_reference = "AciIO2dDvb_eK6CEMSZssnYCwUBBhZctvOyZhELCtO5oJQQZK21KBRj1wNDdatO_g2zmd6Gj4U1Ea0Ic_QDEJVmM_i34kKe_SB3tbcmW9Gf-uJtdB4KtygaZkIaSDEhl0Z2kQoc6Vjo2IBCei37nmxpIoWEEPf9oKkV4z67C9nb6g_EtE3xPxupud2VTEOhjC6D9NZavkgEBt89pNkPXIcwqvLLGmj6L0b3hr_DosfCKVFTuFvlzMGboaBuRZZcZk4Kvlyq2_LXO-Gj5_ecnElyJMtKKIKXb-oGhV__JRGz_Q_rGeWypwyA";
            //placePhotoRequest.photoSpec = photoSpec;

            //Byte[] photoBytes = await googleAPIContext.Place.PlacePhoto(placePhotoRequest);

            //PlacePhotoResModel placePhotoResModel = new PlacePhotoResModel();
            //Form form = new Form();
            //PictureBox pictureBox = new PictureBox();
            //pictureBox.Width = 400;
            //pictureBox.Height = 400;
            //pictureBox.Image = new Bitmap(new MemoryStream(photoBytes));
            //form.Controls.Add(pictureBox);
            //form.ShowDialog();
            #endregion

            #region Routes
            //RoutesRequest routesRequest = new RoutesRequest("台北火車站", "西門町", TravelMode.DRIVE, AddressType.Address);
            //RoutesResModel routesResModel = await googleAPIContext.Route.GetRoutes(routesRequest);
            //var temp = routesResModel.routes[0].polyline.encodedPolyline;
            #endregion


            Console.ReadLine();

        }
    }
}

using GoogleMap.SDK.API.Converts;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using HttpUtility;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Routes
{
    public class RoutesService : BaseService, IRouteService
    {
        public readonly string HttpsUri = "https://routes.googleapis.com/directions/v2:computeRoutes";


        public RoutesService(IHttpRequest request, IConfiguration configuration) : base(request, configuration)
        {
        }

        public Task<RoutesResModel> GetRoutes(RoutesRequest routesRequest)
        {
            base._httpRequest.AddHeader("X-Goog-Api-Key", base.apiKey);
            base._httpRequest.AddHeader("X-Goog-FieldMask", "routes.legs,routes.distanceMeters,routes.duration,routes.polyline.encodedPolyline");

            RoutesPostModel routesPostModel = new RoutesPostModel();
            routesPostModel.origin = new RoutesPostModel.Address(routesRequest.origin, routesRequest.addressType);
            routesPostModel.destination = new RoutesPostModel.Address(routesRequest.destination, routesRequest.addressType);
            routesPostModel.travelMode = routesRequest.mode.ToString();
            routesPostModel.routingPreference = routesRequest.routingPreference;
            routesPostModel.computeAlternativeRoutes = routesRequest.computeAlternativeRoutes;
            routesPostModel.routeModifiers = new RoutesPostModel.Routemodifiers(routesRequest.avoidTolls, routesRequest.avoidHighways, routesRequest.avoidFerries);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new RoutesConverter() }
            };
            return base._httpRequest.PostAsync<RoutesResModel>(HttpsUri, routesPostModel, settings: settings);
        }
    }
}

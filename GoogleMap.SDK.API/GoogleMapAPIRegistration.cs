using GoogleMap.SDK.API.Services.Direction;
using GoogleMap.SDK.API.Services.Geocoding;
using GoogleMap.SDK.API.Services.Place;
using GoogleMap.SDK.API.Services.Routes;
using GoogleMap.SDK.API.Services.StaticMap;
using GoogleMap.SDK.Contract.GoogleMapAPI;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using HttpUtility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API
{
    public static class GoogleMapAPIRegistration
    {
        public static void AddGoogleMapAPIRegistration(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddSingleton<IHttpRequest, HttpRequest>();
            collection.AddSingleton<IGoogleAPIContext, GoogleAPIContext>();
            collection.AddSingleton<IGeocodingService, GeocodingService>();
            collection.AddTransient<IPlaceService, PlaceSercvice>();
            collection.AddTransient<IRouteService, RoutesService>();
            collection.AddTransient<IStaticMapService, StaticMapService>();
            collection.AddTransient<IDirectionService, DirectionService>();

        }
    }
}

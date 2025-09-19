using GoogleMap.SDK.API.Services.Place;
using GoogleMap.SDK.Contract.GoogleMapAPI;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.FindPlace;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using IOCServiceCollection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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


            FindPlaceInputFields[] fields = { FindPlaceInputFields.name, FindPlaceInputFields.geometry, FindPlaceInputFields.type };
            FindPlaceRequest findPlaceRequest = new FindPlaceRequest(fields);
            findPlaceRequest.input = "永和豆漿大王";

            FindPlaceResModel findPlaceResModel = await googleAPIContext.Place.FindPlace(findPlaceRequest);
            Console.WriteLine(findPlaceResModel.candidates[0].name);

            Console.ReadLine();

        }
    }
}

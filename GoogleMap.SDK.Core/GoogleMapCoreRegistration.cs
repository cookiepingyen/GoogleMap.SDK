using GoogleMap.SDK.API;
using GoogleMap.SDK.Core.Component.AutoComplete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.Core
{
    public static class GoogleMapCoreRegistration
    {
        public static void AddGoogleMapCoreRegistration(this IServiceCollection collection)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            collection.Add(new ServiceDescriptor(typeof(IConfiguration), configuration));
            collection.AddGoogleMapAPIRegistration(configuration);


            collection.AddSingleton<IAutoCompletePresenter, PlaceAutoCompletePresenter>();
        }

    }
}

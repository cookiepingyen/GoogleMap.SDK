using GoogleMap.SDK.Contract.GoogleMap;
using GoogleMap.SDK.Core.Service;
using GoogleMap.SDK.UI.Winform.Components.AutoComplete;
using GoogleMap.SDK.UI.Winform.Components.GoogleMap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.UI.Winform
{
    public static class GoogleMapWinformRegistration
    {
        public static void AddGoogleMapWinformRegistration(this IServiceCollection collection)
        {
            collection.AddTransient<IAutoCompleteView, PlaceAutoCompleteView>();
            collection.AddTransient<IOverlay, MapOverlay>();
            collection.AddTransient<IOverlayService, OverlayService>();
            collection.AddTransient<IMapControl, GoogleMapControl>();

        }


    }
}

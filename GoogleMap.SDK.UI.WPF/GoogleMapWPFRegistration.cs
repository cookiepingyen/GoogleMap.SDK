using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.GoogleMap;
using GoogleMap.SDK.Core.Service;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete;
using GoogleMap.SDK.UI.WPF.Components.GoogleMap;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.UI.WPF
{
    public static class GoogleMapWPFRegistration
    {
        public static void AddGoogleMapWPFRegistration(this IServiceCollection collection)
        {
            collection.AddTransient<IAutoCompleteView, PlaceAutoCompleteView>();
            collection.AddTransient<IOverlay, MapOverlay>();
            collection.AddTransient<IOverlayService, OverlayService>();
            collection.AddTransient<IMapControl, GoogleMapControl>();

        }
    }
}

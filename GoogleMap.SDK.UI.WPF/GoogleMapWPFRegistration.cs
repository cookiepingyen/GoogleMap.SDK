using GoogleMap.SDK.UI.WPF.Components.AutoComplete;
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
            collection.AddSingleton<IAutoCompleteView, PlaceAutoCompleteView>();
        }
    }
}

using GoogleMap.SDK.UI.Winform.Components.AutoComplete;
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
        public static void AddGoogleMapWinformRegistration(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddSingleton<IAutoCompleteView, PlaceAutoCompleteView>();
        }


    }
}

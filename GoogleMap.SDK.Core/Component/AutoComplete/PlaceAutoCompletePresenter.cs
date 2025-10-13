using GoogleMap.SDK.API.Services.Place;
using GoogleMap.SDK.API.Services.StaticMap;
using GoogleMap.SDK.Contract.Components.AutoComplete;
using GoogleMap.SDK.Contract.GoogleMapAPI;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceAutocomplete;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GoogleMap.SDK.Contract.Components.AutoComplete.AutoCompleteContract;

namespace GoogleMap.SDK.Core.Component.AutoComplete
{
    public class PlaceAutoCompletePresenter : IAutoCompletePresenter
    {
        IGoogleAPIContext googleAPIContext;
        public PlaceAutoCompletePresenter(IGoogleAPIContext googleAPIContext)
        {
            this.googleAPIContext = googleAPIContext;
        }

        public async Task<List<object>> GetDataSource(string text)
        {

            Location location = new Location();
            location.Latitude = 25.0452244;
            location.Longitude = 121.5202049;

            PlaceAutocompleteRequest placeAutocompleteRequest = new PlaceAutocompleteRequest();
            placeAutocompleteRequest.input = text;
            placeAutocompleteRequest.location = location;
            placeAutocompleteRequest.radius = 500;
            placeAutocompleteRequest.types = LocationType.establishment;

            PlaceAutocompleteResModel placeAutocompleteResModel = await googleAPIContext.Place.PlaceAutocomplete(placeAutocompleteRequest);

            List<object> predictionDatas = placeAutocompleteResModel.predictions.Select(x => (object)new ComboBoxData(x.structured_formatting.main_text, x.place_id)).ToList();
            return predictionDatas;
        }

        public async Task<object> GetItemDetail(string selectedItem)
        {
            PlaceDetailRequest placeDetailRequest = new PlaceDetailRequest();
            placeDetailRequest.placeId = selectedItem;
            placeDetailRequest.fields = new PlaceDetailInputFields[] { PlaceDetailInputFields.name, PlaceDetailInputFields.formatted_address, PlaceDetailInputFields.type };

            PlaceDetailResModel placeDetailResModel = await googleAPIContext.Place.PlaceDetail(placeDetailRequest);

            return placeDetailResModel;
        }
    }
}

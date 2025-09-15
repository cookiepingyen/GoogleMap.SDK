using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.FindPlace;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.NearbySearch;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceAutocomplete;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.TextSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Service
{
    public interface IPlaceService
    {
        Task<FindPlaceResModel> FindPlace(FindPlaceRequest findPlaceRequest, string urlInput = null);
        Task<NearbySearchResModel> NearbySearch(NearbySearchRequest nearbySearchRequest, string urlInput = null);
        Task<TextSearchResModel> TextSearch(TextSearchRequest textSearchRequest, string urlInput = null);
        Task<PlaceAutocompleteResModel> PlaceAutocomplete(PlaceAutocompleteRequest placeAutocompleteRequest, string urlInput = null);
        Task<PlaceDetailResModel> PlaceDetail(PlaceDetailRequest placeDetailRequest, string urlInput = null);

    }
}

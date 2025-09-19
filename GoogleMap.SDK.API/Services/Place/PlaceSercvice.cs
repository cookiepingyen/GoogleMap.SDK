using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.FindPlace;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.NearbySearch;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceAutocomplete;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlaceDetail;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlacePhoto;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.TextSearch;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpUtility;
using Microsoft.Extensions.Configuration;

namespace GoogleMap.SDK.API.Services.Place
{

    //FindPlaceResponse response = await apiContext.Place.FindPlace(findRequest);

    public class PlaceSercvice : BaseService, IPlaceService
    {
        public PlaceSercvice(IHttpRequest request, IConfiguration configuration) : base(request, configuration)
        {
        }

        public Task<FindPlaceResModel> FindPlace(FindPlaceRequest findPlaceRequest)
        {
            return base.GetAsync<FindPlaceResModel>(findPlaceRequest);
        }

        public Task<NearbySearchResModel> NearbySearch(NearbySearchRequest nearbySearchRequest)
        {
            return base.GetAsync<NearbySearchResModel>(nearbySearchRequest);
        }

        public Task<TextSearchResModel> TextSearch(TextSearchRequest textSearchRequest)
        {
            return base.GetAsync<TextSearchResModel>(textSearchRequest);

        }

        public Task<PlaceAutocompleteResModel> PlaceAutocomplete(PlaceAutocompleteRequest placeAutocompleteRequest)
        {
            return base.GetAsync<PlaceAutocompleteResModel>(placeAutocompleteRequest);
        }

        public Task<PlaceDetailResModel> PlaceDetail(PlaceDetailRequest placeDetailRequest)
        {
            return base.GetAsync<PlaceDetailResModel>(placeDetailRequest);
        }

        public Task<Byte[]> PlacePhoto(PlacePhotoRequest placePhotoRequest)
        {
            return base.GetAsync<Byte[]>(placePhotoRequest);
        }
    }
}

using GoogleMap.SDK.Contract.GoogleMapAPI;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API
{
    internal class GoogleAPIContext : IGoogleAPIContext
    {
        private IPlaceService _place;
        private IRouteService _route;
        private IStaticMapService _staticMap;
        private IPlaceService _geocoding;
        private IDirectionService _direction;
        public IPlaceService Place => _place;

        public IRouteService Route => _route;

        public IStaticMapService StaticMap => throw new NotImplementedException();

        public IGeocodingService Geocoding => throw new NotImplementedException();

        public IDirectionService Direction => throw new NotImplementedException();


        public GoogleAPIContext(IPlaceService Place, IRouteService Route)
        {
            this._place = Place;
            this._route = Route;
        }
    }
}

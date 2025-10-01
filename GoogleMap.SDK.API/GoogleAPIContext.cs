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
        private IGeocodingService _geocoding;
        private IDirectionService _direction;
        public IPlaceService Place => _place;

        public IRouteService Route => _route;

        public IStaticMapService StaticMap => _staticMap;

        public IGeocodingService Geocoding => _geocoding;

        public IDirectionService Direction => _direction;


        public GoogleAPIContext(IPlaceService place, IRouteService route, IStaticMapService staticMap, IGeocodingService geocoding, IDirectionService directionService)
        {
            this._place = place;
            this._route = route;
            this._staticMap = staticMap;
            this._geocoding = geocoding;
            this._direction = directionService;
        }
    }
}

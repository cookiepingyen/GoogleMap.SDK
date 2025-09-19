using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI
{
    public interface IGoogleAPIContext
    {
        IPlaceService Place { get; }
        IRouteService Route { get; }
        IStaticMapService StaticMap { get; }
        IGeocodingService Geocoding { get; }
        IDirectionService Direction { get; }
    }
}

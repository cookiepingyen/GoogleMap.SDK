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
        IPlaceService Place { get; set; }
        IRouteService Route { get; set; }
        IStaticMapService StaticMap { get; set; }
        IGeocodingService Geocoding { get; set; }
        IDirectionService Direction { get; set; }
    }
}

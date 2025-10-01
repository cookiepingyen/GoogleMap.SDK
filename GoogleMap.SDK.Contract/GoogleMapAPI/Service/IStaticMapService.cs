using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.PlacePhoto;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.StaticMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Service
{
    public interface IStaticMapService
    {
        Task<Byte[]> GetStaticMap(StaticMapRequest staticMapRequest);
    }
}

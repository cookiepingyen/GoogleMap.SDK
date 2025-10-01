using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.FindPlace;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.StaticMap;
using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using HttpUtility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.StaticMap
{
    public class StaticMapService : BaseService, IStaticMapService
    {
        public StaticMapService(IHttpRequest request, IConfiguration configuration) : base(request, configuration)
        {
        }
        public Task<byte[]> GetStaticMap(StaticMapRequest staticMapRequest)
        {
            return base.GetByteArrayAsync(staticMapRequest);
        }
    }

}

using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Service
{
    public interface IRouteService
    {
        Task<RoutesResModel> GetRoutes(RoutesRequest routesRequest);
    }
}

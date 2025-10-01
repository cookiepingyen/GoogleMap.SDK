using GoogleMap.SDK.Contract.GoogleMapAPI.Service;
using HttpUtility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Direction
{
    public class DirectionService : BaseService, IDirectionService
    {
        public DirectionService(IHttpRequest request, IConfiguration configuration) : base(request, configuration)
        {
        }
    }
}

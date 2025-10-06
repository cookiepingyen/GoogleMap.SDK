using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Direction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Service
{
    public interface IDirectionService
    {
        Task<DirectionResModel> GetDirections(DirectionRequest directionRequest, string urlInput = null);
    }
}

using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.StaticMap
{
    public class Markers
    {
        public string label { get; set; }
        public string color { get; set; }

        [Separator(',', hideFieldName = true)]
        public double[] location { get; set; }
    }
}

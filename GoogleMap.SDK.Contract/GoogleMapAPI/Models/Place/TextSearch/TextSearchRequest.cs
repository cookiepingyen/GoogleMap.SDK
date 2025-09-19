using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.Place.TextSearch
{
    public class TextSearchRequest : BaseRequest
    {
        protected override string Endpoint => "place/textsearch/json";

        [Require]
        public string query { get; set; }

        [Require]
        public int radius { get; set; }

        public TextSearchRequest() { }

        public TextSearchRequest(string query, int radius = 50000)
        {
            this.query = query;
            this.radius = radius;
        }
    }
}

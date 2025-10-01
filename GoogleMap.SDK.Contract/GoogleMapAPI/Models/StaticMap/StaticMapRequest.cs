using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models.StaticMap
{
    public class StaticMapRequest : BaseRequest
    {
        protected override string Endpoint => "staticmap";


        //size:300x400
        [Require]
        [Restructure("Size", 'x')]
        public Size size { get; set; }

        [Optional]
        public int zoom { get; set; }

        [Require]
        public string center { get; set; }

        [Restructure("markers", '|', showFieldName: true)]
        public Markers[] markers { get; set; }

        public StaticMapRequest(string center, int room, int width, int height, Markers[] markers = null)
        {
            Size size = new Size();
            size.Width = width;
            size.Height = height;
            this.size = size;
            this.zoom = room;
            this.center = center;
            this.markers = markers;
        }
    }
}

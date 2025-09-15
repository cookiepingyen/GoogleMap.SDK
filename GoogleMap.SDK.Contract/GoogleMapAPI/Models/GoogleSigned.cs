using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models
{
    public class GoogleSigned
    {
        private static string _apiKey;
        public GoogleSigned(string apiKeyInput)
        {
            _apiKey = apiKeyInput;
        }

        public static string API_KEY
        {
            get { return _apiKey; }
        }
    }
}

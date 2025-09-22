using GoogleMap.SDK.API.Utility;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Converts
{
    public class RoutesConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            //判斷轉換器是否適用於某種類型（例如 int）
            bool canconvert = objectType == typeof(Location[]);

            return canconvert;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // 當 JsonString 的欄位名稱是 'overview_polyline' 的時候
            if (reader.Path.EndsWith("encodedPolyline"))
            {
                // 確認當前的欄位是否為 overview_polyline
                string points = reader.Value.ToString();
                Location[] result = GooglePoints.Decode(points).ToArray();

                return result;
            }
            else
            {
                // 如果不是weightScore欄位，則直接返回原始值
                return existingValue;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

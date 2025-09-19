using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GoogleMap.SDK.Contract.GoogleMapAPI.Models.Enums;
using GoogleMap.SDK.Contract.GoogleMapAPI.Attributes;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Models
{
    public abstract class BaseRequest
    {
        public string Language { get; set; } = "zh-TW";

        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        protected virtual string BaseURL { get; } = "https://maps.googleapis.com/maps/api";
        protected abstract string Endpoint { get; }
        public string ToUri(string key = "")
        {
            FormatQueryString(this);
            string queryString = AssembleKeyValuePairs();
            string apiKey = !String.IsNullOrEmpty(key) ? $"&key={key}" : "";
            string finalUrl = $"{BaseURL}/{Endpoint}?{queryString}{apiKey}";
            return finalUrl;
        }


        public object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return null;
            }
            return null;
        }


        public void FormatQueryString(object request, SearchType searchType = SearchType.None)
        {
            var props = request.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in props)
            {
                RequireAttribute requireAttribute = propertyInfo.GetCustomAttribute<RequireAttribute>();
                // requireAttribute 不是 null 再去判斷裡面的值是否是null
                if (requireAttribute != null && propertyInfo.GetValue(request) == null)
                {
                    throw new Exception("請輸入必填欄位");
                }

                OptionalAttribute optionalAttribute = propertyInfo.GetCustomAttribute<OptionalAttribute>();
                if (optionalAttribute != null && propertyInfo.GetValue(request)?.ToString() == GetDefaultValue(propertyInfo.PropertyType)?.ToString())
                {
                    continue;
                }


                if (propertyInfo.PropertyType.IsArray)
                {
                    List<string> items = new List<string>();
                    foreach (var arrayItem in (IEnumerable)propertyInfo.GetValue(request))
                    {
                        items.Add(arrayItem.ToString());
                    }

                    // 如果陣列有掛上Separator, 就使用 Separator.symbol 當作分隔符
                    SeparatorAttribute separatorAttribute = propertyInfo.GetCustomAttribute<SeparatorAttribute>();
                    string separator = (separatorAttribute == null) ? "%2C" : separatorAttribute.symbol.ToString();

                    string value = string.Join(separator, items);
                    string key = propertyInfo.Name;
                    keyValuePairs.Add(key, value);
                }
                else if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string))
                {
                    // 這邊可能為 Restructure or Atleast
                    // 裝飾模式
                    var propertyObject = propertyInfo.GetValue(request);
                    RestructureAttribute restructureAttribute = propertyInfo.GetCustomAttribute<RestructureAttribute>();
                    if (restructureAttribute != null)
                    {
                        FormatQueryString(propertyObject, SearchType.Restructure);
                        keyValuePairs.Where(keyValue => keyValue.Key.Contains("re-"));
                        ReAssignKeyValue(propertyInfo.Name, restructureAttribute.Symbol.ToString());
                    }

                    // atleast 情況
                    AtleastAttribute atleastAttribute = propertyInfo.GetCustomAttribute<AtleastAttribute>();
                    if (atleastAttribute != null)
                    {
                        FormatQueryString(propertyObject, SearchType.Atleast);
                        int atleastCount = keyValuePairs.Where(keyValue => keyValue.Key.Contains("atleast-")).Count();
                        RollbackKeyValue();
                        if (atleastCount < atleastAttribute.constructureNum)
                        {
                            throw new Exception($"必填參數至少需要 {atleastAttribute.constructureNum} 個");
                        }
                    }
                }
                else
                {
                    if (propertyInfo.GetValue(request) == null)
                    {
                        continue;
                    }
                    string key = propertyInfo.Name.ToLower();

                    string value = "";
                    if (propertyInfo.PropertyType == typeof(bool))
                    {
                        value = propertyInfo.GetValue(request).ToString().ToLower();
                    }
                    else
                    {
                        value = propertyInfo.GetValue(request).ToString();
                    }


                    string encodeValue = System.Web.HttpUtility.UrlEncode(value);

                    switch (searchType)
                    {
                        case SearchType.Restructure:
                            key = $"re-{key}";
                            break;
                        case SearchType.Atleast:
                            key = $"atleast-{key}";
                            break;
                    }
                    keyValuePairs.Add(key, encodeValue);
                }

            }
        }


        public void ReAssignKeyValue(string keyInput, string symbol)
        {
            List<KeyValuePair<string, string>> removeableList = keyValuePairs.Where(keyValue => keyValue.Key.Contains("re-")).ToList();
            List<string> valueList = new List<string>();
            string encodeSymbol = HttpUtility.UrlEncode(symbol);

            foreach (var keyValuePair in removeableList)
            {
                keyValuePairs.Remove(keyValuePair.Key);
                valueList.Add(keyValuePair.Value);
            }

            string qsValue = string.Join(encodeSymbol, valueList);
            string qsKey = keyInput;

            keyValuePairs.Add(qsKey, qsValue);
        }


        public void RollbackKeyValue()
        {
            List<KeyValuePair<string, string>> needRollbackList = keyValuePairs.Where(keyValue => keyValue.Key.Contains("atleast-")).ToList();

            foreach (var keyValuePair in needRollbackList)
            {
                string originKey = keyValuePair.Key.Replace("atleast-", "");
                keyValuePairs.Remove(keyValuePair.Key);
                keyValuePairs.Add(originKey, keyValuePair.Value);
            }

        }

        public string AssembleKeyValuePairs()
        {
            string queryString = "";
            foreach (var dictionary in keyValuePairs)
            {
                queryString += $"{dictionary.Key}={dictionary.Value}&";
            }
            return queryString.TrimEnd('&');
        }
    }
}

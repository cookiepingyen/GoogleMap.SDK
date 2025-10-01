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
                    List<object> items = new List<object>();
                    foreach (var arrayItem in (IEnumerable)propertyInfo.GetValue(request))
                    {
                        items.Add(arrayItem);
                    }

                    RestructureAttribute restructureAttribute = propertyInfo.GetCustomAttribute<RestructureAttribute>();
                    if (restructureAttribute != null)
                    {
                        foreach (object item in items)
                        {
                            //將當前物件丟回去進行遞迴,把每一個property全部都塞到 keyValuePairs 的 Dictionarty裡面
                            FormatQueryString(item, SearchType.Restructure);
                            keyValuePairs.Where(keyValue => keyValue.Key.Contains("re-"));
                            //從keyValuePairs 找出re-相關的Dictionary裡面的item , 重新根據 restructureAttribute 重組成想要的字串
                            //re-width,300 re-height,400
                            //size,300x400
                            ReAssignKeyValue(item.GetType().Name.ToLower(), restructureAttribute.Symbol.ToString(), restructureAttribute.ShowFieldName);

                        }
                    }


                    // 如果陣列有掛上Separator, 就使用 Separator.symbol 當作分隔符
                    SeparatorAttribute separatorAttribute = propertyInfo.GetCustomAttribute<SeparatorAttribute>();
                    if (separatorAttribute != null)
                    {
                        string separator = (separatorAttribute == null) ? "%2C" : separatorAttribute.symbol.ToString();

                        string value = string.Join(separator, items);
                        string key = separatorAttribute.hideFieldName ? $"re_{propertyInfo.DeclaringType.Name.ToLower()}-" : propertyInfo.Name;
                        keyValuePairs.Add(key, value);
                    }

                }
                else if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string))
                {
                    // 這邊可能為 Restructure or Atleast
                    // 裝飾模式
                    var temp = propertyInfo.GetValue(request);
                    RestructureAttribute restructureAttribute = propertyInfo.GetCustomAttribute<RestructureAttribute>();
                    if (restructureAttribute != null)
                    {
                        FormatQueryString(temp, SearchType.Restructure);
                        keyValuePairs.Where(keyValue => keyValue.Key.Contains("re-"));
                        string name = !String.IsNullOrEmpty(restructureAttribute.RestructureName) ? propertyInfo.Name : propertyInfo.DeclaringType.Name;

                        ReAssignKeyValue(name, restructureAttribute.Symbol.ToString(), restructureAttribute.ShowFieldName);
                    }

                    // atleast 情況
                    AtleastAttribute atleastAttribute = propertyInfo.GetCustomAttribute<AtleastAttribute>();
                    if (atleastAttribute != null)
                    {
                        FormatQueryString(temp, SearchType.Atleast);
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
                            key = $"re_{propertyInfo.DeclaringType.Name.ToLower()}-{key}";
                            break;
                        case SearchType.Atleast:
                            key = $"atleast-{key}";
                            break;
                    }
                    keyValuePairs.Add(key, encodeValue); //re-width,300 re-height,400
                }

            }
        }


        public void ReAssignKeyValue(string keyInput, string symbol, bool retainFieldName = false)
        {
            List<KeyValuePair<string, string>> removeableList = keyValuePairs.Where(keyValue => keyValue.Key.Contains($"re_{keyInput}-")).ToList();
            List<string> valueList = new List<string>();
            string encodeSymbol = System.Web.HttpUtility.UrlEncode(symbol);

            foreach (var keyValuePair in removeableList)
            {
                keyValuePairs.Remove(keyValuePair.Key);
                string prefix = "";
                if (retainFieldName)
                {
                    string key = keyValuePair.Key.Replace($"re_{keyInput}-", "");
                    prefix = !string.IsNullOrEmpty(key) ? $"{key}:" : key;
                }
                valueList.Add(prefix + keyValuePair.Value);
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

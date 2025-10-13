using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.Components.AutoComplete
{
    public class ComboBoxData
    {
        public string Key { get; set; }
        public string Value { get; set; }


        public ComboBoxData(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Attributes
{
    public class SeparatorAttribute : Attribute
    {
        public char symbol { get; set; }
        public bool hideFieldName { get; set; }

        public SeparatorAttribute(char symbol, bool hideFieldName = false)
        {
            this.symbol = symbol;
            this.hideFieldName = hideFieldName;
        }
    }
}

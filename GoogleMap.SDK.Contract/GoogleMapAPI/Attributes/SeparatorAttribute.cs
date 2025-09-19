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

        public SeparatorAttribute(char symbol)
        {
            this.symbol = symbol;
        }
    }
}

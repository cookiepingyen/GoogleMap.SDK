using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Attributes
{
    public class RestructureAttribute : Attribute
    {
        public String RestructureName { get; set; }
        public char Symbol { get; set; }
        public RestructureAttribute(string field, char symbol)
        {
            this.RestructureName = field;
            this.Symbol = symbol;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMapAPI.Attributes
{
    public class AtleastAttribute : Attribute
    {
        public int constructureNum { get; set; }

        public AtleastAttribute(int constructureNum)
        {
            this.constructureNum = constructureNum;
        }
    }
}

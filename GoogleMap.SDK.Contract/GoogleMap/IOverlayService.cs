using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.GoogleMap
{
    public interface IOverlayService
    {
        IOverlay GetLayer(string name);

        void RemoveOverlay(string name);
    }
}

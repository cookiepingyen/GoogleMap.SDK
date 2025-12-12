using GoogleMap.SDK.Contract.GoogleMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Core.Service
{
    public class OverlayService : IOverlayService
    {
        public Dictionary<string, IOverlay> overlayDic = new Dictionary<string, IOverlay>();

        IOverlay overlay;

        public OverlayService(IOverlay overlay)
        {
            this.overlay = overlay;
        }

        public IOverlay GetLayer(string name)
        {
            if (overlayDic.ContainsKey(name))
            {
                overlay = overlayDic[name];
            }
            else
            {
                overlay.Name = name;
                overlayDic.Add(name, overlay);
            }
            return overlay;
        }

        public void RemoveOverlay(string name)
        {
            if (overlayDic.ContainsKey(name))
            {
                overlayDic.Remove(name);
            }
        }
    }
}

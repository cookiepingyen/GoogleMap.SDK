using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace GoogleMap.SDK.UI.WPF.Test
{
    public class MyGMapRoute : GMapRoute
    {
        public Brush Stroke = Brushes.Navy;

        public delegate void GMapRouteEvent(MyGMapRoute route);

        public event GMapRouteEvent Route_Click;
        public MyGMapRoute(IEnumerable<PointLatLng> points) : base(points)
        {
        }

        public override Path CreatePath(List<Point> localPath, bool addBlurEffect)
        {
            Path path = base.CreatePath(localPath, addBlurEffect);
            path.Stroke = Stroke;
            path.IsHitTestVisible = true;

            path.MouseRightButtonDown += Path_MouseRightButtonDown1;
            return path;
        }

        private void Path_MouseRightButtonDown1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Route_Click?.Invoke(this);
        }
    }
}

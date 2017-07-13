using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace MapCompare
{
    class MapZoomInTool : MapTool
    {

        public override void OnMapControlMouseDown(IActiveView activeView, int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseDown(activeView, button, shift, x, y, mapX, mapY);

            if (activeView != null && button == 1)
            {
                MapNavigation.ZoomIn(activeView, mapX, mapY);
            }
        }
    }

    class MapZoomOutTool : MapTool
    {
        public override void OnMapControlMouseDown(IActiveView activeView, int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseDown(activeView, button, shift, x, y, mapX, mapY);

            if (activeView != null && button == 1)
            {
                MapNavigation.ZoomOut(activeView, mapX, mapY);
            }
        }
    }

    class MapPanTool : MapTool
    {
        public override void OnMapControlMouseDown(IActiveView activeView, int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseDown(activeView, button, shift, x, y, mapX, mapY);

            if (activeView != null && button == 1)
            {
                MapNavigation.Pan(activeView);
            }
        }
    }
}

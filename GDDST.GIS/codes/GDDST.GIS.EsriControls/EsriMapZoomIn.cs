using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using ESRI.ArcGIS.Controls;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    [Export(typeof(IDsTool))]
    public class EsriMapZoomIn : DsBaseTool
    {
        private IMapControlDefault m_mapCtrl = null;
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "地图放大";
            base.Category = "视图操作";
            base.Message = "当前工具：地图放大";
            base.Tooltip = "地图放大";
            base.Name = "EsriMapZoomIn";
            base.Checked = false;
            base.Deactivate = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "EsriMapZoomIn_16.ico";
            base.m_bitmapNameLarge = "EsriMapZoomIn_32.png";

            base.LoadSmallBitmap();
            base.LoadLargeBitmap();

            if (hook.MapControl is AxMapControl)
            {
                m_mapCtrl = (hook.MapControl as AxMapControl).Object as IMapControlDefault;
            }
        }

        public override void OnActivate()
        {
            base.OnActivate();
            if (m_mapCtrl != null)
            {
                m_mapCtrl.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
            }
        }

        public override void OnMapControlMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseDown(button, shift, x, y, mapX, mapY);

            if (m_mapCtrl != null && button == 1)
            {
                GDDST.GIS.EsriUtils.ViewAgent.ZoomIn(m_mapCtrl.ActiveView, mapX, mapY);
            }
        }
    }
}

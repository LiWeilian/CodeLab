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
    public class EsriMapZoomOut : DsBaseTool
    {
        private IMapControlDefault m_mapCtrl = null;
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "地图缩小";
            base.Category = "视图操作";
            base.Message = "当前工具：地图缩小";
            base.Tooltip = "地图缩小";
            base.Name = "EsriMapZoomOut";
            base.Checked = false;
            base.Deactivate = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "EsriMapZoomOut_16.ico";
            base.m_bitmapNameLarge = "EsriMapZoomOut_32.png";

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
                m_mapCtrl.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
            }
        }

        public override void OnMapControlMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseDown(button, shift, x, y, mapX, mapY);

            if (m_mapCtrl != null && button == 1)
            {
                GDDST.GIS.EsriUtils.ViewAgent.ZoomOut(m_mapCtrl.ActiveView, mapX, mapY);
            }
        }
    }
}

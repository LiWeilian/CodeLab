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
    public class EsriMapPan : DsBaseTool
    {
        private IMapControlDefault m_mapCtrl = null;
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "地图平移";
            base.Category = "视图操作";
            base.Message = "当前工具：地图平移";
            base.Tooltip = "地图平移";
            base.Name = "EsriMapPan";
            base.Checked = false;
            base.Deactivate = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "EsriMapPan_16.ico";
            base.m_bitmapNameLarge = "EsriMapPan_32.png";

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
                m_mapCtrl.MousePointer = esriControlsMousePointer.esriPointerPan;
            }
        }

        public override void OnMapControlMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseDown(button, shift, x, y, mapX, mapY);

            if (m_mapCtrl != null && button == 1)
            {
                esriControlsMousePointer tempPointer = m_mapCtrl.MousePointer;
                m_mapCtrl.MousePointer = esriControlsMousePointer.esriPointerPanning;
                GDDST.GIS.EsriUtils.ViewAgent.Pan(m_mapCtrl.ActiveView);
                m_mapCtrl.MousePointer = tempPointer;
            }
        }

        public override void OnMapControlMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseUp(button, shift, x, y, mapX, mapY);

            if (m_mapCtrl != null)
            {
                m_mapCtrl.MousePointer = esriControlsMousePointer.esriPointerPan;
            }
        }
    }
}

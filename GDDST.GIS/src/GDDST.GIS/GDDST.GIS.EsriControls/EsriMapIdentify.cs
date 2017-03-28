using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    [Export(typeof(IDsTool))]
    public class EsriMapIdentify : DsBaseTool
    {
        private IMapControlDefault m_mapCtrl = null;
        private EsriMapIdentifyResults m_identifyResult = null;
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "信息查看";
            base.Category = "地图查询";
            base.Message = "当前工具：信息查看";
            base.Tooltip = "信息查看";
            base.Name = "EsriMapIdentify";
            base.Checked = false;
            base.Deactivate = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "EsriMapIdentify_16.png";
            base.m_bitmapNameLarge = "EsriMapIdentify_32.png";

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
                m_mapCtrl.MousePointer = esriControlsMousePointer.esriPointerIdentify;
            }
        }

        public override void OnMapControlMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            base.OnMapControlMouseDown(button, shift, x, y, mapX, mapY);

            if (m_mapCtrl != null && button == 1)
            {
                if (m_identifyResult == null)
                {
                    m_identifyResult = new EsriMapIdentifyResults(m_mapCtrl);
                }
                m_app.AddToInfoPanel(m_identifyResult, "信息查看", true, true, true, true);
            }
        }

        public override void OnMapItemAdded(object Item)
        {
            base.OnMapItemAdded(Item);

            if (m_identifyResult != null && Item is ILayer)
            {
                m_identifyResult.MapAddLayer(Item as ILayer);
            }
        }

        public override void OnMapItemDeleted(object Item)
        {
            base.OnMapItemDeleted(Item);

            if (m_identifyResult != null && Item is ILayer)
            {
                m_identifyResult.MapDeleteLayer(Item as ILayer);
            }
        }
    }
}

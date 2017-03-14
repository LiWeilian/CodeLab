using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using ESRI.ArcGIS.Controls;

using GDDST.GIS.PluginEngine;
using GDDST.GIS.EsriUtils;

namespace GDDST.GIS.EsriControls
{
    [Export(typeof(IDsCommand))]
    public class EsriMapNextView : DsBaseCommand
    {
        private IMapControlDefault m_mapCtrl = null;
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "后一视图";
            base.Category = "视图操作";
            base.Message = "当前工具：后一视图";
            base.Tooltip = "后一视图";
            base.Name = "EsriMapNextView";
            base.Checked = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "EsriMapNextView_16.png";
            base.m_bitmapNameLarge = "EsriMapNextView_32.png";

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

            if (m_mapCtrl != null && ViewAgent.HasNext(m_mapCtrl.ActiveView))
            {
                ViewAgent.Next(m_mapCtrl.ActiveView);
            }
        }
    }
}

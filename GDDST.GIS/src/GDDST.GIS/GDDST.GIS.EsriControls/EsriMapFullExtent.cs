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
    public class EsriMapFullExtent : DsBaseCommand
    {
        private IMapControlDefault m_mapCtrl = null;
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "全图显示";
            base.Category = "视图操作";
            base.Message = "当前工具：全图显示";
            base.Tooltip = "全图显示";
            base.Name = "EsriMapFullExtent";
            base.Checked = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "EsriMapFullExtent_16.png";
            base.m_bitmapNameLarge = "EsriMapFullExtent_32.png";

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
                ViewAgent.ZoomAll(m_mapCtrl.ActiveView);
            }
        }
    }
}

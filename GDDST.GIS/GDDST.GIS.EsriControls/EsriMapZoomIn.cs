using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    [Export(typeof(IDsTool))]
    public class EsriMapZoomIn : DsBaseTool
    {

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
        }

        public override void OnActivate()
        {
            base.OnActivate();

        }
    }
}

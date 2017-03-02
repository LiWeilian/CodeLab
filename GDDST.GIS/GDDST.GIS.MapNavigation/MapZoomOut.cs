using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.MapNavigation
{
    [Export(typeof(IDsTool))]
    public class MapZoomOut : DsBaseTool
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "地图缩小";
            base.Category = "视图操作";
            base.Message = "当前工具：地图缩小";
            base.Tooltip = "地图缩小";
            base.Name = "MapZoomOut";
            base.Checked = false;
            base.Deactivate = false;
            base.Enabled = true;
            base.m_bitmapName = "ZoomOut.ico";

            base.LoadBitmap();
        }
    }
}

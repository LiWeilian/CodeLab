using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.MapNavigation
{
    [Export(typeof(IDsCommand))]
    public class MapFullExtent : DsBaseCommand
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "全图";
            base.Category = "视图操作";
            base.Message = "";
            base.Tooltip = "全图";
            base.Name = "MapFullExtent";
            base.Checked = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "MapFullExtent_16.ico";
            base.m_bitmapNameLarge = "MapFullExtent_32.ico";

            base.LoadSmallBitmap();
            base.LoadLargeBitmap();
        }
    }
}

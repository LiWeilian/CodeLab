using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.MapQuery
{
    [Export(typeof(IDsTool))]
    public class Identify : DsBaseTool
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "信息查看";
            base.Category = "地图查询";
            base.Message = "当前工具：信息查看";
            base.Tooltip = "信息查看";
            base.Name = "Identify";
            base.Checked = false;
            base.Deactivate = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "Identify_16.ico";

            base.LoadLargeBitmap();
            base.LoadSmallBitmap();
        }
    }
}

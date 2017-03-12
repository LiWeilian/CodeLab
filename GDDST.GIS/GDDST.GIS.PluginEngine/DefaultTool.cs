using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace GDDST.GIS.PluginEngine
{
    [Export(typeof(IDsTool))]
    public class DefaultTool : DsBaseTool
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "无操作";
            base.Category = "视图操作";
            base.Message = "当前工具：无";
            base.Tooltip = "";
            base.Name = "DefaultTool";
            base.Checked = false;
            base.Deactivate = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "DefaultTool_16.ico";

            base.LoadSmallBitmap();
        }
    }
}

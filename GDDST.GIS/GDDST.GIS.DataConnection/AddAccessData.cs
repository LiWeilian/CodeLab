using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.DataConnection
{
    [Export(typeof(IDsCommand))]
    public class AddAccessData : DsBaseCommand
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "加载Access数据";
            base.Category = "加载数据";
            base.Message = "";
            base.Tooltip = "加载Access数据";
            base.Name = "AddAccessData";
            base.Checked = false;
            base.Enabled = true;
            base.m_bitmapName = "AddAccessData.ico";

            base.LoadBitmap();
        }
    }
}

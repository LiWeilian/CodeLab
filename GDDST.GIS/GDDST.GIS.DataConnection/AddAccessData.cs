using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            base.m_bitmapNameSmall = "AddAccessData_16.ico";

            base.LoadSmallBitmap();
        }

        public override void OnActivate()
        {
            base.OnActivate();

            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "个人地理数据库(*.mdb)|*.mdb";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriDataConnection
{
    [Export(typeof(IDsCommand))]
    public class AddSDEData : DsBaseCommand
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "加载SDE数据";
            base.Category = "加载数据";
            base.Message = "";
            base.Tooltip = "加载SDE数据";
            base.Name = "AddSDEData";
            base.Checked = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "AddSDEData_16.png";
            base.m_bitmapNameLarge = "AddSDEData_32.png";

            base.LoadSmallBitmap();
            base.LoadLargeBitmap();
        }

        public override void OnActivate()
        {
            base.OnActivate();

            MessageBox.Show("未实现");
        }
    }
}

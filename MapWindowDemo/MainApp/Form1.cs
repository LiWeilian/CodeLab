using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AxMapWinGIS;
using MapWinGIS;

namespace MainApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axMap1.Clear();
            Application.DoEvents();
            MapWinGIS.Shapefile shpFile = new MapWinGIS.ShapefileClass();
            shpFile.Open(@"E:\work_documents\工作文档-项目资料\广州自来水二期\实施\2015\20150612_yaocedian\yi\GEOGROUND.resrgn_fh_1.shp", null);
            axMap1.AddLayer(shpFile, true);
            Application.DoEvents();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmZoomIn;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmZoomOut;
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmPan;
        }

        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            axMap1.ZoomToMaxExtents();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;

namespace MapCompare
{
    public partial class FormMapCompare : Form
    {
        private MapTool m_mapTool = new DefaulMapTool();
        public FormMapCompare()
        {
            InitializeComponent();
            this.tocCtrlLeft.SetBuddyControl(this.mapCtrlLeft);
            this.tocCtrlRight.SetBuddyControl(this.mapCtrlRight);
            this.mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerArrow;
            this.mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }

        private void tsbSelectElement_Click(object sender, EventArgs e)
        {

            this.m_mapTool = new DefaulMapTool();
            this.mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerArrow;
            this.mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerArrow;
        }

        private void tsbZoomIn_Click(object sender, EventArgs e)
        {
            this.m_mapTool = new MapZoomInTool();
            this.mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
            this.mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
        }

        private void tsbZoomOut_Click(object sender, EventArgs e)
        {
            this.m_mapTool = new MapZoomOutTool();
            this.mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
            this.mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
        }

        private void tspPriorView_Click(object sender, EventArgs e)
        {

        }

        private void tsbNextView_Click(object sender, EventArgs e)
        {

        }

        private void tsbPan_Click(object sender, EventArgs e)
        {
            this.m_mapTool = new MapPanTool();
            this.mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerPan;
            this.mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerPan;
        }

        private void tsbFullExtent_Click(object sender, EventArgs e)
        {

        }

        private void tsbRefreshView_Click(object sender, EventArgs e)
        {

        }

        private void mapCtrlLeft_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {

        }

        private void mapCtrlRight_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {

        }

        private void FormMapCompare_Resize(object sender, EventArgs e)
        {
            int mapPnlWidth = this.Width - pnlLayers.Width;
            pnlMapLeft.Width = (int)System.Math.Round((decimal)(mapPnlWidth/2));
        }
    }
}

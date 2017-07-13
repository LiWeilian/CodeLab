using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace MapCompare
{
    public partial class FormMapCompare : Form
    {
        private MapTool m_mapTool = new DefaulMapTool();
        private AxMapControl m_activeMapCtrl = null;
        public FormMapCompare()
        {
            InitializeComponent();
            this.tocCtrlLeft.SetBuddyControl(this.mapCtrlLeft);
            this.tocCtrlRight.SetBuddyControl(this.mapCtrlRight);
            this.mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerArrow;
            this.mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerArrow;


            FormMapCompare_Resize(this, null);

            this.m_activeMapCtrl = mapCtrlLeft;
            tsslMapScale.Text = string.Format("1:{0}", m_activeMapCtrl.MapScale.ToString("0.00"));
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
            MapNavigation.Prior(this.m_activeMapCtrl.ActiveView);
        }

        private void tsbNextView_Click(object sender, EventArgs e)
        {
            MapNavigation.Next(this.m_activeMapCtrl.ActiveView);
        }

        private void tsbPan_Click(object sender, EventArgs e)
        {
            this.m_mapTool = new MapPanTool();
            this.mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerPan;
            this.mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerPan;
        }

        private void tsbFullExtent_Click(object sender, EventArgs e)
        {
            MapNavigation.ZoomAll(this.m_activeMapCtrl.ActiveView);
        }

        private void tsbRefreshView_Click(object sender, EventArgs e)
        {
            mapCtrlLeft.ActiveView.Refresh();
            mapCtrlRight.ActiveView.Refresh();
        }

        private void mapCtrlLeft_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 4)
            {
                esriControlsMousePointer tempPointer = mapCtrlLeft.MousePointer;
                mapCtrlLeft.MousePointer = esriControlsMousePointer.esriPointerPagePanning;
                MapNavigation.Pan(mapCtrlLeft.ActiveView);
                mapCtrlLeft.MousePointer = tempPointer;
                return;
            }

            m_activeMapCtrl = mapCtrlLeft;

            if (this.m_mapTool != null)
            {
                this.m_mapTool.OnMapControlMouseDown(mapCtrlLeft.ActiveView, e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            }
        }

        private void mapCtrlRight_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 4)
            {
                esriControlsMousePointer tempPointer = mapCtrlRight.MousePointer;
                mapCtrlRight.MousePointer = esriControlsMousePointer.esriPointerPagePanning;
                MapNavigation.Pan(mapCtrlRight.ActiveView);
                mapCtrlRight.MousePointer = tempPointer;
                return;
            }

            if (this.m_mapTool != null)
            {
                this.m_mapTool.OnMapControlMouseDown(mapCtrlRight.ActiveView, e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            }
        }

        private void FormMapCompare_Resize(object sender, EventArgs e)
        {
            int mapPnlWidth = this.Width - pnlLayers.Width - 10;
            pnlMapLeft.Width = (int)System.Math.Round((decimal)(mapPnlWidth/2));
        }

        private void mapCtrlLeft_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            mapCtrlLeft.Focus();
            m_activeMapCtrl = mapCtrlLeft;
            tsslCurrCoords.Text = string.Format("{0}, {1}", e.mapX.ToString("0.000000"), e.mapY.ToString("0.000000"));
        }

        private void mapCtrlRight_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            mapCtrlRight.Focus();
            m_activeMapCtrl = mapCtrlRight;
            tsslCurrCoords.Text = string.Format("{0}, {1}", e.mapX.ToString("0.000000"), e.mapY.ToString("0.000000"));
        }

        private void mapCtrlLeft_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            if (m_activeMapCtrl == mapCtrlLeft)
            {
                mapCtrlRight.Map.MapScale = mapCtrlLeft.Map.MapScale;
                double minx, miny, maxx, maxy;
                mapCtrlLeft.Extent.QueryCoords(out minx, out miny, out maxx, out maxy);
                IPoint point = new PointClass();
                point.PutCoords((minx + maxx)/2.0, (miny + maxy)/2.0);
                point.SpatialReference = mapCtrlLeft.SpatialReference;
                mapCtrlRight.CenterAt(point);
                mapCtrlRight.ActiveView.Refresh();
                tsslMapScale.Text = string.Format("1:{0}", m_activeMapCtrl.MapScale.ToString("0.00"));
            }
        }

        private void mapCtrlRight_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            if (m_activeMapCtrl == mapCtrlRight)
            {
                mapCtrlLeft.Map.MapScale = mapCtrlRight.Map.MapScale;
                double minx, miny, maxx, maxy;
                mapCtrlRight.Extent.QueryCoords(out minx, out miny, out maxx, out maxy);
                IPoint point = new PointClass();
                point.PutCoords((minx + maxx) / 2.0, (miny + maxy) / 2.0);
                point.SpatialReference = mapCtrlRight.SpatialReference;
                mapCtrlLeft.CenterAt(point);
                mapCtrlLeft.ActiveView.Refresh();
                tsslMapScale.Text = string.Format("1:{0}", m_activeMapCtrl.MapScale.ToString("0.00"));
            }
        }

        private string GetRasterFilePath()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter =
                     "Tagged Image File Format (*.tif, *.tiff)|*.tif;*.TIF;*.tiff;*.TIFF|" + 
                     "JPEG (*.jpg, *.jpeg)|*.jpg;*.JPG;*.jpeg;*.JPEG|" +
                     "Imagine Image (*.img)|*.img;*.IMG|" +
                     "Bitmap (*.bmp)|*.bmp;*.BMP|" +
                     "Portable Network Graphics (*.png)|*.png;*.PNG|" +
                     "Graphics Interchange Format(*.gif)|*.gif;*.GIF|" +
                     "Arc/Info & Space Imaging BIL (*.bil)|*.bil;*.BIL|" +
                     "Arc/Info & Space Imaging BIP (*.bip)|*.bip;*.BIP|" +
                     "Arc/Info & Space Imaging BSQ (*.bsq)|*.bsq;*.BSQ|" +
                     "DTED Level 0-2 (.dted)|*.dted;*.DTED|" +
                     "ERDAS 7.5 LAN (*.lan)|*.lan;*.LAN|" +
                     "ERDAS 7.5 GIS (*.gis)|*.gis;*.GIS|" +
                     "JP2 (*.jp2)|*.jp2;*.JP2|" +
                     "MrSID (*.sid)|*.sid;*.SID|" +
                     "RAW (*.raw)|*.raw;*.RAW|" +
                     "NTIF (*.ntf)|*.ntf;*.NTF|" +
                     "USGS Ascii DEM (*.dem)|*.dem;*.DEM|" +
                     "X11 Pixmap (*.xpm)|*.xpm;*.XPM|" +
                     "PC Raster (*.map)|*.map;*.MAP|" +
                     "PCI Geomatics Database File (*.pix)|*.pix;*.PIX|" +
                     "JPC (*.jpc)|*.jpc;*.JPC|" +
                     "J2C (*.j2c)|*.j2c;*.J2C|" +
                     "J2K (*.j2k)|*.j2k;*.J2K|" +
                     "HDF (*.hdf)|*.hdf;*.HDF|" +
                     "BSB (*.kap)|*.kap;*.KAP|" +
                     "ER Mapper ECW (*.ecw)|*.ecw;*.ECW";
            ofd.DefaultExt = "*.tif";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            } else
            {
                return null;
            }
        }

        private void tsbOpenRasterLeft_Click(object sender, EventArgs e)
        {
            string rasterFilePath = GetRasterFilePath();
            if (System.IO.File.Exists(rasterFilePath))
            {
                try
                {
                    ILayer tempRasterLayer = DataSource.CreateRasterLayer(rasterFilePath, true, null);
                    if (tempRasterLayer != null)
                    {
                        mapCtrlLeft.AddLayer(tempRasterLayer); ;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("打开影像图失败：{0}", ex.Message));
                }
            }
        }

        private void tsbOpenRasterRight_Click(object sender, EventArgs e)
        {
            string rasterFilePath = GetRasterFilePath();
            if (System.IO.File.Exists(rasterFilePath))
            {
                try
                {
                    ILayer tempRasterLayer = DataSource.CreateRasterLayer(rasterFilePath, true, null);
                    if (tempRasterLayer != null)
                    {
                        mapCtrlRight.AddLayer(tempRasterLayer); ;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("打开影像图失败：{0}", ex.Message));
                }
            }
        }
    }
}

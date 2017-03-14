using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Interop;

using ESRI.ArcGIS.Controls;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    /// <summary>
    /// esriMapControl.xaml 的交互逻辑
    /// </summary>
    public partial class esriMapControl : UserControl
    {

        #region 指针操作
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        #endregion
        public AxMapControl MapControl { get { return this.mapCtrl; } }

        private IDsApplication m_app = null;
        public esriMapControl(IDsApplication hook)
        {
            InitializeComponent();
            m_app = hook;
            m_app.MapControl = mapCtrl;

            InitializeMapControlEvents();
            InitializeNavBar();
        }

        private BitmapSource CreateBitmapImageSource(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return null;
            }
            else
            {
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = bitmap.GetHbitmap();
                    return Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                    {
                        DeleteObject(ptr);
                    }
                }
            }
        }
        private void InitializeNavBar()
        {
            //ZoomIn
            EsriMapZoomIn zoomInTool = new EsriMapZoomIn();
            zoomInTool.OnCreate(m_app);
            imgZoomIn.Source = CreateBitmapImageSource(zoomInTool.LargeBitmap);
            btnZoomIn.Tag = zoomInTool;
            btnZoomIn.Click += delegate (object sender, RoutedEventArgs e)
            {
                zoomInTool.OnActivate();
            };

            //ZoomOut
            EsriMapZoomOut zoomOutTool = new EsriMapZoomOut();
            zoomOutTool.OnCreate(m_app);
            imgZoomOut.Source = CreateBitmapImageSource(zoomOutTool.LargeBitmap);
            btnZoomOut.Tag = zoomOutTool;
            btnZoomOut.Click += delegate (object sender, RoutedEventArgs e)
            {
                zoomOutTool.OnActivate();
            };

            //PriorView
            EsriMapPriorView priorView = new EsriMapPriorView();
            priorView.OnCreate(m_app);
            imgPriorView.Source = CreateBitmapImageSource(priorView.LargeBitmap);
            btnPriorView.Tag = priorView;
            btnPriorView.Click += delegate (object sender, RoutedEventArgs e)
            {
                priorView.OnActivate();
            };


            //NextView
            EsriMapNextView nextView = new EsriMapNextView();
            nextView.OnCreate(m_app);
            imgNextView.Source = CreateBitmapImageSource(nextView.LargeBitmap);
            btnNextView.Tag = priorView;
            btnNextView.Click += delegate (object sender, RoutedEventArgs e)
            {
                nextView.OnActivate();
            };

            //Pan
            EsriMapPan panTool = new EsriMapPan();
            panTool.OnCreate(m_app);
            imgPan.Source = CreateBitmapImageSource(panTool.LargeBitmap);
            btnPan.Tag = panTool;
            btnPan.Click += delegate (object sender, RoutedEventArgs e)
            {
                panTool.OnActivate();
            };

            //FullExtent
            EsriMapFullExtent fullExtCmd = new EsriMapFullExtent();
            fullExtCmd.OnCreate(m_app);
            imgFullExtent.Source = CreateBitmapImageSource(fullExtCmd.LargeBitmap);
            btnFullExtent.Tag = fullExtCmd;
            btnFullExtent.Click += delegate (object sender, RoutedEventArgs e)
            {
                fullExtCmd.OnActivate();
            };
        }

        private void InitializeMapControlEvents()
        {
            #region 鼠标事件
            mapCtrl.OnMouseDown += delegate (object sender, IMapControlEvents2_OnMouseDownEvent e)
            {
                switch (e.button)
                {
                    case 4:
                        esriControlsMousePointer tempPointer = mapCtrl.MousePointer;
                        mapCtrl.MousePointer = esriControlsMousePointer.esriPointerPanning;
                        GDDST.GIS.EsriUtils.ViewAgent.Pan(mapCtrl.ActiveView);
                        mapCtrl.MousePointer = tempPointer;
                        break;
                    default:
                        m_app.CurrentTool.OnMapControlMouseDown(e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
                        break;
                }
            };

            mapCtrl.OnMouseMove += delegate (object sender, IMapControlEvents2_OnMouseMoveEvent e)
            {
                m_app.CurrentTool.OnMapControlMouseMove(e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            };

            mapCtrl.OnMouseUp += delegate (object sender, IMapControlEvents2_OnMouseUpEvent e)
            {
                m_app.CurrentTool.OnMapControlMouseUp(e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            };
            #endregion
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            //隐藏工具栏右端小箭头
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }
    }
}

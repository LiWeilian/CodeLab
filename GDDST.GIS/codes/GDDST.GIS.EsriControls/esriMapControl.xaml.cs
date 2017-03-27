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
using ESRI.ArcGIS.Carto;

using GDDST.GIS.PluginEngine;
using GDDST.GIS.dsSystem;

namespace GDDST.GIS.EsriControls
{
    /// <summary>
    /// esriMapControl.xaml 的交互逻辑
    /// </summary>
    public partial class esriMapControl : UserControl
    {
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
                        dsSystem.Win32.DeleteObject(ptr);
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

            //Refresh
            EsriMapRefresh refreshCmd = new EsriMapRefresh();
            refreshCmd.OnCreate(m_app);
            imgRefresh.Source = CreateBitmapImageSource(refreshCmd.LargeBitmap);
            btnRefresh.Tag = refreshCmd;
            btnRefresh.Click += delegate (object sender, RoutedEventArgs e)
            {
                refreshCmd.OnActivate();
            };

            //Identify
            EsriMapIdentify identifyTool = new EsriMapIdentify();
            identifyTool.OnCreate(m_app);
            imgIdentify.Source = CreateBitmapImageSource(identifyTool.LargeBitmap);
            btnIdentify.Tag = refreshCmd;
            btnIdentify.Click += delegate (object sender, RoutedEventArgs e)
            {
                identifyTool.OnActivate();
            };
        }

        private void InitializeMapControlEvents()
        {
            BindingMapMouseEvents();
            BindingMapKeyEvents();
            BindingMapControlEvents();
            BindingMapViewEvents();
        }

        private void BindingMapMouseEvents()
        {
            #region 鼠标事件
            mapCtrl.OnMouseDown += delegate (object sender, IMapControlEvents2_OnMouseDownEvent e)
            {
                if (m_app == null || m_app.CurrentTool == null)
                {
                    return;
                }
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
                if (m_app == null || m_app.CurrentTool == null)
                {
                    return;
                }
                m_app.CurrentTool.OnMapControlMouseMove(e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            };

            mapCtrl.OnMouseUp += delegate (object sender, IMapControlEvents2_OnMouseUpEvent e)
            {
                if (m_app == null || m_app.CurrentTool == null)
                {
                    return;
                }
                m_app.CurrentTool.OnMapControlMouseUp(e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            };

            mapCtrl.OnDoubleClick += delegate (object sender, IMapControlEvents2_OnDoubleClickEvent e)
            {
                if (m_app == null || m_app.CurrentTool == null)
                {
                    return;
                }
                m_app.CurrentTool.OnMapControlDblClick(e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            };
            #endregion

        }

        private void BindingMapKeyEvents()
        {
            #region 按键事件
            mapCtrl.OnKeyDown += delegate (object sender, IMapControlEvents2_OnKeyDownEvent e)
            {
                if (m_app == null || m_app.CurrentTool == null)
                {
                    return;
                }
                m_app.CurrentTool.OnMapControlKeyDown(e.keyCode, e.shift);
            };

            mapCtrl.OnKeyUp += delegate (object sender, IMapControlEvents2_OnKeyUpEvent e)
            {
                if (m_app == null || m_app.CurrentTool == null)
                {
                    return;
                }
                m_app.CurrentTool.OnMapControlKeyUp(e.keyCode, e.shift);
            };
            #endregion

        }

        private void BindingMapControlEvents()
        {
            #region 地图控件事件
            mapCtrl.OnAfterDraw += delegate (object sender, IMapControlEvents2_OnAfterDrawEvent e)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapAfterDraw(e.display, e.viewDrawPhase);
                }
            };

            mapCtrl.OnAfterScreenDraw += delegate (object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapAfterScreenDraw(e.hdc);
                }
            };

            mapCtrl.OnBeforeScreenDraw += delegate (object sender, IMapControlEvents2_OnBeforeScreenDrawEvent e)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapBeforeScreenDraw(e.hdc);
                }
            };

            mapCtrl.OnExtentUpdated += delegate (object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapExtentUpdate(e.displayTransformation, e.sizeChanged, e.newEnvelope);
                }
            };

            mapCtrl.OnFullExtentUpdated += delegate (object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapFullExtentUpdated(e.displayTransformation, e.newEnvelope);
                }
            };

            mapCtrl.OnSelectionChanged += delegate (object sender, EventArgs e)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }

                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapSelectionChanged();
                }
            };

            mapCtrl.OnViewRefreshed += delegate (object sender, IMapControlEvents2_OnViewRefreshedEvent e)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }

                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapViewRefreshed(e.activeView, e.viewDrawPhase, e.layerOrElement, e.envelope);
                }
            };

            mapCtrl.OnMapReplaced += delegate (object sender, IMapControlEvents2_OnMapReplacedEvent e)
            {
                if (m_app != null || m_app.Plugins != null)
                {
                    foreach (IDsPlugin plugin in m_app.Plugins)
                    {
                        plugin.OnMapReplaced(e.newMap);
                    }
                }

                BindingMapViewEvents();

            };
            #endregion
        }

        public void BindingMapViewEvents()
        {
            #region 地图视图事件
            IMap map = mapCtrl.Map;
            IActiveViewEvents_Event viewEvent = (IActiveViewEvents_Event)map;

            viewEvent.ItemAdded += delegate (object Item)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapItemAdded(Item);
                }
            };

            viewEvent.ItemDeleted += delegate (object Item)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapItemDeleted(Item);
                }
            };

            viewEvent.ContentsChanged += delegate ()
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapContentsChanged();
                }
            };

            viewEvent.ViewRefreshed += delegate (IActiveView View, esriViewDrawPhase phase, object Data, ESRI.ArcGIS.Geometry.IEnvelope envelope)
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
                foreach (IDsPlugin plugin in m_app.Plugins)
                {
                    plugin.OnMapViewRefreshed(View, (int)phase, Data, envelope);
                }
            };

            viewEvent.SelectionChanged += delegate ()
            {
                if (m_app == null || m_app.Plugins == null)
                {
                    return;
                }
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

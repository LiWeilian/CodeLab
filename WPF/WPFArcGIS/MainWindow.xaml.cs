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

using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using System.Windows.Interop;
using System.Windows.Forms.Integration;

namespace WPFArcGIS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            GISInitialize();
            InitializeComponent();

            //AxMapControl mapControl = new AxMapControl();
            //windowsFormsHost3.Child = mapControl;

            mapControl.BeginInit();
            mapControl.OnMouseDown += MapControl_OnMouseDown;
            mapControl.OnMouseUp += MapControl_OnMouseUp;
            mapControl.OnMouseMove += MapControl_OnMouseMove;

            IMap map = (mapControl.Object as IMapControlDefault).Map;
            IActiveViewEvents_Event viewEvent = (IActiveViewEvents_Event)map;
            try
            {

                viewEvent.ItemAdded += ViewEvent_ItemAdded;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            mapControl.EndInit();

            //AxTOCControl tocControl = new AxTOCControl();
            //windowsFormsHost2.Child = tocControl;
            tocControl.SetBuddyControl(mapControl.Object);
        }

        private void ViewEvent_ItemAdded(object Item)
        {
            throw new NotImplementedException();
        }

        private void MapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            this.Title = string.Format("{0}, {1}", e.x, e.y);
        }

        private void MapControl_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            MessageBox.Show("Mouse Up");
        }

        private void MapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //MessageBox.Show("Mouse Down");
        }

        public void GISInitialize()
        {
            try
            {
                //Esri产品绑定
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("绑定ESRI产品时发生错误：{0}", ex.Message));
            }

            try
            {
                //初始化许可证，todo:需补充判断
                IAoInitialize aoInit = new AoInitializeClass();
                aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("初始化ESRI许可证时发生错误：{0}", ex.Message));
            }
        }

        public void GISShutdown()
        {
            try
            {
                IAoInitialize aoInit = new AoInitializeClass();
                aoInit.Shutdown();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("关闭ESRI许可证时发生错误：{0}", ex.Message));
            }
        }
    }
}

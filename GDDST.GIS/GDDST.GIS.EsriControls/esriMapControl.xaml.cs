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
            EsriMapZoomIn zoomInTool = new EsriMapZoomIn();
            zoomInTool.OnCreate(m_app);
            //.Source = CreateBitmapImageSource(zoomInTool.LargeBitmap);
            //btnZoomIn.Click += BtnZoomIn_Click;
            //gridZoomIn.MouseDown += GridZoomIn_MouseDown;
        }

        private void GridZoomIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void BtnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}

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
using System.Windows.Shapes;
using System.Windows.Controls.Ribbon;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using MefPluginDemo;

namespace MefHost01
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {

        [ImportMany]
        private IEnumerable<IMefCommand> m_cmds;
        [ImportMany]
        private IEnumerable<IMefTool> m_tools;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public Window1()
        {
            InitializeComponent();
            textBox.Style = this.TryFindResource("RoundCornerTextBoxStyle") as Style;
            InitializeRibbon();
        }

        private void InitializeRibbon()
        {
            Ribbon ribbon = new Ribbon();

            RibbonTab ribbonTab = new RibbonTab();
            ribbonTab.Header = "地图浏览";

            RibbonGroup ribbonGroup = new RibbonGroup();
            ribbonGroup.Header = "三维";
            RibbonButton rbtn = new RibbonButton();
            rbtn.Label = "三维飞行";
            Bitmap bmp = new Bitmap(string.Format("{0}\\images\\3DFlyAlong32.png", System.Windows.Forms.Application.StartupPath));
            IntPtr ptr = bmp.GetHbitmap();
            rbtn.LargeImageSource = Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ribbonGroup.Items.Add(rbtn);
            ribbonTab.Items.Add(ribbonGroup);

            ribbonGroup = new RibbonGroup();
            ribbonGroup.Header = "属性查询";

            ribbonGroup.Width = 80;

            RibbonTextBox rtb = new RibbonTextBox();
            rtb.Style = FindResource("RoundCornerRibbonTextBoxStyle") as Style;
            rtb.Width = 60;
            ribbonGroup.Items.Add(rtb);

            rtb = new RibbonTextBox();
            rtb.Width = 60;
            ribbonGroup.Items.Add(rtb);

            ribbonTab.Items.Add(ribbonGroup);

            ribbon.Items.Add(ribbonTab);


            #region 根据插件创建控件UI

            RibbonTab pluginTab = new RibbonTab();
            pluginTab.Header = "插件测试";

            RibbonGroup pluginGroup = new RibbonGroup();
            pluginGroup.Header = "插件测试";

            RibbonButton pluginBtn = new RibbonButton();
            pluginBtn.Label = "三维飞行";
            Bitmap pluginBmp = new Bitmap(string.Format("{0}\\images\\3DFlyAlong32.png", System.Windows.Forms.Application.StartupPath));
            IntPtr pluginPtr = pluginBmp.GetHbitmap();
            pluginBtn.LargeImageSource = Imaging.CreateBitmapSourceFromHBitmap(pluginPtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            pluginGroup.Items.Add(pluginBtn);

            pluginTab.Items.Add(pluginGroup);
            ribbon.Items.Add(pluginTab);

            #endregion

            mainGrid.Children.Add(ribbon);


            DeleteObject(ptr);
        }
    }
}

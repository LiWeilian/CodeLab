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

namespace MefHost01
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
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
            ribbonTab.Header = "Commands";
            RibbonGroup ribbonGroup = new RibbonGroup();
            ribbonGroup.Header = "Group 1";
            RibbonButton rbtn = new RibbonButton();
            rbtn.Label = "Button01";
            Bitmap bmp = new Bitmap(string.Format("{0}\\images\\3DFlyAlong32.png", System.Windows.Forms.Application.StartupPath));
            IntPtr ptr = bmp.GetHbitmap();
            rbtn.LargeImageSource = Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ribbonGroup.Items.Add(rbtn);
            ribbonTab.Items.Add(ribbonGroup);
            ribbon.Items.Add(ribbonTab);

            mainGrid.Children.Add(ribbon);

            DeleteObject(ptr);
        }
    }
}

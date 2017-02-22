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

namespace MefHost01
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
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
            ribbonGroup.Items.Add(rbtn);
            ribbonTab.Items.Add(ribbonGroup);
            ribbon.Items.Add(ribbonTab);

            mainGrid.Children.Add(ribbon);
        }
    }
}

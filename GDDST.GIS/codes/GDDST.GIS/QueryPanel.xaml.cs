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

namespace GDDST.GIS
{
    /// <summary>
    /// QueryPanel.xaml 的交互逻辑
    /// </summary>
    public partial class QueryPanel : UserControl
    {
        public QueryPanel()
        {
            InitializeComponent();

            dockPnl.MouseDown += DockPnl_MouseDown;
        }

        private void DockPnl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(string.Format("{0}", e.ToString()));
        }
    }
}

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

namespace DS.GIS.MAIN
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DS.GIS.Controls.MapControlAgent MapCtrlAgent = null;
        public MainWindow()
        {
            InitializeComponent();
            MapCtrlAgent = new Controls.MapControlAgent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fhMainMap.Child = MapCtrlAgent.MapControl;
        }
    }
}

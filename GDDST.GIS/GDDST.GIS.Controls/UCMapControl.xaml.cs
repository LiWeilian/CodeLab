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

namespace GDDST.GIS.Controls
{
    /// <summary>
    /// UCMapControl.xaml 的交互逻辑
    /// </summary>
    public partial class UCMapControl : UserControl
    {
        public UCMapControl()
        {
            InitializeComponent();
            
            mapCtrl.OnMouseDown += delegate(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e) {
                MessageBox.Show(e.ToString());
            };
            
        }
        
    }
}

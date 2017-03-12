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

using ESRI.ArcGIS.Controls;

namespace GDDST.GIS.EsriControls
{
    /// <summary>
    /// esriMapControl.xaml 的交互逻辑
    /// </summary>
    public partial class esriMapControl : UserControl
    {
        public AxMapControl MapControl { get { return this.mapCtrl; } }
        public esriMapControl()
        {
            InitializeComponent();
            mapCtrl.OnMouseDown += MapCtrl_OnMouseDown;
        }

        private void MapCtrl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}

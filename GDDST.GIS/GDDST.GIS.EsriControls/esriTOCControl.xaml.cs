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

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    /// <summary>
    /// esriTOCControl.xaml 的交互逻辑
    /// </summary>
    public partial class esriTOCControl : UserControl
    {
        private IDsApplication m_app = null;
        public esriTOCControl(IDsApplication hook)
        {
            InitializeComponent();
            m_app = hook;
        }
    }
}

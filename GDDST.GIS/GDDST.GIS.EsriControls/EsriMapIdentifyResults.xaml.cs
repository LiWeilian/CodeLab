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

namespace GDDST.GIS.EsriControls
{
    /// <summary>
    /// EsriMapIdentifyResults.xaml 的交互逻辑
    /// </summary>
    public partial class EsriMapIdentifyResults : UserControl
    {
        public EsriMapIdentifyResults()
        {
            InitializeComponent();

            this.Tag = "EsriMapIdentifyResults";

            InitializeLayerList();
        }

        private void InitializeLayerList()
        {
            cbLayers.Items.Clear();
            cbLayers.Items.Add("最顶图层");
            cbLayers.Items.Add("可视图层");
            cbLayers.Items.Add("可选图层");
            cbLayers.Items.Add("全部图层");
        }
    }
}

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

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace GDDST.GIS
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class WinSelectDatasets : Window
    {
        private static class NodeCheckState
        {
            public const int State_UnChecked = 0;
            public const int State_Checked = 1;
            public const int State_HalfChecked = 2;
        }

        private IWorkspace m_workspace = null;
        private bool m_isAutoCheck = false;
        private IGeometryBag m_geoBag = null;

        public List<ILayer> SelectedLayers
        {
            get
            {
                m_geoBag = null;
                List<ILayer> selectedLayers = new List<ILayer>();
                
                TreeNode rootNode = tvDatasets.Nodes[0];

                foreach (TreeNode childNode in rootNode.Nodes)
                {
                    ILayer layer = GetLayerByTreeNode(childNode);
                    if (layer != null)
                    {
                        selectedLayers.Add(layer);
                    }
                }

                return selectedLayers;
            }
        }

        public IEnvelope SelectedExtent
        {
            get
            {
                if (m_geoBag != null && !m_geoBag.Envelope.IsEmpty)
                {
                    return m_geoBag.Envelope;
                }
                else
                {
                    return null;
                }
            }
        }
        public WinSelectDatasets()
        {
            InitializeComponent();
        }
    }
}

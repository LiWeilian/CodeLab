using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;

namespace GDDST.GIS.EsriDataConnection
{
    public partial class FormSelectDatasets : Form
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
                } else
                {
                    return null;
                }
            }
        }
        public FormSelectDatasets(IWorkspace workspace)
        {
            InitializeComponent();

            m_workspace = workspace;

            ListDatasets();
        }

        private ILayer GetLayerByTreeNode(TreeNode treeNode)
        {
            ILayer layer = null;
            if (treeNode.Tag != null)
            {
                if (treeNode.Tag is IFeatureClass && treeNode.Checked)
                {
                    try
                    {
                        IFeatureClass featCls = (IFeatureClass)treeNode.Tag;
                        layer = new FeatureLayerClass();
                        (layer as IFeatureLayer).FeatureClass = featCls;
                        layer.Name = featCls.AliasName;

                        if (m_geoBag == null)
                        {
                            m_geoBag = new GeometryBagClass();
                        }

                        (m_geoBag as IGeometryCollection).AddGeometry((featCls as IGeoDataset).Extent);
                    }
                    catch
                    {
                        layer = null;
                    }
                }
                else if (treeNode.Tag is IFeatureDataset)
                {
                    foreach (TreeNode childNode in treeNode.Nodes)
                    {
                        ILayer subLayer = GetLayerByTreeNode(childNode);
                        if (subLayer != null)
                        {
                            try
                            {
                                if (layer == null)
                                {
                                    layer = new GroupLayerClass();
                                    layer.Name = (treeNode.Tag as IFeatureDataset).Name;
                                }

                                (layer as IGroupLayer).Add(subLayer);
                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }

            return layer;
        }

        private void ListDatasets()
        {
            tvDatasets.Nodes.Clear();

            if (m_workspace == null)
            {
                return;
            }
            
            TreeNode rootNode = tvDatasets.Nodes.Add(System.IO.Path.GetFileNameWithoutExtension(m_workspace.PathName));
            rootNode.ImageIndex = 27;
            rootNode.SelectedImageIndex = 27;
            rootNode.StateImageIndex = 0;

            IEnumDataset datasets = m_workspace.Datasets[esriDatasetType.esriDTAny];
            datasets.Reset();
            IDataset dataset = datasets.Next();
            while (dataset != null)
            {
                if (dataset is IFeatureClass)
                {
                    AddFeatureClassToTree(rootNode, dataset as IFeatureClass);
                }
                else if (dataset is IFeatureDataset)
                {
                    AddFeatureDatasetToTree(rootNode, dataset as IFeatureDataset);
                }
                dataset = datasets.Next();
            }

            tvDatasets.ExpandAll();
        }

        private void AddFeatureDatasetToTree(TreeNode parentNode, IFeatureDataset featDataset)
        {
            TreeNode featDSNode = parentNode.Nodes.Add(featDataset.Name);
            featDSNode.Tag = featDataset;
            featDSNode.ImageIndex = 9;
            featDSNode.SelectedImageIndex = 9;
            featDSNode.StateImageIndex = 0;

            IEnumDataset datasets = featDataset.Subsets;
            datasets.Reset();
            IDataset dataset = datasets.Next();
            while (dataset != null)
            {
                if (dataset is IFeatureClass)
                {
                    AddFeatureClassToTree(featDSNode, dataset as IFeatureClass);
                }
                else if (dataset is IFeatureDataset)
                {
                    AddFeatureDatasetToTree(featDSNode, dataset as IFeatureDataset);
                }
                dataset = datasets.Next();
            }
        }

        private void AddFeatureClassToTree(TreeNode parentNode, IFeatureClass featCls)
        {
            esriGeometryType geoType = featCls.ShapeType;
            TreeNode featClsNode = parentNode.Nodes.Add(featCls.AliasName);
            featClsNode.Tag = featCls;
            featClsNode.StateImageIndex = 0;

            switch (geoType)
            {
                case esriGeometryType.esriGeometryPoint:
                    featClsNode.ImageIndex = 6;
                    featClsNode.SelectedImageIndex = 6;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    featClsNode.ImageIndex = 3;
                    featClsNode.SelectedImageIndex = 3;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    featClsNode.ImageIndex = 7;
                    featClsNode.SelectedImageIndex = 7;
                    break;
                default:
                    featClsNode.ImageIndex = 2;
                    featClsNode.SelectedImageIndex = 2;
                    break;
            }

        }

        private void UpdateChildrenCheckStatus(TreeNode treeNode)
        {
            foreach (TreeNode childNode in treeNode.Nodes)
            {
                m_isAutoCheck = true;
                childNode.Checked = treeNode.Checked;
                UpdateChildrenCheckStatus(childNode);
                m_isAutoCheck = false;
            }
        }

        private void UpdateParentCheckStatus(TreeNode treeNode)
        {
            TreeNode parentNode = treeNode.Parent;
            if (parentNode != null)
            {
                bool selectAll = true;
                foreach (TreeNode childNode in parentNode.Nodes)
                {
                    if (!childNode.Checked)
                    {
                        selectAll = false;
                    }
                }


                m_isAutoCheck = true;
                parentNode.Checked = selectAll;
                m_isAutoCheck = false;

                UpdateParentCheckStatus(parentNode);
            }
        }

        private void tvDatasets_MouseDown(object sender, MouseEventArgs e)
        {
            return;
            TreeViewHitTestInfo treeHitTestInfo = tvDatasets.HitTest(e.Location);
            if (treeHitTestInfo.Node != null
                && treeHitTestInfo.Location == TreeViewHitTestLocations.StateImage)
            {
                //处理子节点
                UpdateChildrenCheckStatus(treeHitTestInfo.Node);
                //处理父节点
                UpdateParentCheckStatus(treeHitTestInfo.Node);
            }
        }

        
        private void UpdateTreeNodesCheckStatus()
        {
            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void tvDatasets_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (m_isAutoCheck)
            {
                return;
            }
            switch (e.Node.Checked)
            {
                case true:
                    e.Node.StateImageIndex = 1;
                    break;
                case false:
                    e.Node.StateImageIndex = 0;
                    break;
            }
            //处理子节点
            UpdateChildrenCheckStatus(e.Node);
            //处理父节点
            UpdateParentCheckStatus(e.Node);
            //处理全树状态
            //UpdateTreeNodesCheckStatus();
        }
    }
}

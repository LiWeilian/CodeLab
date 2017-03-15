using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriDataConnection
{
    [Export(typeof(IDsCommand))]
    public class AddAccessData : DsBaseCommand
    {
        private IWorkspace OpenAccessWorkspace(string fileName)
        {
            IWorkspaceFactory2 wsf = new AccessWorkspaceFactoryClass();
            return wsf.OpenFromFile(fileName, 0);
        }

        private void AddLayerToMap(IMap map, ILayer layer)
        {
            (map as IMapLayers2).InsertLayer(layer, true, 0);
        }

        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "加载Access数据";
            base.Category = "加载数据";
            base.Message = "";
            base.Tooltip = "加载Access数据";
            base.Name = "AddAccessData";
            base.Checked = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "AddAccessData_16.ico";
            base.m_bitmapNameLarge = "AddAccessData_32.png";

            base.LoadSmallBitmap();
            base.LoadLargeBitmap();
        }

        public override void OnActivate()
        {
            base.OnActivate();

            IMapControlDefault mapCtrl = null;

            if (m_app.MapControl is AxMapControl)
            {
                mapCtrl = ((m_app.MapControl as AxMapControl).Object as IMapControlDefault);
            } else
            {
                return;
            }

            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "个人地理数据库(*.mdb)|*.mdb";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                System.Windows.Forms.Application.DoEvents();

                IWorkspace ws = OpenAccessWorkspace(openDlg.FileName);
                if (ws != null)
                {
                    IEnumDataset datasets = ws.Datasets[esriDatasetType.esriDTAny];
                    datasets.Reset();
                    IDataset dataset = datasets.Next();

                    while (dataset != null)
                    {
                        if (dataset is IFeatureDataset)
                        {
                            IGroupLayer groupLayer = new GroupLayerClass();
                            groupLayer.Name = dataset.Name;

                            IEnumDataset subsets = dataset.Subsets;
                            if (subsets != null)
                            {
                                subsets.Reset();
                                IDataset subset = subsets.Next();
                                while (subset != null)
                                {
                                    if (subset is IFeatureClass)
                                    {
                                        IFeatureLayer featLayer = new FeatureLayerClass();
                                        featLayer.Name = subset.Name;
                                        featLayer.FeatureClass = subset as IFeatureClass;

                                        groupLayer.Add(featLayer);
                                    }
                                    subset = subsets.Next();
                                }
                            }
                            AddLayerToMap(mapCtrl.Map, groupLayer);
                        }
                        else if (dataset is IFeatureClass)
                        {
                            IFeatureLayer featLayer = new FeatureLayerClass();
                            featLayer.Name = dataset.Name;
                            featLayer.FeatureClass = dataset as IFeatureClass;

                            AddLayerToMap(mapCtrl.Map, featLayer);
                        }
                        dataset = datasets.Next();
                    }
                }
            }
        }
    }
}

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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace GDDST.GIS.EsriControls
{
    /// <summary>
    /// EsriMapIdentifyResults.xaml 的交互逻辑
    /// </summary>
    public partial class EsriMapIdentifyResults : UserControl
    {
        private IMapControlDefault m_mapCtrl = null;
        public EsriMapIdentifyResults(IMapControlDefault mapCtrl)
        {
            InitializeComponent();

            this.Tag = "EsriMapIdentifyResults";

            this.m_mapCtrl = mapCtrl;
            InitializeLayerList();
        }

        public void DoEsriMapIdentify(IGeometry geo)
        {

            switch (cbLayers.SelectedIndex)
            {
                case 0:
                    //最顶图层
                    break;
                case 1:
                    //可视图层
                    break;
                case 2:
                    //可选图层
                    break;
                case 3:
                    //全部图层
                    break;
                default:
                    //当前选项中的图层
                    IdentifyLayerItem layerItem = (IdentifyLayerItem)cbLayers.SelectedItem;
                    DoLayerIdentify(layerItem.Layer, geo);
                    break;
            }
        }

        public void DoLayerIdentify(ILayer layer, IGeometry geo)
        {
            if (layer != null && layer is IIdentify)
            {
                IIdentify id = layer as IIdentify;
                IArray objs = id.Identify(geo);

                if (objs != null)
                {

                }
            }
        }

        private void AddGroupLayerToList(IGroupLayer groupLayer)
        {
            ICompositeLayer comLayer = (ICompositeLayer)groupLayer;
            int layerCount = comLayer.Count;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer layer = comLayer.Layer[i];
                if (layer is IFeatureLayer)
                {
                    AddFeatureLayerToList((IFeatureLayer)layer);
                }
                else if (layer is IGroupLayer)
                {
                    AddGroupLayerToList((IGroupLayer)layer);
                }
            }
        }

        private void AddFeatureLayerToList(IFeatureLayer featLayer)
        {
            IdentifyLayerItem layerItem = new IdentifyLayerItem();
            layerItem.Layer = featLayer;
            layerItem.LayerName = featLayer.Name;
            ((List<IdentifyLayerItem>)cbLayers.ItemsSource).Add(layerItem);
        }

        public void MapAddLayer(ILayer layer)
        {
            if (layer is IFeatureLayer)
            {
                AddFeatureLayerToList((IFeatureLayer)layer);
            }
            else if (layer is IGroupLayer)
            {
                AddGroupLayerToList((IGroupLayer)layer);
            }

        }

        private void RemoveGroupLayerFromList(IGroupLayer groupLayer)
        {
            ICompositeLayer comLayer = (ICompositeLayer)groupLayer;
            int layerCount = comLayer.Count;
            for (int i = 0; i < layerCount; i++)
            {
                ILayer layer = comLayer.Layer[i];
                if (layer is IFeatureLayer)
                {
                    RemoveFeatureLayerFromList((IFeatureLayer)layer);
                }
                else if (layer is IGroupLayer)
                {
                    RemoveGroupLayerFromList((IGroupLayer)layer);
                }
            }
        }
        private void RemoveFeatureLayerFromList(IFeatureLayer featLayer)
        {
            foreach (IdentifyLayerItem layerItem in (List<IdentifyLayerItem>)cbLayers.ItemsSource)
            {
                if (layerItem.Layer.Equals(featLayer))
                {
                    ((List<IdentifyLayerItem>)cbLayers.ItemsSource).Remove(layerItem);
                }
            }
        }
        public void MapDeleteLayer(ILayer layer)
        {
            if (layer is IFeatureLayer)
            {
                RemoveFeatureLayerFromList((IFeatureLayer)layer);
            }
            else if (layer is IGroupLayer)
            {
                RemoveGroupLayerFromList((IGroupLayer)layer);
            }
        }

        private void InitializeLayerList()
        {
            cbLayers.ItemsSource = null;

            cbLayers.SelectedValuePath = "Layer";
            cbLayers.DisplayMemberPath = "LayerName";

            List<IdentifyLayerItem> layerItemList = new List<IdentifyLayerItem>();

            IdentifyLayerItem layerItem;
            layerItem = new IdentifyLayerItem();
            layerItem.LayerName = "最顶图层";
            layerItemList.Add(layerItem);


            layerItem = new IdentifyLayerItem();
            layerItem.LayerName = "可视图层";
            layerItemList.Add(layerItem);


            layerItem = new IdentifyLayerItem();
            layerItem.LayerName = "可选图层";
            layerItemList.Add(layerItem);


            layerItem = new IdentifyLayerItem();
            layerItem.LayerName = "全部图层";
            layerItemList.Add(layerItem);

            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
            IEnumLayer layers = m_mapCtrl.Map.Layers[null, true];
            if (layers != null)
            {
                layers.Reset();
                ILayer layer = layers.Next();
                while (layer != null)
                {
                    layerItem = new IdentifyLayerItem();
                    layerItem.Layer = layer;
                    layerItem.LayerName = layer.Name;
                    layerItemList.Add(layerItem);

                    layer = layers.Next();
                }
            }

            cbLayers.ItemsSource = layerItemList;

            cbLayers.SelectedIndex = 0;
        }
    }

    class IdentifyLayerItem
    {
        public ILayer Layer { get; set; }
        public string LayerName { get; set; }
    }
}

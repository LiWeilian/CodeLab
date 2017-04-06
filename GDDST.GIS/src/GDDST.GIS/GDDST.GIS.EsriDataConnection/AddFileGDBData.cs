using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
    class AddFileGDBData : DsBaseCommand
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.Caption = "加载文件地理数据库";
            base.Category = "加载数据";
            base.Message = "";
            base.Tooltip = "加载文件地理数据库";
            base.Name = "AddFileGDBData";
            base.Checked = false;
            base.Enabled = true;
            base.m_bitmapNameSmall = "AddFileGDBData_16.png";
            base.m_bitmapNameLarge = "AddFileGDBData_32.png";

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
            }
            else
            {
                return;
            }
            
            string gdbPath = GetFileGDBPath();
            if (Directory.Exists(gdbPath))
            {
                IWorkspace ws = OpenFileGDBWorkspace(gdbPath);
                if (ws != null)
                {
                    FormSelectDatasets frmSelectDS = new FormSelectDatasets(ws);
                    if (frmSelectDS.ShowDialog() == DialogResult.OK)
                    {
                        List<ILayer> layers = frmSelectDS.SelectedLayers;
                        foreach (ILayer layer in layers)
                        {
                            AddLayerToMap(mapCtrl.Map, layer);
                        }

                        mapCtrl.ActiveView.Extent = frmSelectDS.SelectedExtent;
                        mapCtrl.ActiveView.Refresh();
                    }
                }
            }
        }

        private string GetFileGDBPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.ShowNewFolderButton = false;
            fbd.SelectedPath = Application.StartupPath;
            fbd.Description = "选择文件地理数据库(*.gdb)";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string tempPath = fbd.SelectedPath;

                if (System.IO.Path.GetExtension(tempPath).ToUpper() == ".GDB")
                {
                    return fbd.SelectedPath;
                }
            }

            return string.Empty;
        }

        private IWorkspace OpenFileGDBWorkspace(string gdbPath)
        {
            try
            {
                IWorkspaceFactory wsf = new FileGDBWorkspaceFactoryClass();
                return wsf.OpenFromFile(gdbPath, 0);
            }
            catch
            {
                return null;
            }
        }

        private void AddLayerToMap(IMap map, ILayer layer)
        {
            (map as IMapLayers2).InsertLayer(layer, true, 0);
        }
    }
}

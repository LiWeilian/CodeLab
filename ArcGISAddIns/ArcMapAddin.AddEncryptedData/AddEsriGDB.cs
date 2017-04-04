using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.ArcMapUI;


namespace ArcMapAddin.AddEncryptedData
{
    public class AddEsriGDB : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public AddEsriGDB()
        {
        }

        protected override void OnClick()
        {
            string errMsg = string.Empty;
            string gdbPath = GetFileGDBPath();
            if (Directory.Exists(gdbPath))
            {
                EncryptGDB.GDB_Crypt_Status gdbStatus = EncryptGDB.CheckFileGDBStatus(gdbPath);

                switch (gdbStatus)
                {
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_AVAILABLE:
                        DisplayMessage("文件地理数据库无效");
                        return;
                    case EncryptGDB.GDB_Crypt_Status.GCS_ENCRYTED:
                        //判断是否过期
                        DateTime encryptedTime = EncryptGDB.GetFileGDBEncryptedTime(gdbPath);
                        if (encryptedTime < DateTime.Now)
                        {
                            DisplayMessage("文件地理数据库已过期");
                            return;
                        }

                        if (EncryptGDB.DecryptFileGDB(gdbPath, out errMsg))
                        {
                            IWorkspace ws = OpenFileGDBWorkspace(gdbPath);

                            Application.DoEvents();
                            //重新加密数据库
                            EncryptGDB.EncryptFileGDB(gdbPath, encryptedTime, out errMsg);

                            if (ws != null)
                            {
                                FormSelectDatasets frmSelectDatasets = new FormSelectDatasets(ws);
                                if (frmSelectDatasets.ShowDialog() == DialogResult.OK)
                                {
                                    List<ILayer> layers = frmSelectDatasets.SelectedLayers;
                                    IEnvelope extent = frmSelectDatasets.SelectedExtent;
                                    AddLayersToMap(layers, extent);
                                }
                            }
                        } else
                        {
                            DisplayMessage(errMsg);
                        }

                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_CRYTED:
                        break;
                }
            }
        }

        protected override void OnUpdate()
        {
        }

        private void DisplayMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void AddLayersToMap(List<ILayer> layers, IEnvelope extent)
        {
            IMxDocument mxDoc = ArcMap.Application.Document as IMxDocument;
            IMap map = mxDoc.FocusMap;
            foreach (ILayer layer in layers)
            {
                map.AddLayer(layer);
            }
            if (extent != null)
            {
                (map as IActiveView).Extent = extent;
                (map as IActiveView).Refresh();
            }
        }

        private string GetFileGDBPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.ShowNewFolderButton = false;
            string gdbPath = Utils.GetDefaultFileGDBPath();
            if (System.IO.Directory.Exists(gdbPath))
            {
                fbd.SelectedPath = gdbPath;
            } else
            {
                fbd.SelectedPath = Application.StartupPath;
            }
            fbd.Description = "选择文件地理数据库(*.gdb)";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string tempPath = fbd.SelectedPath;

                if (System.IO.Path.GetExtension(tempPath).ToUpper() == ".GDB")
                {
                    Utils.SetDefaultFileGDBPath(fbd.SelectedPath);
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
            catch (Exception ex)
            {
                DisplayMessage(string.Format("创建工作空间失败：{0}", ex.Message));
                return null;
            }
            
        }
        
    }
}

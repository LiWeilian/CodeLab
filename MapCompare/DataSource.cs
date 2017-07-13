using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;

namespace MapCompare
{
    class DataSource
    {
        public static ILayer CreateRasterLayer(string filePath, bool visible, IGroupLayer groupLayer)
        {
            string dir = System.IO.Path.GetDirectoryName(filePath);
            string rasterFileName = System.IO.Path.GetFileName(filePath);

            IWorkspaceFactory wsf = new RasterWorkspaceFactoryClass();
            IWorkspace ws = wsf.OpenFromFile(dir, 0);
            IRasterDataset rd = (ws as IRasterWorkspace).OpenRasterDataset(rasterFileName);

            IRasterLayer rl = new RasterLayerClass();
            rl.CreateFromDataset(rd);

            return rl;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;

namespace DS.GIS.Controls
{
    public class MapControlAgent: IMapControlAgent
    {
        private AxMapControl mapCtrl = null;
        public MapControlAgent()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);

            IAoInitialize aoInit = new AoInitializeClass();
            esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB;
            if (aoInit.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
            {
                aoInit.Initialize(productCode);
            }

            mapCtrl = new AxMapControl();
            mapCtrl.OnDoubleClick += MapCtrl_OnDoubleClick;
        }

        private void MapCtrl_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            MessageBox.Show("This is an ArcGIS MapControl.");
        }

        public Control MapControl
        {
            get { return mapCtrl; }
        }
    }
}

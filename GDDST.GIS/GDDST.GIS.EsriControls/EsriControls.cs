using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows.Controls;

using ESRI.ArcGIS.Controls;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    [Export(typeof(IDsGISControls))]
    public class EsriControls : IDsGISControls
    {
        private UserControl m_mapCtrl = null;
        private UserControl m_legendCtrl = null;

        public EsriControls()
        {
            
        }

        public UserControl LegendControl
        {
            get
            {
                return m_legendCtrl;
            }
        }

        public UserControl MapControl
        {
            get
            {
                return m_mapCtrl;
            }
        }

        public void InitializeControls()
        {
            m_mapCtrl = new esriMapControl();
            m_legendCtrl = new esriTOCControl();
            (m_legendCtrl as esriTOCControl).tocCtrl.SetBuddyControl((m_mapCtrl as esriMapControl).mapCtrl);

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

using ESRI.ArcGIS.Controls;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    [Export(typeof(IDsGISControls))]
    public class EsriControls : IDsGISControls
    {
        private IDsApplication m_app = null;
        private UserControl m_mapCtrl = null;
        private UserControl m_legendCtrl = null;

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

        public object MapControlCore
        {
            get
            {
                return (m_mapCtrl as esriMapControl).mapCtrl;
            }
        }

        public object LegendControlCore
        {
            get
            {
                return (m_legendCtrl as esriTOCControl).tocCtrl;
            }
        }

        public void InitializeControls(IDsApplication hook)
        {
            m_app = hook;
            m_mapCtrl = new esriMapControl();
            m_legendCtrl = new esriTOCControl();

            AxMapControl axMapCtrl = (m_mapCtrl as esriMapControl).mapCtrl;
            AxTOCControl axTocCtrl = (m_legendCtrl as esriTOCControl).tocCtrl;

            axTocCtrl.SetBuddyControl(axMapCtrl);

            axMapCtrl.OnMouseDown += AxMapCtrl_OnMouseDown;
        }

        private void AxMapCtrl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (m_app.CurrentTool != null)
            {
                m_app.CurrentTool.OnMapControlMouseDown(e.button, e.shift, e.x, e.y, e.mapX, e.mapY);
            } else
            {
                MessageBox.Show(string.Format("{0}, {1}", e.mapX, e.mapY));
            }
        }
    }
}

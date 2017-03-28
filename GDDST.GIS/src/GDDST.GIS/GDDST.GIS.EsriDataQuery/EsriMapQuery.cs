using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel.Composition;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriDataQuery
{
    [Export(typeof(IDsPanel))]
    public class EsriMapQuery : DsBasePanel
    {
        public override UserControl PluginPanel
        {
            get
            {
                return base.PluginPanel;
            }

            protected set
            {
                base.PluginPanel = value;
            }
        }
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
            base.PluginPanel = new esriMapQueryUC(m_app);
        }
    }
}

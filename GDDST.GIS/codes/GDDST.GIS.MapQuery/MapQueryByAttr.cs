using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using System.ComponentModel.Composition;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.MapQuery
{
    [Export(typeof(IDsPanel))]
    class MapQueryByAttr : DsBasePanel
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

            base.PluginPanel = new UCMapQueryByAttr();

            (base.PluginPanel as UCMapQueryByAttr).cbLayers.Items.Add(DateTime.Now.ToString());
        }

        public override void OnActivate()
        {
            base.OnActivate();
        }
    }
}

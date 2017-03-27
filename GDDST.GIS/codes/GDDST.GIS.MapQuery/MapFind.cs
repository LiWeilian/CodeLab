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
    //[Export(typeof(IDsPanel))]
    public class MapFind : DsBasePanel
    {
        public override void OnCreate(IDsApplication hook)
        {
            base.m_app = hook;
        }

        public override void OnActivate()
        {
            base.OnActivate();
        }
    }
}

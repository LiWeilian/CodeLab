using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.MapQuery
{
    [Export(typeof(IDsTool))]
    public class Identify : DsBaseTool
    {
        public override void OnCreate(IDsApplication hook)
        {
            throw new NotImplementedException();
        }
    }
}

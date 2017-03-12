using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDDST.GIS.ui
{
    public class MainRibbonDef
    {
        public List<MainRibbonTabDef> RibbonTabs { get; private set; }

        public MainRibbonDef()
        {
            RibbonTabs = new List<MainRibbonTabDef>();
        }
    }

    public class MainRibbonTabDef
    {
        public string Header { get; set; }
        public List<MainRibbonGroupDef> RibbonGroups { get; private set; }

        public MainRibbonTabDef()
        {
            RibbonGroups = new List<MainRibbonGroupDef>();
        }
    }

    public class MainRibbonGroupDef
    {
        public string Header { get; set; }
        public List<MainRibbonComponentDef> RibbonComponents { get; }

        public MainRibbonGroupDef()
        {
            RibbonComponents = new List<MainRibbonComponentDef>();
        }
    }

    public class MainRibbonComponentDef
    {
        public string NameSpace { get; set; }
        public string Label { get; set; }
        
    }
}

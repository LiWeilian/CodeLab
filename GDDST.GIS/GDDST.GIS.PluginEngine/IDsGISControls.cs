﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GDDST.GIS.PluginEngine
{
    public interface IDsGISControls
    {
        void InitializeControls(IDsApplication hook);
        UserControl MapControl { get; }
        UserControl LegendControl { get; }
    }
}

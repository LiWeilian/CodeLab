using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel.Composition;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.Controls
{
    [Export(typeof(IDsMapControl))]
    public class MapControl : IDsMapControl
    {
        public UserControl DsMapControl
        {
            get
            {
                UCMapControl mapCtrl = new UCMapControl();
                //mapCtrl.SetValue(Grid.RowProperty, 1);
                //mapCtrl.SetValue(Grid.ColumnProperty, 1);
                return mapCtrl;
            }
        }  

        public UserControl CreateMapControl(int row, int column)
        {
            UCMapControl mapCtrl = new UCMapControl();
            mapCtrl.SetValue(Grid.RowProperty, row);
            mapCtrl.SetValue(Grid.ColumnProperty, column);
            return mapCtrl;
        }
    }
}

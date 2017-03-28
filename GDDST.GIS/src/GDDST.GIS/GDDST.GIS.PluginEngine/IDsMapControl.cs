using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GDDST.GIS.PluginEngine
{
    public interface IDsMapControl
    {
        UserControl DsMapControl { get; }

        UserControl CreateMapControl(int row, int column);
    }
}

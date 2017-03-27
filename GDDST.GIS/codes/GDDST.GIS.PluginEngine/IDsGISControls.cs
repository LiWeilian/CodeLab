using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GDDST.GIS.PluginEngine
{
    /// <summary>
    /// 定义系统所需的GIS控件
    /// </summary>
    public interface IDsGISControls
    {
        /// <summary>
        /// 加载控件到UI前，执行必要的初始化工作
        /// </summary>
        /// <param name="hook">全局应用程序对象</param>
        void InitializeControls(IDsApplication hook);
        /// <summary>
        /// 地图控件UI对象
        /// </summary>
        UserControl MapControl { get; }
        /// <summary>
        /// 地图控件核心，封装GIS平台提供或者自定义的地图控件对象
        /// </summary>
        object MapControlCore { get; }
        /// <summary>
        /// 图例控件UI对象
        /// </summary>
        UserControl LegendControl { get; }
        /// <summary>
        /// 图例控件核心，封装GIS平台提供或者自定义的图例控件对象
        /// </summary>
        object LegendControlCore { get; }
    }
}

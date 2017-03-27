using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDDST.GIS.PluginEngine
{
    public interface IDsGISInitialize
    {
        /// <summary>
        /// 使用GIS组件前执行必要的初始化工作
        /// </summary>
        void GISInitialize();
        /// <summary>
        /// 退出应用程序前执行必要的关闭工作
        /// </summary>
        void GISShutdown();
    }
}

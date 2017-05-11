/*
 * 1.0.0.0 2016.05.29/李伟廉
 * 1、实现从OPC服务器获取数据，根据数据库映射设置写入数据。 
 * 
 * 1.0.0.1 2016.06.02/李伟廉
 * 1、增加数据库字段值自定义来源的设置，分别是：常量、系统变量、根据从传感器和站点获取的数据按规则得到字段值。
 * 
 * 1.0.0.2 2016.06.05/李伟廉
 * 1、只添加
 * 
 * 1.0.0.3 2016.09.02/李伟廉
 * 1、增加指定项的配置，指定项的名称在配置文件OPCItemMapping.xml中读取。
 * 
 * 1.0.0.4 2016.10.13/李伟廉
 * 1、更新实时数据前，保存传感器状态字段[OUT_OF]值，同步到实时数据中。
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DS.OPC.Client
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}

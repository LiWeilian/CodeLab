﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GDDST.GIS.PluginEngine
{
    /// <summary>
    /// 定义框架主程序属性
    /// </summary>
    public interface IDsApplication
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// 当前工具
        /// </summary>
        IDsTool CurrentTool { get; set; }

        /// <summary>
        /// 地图文档对象
        /// </summary>
        //IMapDocument Document { get; set; }

        /// <summary>
        /// IMapControl接口对象
        /// </summary>
        //IMapControlDefault MapControl { get; set; }

        /// <summary>
        /// 地图控件对象
        /// </summary>
        //AxHost AxMapControl { get; set; }

        /// <summary>
        /// PageLayoutControl对象
        /// </summary>
        //IPageLayoutControlDefault PageLayoutControl { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 状态栏
        /// </summary>
       // StatusStrip StatusBar { set; }

        /// <summary>
        /// 是否可视
        /// </summary>
        Visibility Visibility { get; set; }

        /// <summary>
        /// 程序主窗体
        /// </summary>
        Window MainWindow { get; set; }

        /// <summary>
        /// 停靠面板，可停靠上下左右四个位置
        /// </summary>
        //DockPanel MainDockPanel { get; set; }

        /// <summary>
        /// 当前在图层列表中选中的图层
        /// </summary>
        //ILayer SelectedLayer { get; set; }

        /// <summary>
        /// 当前运行信息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// 鼠标坐标信息
        /// </summary>
        string CoordinateInfo { get; set; }

        /// <summary>
        /// 选择集环境设置
        /// </summary>
        //DSGIS.Environments.SelectionEnvironment SelectionEnvironment { get; set; }

        /// <summary>
        /// 屏幕坐标转换为地图控件坐标
        /// </summary>
        /// <param name="screenPoint">屏幕坐标点</param>
        /// <returns>地图控件坐标点，可能为空</returns>
        System.Drawing.Point? ScreenToMapControlPoint(System.Drawing.Point screenPoint);

        /// <summary>
        /// 屏幕坐标转换为地图坐标
        /// </summary>
        /// <param name="screenPoint">屏幕坐标点</param>
        /// <returns>地图坐标点</returns>
        //ESRI.ArcGIS.Geometry.IPoint ScreenToMapPoint(System.Drawing.Point screenPoint);

        /// <summary>
        /// 默认工具
        /// </summary>
        IDsTool DefaultTool { get; set; }

        /// <summary>
        /// 将当前工具重置为默认工具
        /// </summary>
        void ResetCurrentToolToDefaultTool();

        /// <summary>
        /// 界面样式
        /// </summary>
        IDsUIStyle UIStyle { get; set; }

        /// <summary>
        /// 地图控件
        /// </summary>
        object MapControl { get; set; }

        /// <summary>
        /// 图例控件
        /// </summary>
        object LegendControl { get; set; }

        /// <summary>
        /// 应用程序插件集合
        /// </summary>
        List<IDsPlugin> Plugins { get; }

        /// <summary>
        /// 右停靠面板，用作显示地图信息的停靠容器
        /// </summary>
        object RightDockPanel { get; set; }

        /// <summary>
        /// 左停靠面板，用作显示地图图层信息的停靠容器
        /// </summary>
        object LeftDockPanel { get; set; }
        
        /// <summary>
        /// 主停靠面板，用作显示地图
        /// </summary>
        object MainDockPanel { get; set; }

        /// <summary>
        /// 停靠管理器
        /// </summary>
        object MainDockRoot { get; set; }

        /// <summary>
        /// 将信息窗口添加到信息面板
        /// </summary>
        /// <param name="infoControl">信息窗口控件</param>
        void AddToInfoPanel(UserControl infoControl, string title,
            bool canClose, bool canHide, bool canFloat, bool isUnique);

        //void SetLayoutControls();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace MapCompare
{
    class MapTool
    {
        /// <summary>
        /// 双击地图触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        public virtual void OnMapControlDblClick(IActiveView activeView, int button, int shift, int x, int y, double mapX, double mapY)
        {
        }

        /// <summary>
        /// 鼠标在地图控件点击触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        public virtual void OnMapControlMouseDown(IActiveView activeView, int button, int shift, int x, int y, double mapX, double mapY)
        {
            if (button == 4)
            {
                //地图漫游
            }
        }

        /// <summary>
        /// 鼠标在地图控件移动触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        public virtual void OnMapControlMouseMove(IActiveView activeView, int button, int shift, int x, int y, double mapX, double mapY)
        {

        }

        /// <summary>
        /// 鼠标在地图控件弹起触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        public virtual void OnMapControlMouseUp(IActiveView activeView, int button, int shift, int x, int y, double mapX, double mapY)
        {
        }

        /// <summary>
        /// 地图控件按键按下事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        public virtual void OnMapControlKeyDown(IActiveView activeView, int keyCode, int shift)
        {
        }

        /// <summary>
        /// 地图控件按键弹起事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        public virtual void OnMapControlKeyUp(IActiveView activeView, int keyCode, int shift)
        {
        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="hDC">设备环境句柄</param>
        public virtual void Refresh(IActiveView activeView, int hDC)
        {
        }
    }

    class DefaulMapTool : MapTool
    {

    }
}

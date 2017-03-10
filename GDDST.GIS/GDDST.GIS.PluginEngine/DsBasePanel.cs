using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GDDST.GIS.PluginEngine
{
    public abstract class DsBasePanel : IDsPanel
    {
        protected IDsApplication m_app;

        #region IDsPanel 成员
        public virtual UserControl PluginPanel { get; protected set; }
        #endregion

        #region IDsPlugin 成员

        /// <summary>
        /// 地图重绘事件
        /// </summary>
        /// <param name="display">屏幕显示对象</param>
        /// <param name="viewDrawPhase">绘制类型</param>
        public virtual void OnMapAfterDraw(object display, int viewDrawPhase)
        {

        }

        /// <summary>
        /// 屏幕重绘后事件
        /// </summary>
        /// <param name="hdc">设备环境句柄</param>
        public virtual void OnMapAfterScreenDraw(int hdc)
        {

        }

        /// <summary>
        /// 屏幕重绘前事件
        /// </summary>
        /// <param name="hdc">设备环境句柄</param>
        public virtual void OnMapBeforeScreenDraw(int hdc)
        {

        }

        /// <summary>
        /// 地图显示区域更新事件
        /// </summary>
        /// <param name="displayTransformation">显示转换对象</param>
        /// <param name="sizeChanged">尺寸是否变化</param>
        /// <param name="newEnvelope">更新后的地图显示区域</param>
        public virtual void OnMapExtentUpdate(object displayTransformation, bool sizeChanged, object newEnvelope)
        {

        }

        /// <summary>
        /// 地图区域更新事件
        /// </summary>
        /// <param name="displayTransformation">显示转换对象</param>
        /// <param name="newEnvelope">更新后的地图区域</param>
        public virtual void OnMapFullExtentUpdated(object displayTransformation, object newEnvelope)
        {

        }

        /// <summary>
        /// 选择集改变事件
        /// </summary>
        public virtual void OnMapSelectionChanged()
        {

        }

        /// <summary>
        /// 地图刷新事件
        /// </summary>
        /// <param name="activeView">视图</param>
        /// <param name="viewDrawPhase">刷新类型</param>
        /// <param name="layerOrElement">刷新对象，图层或元素</param>
        /// <param name="envelope">刷新区域</param>
        public virtual void OnMapViewRefreshed(object activeView, int viewDrawPhase, object layerOrElement, object envelope)
        {

        }

        /// <summary>
        /// 添加地图内容事件
        /// </summary>
        /// <param name="Item"></param>
        public virtual void OnMapItemAdded(object Item)
        {

        }

        /// <summary>
        /// 删除地图内容事件
        /// </summary>
        /// <param name="Item"></param>
        public virtual void OnMapItemDeleted(object Item)
        {

        }

        /// <summary>
        /// 地图内容改变事件
        /// </summary>
        public virtual void OnMapContentsChanged()
        {

        }

        /// <summary>
        /// 地图对象被置换事件
        /// </summary>
        /// <param name="newMap">新地图对象</param>
        public virtual void OnMapReplaced(object newMap)
        {

        }

        /// <summary>
        /// 插件UI所在分组，用于控制选中状态等
        /// </summary>
        public string UIGroup { get; protected set; }

        /// <summary>
        /// 激活插件
        /// </summary>
        public virtual void OnActivate()
        {
            if (this.m_app.CurrentTool != null)
            {
                if (this.m_app.CurrentTool == this)
                {
                    return;
                }
                else
                {
                    this.m_app.CurrentTool.OnDeactivate();
                }
            }
        }

        /// <summary>
        /// 转为非激活状态
        /// </summary>
        public virtual void OnDeactivate()
        {
            
        }

        /// <summary>
        /// 插件销毁
        /// </summary>
        public virtual void OnDestroy()
        {

        }

        /// <summary>
        /// 创建时触发的方法
        /// </summary>
        /// <param name="hook">主应用程序对象</param>
        public abstract void OnCreate(IDsApplication hook);

        #endregion
    }
}

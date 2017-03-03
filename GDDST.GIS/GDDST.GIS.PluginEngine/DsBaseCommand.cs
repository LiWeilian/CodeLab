using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDDST.GIS.PluginEngine
{
    public abstract class DsBaseCommand : IDsCommand
    {
        protected IDsApplication m_app;
        protected string m_bitmapNameSmall = string.Empty;
        protected string m_bitmapNameLarge = string.Empty;
        /// <summary>
        /// 加载位图
        /// </summary>
        protected void LoadSmallBitmap()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\..\\images\\" + this.m_bitmapNameSmall;
            try
            {
                this.SmallBitmap = new System.Drawing.Bitmap(path);
            }
            catch (Exception)
            {
                this.SmallBitmap = null;
            }
        }
        protected void LoadLargeBitmap()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\..\\images\\" + this.m_bitmapNameLarge;
            try
            {
                this.LargeBitmap = new System.Drawing.Bitmap(path);
            }
            catch (Exception)
            {
                this.LargeBitmap = null;
            }
        }
        #region IDsCommand 成员
        /// <summary>
        /// 图标
        /// </summary>
        public virtual Bitmap LargeBitmap { get; protected set; }
        /// <summary>
        /// 图标
        /// </summary>
        public virtual Bitmap SmallBitmap { get; protected set; }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Caption { get; protected set; }

        /// <summary>
        /// 所属类别
        /// </summary>
        public virtual string Category { get; protected set; }

        /// <summary>
        /// 选择状态
        /// </summary>
        public virtual bool Checked { get; protected set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public virtual bool Enabled { get; protected set; }

        /// <summary>
        /// 快捷帮助ID
        /// </summary>
        public virtual int HelpContextID { get; protected set; }

        /// <summary>
        /// 帮助文档路径
        /// </summary>
        public virtual string HelpFile { get; protected set; }

        /// <summary>
        /// 鼠标在控件上移动时状态栏的提示文字
        /// </summary>
        public virtual string Message { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// 创建时触发的方法
        /// </summary>
        /// <param name="hook">主应用程序对象</param>
        public abstract void OnCreate(IDsApplication hook);

        /// <summary>
        /// 提示文字
        /// </summary>
        public virtual string Tooltip { get; protected set; }

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

        }

        /// <summary>
        /// 转为非激活状态
        /// </summary>
        public virtual void OnDeactivate()
        {
            this.Checked = false;
        }

        /// <summary>
        /// 插件销毁
        /// </summary>
        public virtual void OnDestroy()
        {

        }
        #endregion
    }
}

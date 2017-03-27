using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDDST.GIS.PluginEngine
{
    public abstract class DsBaseTool : IDsTool
    {
        protected IDsApplication m_app;
        protected string m_bitmapNameSmall = string.Empty;
        protected string m_bitmapNameLarge = string.Empty;
        /// <summary>
        /// 加载位图
        /// </summary>
        protected void LoadSmallBitmap()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "..\\images\\" + this.m_bitmapNameSmall;
            try
            {
                this.SmallBitmap = new System.Drawing.Bitmap(path);
            }
            catch (Exception ex)
            {
                this.SmallBitmap = null;
            }
        }

        protected void LoadLargeBitmap()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "..\\images\\" + this.m_bitmapNameLarge;
            try
            {
                this.LargeBitmap = new System.Drawing.Bitmap(path);
            }
            catch (Exception)
            {
                this.LargeBitmap = null;
            }
        }

        #region IDsTool 成员
        /// <summary>
        /// 图标
        /// </summary>
        public virtual Bitmap LargeBitmap { get; protected set; }

        /// <summary>
        /// 图标
        /// </summary>
        public virtual System.Drawing.Bitmap SmallBitmap { get; protected set; }

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
        /// 提示文字
        /// </summary>
        public virtual string Tooltip { get; protected set; }

        /// <summary>
        /// 鼠标样式
        /// </summary>
        public virtual int Cursor { get; protected set; }

        /// <summary>
        /// 插件激活状态
        /// </summary>
        public virtual bool Deactivate { get; protected set; }

        /// <summary>
        /// 弹出右键菜单触发事件
        /// </summary>
        /// <param name="x">点击位置地图控件X坐标</param>
        /// <param name="y">点击位置地图控件Y坐标</param>
        /// <returns>是否成功触发</returns> 
        public virtual bool OnContextMenu(int x, int y)
        {
            return false;
        }

        /// <summary>
        /// 双击地图触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        public virtual void OnMapControlDblClick(int button, int shift, int x, int y, double mapX, double mapY)
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
        public virtual void OnMapControlMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
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
        public virtual void OnMapControlMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
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
        public virtual void OnMapControlMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
        }

        /// <summary>
        /// 地图控件按键按下事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        public virtual void OnMapControlKeyDown(int keyCode, int shift)
        {
        }

        /// <summary>
        /// 地图控件按键弹起事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        public virtual void OnMapControlKeyUp(int keyCode, int shift)
        {
        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="hDC">设备环境句柄</param>
        public virtual void Refresh(int hDC)
        {
        }

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
            this.m_app.CurrentTool = this;
            this.m_app.Message = this.Message;
            this.Checked = true;
        }

        /// <summary>
        /// 转为非激活状态
        /// </summary>
        public virtual void OnDeactivate()
        {
            this.Checked = false;
        }

        /// <summary>
        /// 创建时触发的方法
        /// </summary>
        /// <param name="hook">主应用程序对象</param>
        public abstract void OnCreate(IDsApplication hook);

        /// <summary>
        /// 插件销毁
        /// </summary>
        public virtual void OnDestroy()
        {

        }

        #endregion
    }
}

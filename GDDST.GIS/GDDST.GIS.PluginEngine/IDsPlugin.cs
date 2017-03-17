using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GDDST.GIS.PluginEngine
{
    /// <summary>
    /// 插件接口
    /// </summary>
    public interface IDsPlugin
    {
        /// <summary>
        /// 插件UI所在分组，用于控制选中状态等
        /// </summary>
        string UIGroup { get; }

        /// <summary>
        /// 激活插件
        /// </summary>
        void OnActivate();

        /// <summary>
        /// 转为非激活状态
        /// </summary>
        void OnDeactivate();

        #region 响应地图控件事件         
        /// <summary>
        /// 地图重绘事件
        /// </summary>
        /// <param name="display">屏幕显示对象</param>
        /// <param name="viewDrawPhase">绘制类型</param>
        void OnMapAfterDraw(object display, int viewDrawPhase);

        /// <summary>
        /// 屏幕重绘后事件
        /// </summary>
        /// <param name="hdc">设备环境句柄</param>
        void OnMapAfterScreenDraw(int hdc);

        /// <summary>
        /// 屏幕重绘前事件
        /// </summary>
        /// <param name="hdc">设备环境句柄</param>
        void OnMapBeforeScreenDraw(int hdc);

        /// <summary>
        /// 地图显示区域更新事件
        /// </summary>
        /// <param name="displayTransformation">显示转换对象</param>
        /// <param name="sizeChanged">尺寸是否变化</param>
        /// <param name="newEnvelope">更新后的地图显示区域</param>
        void OnMapExtentUpdate(object displayTransformation, bool sizeChanged, object newEnvelope);

        /// <summary>
        /// 地图区域更新事件
        /// </summary>
        /// <param name="displayTransformation">显示转换对象</param>
        /// <param name="newEnvelope">更新后的地图区域</param>
        void OnMapFullExtentUpdated(object displayTransformation, object newEnvelope);

        /// <summary>
        /// 地图对象被置换事件
        /// </summary>
        /// <param name="newMap">新地图对象</param>
        void OnMapReplaced(object newMap);

        /// <summary>
        /// 选择集改变事件
        /// </summary>
        void OnMapSelectionChanged();

        /// <summary>
        /// 地图刷新事件
        /// </summary>
        /// <param name="activeView">视图</param>
        /// <param name="viewDrawPhase">刷新类型</param>
        /// <param name="layerOrElement">刷新对象，图层或元素</param>
        /// <param name="envelope">刷新区域</param>
        void OnMapViewRefreshed(object activeView, int viewDrawPhase, object layerOrElement, object envelope);
        #endregion

        #region 响应地图对象事件
        /// <summary>
        /// 添加地图内容事件
        /// </summary>
        /// <param name="Item"></param>
        void OnMapItemAdded(object Item);

        /// <summary>
        /// 删除地图内容事件
        /// </summary>
        /// <param name="Item"></param>
        void OnMapItemDeleted(object Item);

        /// <summary>
        /// 地图内容改变事件
        /// </summary>
        void OnMapContentsChanged();
        #endregion

        /// <summary>
        /// 创建时触发的方法
        /// </summary>
        /// <param name="hook">主应用程序对象</param>
        void OnCreate(IDsApplication hook);

        /// <summary>
        /// 插件销毁方法
        /// </summary>
        void OnDestroy();
    }

    /// <summary>
    /// 命令插件，参考了esriSystemUI.ICommand接口
    /// </summary>
    public interface IDsCommand : IDsPlugin
    {
        /// <summary>
        /// 大图标
        /// </summary>
        Bitmap LargeBitmap { get; }
        /// <summary>
        /// 小图标
        /// </summary>
        Bitmap SmallBitmap { get; }

        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// 所属类别
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 选择状态
        /// </summary>
        bool Checked { get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 快捷帮助ID
        /// </summary>
        int HelpContextID { get; }

        /// <summary>
        /// 帮助文档路径
        /// </summary>
        string HelpFile { get; }

        /// <summary>
        /// 鼠标在控件上移动时状态栏的提示文字
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        ///// <summary>
        ///// 点击事件触发的方法
        ///// </summary>
        //void OnClick();

        /// <summary>
        /// 提示文字
        /// </summary>
        string Tooltip { get; }
    }

    /// <summary>
    /// 工具插件，参考了esriSystemUI.ICommand接口和esriSystemUI.ITool接口
    /// </summary>
    public interface IDsTool : IDsPlugin
    {
        /// <summary>
        /// 大图标
        /// </summary>
        Bitmap LargeBitmap { get; }
        /// <summary>
        /// 图标
        /// </summary>
        Bitmap SmallBitmap { get; }

        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// 所属类别
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 选择状态
        /// </summary>
        bool Checked { get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 快捷帮助ID
        /// </summary>
        int HelpContextID { get; }

        /// <summary>
        /// 帮助文档路径
        /// </summary>
        string HelpFile { get; }

        /// <summary>
        /// 鼠标在控件上移动时状态栏的提示文字
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 提示文字
        /// </summary>
        string Tooltip { get; }

        /// <summary>
        /// 鼠标样式
        /// </summary>
        int Cursor { get; }

        /// <summary>
        /// 插件激活状态
        /// </summary>
        bool Deactivate { get; }

        /// <summary>
        /// 弹出右键菜单触发事件
        /// </summary>
        /// <param name="x">点击位置地图控件X坐标</param>
        /// <param name="y">点击位置地图控件Y坐标</param>
        /// <returns>是否成功触发</returns>       
        bool OnContextMenu(int x, int y);

        /// <summary>
        /// 双击地图触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlDblClick(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 鼠标在地图控件点击触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlMouseDown(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 鼠标在地图控件移动触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlMouseMove(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 鼠标在地图控件弹起触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlMouseUp(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 地图控件按键按下事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        void OnMapControlKeyDown(int keyCode, int shift);

        /// <summary>
        /// 地图控件按键弹起事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        void OnMapControlKeyUp(int keyCode, int shift);

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="hDC">设备环境句柄</param>
        void Refresh(int hDC);
    }

    /// <summary>
    /// 下拉框插件
    /// </summary>
    public interface IDsComboBox : IDsPlugin
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// 是否显示标题
        /// </summary>
        bool ShowCaption { get; }

        /// <summary>
        /// 所属类别
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        bool Editable { get; }

        /// <summary>
        /// 插件激活状态
        /// </summary>
        bool Deactivate { get; set; }

        /// <summary>
        /// UI控件宽度
        /// </summary>
        int Width { get; }

        /// <summary>
        /// 鼠标在控件上移动时状态栏的提示文字
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 提示文字
        /// </summary>
        string Tooltip { get; }

        /// <summary>
        /// 编辑框文本
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 下拉列表内容
        /// </summary>
        //List<IDsPluginDropDownItem> Items { get; }

        /// <summary>
        /// 按键按下事件
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="shift"></param>
        void OnKeyDown(int keyCode, int shift);

        /// <summary>
        /// 按键弹起事件
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="shift"></param>
        void OnKeyUp(int keyCode, int shift);

        /// <summary>
        /// 文本信息改变事件
        /// </summary>
        void OnTextChanged(string newText);

        /// <summary>
        /// 选择项改变
        /// </summary>
        void OnSelectedIndexChanged(int selectedIndex);

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="hDC"></param>
        void Refresh(int hDC);
    }

    /// <summary>
    /// 编辑框插件
    /// </summary>
    public interface IDsEditBox : IDsPlugin
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// 是否显示标题
        /// </summary>
        bool ShowCaption { get; }

        /// <summary>
        /// 所属类别
        /// </summary>
        string Category { get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        bool Editable { get; }

        /// <summary>
        /// UI控件宽度
        /// </summary>
        int Width { get; }

        /// <summary>
        /// 鼠标在控件上移动时状态栏的提示文字
        /// </summary>
        string Message { get; }

        /// <summary>
        /// 提示文字
        /// </summary>
        string Tooltip { get; }

        /// <summary>
        /// 编辑框文本
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 按键按下事件
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="shift"></param>
        void OnKeyDown(int keyCode, int shift);

        /// <summary>
        /// 按键弹起事件
        /// </summary>
        /// <param name="keyCode"></param>
        /// <param name="shift"></param>
        void OnKeyUp(int keyCode, int shift);

        /// <summary>
        /// 文本信息改变事件
        /// </summary>
        void OnTextChanged(string newText);

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="hDC"></param>
        void Refresh(int hDC);

        /// <summary>
        /// 插件激活状态
        /// </summary>
        bool Deactivate { get; set; }
    }

    /// <summary>
    /// 面板插件
    /// </summary>
    public interface IDsPanel : IDsPlugin
    {
        /// <summary>
        /// 自定义面板控件，内部可承载其他控件
        /// </summary>
        UserControl PluginPanel { get; }
    }

    /*
    /// <summary>
    /// 可停靠窗体插件
    /// </summary>
    public interface IDsDockableWindow : DSGIS.PluginEngine.IDsPlugin
    {
        /// <summary>
        /// 标题
        /// </summary>
        string WindowCaption { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string WindowName { get; }

        /// <summary>
        /// 窗体插件的子控件
        /// </summary>
        System.Windows.Forms.Control SubControl { get; }
    }

    /// <summary>
    /// 菜单插件
    /// </summary>
    public interface IDsMenuDef : DSGIS.PluginEngine.IDsPlugin
    {

    }

    /// <summary>
    /// 工具条插件
    /// </summary>
    public interface IDsToolbarDef : DSGIS.PluginEngine.IDsPlugin
    {
    }

    /// <summary>
    /// 工具条插件，直接从插件中获取工具条
    /// </summary>
    public interface IDsToolbarDefEx : DSGIS.PluginEngine.IDsPlugin
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 窗体插件的子控件
        /// </summary>
        System.Windows.Forms.Control Toolbar { get; }

        /// <summary>
        /// 双击地图触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlDblClick(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 鼠标在地图控件点击触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlMouseDown(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 鼠标在地图控件移动触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlMouseMove(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 鼠标在地图控件弹起触发的事件
        /// </summary>
        /// <param name="button">鼠标按键编号，1：左键，2：右键：4中键，可组合使用</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        /// <param name="x">地图控件X坐标</param>
        /// <param name="y">地图控件Y坐标</param>
        /// <param name="mapX">地图X坐标</param>
        /// <param name="mapY">地图Y坐标</param>
        void OnMapControlMouseUp(int button, int shift, int x, int y, double mapX, double mapY);

        /// <summary>
        /// 地图控件按键按下事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        void OnMapControlKeyDown(int keyCode, int shift);

        /// <summary>
        /// 地图控件按键弹起事件
        /// </summary>
        /// <param name="keyCode">键盘按键ASCII码</param>
        /// <param name="shift">辅助按键编号，0：SHITF，1：CTRL，2：ALT，可组合使用</param>
        void OnMapControlKeyUp(int keyCode, int shift);
    }

    /// <summary>
    /// 插件下拉项接口
    /// </summary>
    public interface IDsPluginDropDownItem
    {
        string Text { get; set; }
        object Value { get; set; }
    }
    */
}

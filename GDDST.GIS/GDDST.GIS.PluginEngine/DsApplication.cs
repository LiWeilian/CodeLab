using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Windows;

namespace GDDST.GIS.PluginEngine
{
    [Export(typeof(IDsApplication))]
    public class DsApplication : IDsApplication
    {
        private string m_caption;
        private IDsTool m_currentTool;
        //private IMapDocument m_document;
        //private IMapControlDefault m_mapControl;
        //private IPageLayoutControlDefault m_pageLayoutControl;
        private string m_name;
        //private StatusStrip m_statusBar;
        private bool m_visible;
        private Window m_mainWindow;
        //private DockPanel m_mainDockPanel;
        //private ILayer m_selectedLayer;
        private string m_message;
        //private DSGIS.Environments.SelectionEnvironment m_selectionEnvironment;
        //private AxHost m_axMapControl;
        private IDsTool m_defaultTool = null;

        #region IDsApplication 成员

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption
        {
            get
            {
                return this.m_mainWindow.Title;
            }
            set
            {
                this.m_mainWindow.Title = value;
            }
        }

        /// <summary>
        /// 当前工具
        /// </summary>
        public IDsTool CurrentTool { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_mainWindow.Name;
            }
        }

        

        /// <summary>
        /// 是否可视
        /// </summary>
        public Visibility Visibility
        {
            get
            {
                return this.m_mainWindow.Visibility;
            }
            set
            {
                this.m_mainWindow.Visibility = value;
            }
        }


        /// <summary>
        /// 当前运行信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 鼠标坐标信息
        /// </summary>
        public string CoordinateInfo { get; set; }


        /// <summary>
        /// 屏幕坐标转换为地图控件坐标
        /// </summary>
        /// <param name="screenPoint">屏幕坐标点</param>
        /// <returns>地图控件坐标点，可能为空</returns>
        public System.Drawing.Point? ScreenToMapControlPoint(System.Drawing.Point screenPoint)
        {
            return null;
        }


        /// <summary>
        /// 将当前工具重置为默认工具
        /// </summary>
        public void ResetCurrentToolToDefaultTool()
        {
            if (m_defaultTool != null)
            {
                m_defaultTool.OnActivate();
            }
        }

        /// <summary>
        /// 默认工具
        /// </summary>
        public IDsTool DefaultTool { get; set; }

        public Window MainWindow { get; set; }

        public IDsUIStyle UIStyle
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region
        /*
        /// <summary>
        /// 屏幕坐标转换为地图坐标
        /// </summary>
        /// <param name="screenPoint">屏幕坐标点</param>
        /// <returns>地图坐标点</returns>
        public ESRI.ArcGIS.Geometry.IPoint ScreenToMapPoint(System.Drawing.Point screenPoint)
        {
            if (m_axMapControl != null)
            {
                System.Drawing.Point? ctrlPoint = ScreenToMapControlPoint(screenPoint);
                if (ctrlPoint != null)
                {
                    if (m_mapControl != null)
                    {
                        return m_mapControl.ToMapPoint(ctrlPoint.Value.X, ctrlPoint.Value.Y);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 选择集环境设置
        /// </summary>
        public DSGIS.Environments.SelectionEnvironment SelectionEnvironment
        {
            get
            {
                return this.m_selectionEnvironment;
            }
            set
            {
                this.m_selectionEnvironment = value;
            }
        }

        /// <summary>
        /// 地图控件对象
        /// </summary>
        public AxHost AxMapControl
        {
            get
            {
                return this.m_axMapControl;
            }
            set
            {
                this.m_axMapControl = value;
            }
        }

        /// <summary>
        /// 停靠面板，可停靠上下左右四个位置
        /// </summary>
        public DSGIS.dsSystemUI.Docking.DockPanel MainDockPanel
        {
            get
            {
                return this.m_mainDockPanel;
            }
            set
            {
                this.m_mainDockPanel = value;
            }
        }

        /// <summary>
        /// 当前在图层列表中选中的图层
        /// </summary>
        public ILayer SelectedLayer
        {
            get
            {
                return this.m_selectedLayer;
            }
            set
            {
                this.m_selectedLayer = value;
            }
        }

        /// <summary>
        /// 状态栏
        /// </summary>
        public System.Windows.Forms.StatusStrip StatusBar
        {
            set
            {
                this.m_statusBar = value;
            }
        }

        
        /// <summary>
        /// 地图文档对象
        /// </summary>
        public ESRI.ArcGIS.Carto.IMapDocument Document
        {
            get
            {
                return this.m_document;
            }
            set
            {
                this.m_document = value;
            }
        }

        /// <summary>
        /// IMapControl接口对象
        /// </summary>
        public ESRI.ArcGIS.Controls.IMapControlDefault MapControl
        {
            get
            {
                return this.m_mapControl;
            }
            set
            {
                this.m_mapControl = value;
            }
        }

        /// <summary>
        /// PageLayoutControl对象
        /// </summary>
        public ESRI.ArcGIS.Controls.IPageLayoutControlDefault PageLayoutControl
        {
            get
            {
                return this.m_pageLayoutControl;
            }
            set
            {
                this.m_pageLayoutControl = value;
            }
        }
        */
        #endregion
    }
}

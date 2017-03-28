using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

namespace GDDST.GIS.EsriUtils
{
    /// <summary>
    /// ESRI活动视图IActiveView代理类
    /// 实现地图或打印布局的基本视图操作部分功能实现（放大、缩小等）
    /// </summary>
    public class ViewAgent
    {
        /// <summary>
        /// 中心放大
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static void FixZoomIn(IActiveView activeView)
        {
            if (activeView != null)
            {
                IEnvelope envelope = activeView.Extent;
                if (GeometryUtility.IsValidGeometry(envelope))
                {
                    envelope.Expand(0.75, 0.75, true);
                    activeView.Extent = envelope;
                    activeView.Refresh();
                }
            }
        }

        /// <summary>
        /// 中心缩小
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static void FixZoomOut(IActiveView activeView)
        {
            if (activeView != null)
            {
                IEnvelope envelope = activeView.Extent;
                if (GeometryUtility.IsValidGeometry(envelope))
                {
                    envelope.Expand(1.25, 1.25, true);
                    activeView.Extent = envelope;
                    activeView.Refresh();
                }
            }
        }

        /// <summary>
        /// 缩放到全部范围
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static void ZoomAll(IActiveView activeView)
        {
            if (activeView != null)
            {
                if (GeometryUtility.IsValidGeometry(activeView.FullExtent))
                {
                    activeView.Extent = activeView.FullExtent;
                    activeView.Refresh();
                }
            }
        }

        /// <summary>
        /// 视图放大
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        /// <param name="x">活动视图x坐标</param>
        /// <param name="y">活动视图y坐标</param>
        public static void ZoomIn(IActiveView activeView, double x, double y)
        {
            if (activeView != null)
            {
                IGeometry trackGeom = GeometryUtility.ScreenTrackGeometry(activeView.ScreenDisplay, dsGeometryType.dsGTRectangle);
                IEnvelope envelope;
                if (GeometryUtility.IsValidRubberGeometry(trackGeom))
                    envelope = trackGeom.Envelope;
                else
                {
                    envelope = activeView.Extent;
                    IPoint cp = GeometryUtility.CreatePointByCoord(x, y);
                    envelope.CenterAt(cp);
                    envelope.Expand(0.75, 0.75, true);
                }
                if (GeometryUtility.IsValidGeometry(envelope))
                {
                    activeView.Extent = envelope;
                    activeView.Refresh();
                }
            }
        }

        /// <summary>
        /// 视图缩小
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        /// <param name="x">活动视图x坐标</param>
        /// <param name="y">活动视图y坐标</param>
        public static void ZoomOut(IActiveView activeView, double x, double y)
        {
            if (activeView != null)
            {
                IGeometry trackGeom = GeometryUtility.ScreenTrackGeometry(activeView.ScreenDisplay, dsGeometryType.dsGTRectangle);
                IEnvelope envelope;
                if (GeometryUtility.IsValidRubberGeometry(trackGeom))
                {
                    envelope = GeometryUtility.CreateEnvelopeByCoord(activeView.Extent.XMin - ((trackGeom.Envelope.XMin - activeView.Extent.XMin) * (activeView.Extent.Width / trackGeom.Envelope.Width)),
                                                                     activeView.Extent.YMin - ((trackGeom.Envelope.YMin - activeView.Extent.YMin) * (activeView.Extent.Height / trackGeom.Envelope.Height)),
                                                                     activeView.Extent.XMin - ((trackGeom.Envelope.XMin - activeView.Extent.XMin) * (activeView.Extent.Width / trackGeom.Envelope.Width)) + activeView.Extent.Width * (activeView.Extent.Width / trackGeom.Envelope.Width),
                                                                     activeView.Extent.YMin - ((trackGeom.Envelope.YMin - activeView.Extent.YMin) * (activeView.Extent.Height / trackGeom.Envelope.Height)) + activeView.Extent.Height * (activeView.Extent.Height / trackGeom.Envelope.Height));
                }
                else
                {
                    envelope = activeView.Extent;
                    IPoint cp = GeometryUtility.CreatePointByCoord(x, y);
                    envelope.CenterAt(cp);
                    envelope.Expand(1.25, 1.25, true);
                }
                if (GeometryUtility.IsValidGeometry(envelope))
                {
                    activeView.Extent = envelope;
                    activeView.Refresh();
                }
            }
        }

        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static void Pan(IActiveView activeView)
        {
            if (activeView != null)
            {
                activeView.ScreenDisplay.TrackPan();
            }
        }

        /// <summary>
        /// 平移到几何形状
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        /// <param name="geometry">平移到的ESRI几何形状接口</param>
        public static void PanToGeometry(IActiveView activeView, IGeometry geometry)
        {
            if (activeView != null && GeometryUtility.IsValidGeometry(geometry))
            {
                IPoint cp = GeometryUtility.GeometryCentroid(geometry);
                if (cp == null)
                {
                    IEnvelope envelope = activeView.Extent;
                    envelope.CenterAt(cp);
                    activeView.Extent = envelope;
                    activeView.Refresh();
                }
            }
        }

        /// <summary>
        /// 缩放到几何形状
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        /// <param name="geometry">缩放到的ESRI几何形状接口</param>
        public static void ZoomToGeometry(IActiveView activeView, IGeometry geometry)
        {
            if (activeView != null && GeometryUtility.IsValidGeometry(geometry))
            {
                IEnvelope envelope;
                if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
                {
                    if (activeView.ScreenDisplay.DisplayTransformation.ScaleRatio > 1000)
                    {
                        activeView.ScreenDisplay.DisplayTransformation.ScaleRatio = 1000;
                    }
                    envelope = activeView.Extent;
                    envelope.CenterAt((IPoint)geometry);
                }
                else
                {
                    envelope = geometry.Envelope;
                    envelope.Expand(1.1, 1.1, true);
                }
                if (!GeometryUtility.IsCompatibleEnvlope(activeView.Extent, envelope))
                {
                    activeView.Extent = envelope;
                    activeView.Refresh();
                }
            }
        }

        /// <summary>
        /// 连续缩放
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static void TrackZoom(IActiveView activeView)
        {
            if (activeView != null)
            {
                if (activeView.ScreenDisplay is IScreenDisplayZoom)
                    (activeView.ScreenDisplay as IScreenDisplayZoom).TrackZoom();
            }
        }

        /// <summary>
        /// 是否有前一视图
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static bool HasPrior(IActiveView activeView)
        {
            if (activeView != null && activeView.ExtentStack != null)
                return activeView.ExtentStack.CanUndo();
            else
                return false;
        }

        /// <summary>
        /// 前一视图
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static void Prior(IActiveView activeView)
        {
            if (HasPrior(activeView))
                activeView.ExtentStack.Undo();
        }

        /// <summary>
        /// 是否有后一视图
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static bool HasNext(IActiveView activeView)
        {
            if (activeView != null && activeView.ExtentStack != null)
                return activeView.ExtentStack.CanRedo();
            else
                return false;
        }

        /// <summary>
        /// 后一视图
        /// </summary>
        /// <param name="activeView">ESRI活动视图接口</param>
        public static void Next(IActiveView activeView)
        {
            if (HasNext(activeView))
                activeView.ExtentStack.Redo();
        }
    }
}

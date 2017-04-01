using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace GDDST.GIS.EsriUtils
{
    /// <summary>
    /// ArcGIS的几何形状拓扑操作类，主要为系统提供几何形状的拓扑操作，例如：缓冲、相交、修剪、合并等的方法。
    /// </summary>
    public class TopologicalOperator
    {
        /// <summary>
        /// 缓冲几何形状的函数
        /// </summary>
        /// <param name="geometry">ESRI的几何形状接口</param>
        /// <param name="buffer">缓冲区的大小（地图单位）</param>
        /// <returns>缓冲后的ESRI几何形状接口</returns>
        public static IGeometry Buffer(IGeometry geometry, double buffer)
        {
            if ((GeometryUtility.IsValidGeometry(geometry)) && (geometry is ITopologicalOperator))
            {
                ITopologicalOperator topoOp = (ITopologicalOperator)geometry;
                return topoOp.Buffer(buffer);
            }
            else
                return null;
        }

        /// <summary>
        /// 合并两个几何形状
        /// 注意：
        ///     两个几何形状都必须是高级几何形状（如：point, multipoint, polyline and polygon），
        /// 低级几何形状（如：Line, Circular Arc, Elliptic Arc, Bézier Curve）需要转为高级几何形状
        /// 可使用GeometryUtility.ConvertGeometryToHigh转为高级几何形状。
        /// </summary>
        /// <param name="beUnion">被合并ESRI的几何形状接口</param>
        /// <param name="unioned">合并的ESRI几何形状接口</param>
        public static void Union(IGeometry beUnion, ref IGeometry unioned)
        {
            if (unioned == null)
                unioned = beUnion;
            else
            {
                if ((GeometryUtility.IsValidGeometry(unioned)) && (unioned is ITopologicalOperator))
                {
                    ITopologicalOperator topoOp = (ITopologicalOperator)unioned;
                    topoOp.Simplify();
                    IGeometry geometry = topoOp.Union(beUnion);
                    if (geometry != null)
                        unioned = geometry;
                }
            }
        }

        /// <summary>
        /// 合并两个几何形状，并传出合并后的几何形状和错误信息。
        /// 注意：
        ///     两个几何形状都必须是高级几何形状（如：point, multipoint, polyline and polygon），
        /// 低级几何形状（如：Line, Circular Arc, Elliptic Arc, Bézier Curve）需要转为高级几何形状
        /// 可使用GeometryUtility.ConvertGeometryToHigh转为高级几何形状。
        /// </summary>
        /// <param name="geometry1">待合并的几何形状1</param>
        /// <param name="geometry2">待合并的几何形状2</param>
        /// <param name="Unioned">合并后的几何形状</param>
        /// <param name="ErrMsg">错误信息</param>
        public static void Union(IGeometry geometry1, IGeometry geometry2, out IGeometry Unioned, out string ErrMsg)
        {
            ErrMsg = "";
            Unioned = null;
            if (GeometryUtility.IsHighLevelGeometry(geometry1) && GeometryUtility.IsHighLevelGeometry(geometry2))
            {
                ITopologicalOperator topoOp = (ITopologicalOperator)geometry1;
                IGeometry geometry = topoOp.Union(geometry2);
                if (GeometryUtility.IsValidGeometry(geometry))
                {
                    try
                    {
                        Simplify(geometry);
                        Unioned = geometry;
                    }
                    catch (Exception ex)
                    {
                        ErrMsg = ex.Message;
                    }
                }
                else
                    ErrMsg = "传入的几何形状合并失败。";
            }
            else
                ErrMsg = "传入的几何形状不是高级几何形状，不能合并。";
        }

        /// <summary>
        /// 简化几何形状
        /// 对传入的几何形状直接进行简化
        /// 主要在编辑时使用
        /// </summary>
        /// <param name="geometry">需要被简化的几何形状</param>
        public static void Simplify(IGeometry geometry)
        {
            if (geometry != null)
            {
                if (geometry is ITopologicalOperator2)
                {
                    ITopologicalOperator2 topoOp = geometry as ITopologicalOperator2;
                    topoOp.IsKnownSimple_2 = false;
                    switch (geometry.GeometryType)
                    {
                        case esriGeometryType.esriGeometryPolygon:
                            {
                                IPolygon poly = (IPolygon)geometry;
                                poly.SimplifyPreserveFromTo();
                                break;
                            }
                        case esriGeometryType.esriGeometryPolyline:
                            {
                                IPolyline polyLineGeom = (IPolyline)geometry;
                                polyLineGeom.SimplifyNetwork();
                                break;
                            }
                        default:
                            {
                                topoOp.Simplify();
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// 简化几何形状
        /// 不直接对传入的几何形状进行简化，而是克隆传入参数再进行简化出结果
        /// 主要在编辑时使用
        /// </summary>
        /// <param name="geometry">需要被简化的几何形状</param>
        /// <returns>简化后的几何形状</returns>
        public static IGeometry QuerySimplify(IGeometry geometry)
        {
            if (geometry == null)
                return null;
            else
            {
                IGeometry geom = (geometry as IClone).Clone() as IGeometry;
                if (geom is ITopologicalOperator2)
                {
                    ITopologicalOperator2 topoOp = geom as ITopologicalOperator2;
                    topoOp.IsKnownSimple_2 = false;

                    switch (geom.GeometryType)
                    {
                        case esriGeometryType.esriGeometryPolygon:
                            {
                                IPolygon poly = (IPolygon)geom;
                                poly.SimplifyPreserveFromTo();
                                return poly;
                            }
                        case esriGeometryType.esriGeometryPolyline:
                            {
                                IPolyline polyLineGeom = (IPolyline)geom;
                                polyLineGeom.SimplifyNetwork();
                                return polyLineGeom;
                            }
                        default:
                            {
                                topoOp.Simplify();
                                return (IGeometry)topoOp;
                            }
                    }
                }
                else
                    return geometry;
            }
        }

        /// <summary>
        /// 获取两个几何形状的交集，并传出相交后的几何形状和错误信息。
        /// </summary>
        /// <param name="geometry1">待相交的几何形状1</param>
        /// <param name="geometry2">待相交的几何形状2</param>
        /// <param name="Intersected">相交后的几何形状</param>
        /// <param name="ErrMsg">错误信息</param>
        public static void Intersect(IGeometry geometry1, IGeometry geometry2, out IGeometry Intersected, out string ErrMsg)
        {
            ErrMsg = "";
            Intersected = null;
            if (GeometryUtility.IsHighLevelGeometry(geometry1) && GeometryUtility.IsHighLevelGeometry(geometry2))
            {
                if (!RelationalOperator.Disjoint(geometry1, geometry2))
                {
                    esriGeometryDimension resultDimension = esriGeometryDimension.esriGeometryNoDimension;
                    if (geometry1.Dimension <= geometry2.Dimension)
                        resultDimension = geometry1.Dimension;
                    else
                        resultDimension = geometry2.Dimension;
                    ITopologicalOperator topoOp = (ITopologicalOperator)geometry1;
                    IGeometry geometry = topoOp.Intersect(geometry2, resultDimension);
                    if (GeometryUtility.IsValidGeometry(geometry))
                    {
                        try
                        {
                            Simplify(geometry);
                            Intersected = geometry;
                        }
                        catch (Exception ex)
                        {
                            ErrMsg = ex.Message;
                        }
                    }
                    else
                        ErrMsg = "传入的几何形状获取交集失败。";
                }
                else
                    ErrMsg = "传入的几何形状没有交集。";
            }
            else
                ErrMsg = "传入的几何形状不是高级几何形状，不能获取交集。";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESRI.ArcGIS.Geometry;

namespace GDDST.GIS.EsriUtils
{
    /// <summary>
    /// ArcGIS的几何形状空间关系操作类，主要为系统提供几何形状的空间关系判断，例如：包含、相交、相接、交叉等。
    /// </summary>
    public class RelationalOperator
    {
        /// <summary>
        /// 判断两个几何形状是否没有交集
        /// </summary>
        /// <param name="geometry1">待判断的几何形状1</param>
        /// <param name="geometry2">待判断的几何形状2</param>
        /// <returns>是否没有交集</returns>
        public static bool Disjoint(IGeometry geometry1, IGeometry geometry2)
        {
            if (GeometryUtility.IsHighLevelGeometry(geometry1) && GeometryUtility.IsHighLevelGeometry(geometry2))
            {
                try
                {
                    IRelationalOperator relateOP = (IRelationalOperator)geometry1;
                    return relateOP.Disjoint(geometry2);
                }
                catch
                {
                    return true;
                }
            }
            else
                return true;
        }
    }
}

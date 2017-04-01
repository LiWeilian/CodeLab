using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace GDDST.GIS.EsriUtils
{
    /// <summary>
    /// 封装实现Esri有关单位转换的操作
    /// </summary>
    public class Units
    {
        public static double ConvertScreenPixelsToMapUnits(IScreenDisplay scrnDsp, double pixels)
        {
            IDisplayTransformation dspTransform = scrnDsp.DisplayTransformation;
            double refScale = dspTransform.ReferenceScale;

            if (refScale != 0)
            {
                double scaleRatio =  dspTransform.ScaleRatio;
                double pixelCount = dspTransform.ToPoints(scaleRatio);
                double pixelSize = (double)scaleRatio / (double)pixelCount;
                return pixelSize * pixels;
            } else
            {
                tagRECT deviceFrame = dspTransform.get_DeviceFrame();
                int pixelExtent = deviceFrame.right - deviceFrame.left;

                IEnvelope visBounds = dspTransform.VisibleBounds;
                double pixelSize = (double)visBounds.Width / (double)pixelExtent;
                return pixelSize * pixels;
            }
        }
    }
}

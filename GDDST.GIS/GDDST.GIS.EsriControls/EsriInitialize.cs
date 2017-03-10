using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

using ESRI.ArcGIS.esriSystem;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    [Export(typeof(IDsGISInitialize))]
    public class EsriInitialize : IDsGISInitialize
    {
        public void GISInitialize()
        {
            try
            {
                //Esri产品绑定
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("绑定ESRI产品时发生错误：{0}", ex.Message));
            }

            try
            {
                //初始化许可证，todo:需补充判断
                IAoInitialize aoInit = new AoInitializeClass();
                aoInit.Initialize(esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("初始化ESRI许可证时发生错误：{0}", ex.Message));
            }
        }

        public void GISShutdown()
        {
            try
            {
                IAoInitialize aoInit = new AoInitializeClass();
                aoInit.Shutdown();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("关闭ESRI许可证时发生错误：{0}", ex.Message));
            }
        }
    }
}

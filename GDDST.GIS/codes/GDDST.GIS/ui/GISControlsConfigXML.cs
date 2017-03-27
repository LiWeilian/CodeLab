using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GDDST.GIS.ui
{
    class GISControlsConfigXML
    {
        private XmlDocument m_xmlDoc = null;
        public GISControlsConfigXML()
        {
            m_xmlDoc = new XmlDocument();
            try
            {
                m_xmlDoc.Load(string.Format("{0}..\\config\\ui.xml", AppDomain.CurrentDomain.BaseDirectory));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("加载界面配置文件错误：{0}", ex.Message));
            }
        }

        private XmlNode GetGISControlsNode()
        {
            XmlNode gisCtrlsNode = null;

            if (m_xmlDoc == null)
            {
                return gisCtrlsNode;
            }
            else
            {
                return m_xmlDoc.DocumentElement.SelectSingleNode("GISControls");
            }
        }

        public GISControlsDef CreateGISControlsDef()
        {
            GISControlsDef gisCtrlsDef = new GISControlsDef();

            XmlNode gisCtrlsNode = GetGISControlsNode();
            if (gisCtrlsNode != null)
            {
                XmlNode gisCtrlsComNode = gisCtrlsNode.SelectSingleNode("Component");
                if (gisCtrlsComNode != null)
                {
                    gisCtrlsDef.NameSpace = gisCtrlsComNode.Attributes["namespace"] != null ? gisCtrlsComNode.Attributes["namespace"].Value : string.Empty;
                }
            }

            return gisCtrlsDef;
        }
    }
}

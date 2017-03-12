using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Controls.Ribbon;

namespace GDDST.GIS.ui
{
    class MainRibbonConfigXML
    {
        private XmlDocument m_xmlDoc = null;

        public MainRibbonConfigXML()
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

        private XmlNode GetMainRibbonNode()
        {
            XmlNode mainRibbonNode = null;

            if (m_xmlDoc == null)
            {
                return mainRibbonNode;
            } else
            {
                return m_xmlDoc.DocumentElement.SelectSingleNode("MainRibbon");
            }
        }

        public MainRibbonDef CreateMainRibbonDef()
        {
            MainRibbonDef mainRibbonDef = new MainRibbonDef();

            XmlNode mainRibbonNode = GetMainRibbonNode();
            if (mainRibbonNode != null)
            {
                XmlNodeList ribbonTabNodes = mainRibbonNode.SelectNodes("RibbonTab");

                foreach (XmlNode ribbonTabNode in ribbonTabNodes)
                {
                    MainRibbonTabDef ribbonTab = new MainRibbonTabDef() {
                        Header = ribbonTabNode.Attributes["header"] != null ? ribbonTabNode.Attributes["header"].Value : string.Empty
                    };

                    XmlNodeList ribbonGroupNodes = ribbonTabNode.SelectNodes("RibbonGroup");
                    foreach (XmlNode ribbonGroupNode in ribbonGroupNodes)
                    {
                        MainRibbonGroupDef ribbonGroup = new MainRibbonGroupDef() {
                            Header = ribbonGroupNode.Attributes["header"] != null ? ribbonGroupNode.Attributes["header"].Value : string.Empty
                        };

                        XmlNodeList ribbonComponentNodes = ribbonGroupNode.SelectNodes("Component");
                        foreach (XmlNode ribbonComNode in ribbonComponentNodes)
                        {
                            MainRibbonComponentDef ribbonCom = new MainRibbonComponentDef();

                            ribbonCom.NameSpace = ribbonComNode.Attributes["namespace"] != null ? ribbonComNode.Attributes["namespace"].Value : string.Empty;
                            ribbonCom.Label = ribbonComNode.Attributes["label"] != null ? ribbonComNode.Attributes["label"].Value : string.Empty;

                            ribbonGroup.RibbonComponents.Add(ribbonCom);
                        }

                        ribbonTab.RibbonGroups.Add(ribbonGroup);
                    }

                    mainRibbonDef.RibbonTabs.Add(ribbonTab);
                }
            }

            return mainRibbonDef;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DS.OPC.Client
{
    class OPCItemMappingConfig
    {
        private XmlDocument m_xmlDoc = null;
        private List<OPCItemMapping> m_OPCItemMappingList;
        public OPCItemMappingConfig(string configFile)
        {
            try
            {
                m_OPCItemMappingList = new List<OPCItemMapping>();

                this.m_xmlDoc = new XmlDocument();
                this.m_xmlDoc.Load(configFile);
                LoadConfiguration();
            }
            catch
            {
                this.m_xmlDoc = null;
            }
            
        }

        public OPCItemMapping GetCurrentOPCItemMappingConfig(string serverHost, string OPCServerName)
        {
            foreach (OPCItemMapping mapping in m_OPCItemMappingList)
            {
                if ((mapping.ServerHost == serverHost) && (mapping.OPCServerName == OPCServerName))
                {   
                    return mapping;
                }
            }

            return null;
        }

        private void LoadConfiguration()
        {
            m_OPCItemMappingList.Clear();

            XmlNodeList serverHostNodes = this.m_xmlDoc.SelectNodes("OPCItemMapping/serverhost");
            foreach (XmlNode serverHostNode in serverHostNodes)
            {
                string serverHost = serverHostNode.Attributes["hostname"].Value;
                foreach (XmlNode OPCServerNode in serverHostNode.ChildNodes)
                {
                    if(OPCServerNode.LocalName.ToLower() == "opcserver")
                    {
                        string opcServerName = OPCServerNode.Attributes["servername"].Value;
                        OPCItemMapping opcItemMapping = new OPCItemMapping();
                        opcItemMapping.ServerHost = serverHost;
                        opcItemMapping.OPCServerName = opcServerName;

                        foreach (XmlNode opcItemNode in OPCServerNode.ChildNodes)
                        {
                            if (opcItemNode.LocalName.ToLower() == "opcitem")
                            {
                                XmlNode mappingNode = opcItemNode.SelectSingleNode("Mapping");
                                if (mappingNode != null)
                                {
                                    //string opcItemID = opcItemNode.Attributes["name"].Value;
                                    OPCItemMappingInfo opcItemMappingInfo = new OPCItemMappingInfo();
                                    opcItemMappingInfo.OPCItemID = opcItemNode.Attributes["name"].Value;

                                    foreach (XmlNode fieldNode in mappingNode.ChildNodes)
                                    {
                                        if (fieldNode.LocalName == "field")
                                        {
                                            OPCGroupDatabaseFieldConfig field = new OPCGroupDatabaseFieldConfig();
                                            //
                                            field.FieldName = fieldNode.Attributes["fieldname"].Value;
                                            //
                                            int sourceOPCItem = 0;
                                            field.SourceOPCItem = int.TryParse(fieldNode.Attributes["sourceopcitem"].Value, out sourceOPCItem)
                                                && Enum.IsDefined(typeof(OPCClientDBFieldMapping.EnumOPCItem), sourceOPCItem)
                                                ? sourceOPCItem : (int)OPCClientDBFieldMapping.EnumOPCItem.未设置;
                                            //
                                            field.SourceCustom = fieldNode.Attributes["sourcecustom"].Value;
                                            //
                                            int dataType = 0;
                                            field.DataType = int.TryParse(fieldNode.Attributes["datatype"].Value, out dataType)
                                                && Enum.IsDefined(typeof(OPCClientDBFieldMapping.EnumDataType), dataType)
                                                ? dataType : (int)OPCClientDBFieldMapping.EnumDataType.CHAR;
                                            //
                                            field.SEQName = fieldNode.Attributes["seqname"].Value;
                                            //
                                            int autoInc = 0;
                                            field.AutoInc = int.TryParse(fieldNode.Attributes["autoinc"].Value, out autoInc)
                                                && Enum.IsDefined(typeof(OPCClientDBFieldMapping.EnumAutoInc), autoInc)
                                                ? autoInc : (int)OPCClientDBFieldMapping.EnumAutoInc.NO;
                                            //
                                            int isEntityId = 0;
                                            field.IsEntityIdentity = int.TryParse(fieldNode.Attributes["isentityidentity"].Value, out isEntityId)
                                                && Enum.IsDefined(typeof(OPCClientDBFieldMapping.EnumIsEntityIdentity), isEntityId)
                                                ? isEntityId : (int)OPCClientDBFieldMapping.EnumIsEntityIdentity.NO;

                                            if (field.FieldName != null & field.FieldName.Trim() != string.Empty)
                                            {
                                                opcItemMappingInfo.DBFieldConfigList.Add(field);
                                            }
                                        }
                                    }
                                    opcItemMapping.OPCItemList.Add(opcItemMappingInfo);
                                }
                            }
                        }
                        m_OPCItemMappingList.Add(opcItemMapping);
                    }
                }
            }
        }

        private string GetServerHost(out XmlNode serverHostNode)
        {
            serverHostNode = this.m_xmlDoc.SelectSingleNode("OPCItemMapping/serverhost");
            return serverHostNode != null ? serverHostNode.Attributes["hostname"].Value : string.Empty;
        }

        private string GetOPCServerName(out XmlNode opcServerNode)
        {
            opcServerNode = this.m_xmlDoc.SelectSingleNode("OPCItemMapping/serverhost/opcserver");
            return opcServerNode != null ? opcServerNode.Attributes["servername"].Value : string.Empty;
        }
    }
}

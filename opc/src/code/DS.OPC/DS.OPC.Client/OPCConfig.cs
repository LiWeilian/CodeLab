using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace DS.OPC.Client
{
    class OPCConfig
    {
        private XmlDocument m_xmlDoc = new XmlDocument();

        public bool IsValid { get { return this.m_xmlDoc != null; } }
        public string ServerHost { get; set; }
        public string OPCServerName { get; set; }
        public List<OPCGroupConfig> Groups { get; private set; }
        public OPCConfig(string configFile)
        {
            try
            {
                m_xmlDoc.Load(configFile);
                LoadConfiguration();
            }
            catch
            {
                this.m_xmlDoc = null;
            }
        }

        public OPCConfig()
        {
            Groups = new List<OPCGroupConfig>();
        }

        #region 读取设置
        private void LoadConfiguration()
        {
            XmlNode serverHostNode;
            ServerHost = GetServerHost(out serverHostNode);
            XmlNode opcServerNode;
            OPCServerName = GetOPCServerName(out opcServerNode);
            Groups = GetOPCGroupsConfig(opcServerNode);
        }

        private string GetServerHost(out XmlNode serverHostNode)
        {
            serverHostNode = this.m_xmlDoc.SelectSingleNode("opcclient/serverhost");
            return serverHostNode != null ? serverHostNode.Attributes["hostname"].Value : string.Empty;
        }

        private string GetOPCServerName(out XmlNode opcServerNode)
        {
            opcServerNode = this.m_xmlDoc.SelectSingleNode("opcclient/serverhost/opcserver");
            return opcServerNode != null ? opcServerNode.Attributes["servername"].Value : string.Empty;
        }

        private List<OPCGroupConfig> GetOPCGroupsConfig(XmlNode opcServerNode)
        {
            List<OPCGroupConfig> groups = new List<OPCGroupConfig>();

            foreach (XmlNode opcGroupNode in opcServerNode.ChildNodes)
            {
                OPCGroupConfig group = GetOPCGroupConfig(opcGroupNode);
                if (group != null)
                {
                    groups.Add(group);
                }
            }

            return groups;
        }

        private OPCGroupConfig GetOPCGroupConfig(XmlNode opcGroupNode)
        {
            if (opcGroupNode == null)
            {
                return null;
            }

            OPCGroupConfig group = new OPCGroupConfig();
            group.GroupName = opcGroupNode.Attributes["groupname"].Value;
            group.IsActive = opcGroupNode.Attributes["isactive"].Value == "1";
            group.IsSubscribed = opcGroupNode.Attributes["issubscribed"].Value == "1";
            group.AutoInsert = opcGroupNode.Attributes["autoinsert"].Value == "1";
            float deadband = 0;
            group.Deadband = float.TryParse(opcGroupNode.Attributes["deadband"].Value, out deadband) && deadband >= 0
                ? deadband : 0;
            int updateRate = 0;
            group.UpdateRate = int.TryParse(opcGroupNode.Attributes["updaterate"].Value, out updateRate) && updateRate > 0
                ? updateRate : 1000;
            int timeoutsetting = 0;
            group.TimeoutSetting = int.TryParse(opcGroupNode.Attributes["timeoutsetting"].Value, out timeoutsetting) && timeoutsetting >= 0
                ? timeoutsetting : 0;

            foreach (XmlNode node in opcGroupNode.ChildNodes)
            {
                switch (node.LocalName)
                {
                    case "opcitems":
                        List<string> opcItems = GetOPCGroupItems(node);
                        foreach (string opcItem in opcItems)
                        {
                            group.OPCItems.Add(opcItem);
                        }
                        break;
                    case "database-connection":
                        OPCGroupDatabaseConnectionConfig dbConnConfig = GetOPCDBConnectionConfig(node);

                        group.DBConnection.DatabaseType = dbConnConfig.DatabaseType;
                        group.DBConnection.ServerName = dbConnConfig.ServerName;
                        group.DBConnection.DatabaseName = dbConnConfig.DatabaseName;
                        group.DBConnection.UserName = dbConnConfig.UserName;
                        group.DBConnection.Password = dbConnConfig.Password;
                        group.DBConnection.NewDataTable = dbConnConfig.NewDataTable;
                        group.DBConnection.HistoryDataTable = dbConnConfig.HistoryDataTable;

                        group.DBConnection.Fields.Clear();
                        foreach (OPCGroupDatabaseFieldConfig field in dbConnConfig.Fields)
                        {
                            group.DBConnection.Fields.Add(field);
                        }
                        break;
                    default:
                        break;
                }
            }

            return group;
        }

        private List<string> GetOPCGroupItems(XmlNode opcGroupItemsNode)
        {
            List<string> items = new List<string>();
            foreach (XmlNode itemNode in opcGroupItemsNode)
            {
                if (itemNode.LocalName == "opcitem")
                {
                    items.Add(itemNode.Attributes["itemid"].Value);
                }
            }
            return items;
        }

        private OPCGroupDatabaseConnectionConfig GetOPCDBConnectionConfig(XmlNode opcGroupDBConnNode)
        {
            OPCGroupDatabaseConnectionConfig connConfig = new OPCGroupDatabaseConnectionConfig();

            int databaseType = 0;
            connConfig.DatabaseType = int.TryParse(opcGroupDBConnNode.Attributes["databasetype"].Value, out databaseType) && databaseType >= 0 && databaseType <= 1
                ? databaseType : 0;

            foreach (XmlNode connNode in opcGroupDBConnNode)
            {
                switch (connNode.LocalName)
                {
                    case "connection":
                        connConfig.ServerName = connNode.Attributes["servername"].Value;
                        connConfig.DatabaseName = connNode.Attributes["databasename"].Value;
                        connConfig.UserName = connNode.Attributes["username"].Value;
                        connConfig.Password = connNode.Attributes["password"].Value;
                        break;
                    case "newdatatable":
                        connConfig.NewDataTable = connNode.Attributes["tablename"].Value;
                        break;
                    case "historydatatable":
                        connConfig.HistoryDataTable = connNode.Attributes["tablename"].Value;
                        break;
                    case "fields":
                        List<OPCGroupDatabaseFieldConfig> fields = GetOPCDBFieldsConfig(connNode);
                        foreach (OPCGroupDatabaseFieldConfig fieldConfig in fields)
                        {
                            connConfig.Fields.Add(fieldConfig);
                        }
                        break;
                    default:
                        break;
                }
            }

            return connConfig;
        }

        private List<OPCGroupDatabaseFieldConfig> GetOPCDBFieldsConfig(XmlNode opcGroupDBFieldsNode)
        {
            List<OPCGroupDatabaseFieldConfig> fields = new List<OPCGroupDatabaseFieldConfig>();

            foreach (XmlNode fieldNode in opcGroupDBFieldsNode)
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
                        fields.Add(field);
                    }
                }
            }

            return fields;
        }
        #endregion

        #region 保存设置
        public void SaveConfiguration(string saveFile)
        {
            if (this.m_xmlDoc == null)
            {
                this.m_xmlDoc = new XmlDocument();
            }

            //声明 <?xml version="1.0" encoding="GBK" ?>
            XmlDeclaration xmldeclaration = m_xmlDoc.CreateXmlDeclaration("1.0", "GBK", "yes");
            m_xmlDoc.AppendChild(xmldeclaration);

            //根节点 <opcclient/>
            XmlNode root = m_xmlDoc.CreateElement("opcclient");
            m_xmlDoc.AppendChild(root);

            //服务器宿主 <serverhost hostname="xxx" />
            if (ServerHost.Trim() == string.Empty)
                return;
            XmlNode serverHostNode = m_xmlDoc.CreateElement("serverhost");
            XmlAttribute hostNameAttr = m_xmlDoc.CreateAttribute("hostname");
            hostNameAttr.Value = ServerHost;
            serverHostNode.Attributes.Append(hostNameAttr);
            root.AppendChild(serverHostNode);

            //服务器 <opcserver servername="" />
            if (OPCServerName.Trim() == string.Empty)
                return;
            XmlNode opcServerNode = m_xmlDoc.CreateElement("opcserver");
            XmlAttribute opcServerNameAttr = m_xmlDoc.CreateAttribute("servername");
            opcServerNameAttr.Value = OPCServerName;
            opcServerNode.Attributes.Append(opcServerNameAttr);
            serverHostNode.AppendChild(opcServerNode);

            //OPCGroup <opcgroup groupname="xx" deadband="0" updaterate="1000" isactive="1" issubscribed="1" timeoutsetting="120" autoinsert="1"/>
            foreach (OPCGroupConfig groupCfg in Groups)
            {
                if (groupCfg.GroupName == null || groupCfg.GroupName.Trim() == string.Empty)
                    continue;

                XmlNode opcGroupNode = m_xmlDoc.CreateElement("opcgroup");
                opcServerNode.AppendChild(opcGroupNode);

                //groupname
                XmlAttribute opcGroupNameAttr = m_xmlDoc.CreateAttribute("groupname");
                opcGroupNameAttr.Value = groupCfg.GroupName;
                opcGroupNode.Attributes.Append(opcGroupNameAttr);
                //deadband
                XmlAttribute opcDeadbandAttr = m_xmlDoc.CreateAttribute("deadband");
                opcDeadbandAttr.Value = groupCfg.Deadband.ToString();
                opcGroupNode.Attributes.Append(opcDeadbandAttr);
                //updaterate
                XmlAttribute opcUpdateRateAttr = m_xmlDoc.CreateAttribute("updaterate");
                opcUpdateRateAttr.Value = groupCfg.UpdateRate.ToString();
                opcGroupNode.Attributes.Append(opcUpdateRateAttr);
                //isactive
                XmlAttribute opcIsActiveAttr = m_xmlDoc.CreateAttribute("isactive");
                opcIsActiveAttr.Value = Convert.ToInt32(groupCfg.IsActive).ToString();
                opcGroupNode.Attributes.Append(opcIsActiveAttr);
                //issubscribed
                XmlAttribute opcIsSubscribedAttr = m_xmlDoc.CreateAttribute("issubscribed");
                opcIsSubscribedAttr.Value = Convert.ToInt32(groupCfg.IsSubscribed).ToString();
                opcGroupNode.Attributes.Append(opcIsSubscribedAttr);
                //timeoutsetting
                XmlAttribute opcTimeoutSettingAttr = m_xmlDoc.CreateAttribute("timeoutsetting");
                opcTimeoutSettingAttr.Value = groupCfg.TimeoutSetting.ToString();
                opcGroupNode.Attributes.Append(opcTimeoutSettingAttr);
                //autoinsert
                XmlAttribute opcAutoInsertAttr = m_xmlDoc.CreateAttribute("autoinsert");
                opcAutoInsertAttr.Value = Convert.ToInt32(groupCfg.AutoInsert).ToString();
                opcGroupNode.Attributes.Append(opcAutoInsertAttr);

                //OPCItems
                XmlNode opcItemsNode = m_xmlDoc.CreateElement("opcitems");
                opcGroupNode.AppendChild(opcItemsNode);
                foreach (string opcItem in groupCfg.OPCItems)
                {
                    XmlNode opcItemNode = m_xmlDoc.CreateElement("opcitem");
                    XmlAttribute itemIDAttr = m_xmlDoc.CreateAttribute("itemid");
                    itemIDAttr.Value = opcItem;
                    opcItemNode.Attributes.Append(itemIDAttr);
                    opcItemsNode.AppendChild(opcItemNode);
                }

                //database-connection <database-connection databasetype="0" />
                XmlNode dbConnNode = m_xmlDoc.CreateElement("database-connection");
                XmlAttribute databasetTypeAttr = m_xmlDoc.CreateAttribute("databasetype");
                databasetTypeAttr.Value = groupCfg.DBConnection.DatabaseType.ToString();
                dbConnNode.Attributes.Append(databasetTypeAttr);
                opcGroupNode.AppendChild(dbConnNode);

                //connection
                XmlNode connNode = m_xmlDoc.CreateElement("connection");
                XmlAttribute dbServerNameAttr = m_xmlDoc.CreateAttribute("servername");
                dbServerNameAttr.Value = groupCfg.DBConnection.ServerName;
                connNode.Attributes.Append(dbServerNameAttr);
                XmlAttribute databaseNameAttr = m_xmlDoc.CreateAttribute("databasename");
                databaseNameAttr.Value = groupCfg.DBConnection.DatabaseName;
                connNode.Attributes.Append(databaseNameAttr);
                XmlAttribute userNameAttr = m_xmlDoc.CreateAttribute("username");
                userNameAttr.Value = groupCfg.DBConnection.UserName;
                connNode.Attributes.Append(userNameAttr);
                XmlAttribute passwordAttr = m_xmlDoc.CreateAttribute("password");
                passwordAttr.Value = groupCfg.DBConnection.Password;
                connNode.Attributes.Append(passwordAttr);
                dbConnNode.AppendChild(connNode);
                //newdatatable
                XmlNode newDataTableNode = m_xmlDoc.CreateElement("newdatatable");
                XmlAttribute newDataTableNameAttr = m_xmlDoc.CreateAttribute("tablename");
                newDataTableNameAttr.Value = groupCfg.DBConnection.NewDataTable;
                newDataTableNode.Attributes.Append(newDataTableNameAttr);
                dbConnNode.AppendChild(newDataTableNode);
                //historydatatable
                XmlNode historyDataTableNode = m_xmlDoc.CreateElement("historydatatable");
                XmlAttribute historyDataTableNameAttr = m_xmlDoc.CreateAttribute("tablename");
                historyDataTableNameAttr.Value = groupCfg.DBConnection.HistoryDataTable;
                historyDataTableNode.Attributes.Append(historyDataTableNameAttr);
                dbConnNode.AppendChild(historyDataTableNode);

                //fields
                XmlNode fieldsNode = m_xmlDoc.CreateElement("fields");
                dbConnNode.AppendChild(fieldsNode);
                foreach (OPCGroupDatabaseFieldConfig fieldCfg in groupCfg.DBConnection.Fields)
                {
                    XmlNode fieldNode = m_xmlDoc.CreateElement("field");
                    fieldsNode.AppendChild(fieldNode);

                    XmlAttribute fieldNameAttr = m_xmlDoc.CreateAttribute("fieldname");
                    fieldNameAttr.Value = fieldCfg.FieldName;
                    fieldNode.Attributes.Append(fieldNameAttr);

                    XmlAttribute sourceOPCItemAttr = m_xmlDoc.CreateAttribute("sourceopcitem");
                    sourceOPCItemAttr.Value = fieldCfg.SourceOPCItem.ToString();
                    fieldNode.Attributes.Append(sourceOPCItemAttr);

                    XmlAttribute sourceCustomAttr = m_xmlDoc.CreateAttribute("sourcecustom");
                    sourceCustomAttr.Value = fieldCfg.SourceCustom;
                    fieldNode.Attributes.Append(sourceCustomAttr);

                    XmlAttribute dataTypeAttr = m_xmlDoc.CreateAttribute("datatype");
                    dataTypeAttr.Value = fieldCfg.DataType.ToString();
                    fieldNode.Attributes.Append(dataTypeAttr);

                    XmlAttribute seqNameAttr = m_xmlDoc.CreateAttribute("seqname");
                    seqNameAttr.Value = fieldCfg.SEQName;
                    fieldNode.Attributes.Append(seqNameAttr);

                    XmlAttribute autoIncAttr = m_xmlDoc.CreateAttribute("autoinc");
                    autoIncAttr.Value = fieldCfg.AutoInc.ToString();
                    fieldNode.Attributes.Append(autoIncAttr);

                    XmlAttribute isEntityIdAttr = m_xmlDoc.CreateAttribute("isentityidentity");
                    isEntityIdAttr.Value = fieldCfg.IsEntityIdentity.ToString();
                    fieldNode.Attributes.Append(isEntityIdAttr);
                }
            }

            //保存
            m_xmlDoc.Save(saveFile);
        }

        #endregion
    }

    class OPCGroupConfig
    {
        public string GroupName { get; set; }
        public float Deadband { get; set; }
        public int UpdateRate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSubscribed { get; set; }
        public int TimeoutSetting { get; set; }
        public bool AutoInsert { get; set; }
        public List<string> OPCItems { get; private set; }
        public OPCGroupDatabaseConnectionConfig DBConnection { get; private set; }

        public OPCGroupConfig()
        {
            OPCItems = new List<string>();
            DBConnection = new OPCGroupDatabaseConnectionConfig();
            Deadband = 0;
            UpdateRate = 1000;
            IsActive = true;
            IsSubscribed = true;
            TimeoutSetting = 0;
        }
    }

    class OPCGroupDatabaseConnectionConfig
    {
        public int DatabaseType { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewDataTable { get; set; }
        public string HistoryDataTable { get; set; }
        public List<OPCGroupDatabaseFieldConfig> Fields { get; private set; }
        public OPCGroupDatabaseConnectionConfig()
        {
            Fields = new List<OPCGroupDatabaseFieldConfig>();
            DatabaseType = 0;
        }
    }

    class OPCGroupDatabaseFieldConfig
    {
        public string FieldName { get; set; }
        public int SourceOPCItem { get; set; }
        public string SourceCustom { get; set; }
        public int DataType { get; set; }
        public string SEQName { get; set; }
        public int AutoInc { get; set; }
        public int IsEntityIdentity { get; set; }
        public OPCGroupDatabaseFieldConfig()
        {
            SourceOPCItem = 0;
            DataType = 2;
            AutoInc = 0;
            IsEntityIdentity = 0;
        }
    }
}

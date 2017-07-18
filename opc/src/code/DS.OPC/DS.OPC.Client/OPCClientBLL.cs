using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPCAutomation;
using System.Windows.Forms;

namespace DS.OPC.Client
{
    class OPCClientBLL
    {
        private OPCClientDAL m_dal = null;

        public List<OPCItem> DataChangedOPCItems { get; private set; }
        public List<OPCClientDBFieldMapping> DBFieldMappings { get; private set; }
        /// <summary>
        /// 上一次更新数据时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 是否自动检查OPC组的状态，以此判断能否正常获取数据
        /// </summary>
        public bool AutoCheckUpdateStatus { get; set; }

        /// <summary>
        /// 某些OPC项的指定映射配置，如在这里设置，OPC项将不使用通用配置
        /// </summary>
        public OPCItemMapping OPCItemMapping { get; set; }

        /// <summary>
        /// 超时时间，单位为秒
        /// </summary>
        public int TimeoutSetting { get; set; }
        public OPCUtils.DatabaseType DatabaseType { get; set; }
        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewDataTableName { get; set; }
        public string HistoryDataTableName { get; set; }
        public bool AutoInsert { get; set; }
        
        public OPCGroup BindGroup { get; set; }

        public OPCClientBLL()
        {
            DataChangedOPCItems = new List<OPCItem>();
            DBFieldMappings = new List<OPCClientDBFieldMapping>();
            LastUpdateTime = DateTime.Now;
            AutoCheckUpdateStatus = false;
            TimeoutSetting = 300;
        }
        public void RefreshDBConnectionSetting()
        {
            this.m_dal = null;
        }

        public OPCItem GetOPCItemByClientHandle(int clientHandle)
        {
            foreach (OPCItem item in this.BindGroup.OPCItems)
            {
                if (clientHandle == item.ClientHandle)
                {
                    return item;
                }
            }

            return null;
        }

        public bool CheckIsTimeout(DateTime checkTime)
        {
            if (AutoCheckUpdateStatus)
            {
                TimeSpan ts = checkTime.Subtract(LastUpdateTime);
                if (TimeoutSetting > 0 && ts.TotalSeconds >= TimeoutSetting)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查OPC项是否有映射配置
        /// </summary>
        /// <returns></returns>
        private bool CheckIfOPCItemDataHasMappingConfig(OPCItemData itemData, out OPCItemMappingInfo opcMappingItem)
        {
            opcMappingItem = null;
            foreach (OPCItemMappingInfo item in OPCItemMapping.OPCItemList)
            {
                if (item.OPCItemID == itemData.ItemID)
                {
                    opcMappingItem = item;
                    return true;
                }
            }
            return false;
        }

        private List<OPCClientItemEntity> GetEntities(List<OPCItemData> itemDataList)
        {
            List<OPCClientItemEntity> itemEntities = new List<OPCClientItemEntity>();
            try
            {
                List<OPCClientDBFieldMapping> dbFieldMappingList;
                foreach (OPCItemData itemData in itemDataList)
                {
                    OPCClientItemEntity itemEntity = new OPCClientItemEntity();
                    //如果是特殊的OPC项，则使用配置的映射
                    OPCItemMappingInfo opcItemMappingInfo = null;
                    if (OPCItemMapping != null)
                    {
                        opcItemMappingInfo = OPCItemMapping.GetOPCItemMappingInfo(itemData.ItemID);
                    }
                    if (opcItemMappingInfo != null)
                    {
                        dbFieldMappingList = opcItemMappingInfo.DBFieldMappingList;
                    } else
                    {
                        dbFieldMappingList = this.DBFieldMappings;
                    }

                    #region 设置数据项实体的各属性
                    string[] ItemIDCol = itemData.ItemID.Split('.');

                    foreach (OPCClientDBFieldMapping fieldMapping in dbFieldMappingList)
                    {
                        OPCClientItemProperty itemProp = new OPCClientItemProperty();

                        //字段名称
                        itemProp.Name = fieldMapping.FieldName;

                        //数据类型
                        itemProp.DataType = (OPCClientDBFieldMapping.EnumDataType)fieldMapping.DataType;

                        //从自定义数据源获取字段值
                        if (fieldMapping.SourceCustom.Trim() != string.Empty)
                        {

                            //以":"开头为变量，否则为常量
                            //当为变量时，以":[0]"开头，为系统变量，不依赖当前程序和数据，如:[0]:GUID，:[0]:DateTime等。
                            //以":[1]"开头，根据同一传感器的其他值按规则获取相应值
                            //以":[2]"开头，根据同一个站点的其他传感器的数据按规则获取相应值
                            string prefix = string.Empty;
                            string prefix2 = string.Empty;
                            string varStr = string.Empty;
                            if (fieldMapping.SourceCustom.Length >= 4)
                            {
                                prefix = fieldMapping.SourceCustom.Substring(0, 4);
                                varStr = fieldMapping.SourceCustom.Substring(4, fieldMapping.SourceCustom.Length - 4);
                            }

                            if (fieldMapping.SourceCustom.Length >= 1)
                            {
                                prefix2 = fieldMapping.SourceCustom.Substring(0, 1);
                            }

                            string itemValue = string.Empty;
                            switch (prefix)
                            {
                                case ":[0]":  //如":[0]:GUID"可以获取系统生成的GUID，":[0]:NOW"可以获取当前时间。
                                    itemValue = OPCUtils.GetCustomSystemValue(varStr);
                                    break;
                                case ":[1]":  //如":[1]:SENSOR_NAME"，可以从数据实体中获取属性"SENSOR_NAME"的值，然后根据规则返回所需的值。
                                    string fieldName = varStr.Substring(1, varStr.Length - 1);
                                    IEnumerable<OPCClientItemProperty> tempProps =
                                        itemEntity.Properties.Where(x => x.Name.ToUpper() == fieldName.ToUpper());
                                    if (tempProps != null && tempProps.Count() > 0)
                                    {
                                        itemValue = OPCUtils.GetCustomValueByItemProperty(fieldName, tempProps.First().Value);
                                    }

                                    break;
                                case ":[2]":  //如":[2]:更新时间"，可以从数据列表中找到"local.x02.更新时间"，从中获取Item Value.
                                    string itemName = varStr.Substring(1, varStr.Length - 1);
                                    if (ItemIDCol.Length > 0)
                                    {
                                        string queryItemID = string.Empty;
                                        for (int i = 0; i < ItemIDCol.Length - 1; i++)
                                        {
                                            if (queryItemID == string.Empty)
                                            {
                                                queryItemID = ItemIDCol[i];
                                            }
                                            else
                                            {
                                                queryItemID = queryItemID + "." + ItemIDCol[i];
                                            }
                                        }
                                        if (queryItemID == string.Empty)
                                        {
                                            queryItemID = itemName;
                                        }
                                        else
                                        {
                                            queryItemID = queryItemID + "." + itemName;
                                        }

                                        IEnumerable<OPCItemData> tempItemDataList =
                                            itemDataList.Where(x => x.ItemID.ToUpper() == queryItemID.ToUpper());
                                        if (tempItemDataList != null && tempItemDataList.Count() > 0)
                                        {
                                            itemValue = tempItemDataList.First().Value;
                                        }

                                    }
                                    break;
                                default:
                                    if (prefix2 == ":")
                                    {
                                        //如果是变量，默认获取系统变量
                                        itemValue = OPCUtils.GetCustomSystemValue(fieldMapping.SourceCustom);
                                    }
                                    else
                                    {
                                        //常量
                                        itemValue = fieldMapping.SourceCustom;
                                    }

                                    break;
                            }

                            itemProp.Value = itemValue;
                        }


                        //从OPCItem属性获取字段值
                        if (fieldMapping.SourceOPCItem > 0 && (itemProp.Value == null || itemProp.Value.Trim() == string.Empty))
                        {
                            switch ((OPCClientDBFieldMapping.EnumOPCItem)fieldMapping.SourceOPCItem)
                            {
                                case OPCClientDBFieldMapping.EnumOPCItem.未设置:
                                    itemProp.Value = string.Empty;
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_VALUE:
                                    itemProp.Value = itemData.Value;
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_QUALITY:
                                    itemProp.Value = itemData.Quality;
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_TIMESTAMP:
                                    itemProp.Value = itemData.Timestamp;
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_SERVERHANDLE:
                                    itemProp.Value = itemData.ServerHandle;
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_CLIENTHANDLE:
                                    itemProp.Value = itemData.ClientHandle;
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID:
                                    itemProp.Value = itemData.ItemID;
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_0:
                                    if (ItemIDCol.Length > 0)
                                    {
                                        itemProp.Value = ItemIDCol[0];
                                    }
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_1:
                                    if (ItemIDCol.Length > 1)
                                    {
                                        itemProp.Value = ItemIDCol[1];
                                    }
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_2:
                                    if (ItemIDCol.Length > 2)
                                    {
                                        itemProp.Value = ItemIDCol[2];
                                    }
                                    break;
                                case OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_3:
                                    if (ItemIDCol.Length > 3)
                                    {
                                        itemProp.Value = ItemIDCol[3];
                                    }
                                    break;
                                default:
                                    itemProp.Value = string.Empty;
                                    break;
                            }
                        }

                        //取值序列
                        itemProp.SEQ_Name = fieldMapping.SeqName;

                        //是否自增
                        itemProp.AutoInc = Convert.ToBoolean(fieldMapping.AutoInc);

                        //是否标识，用于判断是否同一个站点、传感器
                        itemProp.IsEntityIdentity = Convert.ToBoolean(fieldMapping.IsEntityIdentity);

                        bool propIsValid = false;
                        switch (itemProp.DataType)
                        {
                            case OPCClientDBFieldMapping.EnumDataType.INT:
                                int checkInt;
                                propIsValid = int.TryParse(itemProp.Value, out checkInt);
                                break;
                            case OPCClientDBFieldMapping.EnumDataType.NUMERIC:
                                double checkDouble;
                                propIsValid = double.TryParse(itemProp.Value, out checkDouble);
                                break;
                            case OPCClientDBFieldMapping.EnumDataType.CHAR:
                                propIsValid = true;
                                break;
                            case OPCClientDBFieldMapping.EnumDataType.NCHAR:
                                propIsValid = true;
                                break;
                            case OPCClientDBFieldMapping.EnumDataType.DATETIME:
                                DateTime checkDateTime;
                                propIsValid = DateTime.TryParse(itemProp.Value, out checkDateTime);
                                break;
                            case OPCClientDBFieldMapping.EnumDataType.BINARY:
                                propIsValid = true;
                                break;
                            default:
                                propIsValid = false;
                                break;
                        }

                        if (!propIsValid)
                        {
                            itemEntity = null;
                            break;
                        }
                        else
                        {
                            itemEntity.Properties.Add(itemProp);
                        }
                    }
                    #endregion

                    if (itemEntity != null)
                    {
                        itemEntities.Add(itemEntity);
                    }
                }
            } catch (Exception e)
            {
                OPCLog.Error(string.Format("获取OPC数据项映射配置时发生错误：{0}", e.Message));
            }
            

            return itemEntities;
        }
        /// <summary>
        /// 将更新前数据的异常状态同步到更新后数据
        /// </summary>
        /// <param name="itemEntity"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private void SyncSensorStatus(OPCClientItemEntity itemEntity, string status)
        {
            foreach (OPCClientItemProperty prop in itemEntity.Properties)
            {
                if (prop.Name == OPCUtils.SENSOR_STATUS_FIELD)
                {
                    prop.Value = status;
                }
            }
        }
        public bool UpdateNewDataTable(List<OPCItemData> itemDataList, out string errMsg)
        {
            //MessageBox.Show("NewDataTable ItemDataList: " + itemDataList.Count.ToString());
            errMsg = string.Empty;

            #region 检查数据连接设置
            if (ServerName.Trim() == string.Empty
                || UserName.Trim() == string.Empty
                || Password.Trim() == string.Empty
                || NewDataTableName.Trim() == string.Empty)
            {
                errMsg = "数据库连接设置无效";
                OPCLog.Error("数据库连接设置无效");
                return false;
            }
            if (this.m_dal == null)
            {
                switch (DatabaseType)
                {
                    case OPCUtils.DatabaseType.MSSQLServer:
                        this.m_dal = new OPCClientMSSQLDAL(ServerName, DatabaseName, UserName, Password);
                        break;
                    case OPCUtils.DatabaseType.Oracle:
                        this.m_dal = new OPCClientOracleDAL(ServerName, UserName, Password);
                        break;
                    default:
                        break;
                }
                if (!this.m_dal.CreateConnection(out errMsg))
                    return false;
            }
            #endregion

            #region 清空原有实时数据
            /*
            if (!this.m_dal.ClearItems(NewDataTableName, out errMsg))
            {
                return false;
            }
            */
            #endregion

            List<OPCClientItemEntity> itemEntities = GetEntities(itemDataList);

            foreach (OPCClientItemEntity itemEntity in itemEntities)
            {
                string dalErr = string.Empty;
                string status = this.m_dal.QueryItemDataStatus(NewDataTableName, itemEntity, out dalErr);
                if (status != string.Empty)
                {
                    SyncSensorStatus(itemEntity, status);
                }
                if (!this.m_dal.DeleteItem(NewDataTableName, itemEntity, out dalErr) ||
                    !this.m_dal.InsertItem(NewDataTableName, itemEntity, out dalErr))
                {
                    errMsg = dalErr;
                }                
            }

            return true;
        }

        public bool InsertToHistoryDataTable(List<OPCItemData> itemDataList, out string errMsg)
        {
            errMsg = string.Empty;

            #region 检查数据连接设置
            if (ServerName.Trim() == string.Empty
                || UserName.Trim() == string.Empty
                || Password.Trim() == string.Empty
                || NewDataTableName.Trim() == string.Empty)
            {
                errMsg = "数据库连接设置无效";
                OPCLog.Error("数据库连接设置无效");
                return false;
            }
            if (this.m_dal == null)
            {
                switch (DatabaseType)
                {
                    case OPCUtils.DatabaseType.MSSQLServer:
                        this.m_dal = new OPCClientMSSQLDAL(ServerName, DatabaseName, UserName, Password);
                        break;
                    case OPCUtils.DatabaseType.Oracle:
                        this.m_dal = new OPCClientOracleDAL(ServerName, UserName, Password);
                        break;
                    default:
                        break;
                }
                if (!this.m_dal.CreateConnection(out errMsg))
                    return false;
            }
            #endregion
            //OPCUtils.LogDataChangeTime("数据刷新-插入历史数据-插入数据表-获取数据实体");
            List<OPCClientItemEntity> itemEntities = GetEntities(itemDataList);

            //OPCUtils.LogDataChangeTime("数据刷新-插入历史数据-插入数据表-开始插入");
            foreach (OPCClientItemEntity itemEntity in itemEntities)
            {
                //OPCUtils.LogDataChangeTime("数据刷新-插入历史数据-插入数据表-插入实体");
                string dalErr = string.Empty;
                if (!this.m_dal.InsertItem(HistoryDataTableName, itemEntity, out dalErr))
                {
                    errMsg = dalErr;
                }
            }
            //OPCUtils.LogDataChangeTime("数据刷新-插入历史数据-插入数据表-结束插入");
            return true;
        }

    }
}

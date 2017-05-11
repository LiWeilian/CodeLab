using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.OPC.Client
{
    class OPCItemMappingInfo
    {
        public string OPCItemID { get; set; }
        public List<OPCGroupDatabaseFieldConfig> DBFieldConfigList { get; private set; }
        public List<OPCClientDBFieldMapping> DBFieldMappingList {
            get 
            {
                List<OPCClientDBFieldMapping> result = new List<OPCClientDBFieldMapping>();
                foreach (OPCGroupDatabaseFieldConfig fieldCfg in DBFieldConfigList)
                {
                    OPCClientDBFieldMapping fieldMapping = new OPCClientDBFieldMapping();
                    fieldMapping.FieldName = fieldCfg.FieldName;
                    fieldMapping.SourceOPCItem = fieldCfg.SourceOPCItem;
                    fieldMapping.SourceCustom = fieldCfg.SourceCustom;
                    fieldMapping.DataType = fieldCfg.DataType;
                    fieldMapping.SeqName = fieldCfg.SEQName;
                    fieldMapping.AutoInc = fieldCfg.AutoInc;
                    fieldMapping.IsEntityIdentity = fieldCfg.IsEntityIdentity;

                    result.Add(fieldMapping);
                }
                return result; 
            }
        }
        public OPCItemMappingInfo()
        {
            DBFieldConfigList = new List<OPCGroupDatabaseFieldConfig>();
        }
    }
    class OPCItemMapping
    {
        
        public string ServerHost { get; set; }
        public string OPCServerName { get; set; }
        public List<OPCItemMappingInfo> OPCItemList { get; private set; }

        public OPCItemMapping()
        {
            OPCItemList = new List<OPCItemMappingInfo>();
        }

        public OPCItemMappingInfo GetOPCItemMappingInfo(string opcItemID)
        {
            foreach (OPCItemMappingInfo item in OPCItemList)
            {
                if (item.OPCItemID.Equals(opcItemID, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }

            return null;
        }
    }
}

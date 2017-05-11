using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.OPC.Client
{
    /// <summary>
    /// OPC数据库实体单个属性信息
    /// </summary>
    class OPCClientItemProperty
    {
        public enum EnumDataType
        {
            INT         = 0,
            NUMERIC     = 1,
            CHAR        = 2,
            NCHAR       = 3,
            DATETIME    = 4,
            BINARY      = 5

        }
        public string Name { get; set; }
        public string Value { get; set; }
        public OPCClientDBFieldMapping.EnumDataType DataType { get; set; }
        public string SEQ_Name { get; set; }
        public bool AutoInc { get; set; }

        public bool IsEntityIdentity { get; set; }
    }
}

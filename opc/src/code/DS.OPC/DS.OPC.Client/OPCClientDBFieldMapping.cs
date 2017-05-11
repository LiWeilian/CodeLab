using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.OPC.Client
{
    /// <summary>
    /// 数据库字段映射配置，用于数据库字段配置界面
    /// </summary>
    class OPCClientDBFieldMapping
    {
        public enum EnumOPCItem
        {
            未设置 = 0,//被迫如此，如果被打就改成英文
            ITEM_VALUE = 1,
            ITEM_QUALITY = 2,
            ITEM_TIMESTAMP = 3,
            ITEM_SERVERHANDLE = 4,
            ITEM_CLIENTHANDLE = 5,
            ITEM_ID = 6,
            ITEM_ID_0 = 7,
            ITEM_ID_1 = 8,
            ITEM_ID_2 = 9,
            ITEM_ID_3 = 10
        }
        public enum EnumDataType
        {
            INT = 0,
            NUMERIC = 1,
            CHAR = 2,
            NCHAR = 3,
            DATETIME = 4,
            BINARY = 5
        }
        public enum EnumAutoInc
        {
            NO  = 0,
            YES = 1
        }
        public enum EnumIsEntityIdentity
        {
            NO  = 0,
            YES = 1
        }
        /// <summary>
        ///数据库字段名称 
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        ///数据源OPCItem，下拉枚举值
        /// </summary>
        public int SourceOPCItem { get; set; }
        /// <summary>
        /// 自定义数据源，使用程序可解析的角色
        /// </summary>
        public string SourceCustom { get; set; }
        /// <summary>
        /// 数据类型，下拉枚举值
        /// </summary>
        public int DataType { get; set; }
        /// <summary>
        /// 取值序列，用于Oracle，使用序列获取字段值
        /// </summary>
        public string SeqName { get; set; }
        /// <summary>
        /// 是否自增，下拉枚举值，用于MS SQL Server，字段值自增，必须为int
        /// </summary>
        public int AutoInc { get; set; }

        /// <summary>
        /// 是否实体标识，标识实体的站点、传感器。
        /// </summary>
        public int IsEntityIdentity { get; set; }

        /// <summary>
        /// 克隆当前对象
        /// </summary>
        /// <returns>新映射对象</returns>
        public OPCClientDBFieldMapping Clone()
        {
            OPCClientDBFieldMapping newObj = new OPCClientDBFieldMapping();
            newObj.FieldName = this.FieldName;
            newObj.SourceOPCItem = this.SourceOPCItem;
            newObj.SourceCustom = this.SourceCustom;
            newObj.DataType = this.DataType;
            newObj.SeqName = this.SeqName;
            newObj.AutoInc = this.AutoInc;
            newObj.IsEntityIdentity = this.IsEntityIdentity;

            return newObj;
        }
    }
}

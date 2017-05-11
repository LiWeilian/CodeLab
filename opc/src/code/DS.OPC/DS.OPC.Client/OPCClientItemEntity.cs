using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.OPC.Client
{
    /// <summary>
    /// OPCItem数据库实体，用于保存单条记录
    /// </summary>
    class OPCClientItemEntity
    {
        /// <summary>
        /// 记录属性集合
        /// </summary>
        public List<OPCClientItemProperty> Properties { get; private set; }

        public OPCClientItemEntity()
        {
            Properties = new List<OPCClientItemProperty>();
        }
    }
}

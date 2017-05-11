using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.OPC.Client
{
    /// <summary>
    /// OPCItem数据内容，与ListView界面列出项相关
    /// </summary>
    class OPCItemData
    {
        public string ItemID { get; set; }
        public string Value { get; set; }
        public string Quality { get; set; }
        public string Timestamp { get; set; }
        public string ServerHandle { get; set; }
        public string ClientHandle { get; set; }
        public string DataType { get; set; }
    }
}

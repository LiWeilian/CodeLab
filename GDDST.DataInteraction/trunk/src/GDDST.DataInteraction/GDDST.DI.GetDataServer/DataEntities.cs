using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GDDST.DI.GetDataServer
{
    class DataEntity
    {

    }

    class ModbusTcpDataEntity : DataEntity
    {
        public string RID { get; set; }
        public string Station { get; set; }
        public string Device_Addr { get; set; }
        public string Sensor_Type { get; set; }
        public string Sensor_Name { get; set; }
        public ushort Ori_Value { get; set; }
        public double Trans_Value { get; set; }
        public string Trans_Unit { get; set; }
        public DateTime DataAcqTime { get; set; }
    }
}

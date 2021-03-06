﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Text;

namespace GDDST.DI.DataServiceWCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IDataService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此添加您的服务操作
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbusrtu",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusRTUResponseBody RequestModbusRTUData(ModbusRTURequestBody request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbustcp",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusTCPResponseBody RequestModbusTCPData(ModbusTCPRequestBody request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbustcp/rmr",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusTCPResponseBody RequestModbusTCPMultiRegData(ModbusTCPRequestBody request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbustcp/rmrtxt",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusTCPResponseBody RequestModbusTCPMultiRegDataWithText(object request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbustcp/rmc",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusTCPResponseBody RequestModbusTCPMultiCoilData(ModbusTCPRequestBody request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbustcp/rmctxt",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusTCPResponseBody RequestModbusTCPMultiCoilDataWithText(string request);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbustcp/wmr",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusTCPResponseBody WriteModbusTCPData(ModbusTCPWriteDataBody writeInfo);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "modbustcp/wmc",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ModbusTCPResponseBody WriteModbusTCPCoilStatus(ModbusTCPWriteDataBody writeInfo);
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    // 可以将 XSD 文件添加到项目中。在生成项目后，可以通过命名空间“GDDST.DI.DataServiceWCF.ContractType”直接使用其中定义的数据类型。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

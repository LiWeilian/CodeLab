using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace WCFDemo.WinSvc
{
    [ServiceContract(Namespace = "http://WCFDemo/WinSvc")]
    public interface ICalculator
    {
        [OperationContract]
        [WebInvoke(Method = "POST", 
            UriTemplate = "add", 
            RequestFormat = WebMessageFormat.Json, 
            ResponseFormat = WebMessageFormat.Json)]
        double Add(CalcParams p);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "subtract",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        double Subtract(CalcParams p);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "multiply",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        double Multiply(CalcParams p);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "divide",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        double Divide(CalcParams p);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "json/{id}")]
        string JSONData(string id);
    }

    public class CalcParams
    {
        public double N1 { get; set; }
        public double N2 { get; set; }
    }
}

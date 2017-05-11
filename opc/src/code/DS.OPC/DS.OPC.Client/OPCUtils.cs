using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DS.OPC.Client
{
    class OPCUtils
    {
        public static string SENSOR_STATUS_FIELD = "OUT_OF";
        public enum SENSOR_TYPE
        {
            
        }

        public enum DatabaseType
        {
            MSSQLServer = 0,
            Oracle = 1,
        }

        public static string GetSensorTypeBySensorName(string sensorName)
        {
            string sensorType;
            switch (sensorName)
            {
                case "温度":
                    sensorType = "1";
                    break;
                case "压力":
                    sensorType = "2";
                    break;
                case "标况瞬时流量":
                    sensorType = "3";
                    break;
                case "工况瞬时流量":
                    sensorType = "4";
                    break;
                case "标况累计流量":
                    sensorType = "5";
                    break;
                case "工况累计流量":
                    sensorType = "6";
                    break;
                default:
                    sensorType = "0";
                    break;
            }
            return sensorType;
        }

        public static string GetCustomFieldValue(string format)
        {
            string fieldValue;
            switch (format.ToUpper())
            {
                case ":GUID":
                    fieldValue = Guid.NewGuid().ToString();
                    break;
                case ":NOW":
                    fieldValue = DateTime.Now.ToString();
                    break;
                default:
                    fieldValue = format;
                    break;
            }

            return fieldValue;
        }

        public static string GetCustomSystemValue(string sysVar)
        {
            string value = string.Empty;
            switch (sysVar.ToUpper().Trim())
            {
                case ":GUID":
                    value = Guid.NewGuid().ToString();
                    break;
                case ":NOW":
                    value = DateTime.Now.ToString();
                    break;
                default:
                    value = sysVar;
                    break;
            }
            return value;
        }

        public static string GetCustomValueByItemProperty(string propertyName, string propertyValue)
        {
            string customValue = string.Empty;
            switch (propertyName.ToUpper())
            {
                case "SENSOR_NAME":
                    switch (propertyValue.ToUpper())
                    {
                        case "温度":
                            customValue = "1";
                            break;
                        case "压力":
                            customValue = "2";
                            break;
                        case "标况瞬时流量":
                            customValue = "3";
                            break;
                        case "工况瞬时流量":
                            customValue = "4";
                            break;
                        case "标况累计流量":
                            customValue = "5";
                            break;
                        case "工况累计流量":
                            customValue = "6";
                            break;
                        default:
                            customValue = "0";
                            break;
                    }
                    break;
                default:
                    break;
            }

            return customValue;
        }

        public static void LogDataChangeTime(string msg)
        {
            string fileName = Application.StartupPath + "\\DataChangeLog.log";
            FileStream fs;
            if (File.Exists(fileName))
            {
                fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            }

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(string.Format(DateTime.Now.ToString() + ": {0}", msg));
            sw.Flush();
            sw.Close();
            fs.Close();
        }

    }

    
}

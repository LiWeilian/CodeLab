using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ArcMapAddin.EncryptGDB
{
    class EncryptGDB
    {
        const string gdbFileName = "gdb";
        const string esrigdbFileName = "esrigdb";
        const string esritimestampsFileName = "esritimestamp";
        public enum GDB_Crypt_Status
        {
            GCS_NOT_AVAILABLE,
            GCS_ENCRYTED,
            GCS_NOT_CRYTED
        }

        public static GDB_Crypt_Status CheckAccessGDBStatus(string gdbPath)
        {
            byte[] fileHeader = new byte[16];

            try
            {
                FileStream fs = new FileStream(gdbPath, FileMode.Open, FileAccess.Read);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(fileHeader, 0, fileHeader.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format("打开文件出现错误:\r\n{0}", e.Message));
                return GDB_Crypt_Status.GCS_NOT_AVAILABLE;
            }

            try
            {
                if (fileHeader[0] == 0x00
                    && fileHeader[1] == 0x01
                    && fileHeader[2] == 0x00
                    && fileHeader[3] == 0x00
                    && fileHeader[4] == 0x53
                    && fileHeader[5] == 0x74
                    && fileHeader[6] == 0x61
                    && fileHeader[7] == 0x6e
                    && fileHeader[8] == 0x64
                    && fileHeader[9] == 0x61
                    && fileHeader[10] == 0x72
                    && fileHeader[11] == 0x64
                    && fileHeader[12] == 0x20
                    && fileHeader[13] == 0x4a
                    && fileHeader[14] == 0x65
                    && fileHeader[15] == 0x74)
                {
                    //Access数据库文件头{ 0x00, 0x01, 0x00, 0x00, 0x53, 0x74, 0x61, 0x6e, 0x64, 0x61, 0x72, 0x64, 0x20, 0x4a, 0x65, 0x74};
                    return GDB_Crypt_Status.GCS_NOT_CRYTED;
                }
                else if (fileHeader[10] == 0x00
                    && fileHeader[11] == 0x00
                    && fileHeader[12] == 0x00
                    && fileHeader[13] == 0x00
                    && fileHeader[14] == 0x00
                    && fileHeader[15] == 0x00)
                {
                    return GDB_Crypt_Status.GCS_ENCRYTED;
                } else
                {
                    return GDB_Crypt_Status.GCS_NOT_AVAILABLE;
                }
                
            }
            catch (Exception)
            {
                return GDB_Crypt_Status.GCS_NOT_AVAILABLE;
            }
        }

        public static GDB_Crypt_Status CheckFileGDBStatus(string gdbPath)
        {
            if (File.Exists(string.Format("{0}\\{1}", gdbPath, gdbFileName)))
            {
                return GDB_Crypt_Status.GCS_NOT_CRYTED;
            }
            else if (File.Exists(string.Format("{0}\\{1}", gdbPath, esrigdbFileName)))
            {
                return GDB_Crypt_Status.GCS_ENCRYTED;
            }
            else
            {
                return GDB_Crypt_Status.GCS_NOT_AVAILABLE;
            }
        }

        public static bool EncryptAccessGDB(string gdbPath, DateTime encryptedTime, out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                int unixTime = ConvertDateTimeInt(encryptedTime);
                byte[] bytes = UTF8Encoding.UTF8.GetBytes(unixTime.ToString());

                byte[] encryptBytes = new byte[16];

                for (int i = 0; i < bytes.Length; i++)
                {
                    encryptBytes[i] = bytes[i];
                }

                FileStream fs = new FileStream(gdbPath, FileMode.Open, FileAccess.ReadWrite);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(encryptBytes, 0, encryptBytes.Length);
                fs.Flush();
                fs.Close();

                return true;
            }
            catch (Exception e)
            {
                errMsg = string.Format("加密Access地理数据库失败：{0}", e.Message);
                return false;
            }
            
        }

        public static bool DecryptAccessGDB(string gdbPath, out string errMsg)
        {
            errMsg = string.Empty;

            try
            {
                //Access数据库文件头
                byte[] decrytBytes = new byte[16] { 0x00, 0x01, 0x00, 0x00, 0x53, 0x74, 0x61, 0x6e, 0x64, 0x61, 0x72, 0x64, 0x20, 0x4a, 0x65, 0x74};

                FileStream fs = new FileStream(gdbPath, FileMode.Open, FileAccess.ReadWrite);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(decrytBytes, 0, decrytBytes.Length);
                fs.Flush();
                fs.Close();

                return true;
            }
            catch (Exception e)
            {
                errMsg = string.Format("解密Access地理数据库失败：{0}", e.Message);
                return false;
            }
        }

        public static bool EncryptFileGDB(string gdbPath, DateTime encryptedTime, out string errMsg)
        {
            errMsg = string.Empty;

            string gdbFilePath = string.Format(string.Format("{0}\\{1}", gdbPath, gdbFileName));
            string esrigdbFilePath = string.Format(string.Format("{0}\\{1}", gdbPath, esrigdbFileName));
            try
            {
                File.Copy(gdbFilePath, esrigdbFilePath, true);
                File.Delete(gdbFilePath);
            }
            catch (Exception e)
            {
                if (File.Exists(esrigdbFilePath))
                {
                    File.Copy(esrigdbFilePath, gdbFilePath, true);
                }
                errMsg = string.Format("加密文件地理数据库失败：{0}", e.Message);
                return false;
            }

            string esritimestampsFilePath = string.Format(string.Format("{0}\\{1}", gdbPath, esritimestampsFileName));
            try
            {
                if (File.Exists(esritimestampsFilePath))
                {
                    File.Delete(esritimestampsFilePath);
                }

                SetFileGDBEncryptedTime(esritimestampsFilePath, encryptedTime, out errMsg);
                return true;
            }
            catch (Exception e)
            {

                if (File.Exists(esrigdbFilePath))
                {
                    File.Copy(esrigdbFilePath, gdbFilePath, true);
                }
                errMsg = string.Format("加密文件地理数据库失败：{0}", e.Message);
                return false;
            }
        }

        public static bool DecryptFileGDB(string gdbPath, out string errMsg)
        {
            errMsg = string.Empty;

            string gdbFilePath = string.Format(string.Format("{0}\\{1}", gdbPath, gdbFileName));
            string esrigdbFilePath = string.Format(string.Format("{0}\\{1}", gdbPath, esrigdbFileName));
            try
            {
                File.Copy(esrigdbFilePath, gdbFilePath, true);
                return true;
            }
            catch (Exception e)
            {
                errMsg = string.Format("解密文件地理数据库失败：{0}", e.Message);
                return false;
            }
        }

        public static DateTime GetFileGDBEncryptedTime(string gdbPath)
        {
            string timestampsPath = string.Format(string.Format("{0}\\{1}", gdbPath, esritimestampsFileName));

            string timeStamp = string.Empty;

            if (File.Exists(timestampsPath))
            {
                StreamReader sr = new StreamReader(timestampsPath);

                timeStamp = sr.ReadLine();
                
                sr.Close();
            }

            return GetTime(timeStamp);
        }

        private static bool SetFileGDBEncryptedTime(string timestampsPath, DateTime encryptedTime, out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                int unixTime = ConvertDateTimeInt(encryptedTime);

                FileStream fs = new FileStream(timestampsPath, FileMode.Create, FileAccess.Write);

                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(unixTime.ToString());

                sw.Flush();

                sw.Close();
                fs.Close();

                return true;
            }
            catch (Exception e)
            {
                errMsg = string.Format("解密文件数据库失败：{0}", e.Message);
                return false;
            }

        }

        /// <summary>  
        /// Unix时间戳转为C#格式时间  
        /// </summary>  
        /// <param name="timeStamp">Unix时间戳格式,例如1482115779</param>  
        /// <returns>C#格式时间</returns>  
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime;
            if (long.TryParse(timeStamp + "0000000", out lTime))
            {
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow);
            } else
            {
                return dtStart;
            }
        }


        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}

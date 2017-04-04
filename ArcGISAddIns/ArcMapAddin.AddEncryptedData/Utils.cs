using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArcMapAddin.AddEncryptedData
{
    class Utils
    {
        public static string GetDefaultFileGDBPath()
        {
            string gdbPath = string.Empty;

            string configFilePath = string.Format("{0}\\arcgisaddinfilegdbpath",
                System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            if (File.Exists(configFilePath))
            {
                try
                {

                    StreamReader sr = new StreamReader(configFilePath);
                    gdbPath = sr.ReadLine();
                    sr.Close();
                }
                catch
                {
                    gdbPath = string.Empty;
                }
            }
            return gdbPath;
        }
        
        public static void SetDefaultFileGDBPath(string gdbPath)
        {
            string configFilePath = string.Format("{0}\\arcgisaddinfilegdbpath",
                System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            try
            {

                FileStream fs = new FileStream(configFilePath, FileMode.Create, FileAccess.Write);

                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(gdbPath);

                sw.Flush();

                sw.Close();
                fs.Close();
            }
            catch
            {
            }
        }

        public static string GetDefaultAccessGDBPath()
        {
            string gdbPath = string.Empty;

            string configFilePath = string.Format("{0}\\arcgisaddinAccessgdbpath",
                System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            if (File.Exists(configFilePath))
            {
                try
                {

                    StreamReader sr = new StreamReader(configFilePath);
                    gdbPath = sr.ReadLine();
                    sr.Close();
                }
                catch
                {
                    gdbPath = string.Empty;
                }
            }
            return gdbPath;
        }

        public static void SetDefaultAccessGDBPath(string gdbPath)
        {
            string configFilePath = string.Format("{0}\\arcgisaddinAccessgdbpath",
                System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            try
            {

                FileStream fs = new FileStream(configFilePath, FileMode.Create, FileAccess.Write);

                StreamWriter sw = new StreamWriter(fs);

                sw.WriteLine(gdbPath);

                sw.Flush();

                sw.Close();
                fs.Close();
            }
            catch
            {
            }
        }
    }
}

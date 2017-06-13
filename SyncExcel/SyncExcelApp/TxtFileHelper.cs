using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SyncExcelApp
{
    class TxtData
    {
        public string 系统主键 { get; set; }
        public string 街路巷名称 { get; set; }
        public string 门牌地址名称 { get; set; }
        public string X纬度 { get; set; }
        public string Y经度 { get; set; }
    }
    class TxtFileHelper
    {
        public static List<TxtData> GetData(string fileName)
        {
            List<TxtData> txtDataList = new List<TxtData>();

            System.IO.StreamReader sr = new System.IO.StreamReader(fileName, System.Text.Encoding.GetEncoding("GBK"));

            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strs1 = line.Split(',');
                if (strs1.Length == 3)
                {
                    string[] strs2 = strs1[0].Split(' ');
                    if (strs2.Length == 3)
                    {
                        TxtData txtData = new TxtData();
                        txtData.系统主键 = strs2[0];
                        txtData.街路巷名称 = strs2[1];
                        txtData.门牌地址名称 = strs2[2];
                        txtData.X纬度 = strs1[1];
                        txtData.Y经度 = strs1[2];

                        txtDataList.Add(txtData);
                    }
                }
            }

            return txtDataList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncExcelApp
{
    public partial class FormSyncExcel : Form
    {
        private string currentSheetName = string.Empty;
        private void DisplayRunMessage(string runMsg)
        {
            txtRunMsg.AppendText(string.Format("{0}\r\n", DateTime.Now.ToString()));
            txtRunMsg.AppendText(string.Format("{0}\r\n", runMsg));
            txtRunMsg.AppendText("\r\n");
            Application.DoEvents();
        }

        private void DisplayExcelData()
        {
            try
            {
                DataSet ds = ExcelHelper.GetReader(string.Format("SELECT * FROM [{0}]", currentSheetName));
                dgvExcelData.DataSource = ds;
                dgvExcelData.DataMember = "TempTable";
            }
            catch (Exception ex)
            {
                string errorMsg = string.Format("读取Excel数据时发生错误：{0}",
                        ex.Message);

                DisplayRunMessage(errorMsg);
            }
        }
        public FormSyncExcel()
        {
            InitializeComponent();
        }

        private void btnOpenExcel_Click(object sender, EventArgs e)
        {
            currentSheetName = string.Empty;
            ExcelHelper.CloseConnection();
            dgvExcelData.DataSource = null;
            txtExcelFile.Text = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel 文件(*.xls;*.xlsx)|*.xls;*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtExcelFile.Text = ofd.FileName;
                ExcelHelper.CreateConnection(ofd.FileName);
                
                try
                {

                    currentSheetName = ExcelHelper.GetExcelSheetName();
                }       
                catch (Exception ex)
                {
                    string errorMsg = string.Format("获取Excel数据表名称时发生错误：{0}",
                            ex.Message);

                    DisplayRunMessage(errorMsg);
                }

                DisplayExcelData();
            }
        }

        private void btnOpenTxt_Click(object sender, EventArgs e)
        {
            dgvTxtData.DataSource = null;
            txtTxtFile.Text = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtExcelFile.Text = ofd.FileName;
                List<TxtData> txtDataList = TxtFileHelper.GetData(ofd.FileName);
                dgvTxtData.DataSource = txtDataList;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //ExcelHelper.ExecuteCommand("UPDATE [查询结果$] SET [X纬度] = '22.0',[Y经度] = '113.0' WHERE [系统主键] = 'GZPCS3707201304210000000009879549'");
            
            if (dgvTxtData.DataSource != null)
            {
                DisplayRunMessage("开始更新...");
                int idx = 0;
                int updateCount = 0;

                List<TxtData> txtDataList = (List<TxtData>)dgvTxtData.DataSource;

                foreach (TxtData txtData in txtDataList)
                {
                    Application.DoEvents();
                    idx++;
                    string updateSql = string.Format("UPDATE [{0}] SET [X纬度] = '{1}',[Y经度] = '{2}' WHERE [系统主键] = '{3}' AND [街路巷名称] = '{4}' AND [门牌地址名称] = '{5}'",
                        currentSheetName, txtData.X纬度, txtData.Y经度, txtData.系统主键, txtData.街路巷名称, txtData.门牌地址名称);
                    try
                    {
                        ExcelHelper.ExecuteCommand(updateSql);
                        updateCount++;
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = string.Format("更新数据时发生错误：{0}，文本文件序号：[{1}]，系统主键[{2}]，街路巷名称[{3}]，门牌地址名称[{4}]，X纬度[{5}]，Y经度[{6}]",
                            ex.Message, idx, txtData.系统主键, txtData.街路巷名称, txtData.门牌地址名称, txtData.X纬度, txtData.Y经度);

                        DisplayRunMessage(errorMsg);
                    }
                }

                DisplayRunMessage(string.Format("更新完毕，共更新[{0}]条记录", updateCount));

                DisplayExcelData();
            }
        }
    }
}

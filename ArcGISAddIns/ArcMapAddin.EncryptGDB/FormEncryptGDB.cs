using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ArcMapAddin.EncryptGDB
{
    public partial class FormEncryptGDB : Form
    {
        public FormEncryptGDB()
        {
            InitializeComponent();

            InitializeControlStatus();
        }

        private void DisplayMessage(string msg)
        {
            MessageBox.Show(msg);
        }

        private void InitializeControlStatus()
        {
            txtGDBPath.Text = string.Empty;
            dtpEncryptDate.Enabled = false;
            dtpEncryptDate.Value = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            btnEncrypt.Enabled = false;
            btnDecrypt.Enabled = false;
        }

        private void DoCheckFileGDBStatus(string gdbPath)
        {
            EncryptGDB.GDB_Crypt_Status status = EncryptGDB.CheckFileGDBStatus(gdbPath);
            switch (status)
            {
                case EncryptGDB.GDB_Crypt_Status.GCS_NOT_AVAILABLE:
                    DisplayMessage("文件地理数据库无效");
                    break;
                case EncryptGDB.GDB_Crypt_Status.GCS_ENCRYTED:
                    btnEncrypt.Enabled = false;
                    dtpEncryptDate.Enabled = false;
                    btnDecrypt.Enabled = true;
                    break;
                case EncryptGDB.GDB_Crypt_Status.GCS_NOT_CRYTED:
                    btnEncrypt.Enabled = true;
                    dtpEncryptDate.Enabled = true;
                    btnDecrypt.Enabled = false;
                    break;
            }
        }

        private void DoCheckAccessGDBStatus(string gdbPath)
        {
            EncryptGDB.GDB_Crypt_Status status = EncryptGDB.CheckAccessGDBStatus(gdbPath);
            switch (status)
            {
                case EncryptGDB.GDB_Crypt_Status.GCS_NOT_AVAILABLE:
                    DisplayMessage("Access地理数据库无效");
                    break;
                case EncryptGDB.GDB_Crypt_Status.GCS_ENCRYTED:
                    btnEncrypt.Enabled = false;
                    dtpEncryptDate.Enabled = false;
                    btnDecrypt.Enabled = true;
                    break;
                case EncryptGDB.GDB_Crypt_Status.GCS_NOT_CRYTED:
                    btnEncrypt.Enabled = true;
                    dtpEncryptDate.Enabled = true;
                    btnDecrypt.Enabled = false;
                    break;
            }
        }

        private void btnOpenGDBPath_Click(object sender, EventArgs e)
        {
            if (rbAccessGDB.Checked)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Access地理数据库(*.mdb)|*.mdb";
                ofd.Title = "请选择Access地理数据库";
                ofd.Multiselect = false;

                string defaultPath = Utils.GetDefaultAccessGDBPath();
                if (Directory.Exists(defaultPath))
                {
                    ofd.InitialDirectory = defaultPath;
                }
                else
                {
                    ofd.InitialDirectory = Application.StartupPath;
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string newDefaultPath = Path.GetDirectoryName(ofd.FileName);

                    txtGDBPath.Text = ofd.FileName;

                    Utils.SetDefaultAccessGDBPath(newDefaultPath);

                    DoCheckAccessGDBStatus(txtGDBPath.Text);
                }
            }
            else if (rbFileGDB.Checked)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "选择文件地理数据库(*.gdb)";

                string defaultPath = Utils.GetDefaultFileGDBPath();
                if (Directory.Exists(defaultPath))
                {
                    fbd.SelectedPath = defaultPath;
                }
                else
                {
                    fbd.SelectedPath = Application.StartupPath;
                }

                fbd.ShowNewFolderButton = false;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string tempPath = fbd.SelectedPath;

                    if (Path.GetExtension(tempPath).ToUpper() == ".GDB")
                    {
                        txtGDBPath.Text = fbd.SelectedPath;
                        Utils.SetDefaultFileGDBPath(fbd.SelectedPath);
                        DoCheckFileGDBStatus(txtGDBPath.Text);
                    }
                }
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            string gdbPath = txtGDBPath.Text;
            string errMsg;

            if (rbAccessGDB.Checked)
            {
                EncryptGDB.GDB_Crypt_Status status = EncryptGDB.CheckAccessGDBStatus(gdbPath);
                switch (status)
                {
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_AVAILABLE:
                        DisplayMessage("Access地理数据库无效");
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_ENCRYTED:
                        DisplayMessage("Access地理数据库已加密，无需再次加密");
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_CRYTED:
                        if (EncryptGDB.EncryptAccessGDB(gdbPath, dtpEncryptDate.Value, out errMsg))
                        {
                            DisplayMessage("Access地理数据库加密成功");
                        }
                        else
                        {
                            DisplayMessage(errMsg);
                        }
                        break;
                }

                DoCheckAccessGDBStatus(gdbPath);
            }
            else if (rbFileGDB.Checked)
            {
                EncryptGDB.GDB_Crypt_Status status = EncryptGDB.CheckFileGDBStatus(gdbPath);
                switch (status)
                {
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_AVAILABLE:
                        DisplayMessage("文件地理数据库无效");
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_ENCRYTED:
                        DisplayMessage("文件地理数据库已加密，无需再次加密");
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_CRYTED:
                        if (EncryptGDB.EncryptFileGDB(gdbPath, dtpEncryptDate.Value, out errMsg))
                        {
                            DisplayMessage("文件地理数据库加密成功");
                        }
                        else
                        {
                            DisplayMessage(errMsg);
                        }
                        break;
                }

                DoCheckFileGDBStatus(gdbPath);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            string gdbPath = txtGDBPath.Text;
            string errMsg;

            if (rbAccessGDB.Checked)
            {
                EncryptGDB.GDB_Crypt_Status status = EncryptGDB.CheckAccessGDBStatus(gdbPath);
                switch (status)
                {
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_AVAILABLE:
                        DisplayMessage("Access地理数据库无效");
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_ENCRYTED:
                        if (EncryptGDB.DecryptAccessGDB(gdbPath, out errMsg))
                        {
                            DisplayMessage("Access地理数据库解密成功");
                        }
                        else
                        {
                            DisplayMessage(errMsg);
                        }
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_CRYTED:
                        DisplayMessage("Access地理数据库未加密，无需再次解密");
                        break;
                }

                DoCheckAccessGDBStatus(gdbPath);
            }
            else if (rbFileGDB.Checked)
            {
                EncryptGDB.GDB_Crypt_Status status = EncryptGDB.CheckFileGDBStatus(gdbPath);
                switch (status)
                {
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_AVAILABLE:
                        DisplayMessage("文件地理数据库无效");
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_ENCRYTED:
                        if (EncryptGDB.DecryptFileGDB(gdbPath, out errMsg))
                        {
                            DisplayMessage("文件地理数据库解密成功");
                        }
                        else
                        {
                            DisplayMessage(errMsg);
                        }
                        break;
                    case EncryptGDB.GDB_Crypt_Status.GCS_NOT_CRYTED:
                        DisplayMessage("文件地理数据库未加密，无需再次解密");
                        break;
                }

                DoCheckFileGDBStatus(gdbPath);
            }
        }

        private void rbFileGDB_CheckedChanged(object sender, EventArgs e)
        {
            txtGDBPath.Text = string.Empty;
            InitializeControlStatus();
        }

        private void rbAccessGDB_CheckedChanged(object sender, EventArgs e)
        {
            txtGDBPath.Text = string.Empty;
            InitializeControlStatus();
        }
    }
}

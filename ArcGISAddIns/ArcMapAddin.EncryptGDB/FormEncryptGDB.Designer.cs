namespace ArcMapAddin.EncryptGDB
{
    partial class FormEncryptGDB
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEncryptGDB));
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.sslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbGDB = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenGDBPath = new System.Windows.Forms.Button();
            this.txtGDBPath = new System.Windows.Forms.TextBox();
            this.rbFileGDB = new System.Windows.Forms.RadioButton();
            this.rbAccessGDB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEncryptDate = new System.Windows.Forms.DateTimePicker();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.ssStatus.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbGDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslStatus});
            this.ssStatus.Location = new System.Drawing.Point(0, 164);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(527, 22);
            this.ssStatus.TabIndex = 1;
            // 
            // sslStatus
            // 
            this.sslStatus.Name = "sslStatus";
            this.sslStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbGDB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpEncryptDate);
            this.panel1.Controls.Add(this.btnDecrypt);
            this.panel1.Controls.Add(this.btnEncrypt);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(527, 164);
            this.panel1.TabIndex = 2;
            // 
            // gbGDB
            // 
            this.gbGDB.Controls.Add(this.label2);
            this.gbGDB.Controls.Add(this.btnOpenGDBPath);
            this.gbGDB.Controls.Add(this.txtGDBPath);
            this.gbGDB.Controls.Add(this.rbFileGDB);
            this.gbGDB.Controls.Add(this.rbAccessGDB);
            this.gbGDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbGDB.Location = new System.Drawing.Point(0, 0);
            this.gbGDB.Name = "gbGDB";
            this.gbGDB.Size = new System.Drawing.Size(527, 102);
            this.gbGDB.TabIndex = 8;
            this.gbGDB.TabStop = false;
            this.gbGDB.Text = "数据库类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "数据库目录：";
            // 
            // btnOpenGDBPath
            // 
            this.btnOpenGDBPath.Location = new System.Drawing.Point(419, 64);
            this.btnOpenGDBPath.Name = "btnOpenGDBPath";
            this.btnOpenGDBPath.Size = new System.Drawing.Size(77, 26);
            this.btnOpenGDBPath.TabIndex = 11;
            this.btnOpenGDBPath.Text = "打开";
            this.btnOpenGDBPath.UseVisualStyleBackColor = true;
            this.btnOpenGDBPath.Click += new System.EventHandler(this.btnOpenGDBPath_Click);
            // 
            // txtGDBPath
            // 
            this.txtGDBPath.BackColor = System.Drawing.SystemColors.Window;
            this.txtGDBPath.HideSelection = false;
            this.txtGDBPath.Location = new System.Drawing.Point(19, 67);
            this.txtGDBPath.Name = "txtGDBPath";
            this.txtGDBPath.ReadOnly = true;
            this.txtGDBPath.Size = new System.Drawing.Size(394, 21);
            this.txtGDBPath.TabIndex = 10;
            // 
            // rbFileGDB
            // 
            this.rbFileGDB.AutoSize = true;
            this.rbFileGDB.Location = new System.Drawing.Point(157, 23);
            this.rbFileGDB.Name = "rbFileGDB";
            this.rbFileGDB.Size = new System.Drawing.Size(107, 16);
            this.rbFileGDB.TabIndex = 9;
            this.rbFileGDB.Text = "文件地理数据库";
            this.rbFileGDB.UseVisualStyleBackColor = true;
            this.rbFileGDB.CheckedChanged += new System.EventHandler(this.rbFileGDB_CheckedChanged);
            // 
            // rbAccessGDB
            // 
            this.rbAccessGDB.AutoSize = true;
            this.rbAccessGDB.Checked = true;
            this.rbAccessGDB.Location = new System.Drawing.Point(19, 23);
            this.rbAccessGDB.Name = "rbAccessGDB";
            this.rbAccessGDB.Size = new System.Drawing.Size(119, 16);
            this.rbAccessGDB.TabIndex = 8;
            this.rbAccessGDB.TabStop = true;
            this.rbAccessGDB.Text = "Access地理数据库";
            this.rbAccessGDB.UseVisualStyleBackColor = true;
            this.rbAccessGDB.CheckedChanged += new System.EventHandler(this.rbAccessGDB_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "使用期限：";
            // 
            // dtpEncryptDate
            // 
            this.dtpEncryptDate.Location = new System.Drawing.Point(96, 113);
            this.dtpEncryptDate.Name = "dtpEncryptDate";
            this.dtpEncryptDate.Size = new System.Drawing.Size(145, 21);
            this.dtpEncryptDate.TabIndex = 3;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Image = global::ArcMapAddin.EncryptGDB.Properties.Resources.Decrypt;
            this.btnDecrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDecrypt.Location = new System.Drawing.Point(419, 108);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(79, 35);
            this.btnDecrypt.TabIndex = 2;
            this.btnDecrypt.Text = "解密";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Image = global::ArcMapAddin.EncryptGDB.Properties.Resources.Encrypt;
            this.btnEncrypt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEncrypt.Location = new System.Drawing.Point(334, 108);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(79, 35);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // FormEncryptGDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 186);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ssStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEncryptGDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "加密地理数据库";
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbGDB.ResumeLayout(false);
            this.gbGDB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel sslStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.GroupBox gbGDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenGDBPath;
        private System.Windows.Forms.TextBox txtGDBPath;
        private System.Windows.Forms.RadioButton rbFileGDB;
        private System.Windows.Forms.RadioButton rbAccessGDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEncryptDate;
    }
}


namespace SyncExcelApp
{
    partial class FormSyncExcel
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
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.btnOpenExcel = new System.Windows.Forms.Button();
            this.btnOpenTxt = new System.Windows.Forms.Button();
            this.txtTxtFile = new System.Windows.Forms.TextBox();
            this.gbExcelData = new System.Windows.Forms.GroupBox();
            this.dgvExcelData = new System.Windows.Forms.DataGridView();
            this.gbTxtData = new System.Windows.Forms.GroupBox();
            this.dgvTxtData = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.gbRunMsg = new System.Windows.Forms.GroupBox();
            this.txtRunMsg = new System.Windows.Forms.TextBox();
            this.gbExcelData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelData)).BeginInit();
            this.gbTxtData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTxtData)).BeginInit();
            this.gbRunMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.Location = new System.Drawing.Point(12, 12);
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.ReadOnly = true;
            this.txtExcelFile.Size = new System.Drawing.Size(281, 21);
            this.txtExcelFile.TabIndex = 0;
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(299, 12);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(75, 23);
            this.btnOpenExcel.TabIndex = 2;
            this.btnOpenExcel.Text = "打开Excel";
            this.btnOpenExcel.UseVisualStyleBackColor = true;
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // btnOpenTxt
            // 
            this.btnOpenTxt.Location = new System.Drawing.Point(674, 10);
            this.btnOpenTxt.Name = "btnOpenTxt";
            this.btnOpenTxt.Size = new System.Drawing.Size(75, 23);
            this.btnOpenTxt.TabIndex = 6;
            this.btnOpenTxt.Text = "打开Txt";
            this.btnOpenTxt.UseVisualStyleBackColor = true;
            this.btnOpenTxt.Click += new System.EventHandler(this.btnOpenTxt_Click);
            // 
            // txtTxtFile
            // 
            this.txtTxtFile.Location = new System.Drawing.Point(386, 12);
            this.txtTxtFile.Name = "txtTxtFile";
            this.txtTxtFile.ReadOnly = true;
            this.txtTxtFile.Size = new System.Drawing.Size(282, 21);
            this.txtTxtFile.TabIndex = 5;
            // 
            // gbExcelData
            // 
            this.gbExcelData.Controls.Add(this.dgvExcelData);
            this.gbExcelData.Location = new System.Drawing.Point(8, 38);
            this.gbExcelData.Name = "gbExcelData";
            this.gbExcelData.Size = new System.Drawing.Size(366, 211);
            this.gbExcelData.TabIndex = 7;
            this.gbExcelData.TabStop = false;
            this.gbExcelData.Text = "Excel数据";
            // 
            // dgvExcelData
            // 
            this.dgvExcelData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcelData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExcelData.Location = new System.Drawing.Point(3, 17);
            this.dgvExcelData.Name = "dgvExcelData";
            this.dgvExcelData.RowTemplate.Height = 23;
            this.dgvExcelData.Size = new System.Drawing.Size(360, 191);
            this.dgvExcelData.TabIndex = 5;
            // 
            // gbTxtData
            // 
            this.gbTxtData.Controls.Add(this.dgvTxtData);
            this.gbTxtData.Location = new System.Drawing.Point(386, 39);
            this.gbTxtData.Name = "gbTxtData";
            this.gbTxtData.Size = new System.Drawing.Size(366, 210);
            this.gbTxtData.TabIndex = 8;
            this.gbTxtData.TabStop = false;
            this.gbTxtData.Text = "Txt数据";
            // 
            // dgvTxtData
            // 
            this.dgvTxtData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTxtData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTxtData.Location = new System.Drawing.Point(3, 17);
            this.dgvTxtData.Name = "dgvTxtData";
            this.dgvTxtData.RowTemplate.Height = 23;
            this.dgvTxtData.Size = new System.Drawing.Size(360, 190);
            this.dgvTxtData.TabIndex = 0;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(758, 55);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // gbRunMsg
            // 
            this.gbRunMsg.Controls.Add(this.txtRunMsg);
            this.gbRunMsg.Location = new System.Drawing.Point(8, 255);
            this.gbRunMsg.Name = "gbRunMsg";
            this.gbRunMsg.Size = new System.Drawing.Size(741, 170);
            this.gbRunMsg.TabIndex = 11;
            this.gbRunMsg.TabStop = false;
            this.gbRunMsg.Text = "运行信息";
            // 
            // txtRunMsg
            // 
            this.txtRunMsg.BackColor = System.Drawing.SystemColors.Window;
            this.txtRunMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRunMsg.Location = new System.Drawing.Point(3, 17);
            this.txtRunMsg.Multiline = true;
            this.txtRunMsg.Name = "txtRunMsg";
            this.txtRunMsg.ReadOnly = true;
            this.txtRunMsg.Size = new System.Drawing.Size(735, 150);
            this.txtRunMsg.TabIndex = 11;
            // 
            // FormSyncExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 447);
            this.Controls.Add(this.gbRunMsg);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.gbTxtData);
            this.Controls.Add(this.gbExcelData);
            this.Controls.Add(this.btnOpenTxt);
            this.Controls.Add(this.txtTxtFile);
            this.Controls.Add(this.btnOpenExcel);
            this.Controls.Add(this.txtExcelFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSyncExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新Excel数据";
            this.gbExcelData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcelData)).EndInit();
            this.gbTxtData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTxtData)).EndInit();
            this.gbRunMsg.ResumeLayout(false);
            this.gbRunMsg.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtExcelFile;
        private System.Windows.Forms.Button btnOpenExcel;
        private System.Windows.Forms.Button btnOpenTxt;
        private System.Windows.Forms.TextBox txtTxtFile;
        private System.Windows.Forms.GroupBox gbExcelData;
        private System.Windows.Forms.DataGridView dgvExcelData;
        private System.Windows.Forms.GroupBox gbTxtData;
        private System.Windows.Forms.DataGridView dgvTxtData;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.GroupBox gbRunMsg;
        private System.Windows.Forms.TextBox txtRunMsg;
    }
}


namespace DS.OPC.Client
{
    partial class FormDBConnectionSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDBConnectionSetting));
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.gpDBConnSetting = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.cbDatabaseType = new System.Windows.Forms.ComboBox();
            this.gpDataTableSetting = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHistoryDataTableName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNewDataTableName = new System.Windows.Forms.TextBox();
            this.gpDataFieldSetting = new System.Windows.Forms.GroupBox();
            this.dvgDBFieldMapping = new System.Windows.Forms.DataGridView();
            this.btnRemoveDBField = new System.Windows.Forms.Button();
            this.btnAddDBField = new System.Windows.Forms.Button();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSourceOPCItem = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colSouceCustom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colSeqNamw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAutoInc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colIsEntityIdentity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gpDBConnSetting.SuspendLayout();
            this.gpDataTableSetting.SuspendLayout();
            this.gpDataFieldSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDBFieldMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(555, 181);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(75, 23);
            this.btnTestConnection.TabIndex = 17;
            this.btnTestConnection.Text = "测试连接";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(591, 467);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(510, 467);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // gpDBConnSetting
            // 
            this.gpDBConnSetting.Controls.Add(this.label5);
            this.gpDBConnSetting.Controls.Add(this.label4);
            this.gpDBConnSetting.Controls.Add(this.label3);
            this.gpDBConnSetting.Controls.Add(this.label2);
            this.gpDBConnSetting.Controls.Add(this.label1);
            this.gpDBConnSetting.Controls.Add(this.txtPassword);
            this.gpDBConnSetting.Controls.Add(this.txtUserName);
            this.gpDBConnSetting.Controls.Add(this.txtDatabase);
            this.gpDBConnSetting.Controls.Add(this.txtServer);
            this.gpDBConnSetting.Controls.Add(this.cbDatabaseType);
            this.gpDBConnSetting.Location = new System.Drawing.Point(12, 12);
            this.gpDBConnSetting.Name = "gpDBConnSetting";
            this.gpDBConnSetting.Size = new System.Drawing.Size(696, 104);
            this.gpDBConnSetting.TabIndex = 15;
            this.gpDBConnSetting.TabStop = false;
            this.gpDBConnSetting.Text = "数据库连接设置";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(386, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "数据库";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "服务器";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "数据库类型";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(421, 73);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(197, 21);
            this.txtPassword.TabIndex = 14;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(421, 46);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(197, 21);
            this.txtUserName.TabIndex = 13;
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(84, 73);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(197, 21);
            this.txtDatabase.TabIndex = 12;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(84, 46);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(197, 21);
            this.txtServer.TabIndex = 11;
            // 
            // cbDatabaseType
            // 
            this.cbDatabaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatabaseType.FormattingEnabled = true;
            this.cbDatabaseType.Items.AddRange(new object[] {
            "MS SQL Server",
            "Oracle"});
            this.cbDatabaseType.Location = new System.Drawing.Point(84, 20);
            this.cbDatabaseType.Name = "cbDatabaseType";
            this.cbDatabaseType.Size = new System.Drawing.Size(197, 20);
            this.cbDatabaseType.TabIndex = 10;
            // 
            // gpDataTableSetting
            // 
            this.gpDataTableSetting.Controls.Add(this.label7);
            this.gpDataTableSetting.Controls.Add(this.txtHistoryDataTableName);
            this.gpDataTableSetting.Controls.Add(this.label6);
            this.gpDataTableSetting.Controls.Add(this.txtNewDataTableName);
            this.gpDataTableSetting.Location = new System.Drawing.Point(12, 122);
            this.gpDataTableSetting.Name = "gpDataTableSetting";
            this.gpDataTableSetting.Size = new System.Drawing.Size(696, 53);
            this.gpDataTableSetting.TabIndex = 16;
            this.gpDataTableSetting.TabStop = false;
            this.gpDataTableSetting.Text = "数据表设置";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(350, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "历史数据表";
            // 
            // txtHistoryDataTableName
            // 
            this.txtHistoryDataTableName.Location = new System.Drawing.Point(421, 20);
            this.txtHistoryDataTableName.Name = "txtHistoryDataTableName";
            this.txtHistoryDataTableName.Size = new System.Drawing.Size(197, 21);
            this.txtHistoryDataTableName.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(13, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "实时数据表";
            // 
            // txtNewDataTableName
            // 
            this.txtNewDataTableName.Location = new System.Drawing.Point(84, 20);
            this.txtNewDataTableName.Name = "txtNewDataTableName";
            this.txtNewDataTableName.Size = new System.Drawing.Size(197, 21);
            this.txtNewDataTableName.TabIndex = 15;
            // 
            // gpDataFieldSetting
            // 
            this.gpDataFieldSetting.Controls.Add(this.dvgDBFieldMapping);
            this.gpDataFieldSetting.Location = new System.Drawing.Point(12, 210);
            this.gpDataFieldSetting.Name = "gpDataFieldSetting";
            this.gpDataFieldSetting.Size = new System.Drawing.Size(696, 251);
            this.gpDataFieldSetting.TabIndex = 17;
            this.gpDataFieldSetting.TabStop = false;
            this.gpDataFieldSetting.Text = "字段设置";
            // 
            // dvgDBFieldMapping
            // 
            this.dvgDBFieldMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgDBFieldMapping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFieldName,
            this.colSourceOPCItem,
            this.colSouceCustom,
            this.colDataType,
            this.colSeqNamw,
            this.colAutoInc,
            this.colIsEntityIdentity});
            this.dvgDBFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgDBFieldMapping.Location = new System.Drawing.Point(3, 17);
            this.dvgDBFieldMapping.Name = "dvgDBFieldMapping";
            this.dvgDBFieldMapping.RowTemplate.Height = 23;
            this.dvgDBFieldMapping.Size = new System.Drawing.Size(690, 231);
            this.dvgDBFieldMapping.TabIndex = 1;
            // 
            // btnRemoveDBField
            // 
            this.btnRemoveDBField.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveDBField.Image")));
            this.btnRemoveDBField.Location = new System.Drawing.Point(714, 269);
            this.btnRemoveDBField.Name = "btnRemoveDBField";
            this.btnRemoveDBField.Size = new System.Drawing.Size(36, 36);
            this.btnRemoveDBField.TabIndex = 19;
            this.btnRemoveDBField.UseVisualStyleBackColor = true;
            this.btnRemoveDBField.Click += new System.EventHandler(this.btnRemoveDBField_Click);
            // 
            // btnAddDBField
            // 
            this.btnAddDBField.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDBField.Image")));
            this.btnAddDBField.Location = new System.Drawing.Point(714, 227);
            this.btnAddDBField.Name = "btnAddDBField";
            this.btnAddDBField.Size = new System.Drawing.Size(36, 36);
            this.btnAddDBField.TabIndex = 20;
            this.btnAddDBField.UseVisualStyleBackColor = true;
            this.btnAddDBField.Click += new System.EventHandler(this.btnAddDBField_Click);
            // 
            // colFieldName
            // 
            this.colFieldName.DataPropertyName = "FieldName";
            this.colFieldName.HeaderText = "字段名称";
            this.colFieldName.Name = "colFieldName";
            // 
            // colSourceOPCItem
            // 
            this.colSourceOPCItem.DataPropertyName = "SourceOPCItem";
            this.colSourceOPCItem.HeaderText = "OPC数据项";
            this.colSourceOPCItem.Name = "colSourceOPCItem";
            this.colSourceOPCItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSourceOPCItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colSouceCustom
            // 
            this.colSouceCustom.DataPropertyName = "SourceCustom";
            this.colSouceCustom.HeaderText = "自定义数据源";
            this.colSouceCustom.Name = "colSouceCustom";
            // 
            // colDataType
            // 
            this.colDataType.DataPropertyName = "DataType";
            this.colDataType.HeaderText = "数据类型";
            this.colDataType.Name = "colDataType";
            this.colDataType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDataType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colDataType.Width = 80;
            // 
            // colSeqNamw
            // 
            this.colSeqNamw.DataPropertyName = "SeqName";
            this.colSeqNamw.HeaderText = "取值序列";
            this.colSeqNamw.Name = "colSeqNamw";
            this.colSeqNamw.Width = 80;
            // 
            // colAutoInc
            // 
            this.colAutoInc.DataPropertyName = "AutoInc";
            this.colAutoInc.HeaderText = "是否自增";
            this.colAutoInc.Name = "colAutoInc";
            this.colAutoInc.Width = 80;
            // 
            // colIsEntityIdentity
            // 
            this.colIsEntityIdentity.DataPropertyName = "IsEntityIdentity";
            this.colIsEntityIdentity.HeaderText = "是否标识";
            this.colIsEntityIdentity.Name = "colIsEntityIdentity";
            this.colIsEntityIdentity.Width = 80;
            // 
            // FormDBConnectionSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 509);
            this.Controls.Add(this.btnAddDBField);
            this.Controls.Add(this.btnRemoveDBField);
            this.Controls.Add(this.gpDataFieldSetting);
            this.Controls.Add(this.gpDataTableSetting);
            this.Controls.Add(this.gpDBConnSetting);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTestConnection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDBConnectionSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库连接设置";
            this.gpDBConnSetting.ResumeLayout(false);
            this.gpDBConnSetting.PerformLayout();
            this.gpDataTableSetting.ResumeLayout(false);
            this.gpDataTableSetting.PerformLayout();
            this.gpDataFieldSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgDBFieldMapping)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gpDBConnSetting;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ComboBox cbDatabaseType;
        private System.Windows.Forms.GroupBox gpDataTableSetting;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNewDataTableName;
        private System.Windows.Forms.GroupBox gpDataFieldSetting;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHistoryDataTableName;
        private System.Windows.Forms.Button btnRemoveDBField;
        private System.Windows.Forms.Button btnAddDBField;
        private System.Windows.Forms.DataGridView dvgDBFieldMapping;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colSourceOPCItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSouceCustom;
        private System.Windows.Forms.DataGridViewComboBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeqNamw;
        private System.Windows.Forms.DataGridViewComboBoxColumn colAutoInc;
        private System.Windows.Forms.DataGridViewComboBoxColumn colIsEntityIdentity;
    }
}
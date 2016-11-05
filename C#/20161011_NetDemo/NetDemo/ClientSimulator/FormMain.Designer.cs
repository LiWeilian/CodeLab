namespace ClientSimulator
{
    partial class FormMain
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.gbRunMsg = new System.Windows.Forms.GroupBox();
            this.txtRunMsg = new System.Windows.Forms.TextBox();
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.btnConnToServer = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSendToServer = new System.Windows.Forms.Button();
            this.gbReceiveMsg = new System.Windows.Forms.GroupBox();
            this.txtRecMsg = new System.Windows.Forms.TextBox();
            this.gbSendMsg = new System.Windows.Forms.GroupBox();
            this.txtSendMsg = new System.Windows.Forms.TextBox();
            this.pnlLeft.SuspendLayout();
            this.gbRunMsg.SuspendLayout();
            this.gbConnection.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbReceiveMsg.SuspendLayout();
            this.gbSendMsg.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbRunMsg);
            this.pnlLeft.Controls.Add(this.gbConnection);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(231, 437);
            this.pnlLeft.TabIndex = 3;
            // 
            // gbRunMsg
            // 
            this.gbRunMsg.Controls.Add(this.txtRunMsg);
            this.gbRunMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRunMsg.Location = new System.Drawing.Point(0, 147);
            this.gbRunMsg.Name = "gbRunMsg";
            this.gbRunMsg.Size = new System.Drawing.Size(231, 290);
            this.gbRunMsg.TabIndex = 2;
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
            this.txtRunMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRunMsg.Size = new System.Drawing.Size(225, 270);
            this.txtRunMsg.TabIndex = 1;
            // 
            // gbConnection
            // 
            this.gbConnection.Controls.Add(this.btnConnToServer);
            this.gbConnection.Controls.Add(this.txtServerPort);
            this.gbConnection.Controls.Add(this.label2);
            this.gbConnection.Controls.Add(this.label1);
            this.gbConnection.Controls.Add(this.txtServerIP);
            this.gbConnection.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbConnection.Location = new System.Drawing.Point(0, 0);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Size = new System.Drawing.Size(231, 147);
            this.gbConnection.TabIndex = 1;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "连接设置";
            // 
            // btnConnToServer
            // 
            this.btnConnToServer.Location = new System.Drawing.Point(91, 105);
            this.btnConnToServer.Name = "btnConnToServer";
            this.btnConnToServer.Size = new System.Drawing.Size(75, 23);
            this.btnConnToServer.TabIndex = 4;
            this.btnConnToServer.Text = "连接";
            this.btnConnToServer.UseVisualStyleBackColor = true;
            this.btnConnToServer.Click += new System.EventHandler(this.btnConnToServer_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(13, 78);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(153, 21);
            this.txtServerPort.TabIndex = 3;
            this.txtServerPort.Text = "30000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(13, 39);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(153, 21);
            this.txtServerIP.TabIndex = 0;
            this.txtServerIP.Text = "172.16.1.25";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSendToServer);
            this.panel1.Controls.Add(this.gbReceiveMsg);
            this.panel1.Controls.Add(this.gbSendMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(231, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 437);
            this.panel1.TabIndex = 4;
            // 
            // btnSendToServer
            // 
            this.btnSendToServer.Location = new System.Drawing.Point(284, 205);
            this.btnSendToServer.Name = "btnSendToServer";
            this.btnSendToServer.Size = new System.Drawing.Size(75, 23);
            this.btnSendToServer.TabIndex = 4;
            this.btnSendToServer.Text = "发送";
            this.btnSendToServer.UseVisualStyleBackColor = true;
            this.btnSendToServer.Click += new System.EventHandler(this.btnSendToServer_Click);
            // 
            // gbReceiveMsg
            // 
            this.gbReceiveMsg.Controls.Add(this.txtRecMsg);
            this.gbReceiveMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbReceiveMsg.Location = new System.Drawing.Point(0, 227);
            this.gbReceiveMsg.Name = "gbReceiveMsg";
            this.gbReceiveMsg.Size = new System.Drawing.Size(381, 210);
            this.gbReceiveMsg.TabIndex = 3;
            this.gbReceiveMsg.TabStop = false;
            this.gbReceiveMsg.Text = "接收消息";
            // 
            // txtRecMsg
            // 
            this.txtRecMsg.BackColor = System.Drawing.SystemColors.Window;
            this.txtRecMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecMsg.Location = new System.Drawing.Point(3, 17);
            this.txtRecMsg.Multiline = true;
            this.txtRecMsg.Name = "txtRecMsg";
            this.txtRecMsg.ReadOnly = true;
            this.txtRecMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecMsg.Size = new System.Drawing.Size(375, 190);
            this.txtRecMsg.TabIndex = 1;
            // 
            // gbSendMsg
            // 
            this.gbSendMsg.Controls.Add(this.txtSendMsg);
            this.gbSendMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSendMsg.Location = new System.Drawing.Point(0, 0);
            this.gbSendMsg.Name = "gbSendMsg";
            this.gbSendMsg.Size = new System.Drawing.Size(381, 199);
            this.gbSendMsg.TabIndex = 2;
            this.gbSendMsg.TabStop = false;
            this.gbSendMsg.Text = "发送消息";
            // 
            // txtSendMsg
            // 
            this.txtSendMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSendMsg.Location = new System.Drawing.Point(3, 17);
            this.txtSendMsg.Multiline = true;
            this.txtSendMsg.Name = "txtSendMsg";
            this.txtSendMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSendMsg.Size = new System.Drawing.Size(375, 179);
            this.txtSendMsg.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 437);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户端模拟器";
            this.pnlLeft.ResumeLayout(false);
            this.gbRunMsg.ResumeLayout(false);
            this.gbRunMsg.PerformLayout();
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbReceiveMsg.ResumeLayout(false);
            this.gbReceiveMsg.PerformLayout();
            this.gbSendMsg.ResumeLayout(false);
            this.gbSendMsg.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.Button btnConnToServer;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbReceiveMsg;
        private System.Windows.Forms.GroupBox gbSendMsg;
        private System.Windows.Forms.GroupBox gbRunMsg;
        private System.Windows.Forms.Button btnSendToServer;
        private System.Windows.Forms.TextBox txtRunMsg;
        private System.Windows.Forms.TextBox txtRecMsg;
        private System.Windows.Forms.TextBox txtSendMsg;
    }
}


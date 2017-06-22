namespace CSharpCallDCOM
{
    partial class Form1
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
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtClsID = new System.Windows.Forms.TextBox();
            this.btnCall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(53, 44);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(383, 21);
            this.txtServerIP.TabIndex = 0;
            // 
            // txtClsID
            // 
            this.txtClsID.Location = new System.Drawing.Point(54, 92);
            this.txtClsID.Name = "txtClsID";
            this.txtClsID.Size = new System.Drawing.Size(382, 21);
            this.txtClsID.TabIndex = 1;
            this.txtClsID.Text = "F8582CF2-88FB-11D0-B850-00C0F0104305";
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(361, 144);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(75, 23);
            this.btnCall.TabIndex = 2;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 266);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.txtClsID);
            this.Controls.Add(this.txtServerIP);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.TextBox txtClsID;
        private System.Windows.Forms.Button btnCall;
    }
}


namespace MapCompare
{
    partial class FormMapCompare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMapCompare));
            this.tsMapTools = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsMapEditTools = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pnlLayers = new System.Windows.Forms.Panel();
            this.tocCtrlRight = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.tocCtrlLeft = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.pnlMapLeft = new System.Windows.Forms.Panel();
            this.tcMapLeft = new System.Windows.Forms.TabControl();
            this.tpMapLeft = new System.Windows.Forms.TabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.mapCtrlLeft = new ESRI.ArcGIS.Controls.AxMapControl();
            this.pnlMapRight = new System.Windows.Forms.Panel();
            this.tcMapRight = new System.Windows.Forms.TabControl();
            this.tpMapRight = new System.Windows.Forms.TabPage();
            this.mapCtrlRight = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tsMapTools.SuspendLayout();
            this.pnlLayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlLeft)).BeginInit();
            this.pnlMapLeft.SuspendLayout();
            this.tcMapLeft.SuspendLayout();
            this.tpMapLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlLeft)).BeginInit();
            this.pnlMapRight.SuspendLayout();
            this.tcMapRight.SuspendLayout();
            this.tpMapRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlRight)).BeginInit();
            this.SuspendLayout();
            // 
            // tsMapTools
            // 
            this.tsMapTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.tsMapTools.Location = new System.Drawing.Point(0, 0);
            this.tsMapTools.Name = "tsMapTools";
            this.tsMapTools.Size = new System.Drawing.Size(913, 25);
            this.tsMapTools.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // tsMapEditTools
            // 
            this.tsMapEditTools.Location = new System.Drawing.Point(0, 25);
            this.tsMapEditTools.Name = "tsMapEditTools";
            this.tsMapEditTools.Size = new System.Drawing.Size(913, 25);
            this.tsMapEditTools.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(913, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pnlLayers
            // 
            this.pnlLayers.Controls.Add(this.tocCtrlRight);
            this.pnlLayers.Controls.Add(this.tocCtrlLeft);
            this.pnlLayers.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLayers.Location = new System.Drawing.Point(0, 50);
            this.pnlLayers.Name = "pnlLayers";
            this.pnlLayers.Size = new System.Drawing.Size(200, 494);
            this.pnlLayers.TabIndex = 5;
            // 
            // tocCtrlRight
            // 
            this.tocCtrlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tocCtrlRight.Location = new System.Drawing.Point(0, 264);
            this.tocCtrlRight.Name = "tocCtrlRight";
            this.tocCtrlRight.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tocCtrlRight.OcxState")));
            this.tocCtrlRight.Size = new System.Drawing.Size(200, 230);
            this.tocCtrlRight.TabIndex = 1;
            // 
            // tocCtrlLeft
            // 
            this.tocCtrlLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.tocCtrlLeft.Location = new System.Drawing.Point(0, 0);
            this.tocCtrlLeft.Name = "tocCtrlLeft";
            this.tocCtrlLeft.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tocCtrlLeft.OcxState")));
            this.tocCtrlLeft.Size = new System.Drawing.Size(200, 264);
            this.tocCtrlLeft.TabIndex = 0;
            // 
            // pnlMapLeft
            // 
            this.pnlMapLeft.Controls.Add(this.tcMapLeft);
            this.pnlMapLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMapLeft.Location = new System.Drawing.Point(200, 50);
            this.pnlMapLeft.Name = "pnlMapLeft";
            this.pnlMapLeft.Size = new System.Drawing.Size(367, 494);
            this.pnlMapLeft.TabIndex = 3;
            // 
            // tcMapLeft
            // 
            this.tcMapLeft.Controls.Add(this.tpMapLeft);
            this.tcMapLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMapLeft.Location = new System.Drawing.Point(0, 0);
            this.tcMapLeft.Name = "tcMapLeft";
            this.tcMapLeft.SelectedIndex = 0;
            this.tcMapLeft.Size = new System.Drawing.Size(367, 494);
            this.tcMapLeft.TabIndex = 1;
            // 
            // tpMapLeft
            // 
            this.tpMapLeft.Controls.Add(this.axLicenseControl1);
            this.tpMapLeft.Controls.Add(this.mapCtrlLeft);
            this.tpMapLeft.Location = new System.Drawing.Point(4, 22);
            this.tpMapLeft.Name = "tpMapLeft";
            this.tpMapLeft.Padding = new System.Windows.Forms.Padding(3);
            this.tpMapLeft.Size = new System.Drawing.Size(359, 468);
            this.tpMapLeft.TabIndex = 0;
            this.tpMapLeft.UseVisualStyleBackColor = true;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(129, 310);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 4;
            // 
            // mapCtrlLeft
            // 
            this.mapCtrlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapCtrlLeft.Location = new System.Drawing.Point(3, 3);
            this.mapCtrlLeft.Name = "mapCtrlLeft";
            this.mapCtrlLeft.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mapCtrlLeft.OcxState")));
            this.mapCtrlLeft.Size = new System.Drawing.Size(353, 462);
            this.mapCtrlLeft.TabIndex = 3;
            // 
            // pnlMapRight
            // 
            this.pnlMapRight.Controls.Add(this.tcMapRight);
            this.pnlMapRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMapRight.Location = new System.Drawing.Point(567, 50);
            this.pnlMapRight.Name = "pnlMapRight";
            this.pnlMapRight.Size = new System.Drawing.Size(346, 494);
            this.pnlMapRight.TabIndex = 4;
            // 
            // tcMapRight
            // 
            this.tcMapRight.Controls.Add(this.tpMapRight);
            this.tcMapRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMapRight.Location = new System.Drawing.Point(0, 0);
            this.tcMapRight.Name = "tcMapRight";
            this.tcMapRight.SelectedIndex = 0;
            this.tcMapRight.Size = new System.Drawing.Size(346, 494);
            this.tcMapRight.TabIndex = 3;
            // 
            // tpMapRight
            // 
            this.tpMapRight.Controls.Add(this.mapCtrlRight);
            this.tpMapRight.Location = new System.Drawing.Point(4, 22);
            this.tpMapRight.Name = "tpMapRight";
            this.tpMapRight.Padding = new System.Windows.Forms.Padding(3);
            this.tpMapRight.Size = new System.Drawing.Size(338, 468);
            this.tpMapRight.TabIndex = 0;
            this.tpMapRight.UseVisualStyleBackColor = true;
            // 
            // mapCtrlRight
            // 
            this.mapCtrlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapCtrlRight.Location = new System.Drawing.Point(3, 3);
            this.mapCtrlRight.Name = "mapCtrlRight";
            this.mapCtrlRight.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mapCtrlRight.OcxState")));
            this.mapCtrlRight.Size = new System.Drawing.Size(332, 462);
            this.mapCtrlRight.TabIndex = 1;
            // 
            // FormMapCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 566);
            this.Controls.Add(this.pnlMapRight);
            this.Controls.Add(this.pnlMapLeft);
            this.Controls.Add(this.pnlLayers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsMapEditTools);
            this.Controls.Add(this.tsMapTools);
            this.Name = "FormMapCompare";
            this.Text = "地图对比";
            this.tsMapTools.ResumeLayout(false);
            this.tsMapTools.PerformLayout();
            this.pnlLayers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlLeft)).EndInit();
            this.pnlMapLeft.ResumeLayout(false);
            this.tcMapLeft.ResumeLayout(false);
            this.tpMapLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlLeft)).EndInit();
            this.pnlMapRight.ResumeLayout(false);
            this.tcMapRight.ResumeLayout(false);
            this.tpMapRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlRight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMapTools;
        private System.Windows.Forms.ToolStrip tsMapEditTools;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlMapLeft;
        private System.Windows.Forms.TabControl tcMapLeft;
        private System.Windows.Forms.TabPage tpMapLeft;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel pnlMapRight;
        private System.Windows.Forms.TabControl tcMapRight;
        private System.Windows.Forms.TabPage tpMapRight;
        private ESRI.ArcGIS.Controls.AxMapControl mapCtrlRight;
        private ESRI.ArcGIS.Controls.AxMapControl mapCtrlLeft;
        private System.Windows.Forms.Panel pnlLayers;
        private ESRI.ArcGIS.Controls.AxTOCControl tocCtrlRight;
        private ESRI.ArcGIS.Controls.AxTOCControl tocCtrlLeft;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}


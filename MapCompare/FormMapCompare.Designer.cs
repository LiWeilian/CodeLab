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
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsMapEditTools = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pnlLayers = new System.Windows.Forms.Panel();
            this.pnlMapLeft = new System.Windows.Forms.Panel();
            this.tcMapLeft = new System.Windows.Forms.TabControl();
            this.tpMapLeft = new System.Windows.Forms.TabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.mapCtrlLeft = new ESRI.ArcGIS.Controls.AxMapControl();
            this.pnlMapRight = new System.Windows.Forms.Panel();
            this.tcMapRight = new System.Windows.Forms.TabControl();
            this.tpMapRight = new System.Windows.Forms.TabPage();
            this.mapCtrlRight = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tspPriorView = new System.Windows.Forms.ToolStripButton();
            this.tsbNextView = new System.Windows.Forms.ToolStripButton();
            this.tsbPan = new System.Windows.Forms.ToolStripButton();
            this.tsbFullExtent = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshView = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectElement = new System.Windows.Forms.ToolStripButton();
            this.tcLayers = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tocCtrlLeft = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.tocCtrlRight = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tsMapTools.SuspendLayout();
            this.tsMapEditTools.SuspendLayout();
            this.pnlLayers.SuspendLayout();
            this.pnlMapLeft.SuspendLayout();
            this.tcMapLeft.SuspendLayout();
            this.tpMapLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlLeft)).BeginInit();
            this.pnlMapRight.SuspendLayout();
            this.tcMapRight.SuspendLayout();
            this.tpMapRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlRight)).BeginInit();
            this.tcLayers.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlRight)).BeginInit();
            this.SuspendLayout();
            // 
            // tsMapTools
            // 
            this.tsMapTools.AutoSize = false;
            this.tsMapTools.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMapTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelectElement,
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.tspPriorView,
            this.tsbNextView,
            this.tsbPan,
            this.tsbFullExtent,
            this.tsbRefreshView});
            this.tsMapTools.Location = new System.Drawing.Point(0, 0);
            this.tsMapTools.Name = "tsMapTools";
            this.tsMapTools.Size = new System.Drawing.Size(913, 39);
            this.tsMapTools.TabIndex = 0;
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomIn.Image")));
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(36, 44);
            this.tsbZoomIn.Text = "toolStripButton1";
            this.tsbZoomIn.ToolTipText = "放大视图";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsMapEditTools
            // 
            this.tsMapEditTools.AutoSize = false;
            this.tsMapEditTools.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMapEditTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.tsMapEditTools.Location = new System.Drawing.Point(0, 39);
            this.tsMapEditTools.Name = "tsMapEditTools";
            this.tsMapEditTools.Size = new System.Drawing.Size(913, 39);
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
            this.pnlLayers.Controls.Add(this.tcLayers);
            this.pnlLayers.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLayers.Location = new System.Drawing.Point(0, 78);
            this.pnlLayers.Name = "pnlLayers";
            this.pnlLayers.Size = new System.Drawing.Size(200, 466);
            this.pnlLayers.TabIndex = 5;
            // 
            // pnlMapLeft
            // 
            this.pnlMapLeft.Controls.Add(this.tcMapLeft);
            this.pnlMapLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMapLeft.Location = new System.Drawing.Point(200, 78);
            this.pnlMapLeft.Name = "pnlMapLeft";
            this.pnlMapLeft.Size = new System.Drawing.Size(380, 466);
            this.pnlMapLeft.TabIndex = 3;
            // 
            // tcMapLeft
            // 
            this.tcMapLeft.Controls.Add(this.tpMapLeft);
            this.tcMapLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMapLeft.Location = new System.Drawing.Point(0, 0);
            this.tcMapLeft.Name = "tcMapLeft";
            this.tcMapLeft.SelectedIndex = 0;
            this.tcMapLeft.Size = new System.Drawing.Size(380, 466);
            this.tcMapLeft.TabIndex = 1;
            // 
            // tpMapLeft
            // 
            this.tpMapLeft.Controls.Add(this.axLicenseControl1);
            this.tpMapLeft.Controls.Add(this.mapCtrlLeft);
            this.tpMapLeft.Location = new System.Drawing.Point(4, 22);
            this.tpMapLeft.Name = "tpMapLeft";
            this.tpMapLeft.Padding = new System.Windows.Forms.Padding(3);
            this.tpMapLeft.Size = new System.Drawing.Size(372, 440);
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
            this.mapCtrlLeft.Size = new System.Drawing.Size(366, 434);
            this.mapCtrlLeft.TabIndex = 3;
            this.mapCtrlLeft.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.mapCtrlLeft_OnMouseDown);
            // 
            // pnlMapRight
            // 
            this.pnlMapRight.Controls.Add(this.tcMapRight);
            this.pnlMapRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMapRight.Location = new System.Drawing.Point(580, 78);
            this.pnlMapRight.Name = "pnlMapRight";
            this.pnlMapRight.Size = new System.Drawing.Size(333, 466);
            this.pnlMapRight.TabIndex = 4;
            // 
            // tcMapRight
            // 
            this.tcMapRight.Controls.Add(this.tpMapRight);
            this.tcMapRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMapRight.Location = new System.Drawing.Point(0, 0);
            this.tcMapRight.Name = "tcMapRight";
            this.tcMapRight.SelectedIndex = 0;
            this.tcMapRight.Size = new System.Drawing.Size(333, 466);
            this.tcMapRight.TabIndex = 3;
            // 
            // tpMapRight
            // 
            this.tpMapRight.Controls.Add(this.mapCtrlRight);
            this.tpMapRight.Location = new System.Drawing.Point(4, 22);
            this.tpMapRight.Name = "tpMapRight";
            this.tpMapRight.Padding = new System.Windows.Forms.Padding(3);
            this.tpMapRight.Size = new System.Drawing.Size(325, 440);
            this.tpMapRight.TabIndex = 0;
            this.tpMapRight.UseVisualStyleBackColor = true;
            // 
            // mapCtrlRight
            // 
            this.mapCtrlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapCtrlRight.Location = new System.Drawing.Point(3, 3);
            this.mapCtrlRight.Name = "mapCtrlRight";
            this.mapCtrlRight.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mapCtrlRight.OcxState")));
            this.mapCtrlRight.Size = new System.Drawing.Size(319, 434);
            this.mapCtrlRight.TabIndex = 1;
            this.mapCtrlRight.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.mapCtrlRight_OnMouseDown);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomOut.Image")));
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(36, 44);
            this.tsbZoomOut.Text = "toolStripButton2";
            this.tsbZoomOut.ToolTipText = "缩小视图";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // tspPriorView
            // 
            this.tspPriorView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspPriorView.Image = ((System.Drawing.Image)(resources.GetObject("tspPriorView.Image")));
            this.tspPriorView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspPriorView.Name = "tspPriorView";
            this.tspPriorView.Size = new System.Drawing.Size(36, 44);
            this.tspPriorView.Text = "toolStripButton3";
            this.tspPriorView.ToolTipText = "前一视图";
            this.tspPriorView.Click += new System.EventHandler(this.tspPriorView_Click);
            // 
            // tsbNextView
            // 
            this.tsbNextView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextView.Image = ((System.Drawing.Image)(resources.GetObject("tsbNextView.Image")));
            this.tsbNextView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextView.Name = "tsbNextView";
            this.tsbNextView.Size = new System.Drawing.Size(36, 44);
            this.tsbNextView.Text = "toolStripButton4";
            this.tsbNextView.ToolTipText = "后一视图";
            this.tsbNextView.Click += new System.EventHandler(this.tsbNextView_Click);
            // 
            // tsbPan
            // 
            this.tsbPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPan.Image = ((System.Drawing.Image)(resources.GetObject("tsbPan.Image")));
            this.tsbPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPan.Name = "tsbPan";
            this.tsbPan.Size = new System.Drawing.Size(36, 44);
            this.tsbPan.Text = "toolStripButton5";
            this.tsbPan.ToolTipText = "平移视图";
            this.tsbPan.Click += new System.EventHandler(this.tsbPan_Click);
            // 
            // tsbFullExtent
            // 
            this.tsbFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFullExtent.Image = ((System.Drawing.Image)(resources.GetObject("tsbFullExtent.Image")));
            this.tsbFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullExtent.Name = "tsbFullExtent";
            this.tsbFullExtent.Size = new System.Drawing.Size(36, 44);
            this.tsbFullExtent.Text = "toolStripButton6";
            this.tsbFullExtent.ToolTipText = "全图";
            this.tsbFullExtent.Click += new System.EventHandler(this.tsbFullExtent_Click);
            // 
            // tsbRefreshView
            // 
            this.tsbRefreshView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefreshView.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshView.Image")));
            this.tsbRefreshView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshView.Name = "tsbRefreshView";
            this.tsbRefreshView.Size = new System.Drawing.Size(36, 44);
            this.tsbRefreshView.Text = "toolStripButton7";
            this.tsbRefreshView.ToolTipText = "刷新视图";
            this.tsbRefreshView.Click += new System.EventHandler(this.tsbRefreshView_Click);
            // 
            // tsbSelectElement
            // 
            this.tsbSelectElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelectElement.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelectElement.Image")));
            this.tsbSelectElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectElement.Name = "tsbSelectElement";
            this.tsbSelectElement.Size = new System.Drawing.Size(36, 36);
            this.tsbSelectElement.Text = "toolStripButton1";
            this.tsbSelectElement.ToolTipText = "选择";
            this.tsbSelectElement.Click += new System.EventHandler(this.tsbSelectElement_Click);
            // 
            // tcLayers
            // 
            this.tcLayers.Controls.Add(this.tabPage1);
            this.tcLayers.Controls.Add(this.tabPage2);
            this.tcLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcLayers.Location = new System.Drawing.Point(0, 0);
            this.tcLayers.Name = "tcLayers";
            this.tcLayers.SelectedIndex = 0;
            this.tcLayers.Size = new System.Drawing.Size(200, 466);
            this.tcLayers.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tocCtrlLeft);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 440);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tocCtrlRight);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tocCtrlLeft
            // 
            this.tocCtrlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tocCtrlLeft.Location = new System.Drawing.Point(3, 3);
            this.tocCtrlLeft.Name = "tocCtrlLeft";
            this.tocCtrlLeft.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tocCtrlLeft.OcxState")));
            this.tocCtrlLeft.Size = new System.Drawing.Size(186, 434);
            this.tocCtrlLeft.TabIndex = 1;
            // 
            // tocCtrlRight
            // 
            this.tocCtrlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tocCtrlRight.Location = new System.Drawing.Point(3, 3);
            this.tocCtrlRight.Name = "tocCtrlRight";
            this.tocCtrlRight.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tocCtrlRight.OcxState")));
            this.tocCtrlRight.Size = new System.Drawing.Size(186, 440);
            this.tocCtrlRight.TabIndex = 2;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton2.Text = "toolStripButton2";
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
            this.Resize += new System.EventHandler(this.FormMapCompare_Resize);
            this.tsMapTools.ResumeLayout(false);
            this.tsMapTools.PerformLayout();
            this.tsMapEditTools.ResumeLayout(false);
            this.tsMapEditTools.PerformLayout();
            this.pnlLayers.ResumeLayout(false);
            this.pnlMapLeft.ResumeLayout(false);
            this.tcMapLeft.ResumeLayout(false);
            this.tpMapLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlLeft)).EndInit();
            this.pnlMapRight.ResumeLayout(false);
            this.tcMapRight.ResumeLayout(false);
            this.tpMapRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlRight)).EndInit();
            this.tcLayers.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlRight)).EndInit();
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
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.Panel pnlMapRight;
        private System.Windows.Forms.TabControl tcMapRight;
        private System.Windows.Forms.TabPage tpMapRight;
        private ESRI.ArcGIS.Controls.AxMapControl mapCtrlRight;
        private ESRI.ArcGIS.Controls.AxMapControl mapCtrlLeft;
        private System.Windows.Forms.Panel pnlLayers;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripButton tspPriorView;
        private System.Windows.Forms.ToolStripButton tsbNextView;
        private System.Windows.Forms.ToolStripButton tsbPan;
        private System.Windows.Forms.ToolStripButton tsbFullExtent;
        private System.Windows.Forms.ToolStripButton tsbRefreshView;
        private System.Windows.Forms.ToolStripButton tsbSelectElement;
        private System.Windows.Forms.TabControl tcLayers;
        private System.Windows.Forms.TabPage tabPage1;
        private ESRI.ArcGIS.Controls.AxTOCControl tocCtrlLeft;
        private System.Windows.Forms.TabPage tabPage2;
        private ESRI.ArcGIS.Controls.AxTOCControl tocCtrlRight;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}


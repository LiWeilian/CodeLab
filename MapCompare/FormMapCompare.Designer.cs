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
            this.tsbSelectElement = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tspPriorView = new System.Windows.Forms.ToolStripButton();
            this.tsbNextView = new System.Windows.Forms.ToolStripButton();
            this.tsbPan = new System.Windows.Forms.ToolStripButton();
            this.tsbFullExtent = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshView = new System.Windows.Forms.ToolStripButton();
            this.tsMapEditTools = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMapScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslCurrCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlLayers = new System.Windows.Forms.Panel();
            this.tcLayers = new System.Windows.Forms.TabControl();
            this.tpMapLeft = new System.Windows.Forms.TabPage();
            this.tocCtrlLeft = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.tpMapRight = new System.Windows.Forms.TabPage();
            this.tocCtrlRight = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.pnlMapLeft = new System.Windows.Forms.Panel();
            this.mapCtrlLeft = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tsMapLeftTools = new System.Windows.Forms.ToolStrip();
            this.pnlMapRight = new System.Windows.Forms.Panel();
            this.mapCtrlRight = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tsMapRightTools = new System.Windows.Forms.ToolStrip();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.tsbOpenRasterLeft = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenRasterRight = new System.Windows.Forms.ToolStripButton();
            this.tsMapTools.SuspendLayout();
            this.tsMapEditTools.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlLayers.SuspendLayout();
            this.tcLayers.SuspendLayout();
            this.tpMapLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlLeft)).BeginInit();
            this.tpMapRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlRight)).BeginInit();
            this.pnlMapLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlLeft)).BeginInit();
            this.tsMapLeftTools.SuspendLayout();
            this.pnlMapRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlRight)).BeginInit();
            this.tsMapRightTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
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
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomIn.Image")));
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(36, 36);
            this.tsbZoomIn.Text = "toolStripButton1";
            this.tsbZoomIn.ToolTipText = "放大视图";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomOut.Image")));
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(36, 36);
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
            this.tspPriorView.Size = new System.Drawing.Size(36, 36);
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
            this.tsbNextView.Size = new System.Drawing.Size(36, 36);
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
            this.tsbPan.Size = new System.Drawing.Size(36, 36);
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
            this.tsbFullExtent.Size = new System.Drawing.Size(36, 36);
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
            this.tsbRefreshView.Size = new System.Drawing.Size(36, 36);
            this.tsbRefreshView.Text = "toolStripButton7";
            this.tsbRefreshView.ToolTipText = "刷新视图";
            this.tsbRefreshView.Click += new System.EventHandler(this.tsbRefreshView_Click);
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslMapScale,
            this.toolStripStatusLabel2,
            this.tsslCurrCoords});
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(913, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(56, 17);
            this.toolStripStatusLabel1.Text = "比例尺：";
            // 
            // tsslMapScale
            // 
            this.tsslMapScale.Name = "tsslMapScale";
            this.tsslMapScale.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel2.Text = "坐标：";
            // 
            // tsslCurrCoords
            // 
            this.tsslCurrCoords.Name = "tsslCurrCoords";
            this.tsslCurrCoords.Size = new System.Drawing.Size(0, 17);
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
            // tcLayers
            // 
            this.tcLayers.Controls.Add(this.tpMapLeft);
            this.tcLayers.Controls.Add(this.tpMapRight);
            this.tcLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcLayers.Location = new System.Drawing.Point(0, 0);
            this.tcLayers.Name = "tcLayers";
            this.tcLayers.SelectedIndex = 0;
            this.tcLayers.Size = new System.Drawing.Size(200, 466);
            this.tcLayers.TabIndex = 2;
            // 
            // tpMapLeft
            // 
            this.tpMapLeft.Controls.Add(this.tocCtrlLeft);
            this.tpMapLeft.Location = new System.Drawing.Point(4, 22);
            this.tpMapLeft.Name = "tpMapLeft";
            this.tpMapLeft.Padding = new System.Windows.Forms.Padding(3);
            this.tpMapLeft.Size = new System.Drawing.Size(192, 440);
            this.tpMapLeft.TabIndex = 0;
            this.tpMapLeft.Text = "地图1";
            this.tpMapLeft.UseVisualStyleBackColor = true;
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
            // tpMapRight
            // 
            this.tpMapRight.Controls.Add(this.tocCtrlRight);
            this.tpMapRight.Location = new System.Drawing.Point(4, 22);
            this.tpMapRight.Name = "tpMapRight";
            this.tpMapRight.Padding = new System.Windows.Forms.Padding(3);
            this.tpMapRight.Size = new System.Drawing.Size(192, 440);
            this.tpMapRight.TabIndex = 1;
            this.tpMapRight.Text = "地图2";
            this.tpMapRight.UseVisualStyleBackColor = true;
            // 
            // tocCtrlRight
            // 
            this.tocCtrlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tocCtrlRight.Location = new System.Drawing.Point(3, 3);
            this.tocCtrlRight.Name = "tocCtrlRight";
            this.tocCtrlRight.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("tocCtrlRight.OcxState")));
            this.tocCtrlRight.Size = new System.Drawing.Size(186, 434);
            this.tocCtrlRight.TabIndex = 2;
            // 
            // pnlMapLeft
            // 
            this.pnlMapLeft.Controls.Add(this.mapCtrlLeft);
            this.pnlMapLeft.Controls.Add(this.tsMapLeftTools);
            this.pnlMapLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMapLeft.Location = new System.Drawing.Point(200, 78);
            this.pnlMapLeft.Name = "pnlMapLeft";
            this.pnlMapLeft.Size = new System.Drawing.Size(335, 466);
            this.pnlMapLeft.TabIndex = 3;
            // 
            // mapCtrlLeft
            // 
            this.mapCtrlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapCtrlLeft.Location = new System.Drawing.Point(0, 25);
            this.mapCtrlLeft.Margin = new System.Windows.Forms.Padding(5);
            this.mapCtrlLeft.Name = "mapCtrlLeft";
            this.mapCtrlLeft.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mapCtrlLeft.OcxState")));
            this.mapCtrlLeft.Size = new System.Drawing.Size(335, 441);
            this.mapCtrlLeft.TabIndex = 4;
            this.mapCtrlLeft.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.mapCtrlLeft_OnMouseDown);
            this.mapCtrlLeft.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.mapCtrlLeft_OnMouseMove);
            this.mapCtrlLeft.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.mapCtrlLeft_OnViewRefreshed);
            // 
            // tsMapLeftTools
            // 
            this.tsMapLeftTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenRasterLeft});
            this.tsMapLeftTools.Location = new System.Drawing.Point(0, 0);
            this.tsMapLeftTools.Name = "tsMapLeftTools";
            this.tsMapLeftTools.Size = new System.Drawing.Size(335, 25);
            this.tsMapLeftTools.TabIndex = 5;
            this.tsMapLeftTools.Text = "toolStrip1";
            // 
            // pnlMapRight
            // 
            this.pnlMapRight.Controls.Add(this.mapCtrlRight);
            this.pnlMapRight.Controls.Add(this.tsMapRightTools);
            this.pnlMapRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMapRight.Location = new System.Drawing.Point(535, 78);
            this.pnlMapRight.Name = "pnlMapRight";
            this.pnlMapRight.Size = new System.Drawing.Size(378, 466);
            this.pnlMapRight.TabIndex = 4;
            // 
            // mapCtrlRight
            // 
            this.mapCtrlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapCtrlRight.Location = new System.Drawing.Point(0, 25);
            this.mapCtrlRight.Margin = new System.Windows.Forms.Padding(5);
            this.mapCtrlRight.Name = "mapCtrlRight";
            this.mapCtrlRight.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mapCtrlRight.OcxState")));
            this.mapCtrlRight.Size = new System.Drawing.Size(378, 441);
            this.mapCtrlRight.TabIndex = 4;
            this.mapCtrlRight.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.mapCtrlRight_OnMouseDown);
            this.mapCtrlRight.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.mapCtrlRight_OnMouseMove);
            this.mapCtrlRight.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.mapCtrlRight_OnViewRefreshed);
            // 
            // tsMapRightTools
            // 
            this.tsMapRightTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenRasterRight});
            this.tsMapRightTools.Location = new System.Drawing.Point(0, 0);
            this.tsMapRightTools.Name = "tsMapRightTools";
            this.tsMapRightTools.Size = new System.Drawing.Size(378, 25);
            this.tsMapRightTools.TabIndex = 5;
            this.tsMapRightTools.Text = "toolStrip2";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(440, 267);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 6;
            // 
            // tsbOpenRasterLeft
            // 
            this.tsbOpenRasterLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenRasterLeft.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenRasterLeft.Image")));
            this.tsbOpenRasterLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenRasterLeft.Name = "tsbOpenRasterLeft";
            this.tsbOpenRasterLeft.Size = new System.Drawing.Size(23, 22);
            this.tsbOpenRasterLeft.Text = "toolStripButton3";
            this.tsbOpenRasterLeft.ToolTipText = "打开影像图";
            this.tsbOpenRasterLeft.Click += new System.EventHandler(this.tsbOpenRasterLeft_Click);
            // 
            // tsbOpenRasterRight
            // 
            this.tsbOpenRasterRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenRasterRight.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenRasterRight.Image")));
            this.tsbOpenRasterRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenRasterRight.Name = "tsbOpenRasterRight";
            this.tsbOpenRasterRight.Size = new System.Drawing.Size(23, 22);
            this.tsbOpenRasterRight.Text = "toolStripButton4";
            this.tsbOpenRasterRight.ToolTipText = "打开影像图";
            this.tsbOpenRasterRight.Click += new System.EventHandler(this.tsbOpenRasterRight_Click);
            // 
            // FormMapCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 566);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.pnlMapRight);
            this.Controls.Add(this.pnlMapLeft);
            this.Controls.Add(this.pnlLayers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tsMapEditTools);
            this.Controls.Add(this.tsMapTools);
            this.Name = "FormMapCompare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地图对比";
            this.Resize += new System.EventHandler(this.FormMapCompare_Resize);
            this.tsMapTools.ResumeLayout(false);
            this.tsMapTools.PerformLayout();
            this.tsMapEditTools.ResumeLayout(false);
            this.tsMapEditTools.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlLayers.ResumeLayout(false);
            this.tcLayers.ResumeLayout(false);
            this.tpMapLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlLeft)).EndInit();
            this.tpMapRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tocCtrlRight)).EndInit();
            this.pnlMapLeft.ResumeLayout(false);
            this.pnlMapLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlLeft)).EndInit();
            this.tsMapLeftTools.ResumeLayout(false);
            this.tsMapLeftTools.PerformLayout();
            this.pnlMapRight.ResumeLayout(false);
            this.pnlMapRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapCtrlRight)).EndInit();
            this.tsMapRightTools.ResumeLayout(false);
            this.tsMapRightTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMapTools;
        private System.Windows.Forms.ToolStrip tsMapEditTools;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlMapLeft;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.Panel pnlMapRight;
        private System.Windows.Forms.Panel pnlLayers;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripButton tspPriorView;
        private System.Windows.Forms.ToolStripButton tsbNextView;
        private System.Windows.Forms.ToolStripButton tsbPan;
        private System.Windows.Forms.ToolStripButton tsbFullExtent;
        private System.Windows.Forms.ToolStripButton tsbRefreshView;
        private System.Windows.Forms.ToolStripButton tsbSelectElement;
        private System.Windows.Forms.TabControl tcLayers;
        private System.Windows.Forms.TabPage tpMapLeft;
        private ESRI.ArcGIS.Controls.AxTOCControl tocCtrlLeft;
        private System.Windows.Forms.TabPage tpMapRight;
        private ESRI.ArcGIS.Controls.AxTOCControl tocCtrlRight;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private ESRI.ArcGIS.Controls.AxMapControl mapCtrlLeft;
        private ESRI.ArcGIS.Controls.AxMapControl mapCtrlRight;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsslMapScale;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslCurrCoords;
        private System.Windows.Forms.ToolStrip tsMapLeftTools;
        private System.Windows.Forms.ToolStrip tsMapRightTools;
        private System.Windows.Forms.ToolStripButton tsbOpenRasterLeft;
        private System.Windows.Forms.ToolStripButton tsbOpenRasterRight;
    }
}


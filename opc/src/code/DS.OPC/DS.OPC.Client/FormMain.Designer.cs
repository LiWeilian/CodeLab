namespace DS.OPC.Client
{
    partial class frmMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tsslOPCServerState = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslOPCServerStartTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslDBConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlBase = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gpDefineGrpItems = new System.Windows.Forms.GroupBox();
            this.lvGroupItems = new System.Windows.Forms.ListView();
            this.chItemID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chItemValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQuality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chServerHDL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chClientHDL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsGroupItems = new System.Windows.Forms.ToolStrip();
            this.tslItemID = new System.Windows.Forms.ToolStripLabel();
            this.tstbSelectedLeaf = new System.Windows.Forms.ToolStripTextBox();
            this.tsbAddItem = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefreshSync = new System.Windows.Forms.ToolStripButton();
            this.tsbSyncRead = new System.Windows.Forms.ToolStripButton();
            this.tsbAsyncRead = new System.Windows.Forms.ToolStripButton();
            this.tsbAsyncRefresh = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gpItems = new System.Windows.Forms.GroupBox();
            this.lbItems = new System.Windows.Forms.ListBox();
            this.gpBranches = new System.Windows.Forms.GroupBox();
            this.tvOPCBrowser = new System.Windows.Forms.TreeView();
            this.tsBranches = new System.Windows.Forms.ToolStrip();
            this.tslBranchFilter = new System.Windows.Forms.ToolStripLabel();
            this.tstbBranchFilter = new System.Windows.Forms.ToolStripTextBox();
            this.tsbOPCBrowserTree = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gpGroupProperty = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGPUpdateTimeout = new System.Windows.Forms.TextBox();
            this.chkCheckTimeout = new System.Windows.Forms.CheckBox();
            this.pnlGroupPropCtrl = new System.Windows.Forms.Panel();
            this.btnGroupDBConnSetting = new System.Windows.Forms.Button();
            this.btnGPApply = new System.Windows.Forms.Button();
            this.btnGPCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDeadband = new System.Windows.Forms.Label();
            this.lblGPUpdateRate = new System.Windows.Forms.Label();
            this.lblGPIsSubscribed = new System.Windows.Forms.Label();
            this.txtGPDeadBand = new System.Windows.Forms.TextBox();
            this.txtGPUpdateRate = new System.Windows.Forms.TextBox();
            this.cbGPIsSubcribed = new System.Windows.Forms.ComboBox();
            this.lblGPIsActive = new System.Windows.Forms.Label();
            this.cbGPIsActive = new System.Windows.Forms.ComboBox();
            this.txtGPName = new System.Windows.Forms.TextBox();
            this.lblGPName = new System.Windows.Forms.Label();
            this.gpGroups = new System.Windows.Forms.GroupBox();
            this.btnRemoveGroup = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.lvGroups = new System.Windows.Forms.ListView();
            this.chGroupName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpServers = new System.Windows.Forms.GroupBox();
            this.btnConnectServer = new System.Windows.Forms.Button();
            this.btnRefreshServers = new System.Windows.Forms.Button();
            this.lblServers = new System.Windows.Forms.Label();
            this.cbServers = new System.Windows.Forms.ComboBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtServerHost = new System.Windows.Forms.TextBox();
            this.tmCheckGroupStatus = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ssStatus.SuspendLayout();
            this.pnlBase.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gpDefineGrpItems.SuspendLayout();
            this.tsGroupItems.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gpItems.SuspendLayout();
            this.gpBranches.SuspendLayout();
            this.tsBranches.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gpGroupProperty.SuspendLayout();
            this.pnlGroupPropCtrl.SuspendLayout();
            this.gpGroups.SuspendLayout();
            this.gpServers.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslOPCServerState,
            this.tsslOPCServerStartTime,
            this.tsslDBConnection});
            this.ssStatus.Location = new System.Drawing.Point(0, 559);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(894, 22);
            this.ssStatus.TabIndex = 3;
            // 
            // tsslOPCServerState
            // 
            this.tsslOPCServerState.Name = "tsslOPCServerState";
            this.tsslOPCServerState.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslOPCServerStartTime
            // 
            this.tsslOPCServerStartTime.Name = "tsslOPCServerStartTime";
            this.tsslOPCServerStartTime.Size = new System.Drawing.Size(0, 17);
            // 
            // tsslDBConnection
            // 
            this.tsslDBConnection.Name = "tsslDBConnection";
            this.tsslDBConnection.Size = new System.Drawing.Size(0, 17);
            // 
            // pnlBase
            // 
            this.pnlBase.Controls.Add(this.panel2);
            this.pnlBase.Controls.Add(this.panel1);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Location = new System.Drawing.Point(0, 25);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(894, 534);
            this.pnlBase.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gpDefineGrpItems);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(251, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(643, 534);
            this.panel2.TabIndex = 2;
            // 
            // gpDefineGrpItems
            // 
            this.gpDefineGrpItems.Controls.Add(this.lvGroupItems);
            this.gpDefineGrpItems.Controls.Add(this.tsGroupItems);
            this.gpDefineGrpItems.Controls.Add(this.panel3);
            this.gpDefineGrpItems.Controls.Add(this.tsBranches);
            this.gpDefineGrpItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpDefineGrpItems.Location = new System.Drawing.Point(0, 0);
            this.gpDefineGrpItems.Name = "gpDefineGrpItems";
            this.gpDefineGrpItems.Size = new System.Drawing.Size(643, 534);
            this.gpDefineGrpItems.TabIndex = 1;
            this.gpDefineGrpItems.TabStop = false;
            this.gpDefineGrpItems.Text = "设置组内的项";
            // 
            // lvGroupItems
            // 
            this.lvGroupItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chItemID,
            this.chItemValue,
            this.chQuality,
            this.chTimeStamp,
            this.chServerHDL,
            this.chClientHDL});
            this.lvGroupItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGroupItems.FullRowSelect = true;
            this.lvGroupItems.GridLines = true;
            this.lvGroupItems.HideSelection = false;
            this.lvGroupItems.Location = new System.Drawing.Point(3, 267);
            this.lvGroupItems.MultiSelect = false;
            this.lvGroupItems.Name = "lvGroupItems";
            this.lvGroupItems.Size = new System.Drawing.Size(637, 264);
            this.lvGroupItems.TabIndex = 6;
            this.lvGroupItems.UseCompatibleStateImageBehavior = false;
            this.lvGroupItems.View = System.Windows.Forms.View.Details;
            this.lvGroupItems.SelectedIndexChanged += new System.EventHandler(this.lvGroupItems_SelectedIndexChanged);
            // 
            // chItemID
            // 
            this.chItemID.Text = "项编号";
            this.chItemID.Width = 150;
            // 
            // chItemValue
            // 
            this.chItemValue.Text = "值";
            this.chItemValue.Width = 75;
            // 
            // chQuality
            // 
            this.chQuality.Text = "数据质量";
            // 
            // chTimeStamp
            // 
            this.chTimeStamp.Text = "时间戳";
            this.chTimeStamp.Width = 120;
            // 
            // chServerHDL
            // 
            this.chServerHDL.Text = "服务器句柄";
            this.chServerHDL.Width = 100;
            // 
            // chClientHDL
            // 
            this.chClientHDL.Text = "客户端句柄";
            this.chClientHDL.Width = 100;
            // 
            // tsGroupItems
            // 
            this.tsGroupItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslItemID,
            this.tstbSelectedLeaf,
            this.tsbAddItem,
            this.tsbRemoveItem,
            this.toolStripSeparator1,
            this.tsbRefreshSync,
            this.tsbSyncRead,
            this.tsbAsyncRead,
            this.tsbAsyncRefresh});
            this.tsGroupItems.Location = new System.Drawing.Point(3, 242);
            this.tsGroupItems.Name = "tsGroupItems";
            this.tsGroupItems.Size = new System.Drawing.Size(637, 25);
            this.tsGroupItems.TabIndex = 5;
            this.tsGroupItems.Text = "toolStrip1";
            // 
            // tslItemID
            // 
            this.tslItemID.Name = "tslItemID";
            this.tslItemID.Size = new System.Drawing.Size(56, 22);
            this.tslItemID.Text = "项编号：";
            // 
            // tstbSelectedLeaf
            // 
            this.tstbSelectedLeaf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbSelectedLeaf.Name = "tstbSelectedLeaf";
            this.tstbSelectedLeaf.Size = new System.Drawing.Size(150, 25);
            this.tstbSelectedLeaf.TextChanged += new System.EventHandler(this.tstbSelectedLeaf_TextChanged);
            // 
            // tsbAddItem
            // 
            this.tsbAddItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddItem.Enabled = false;
            this.tsbAddItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddItem.Image")));
            this.tsbAddItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddItem.Name = "tsbAddItem";
            this.tsbAddItem.Size = new System.Drawing.Size(36, 22);
            this.tsbAddItem.Text = "添加";
            this.tsbAddItem.Click += new System.EventHandler(this.tsbAddItem_Click);
            // 
            // tsbRemoveItem
            // 
            this.tsbRemoveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRemoveItem.Enabled = false;
            this.tsbRemoveItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveItem.Image")));
            this.tsbRemoveItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveItem.Name = "tsbRemoveItem";
            this.tsbRemoveItem.Size = new System.Drawing.Size(36, 22);
            this.tsbRemoveItem.Text = "移除";
            this.tsbRemoveItem.Click += new System.EventHandler(this.tsbRemoveItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefreshSync
            // 
            this.tsbRefreshSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRefreshSync.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshSync.Image")));
            this.tsbRefreshSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshSync.Name = "tsbRefreshSync";
            this.tsbRefreshSync.Size = new System.Drawing.Size(36, 22);
            this.tsbRefreshSync.Text = "刷新";
            this.tsbRefreshSync.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbSyncRead
            // 
            this.tsbSyncRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSyncRead.Image = ((System.Drawing.Image)(resources.GetObject("tsbSyncRead.Image")));
            this.tsbSyncRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSyncRead.Name = "tsbSyncRead";
            this.tsbSyncRead.Size = new System.Drawing.Size(60, 22);
            this.tsbSyncRead.Text = "同步读取";
            this.tsbSyncRead.Click += new System.EventHandler(this.tsbSyncRead_Click);
            // 
            // tsbAsyncRead
            // 
            this.tsbAsyncRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAsyncRead.Image = ((System.Drawing.Image)(resources.GetObject("tsbAsyncRead.Image")));
            this.tsbAsyncRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAsyncRead.Name = "tsbAsyncRead";
            this.tsbAsyncRead.Size = new System.Drawing.Size(60, 22);
            this.tsbAsyncRead.Text = "异步读取";
            this.tsbAsyncRead.Click += new System.EventHandler(this.tsbAsyncRead_Click);
            // 
            // tsbAsyncRefresh
            // 
            this.tsbAsyncRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAsyncRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbAsyncRefresh.Image")));
            this.tsbAsyncRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAsyncRefresh.Name = "tsbAsyncRefresh";
            this.tsbAsyncRefresh.Size = new System.Drawing.Size(60, 22);
            this.tsbAsyncRefresh.Text = "异步刷新";
            this.tsbAsyncRefresh.Click += new System.EventHandler(this.tsbAsyncRefresh_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gpItems);
            this.panel3.Controls.Add(this.gpBranches);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(637, 200);
            this.panel3.TabIndex = 4;
            // 
            // gpItems
            // 
            this.gpItems.Controls.Add(this.lbItems);
            this.gpItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpItems.Location = new System.Drawing.Point(200, 0);
            this.gpItems.Name = "gpItems";
            this.gpItems.Size = new System.Drawing.Size(437, 200);
            this.gpItems.TabIndex = 4;
            this.gpItems.TabStop = false;
            this.gpItems.Text = "项";
            // 
            // lbItems
            // 
            this.lbItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbItems.FormattingEnabled = true;
            this.lbItems.ItemHeight = 12;
            this.lbItems.Location = new System.Drawing.Point(3, 17);
            this.lbItems.Name = "lbItems";
            this.lbItems.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbItems.Size = new System.Drawing.Size(431, 180);
            this.lbItems.TabIndex = 0;
            this.lbItems.SelectedValueChanged += new System.EventHandler(this.lbItems_SelectedValueChanged);
            // 
            // gpBranches
            // 
            this.gpBranches.Controls.Add(this.tvOPCBrowser);
            this.gpBranches.Dock = System.Windows.Forms.DockStyle.Left;
            this.gpBranches.Location = new System.Drawing.Point(0, 0);
            this.gpBranches.Name = "gpBranches";
            this.gpBranches.Size = new System.Drawing.Size(200, 200);
            this.gpBranches.TabIndex = 3;
            this.gpBranches.TabStop = false;
            this.gpBranches.Text = "分枝";
            // 
            // tvOPCBrowser
            // 
            this.tvOPCBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOPCBrowser.Location = new System.Drawing.Point(3, 17);
            this.tvOPCBrowser.Name = "tvOPCBrowser";
            this.tvOPCBrowser.Size = new System.Drawing.Size(194, 180);
            this.tvOPCBrowser.TabIndex = 3;
            this.tvOPCBrowser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvOPCBrowser_AfterSelect);
            // 
            // tsBranches
            // 
            this.tsBranches.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslBranchFilter,
            this.tstbBranchFilter,
            this.tsbOPCBrowserTree});
            this.tsBranches.Location = new System.Drawing.Point(3, 17);
            this.tsBranches.Name = "tsBranches";
            this.tsBranches.Size = new System.Drawing.Size(637, 25);
            this.tsBranches.TabIndex = 7;
            this.tsBranches.Text = "toolStrip2";
            // 
            // tslBranchFilter
            // 
            this.tslBranchFilter.Name = "tslBranchFilter";
            this.tslBranchFilter.Size = new System.Drawing.Size(44, 22);
            this.tslBranchFilter.Text = "过滤器";
            // 
            // tstbBranchFilter
            // 
            this.tstbBranchFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstbBranchFilter.Name = "tstbBranchFilter";
            this.tstbBranchFilter.Size = new System.Drawing.Size(100, 25);
            // 
            // tsbOPCBrowserTree
            // 
            this.tsbOPCBrowserTree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbOPCBrowserTree.Image = ((System.Drawing.Image)(resources.GetObject("tsbOPCBrowserTree.Image")));
            this.tsbOPCBrowserTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOPCBrowserTree.Name = "tsbOPCBrowserTree";
            this.tsbOPCBrowserTree.Size = new System.Drawing.Size(60, 22);
            this.tsbOPCBrowserTree.Text = "列出分枝";
            this.tsbOPCBrowserTree.Click += new System.EventHandler(this.tsbOPCBrowserTree_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gpGroupProperty);
            this.panel1.Controls.Add(this.gpGroups);
            this.panel1.Controls.Add(this.gpServers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 534);
            this.panel1.TabIndex = 1;
            // 
            // gpGroupProperty
            // 
            this.gpGroupProperty.Controls.Add(this.label2);
            this.gpGroupProperty.Controls.Add(this.txtGPUpdateTimeout);
            this.gpGroupProperty.Controls.Add(this.chkCheckTimeout);
            this.gpGroupProperty.Controls.Add(this.pnlGroupPropCtrl);
            this.gpGroupProperty.Controls.Add(this.label1);
            this.gpGroupProperty.Controls.Add(this.lblDeadband);
            this.gpGroupProperty.Controls.Add(this.lblGPUpdateRate);
            this.gpGroupProperty.Controls.Add(this.lblGPIsSubscribed);
            this.gpGroupProperty.Controls.Add(this.txtGPDeadBand);
            this.gpGroupProperty.Controls.Add(this.txtGPUpdateRate);
            this.gpGroupProperty.Controls.Add(this.cbGPIsSubcribed);
            this.gpGroupProperty.Controls.Add(this.lblGPIsActive);
            this.gpGroupProperty.Controls.Add(this.cbGPIsActive);
            this.gpGroupProperty.Controls.Add(this.txtGPName);
            this.gpGroupProperty.Controls.Add(this.lblGPName);
            this.gpGroupProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpGroupProperty.Location = new System.Drawing.Point(0, 245);
            this.gpGroupProperty.Name = "gpGroupProperty";
            this.gpGroupProperty.Size = new System.Drawing.Size(251, 289);
            this.gpGroupProperty.TabIndex = 5;
            this.gpGroupProperty.TabStop = false;
            this.gpGroupProperty.Text = "组属性";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "秒";
            // 
            // txtGPUpdateTimeout
            // 
            this.txtGPUpdateTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGPUpdateTimeout.Location = new System.Drawing.Point(110, 158);
            this.txtGPUpdateTimeout.Name = "txtGPUpdateTimeout";
            this.txtGPUpdateTimeout.Size = new System.Drawing.Size(81, 21);
            this.txtGPUpdateTimeout.TabIndex = 16;
            // 
            // chkCheckTimeout
            // 
            this.chkCheckTimeout.AutoSize = true;
            this.chkCheckTimeout.Location = new System.Drawing.Point(8, 158);
            this.chkCheckTimeout.Name = "chkCheckTimeout";
            this.chkCheckTimeout.Size = new System.Drawing.Size(96, 16);
            this.chkCheckTimeout.TabIndex = 15;
            this.chkCheckTimeout.Text = "检查更新超时";
            this.chkCheckTimeout.UseVisualStyleBackColor = true;
            this.chkCheckTimeout.CheckedChanged += new System.EventHandler(this.chkCheckTimeout_CheckedChanged);
            // 
            // pnlGroupPropCtrl
            // 
            this.pnlGroupPropCtrl.Controls.Add(this.btnGroupDBConnSetting);
            this.pnlGroupPropCtrl.Controls.Add(this.btnGPApply);
            this.pnlGroupPropCtrl.Controls.Add(this.btnGPCancel);
            this.pnlGroupPropCtrl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGroupPropCtrl.Location = new System.Drawing.Point(3, 231);
            this.pnlGroupPropCtrl.Name = "pnlGroupPropCtrl";
            this.pnlGroupPropCtrl.Size = new System.Drawing.Size(245, 55);
            this.pnlGroupPropCtrl.TabIndex = 14;
            // 
            // btnGroupDBConnSetting
            // 
            this.btnGroupDBConnSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGroupDBConnSetting.Location = new System.Drawing.Point(9, 18);
            this.btnGroupDBConnSetting.Name = "btnGroupDBConnSetting";
            this.btnGroupDBConnSetting.Size = new System.Drawing.Size(123, 23);
            this.btnGroupDBConnSetting.TabIndex = 14;
            this.btnGroupDBConnSetting.Text = "数据库连接设置";
            this.btnGroupDBConnSetting.UseVisualStyleBackColor = true;
            this.btnGroupDBConnSetting.Click += new System.EventHandler(this.btnGroupDBConnSetting_Click);
            // 
            // btnGPApply
            // 
            this.btnGPApply.Enabled = false;
            this.btnGPApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGPApply.Location = new System.Drawing.Point(140, 18);
            this.btnGPApply.Name = "btnGPApply";
            this.btnGPApply.Size = new System.Drawing.Size(48, 23);
            this.btnGPApply.TabIndex = 8;
            this.btnGPApply.Text = "设置";
            this.btnGPApply.UseVisualStyleBackColor = true;
            this.btnGPApply.Click += new System.EventHandler(this.btnGPApply_Click);
            // 
            // btnGPCancel
            // 
            this.btnGPCancel.Enabled = false;
            this.btnGPCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGPCancel.Location = new System.Drawing.Point(194, 18);
            this.btnGPCancel.Name = "btnGPCancel";
            this.btnGPCancel.Size = new System.Drawing.Size(48, 23);
            this.btnGPCancel.TabIndex = 7;
            this.btnGPCancel.Text = "取消";
            this.btnGPCancel.UseVisualStyleBackColor = true;
            this.btnGPCancel.Click += new System.EventHandler(this.btnGPCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "毫秒";
            // 
            // lblDeadband
            // 
            this.lblDeadband.AutoSize = true;
            this.lblDeadband.Location = new System.Drawing.Point(6, 134);
            this.lblDeadband.Name = "lblDeadband";
            this.lblDeadband.Size = new System.Drawing.Size(53, 12);
            this.lblDeadband.TabIndex = 9;
            this.lblDeadband.Text = "不敏感区";
            // 
            // lblGPUpdateRate
            // 
            this.lblGPUpdateRate.AutoSize = true;
            this.lblGPUpdateRate.Location = new System.Drawing.Point(6, 107);
            this.lblGPUpdateRate.Name = "lblGPUpdateRate";
            this.lblGPUpdateRate.Size = new System.Drawing.Size(53, 12);
            this.lblGPUpdateRate.TabIndex = 8;
            this.lblGPUpdateRate.Text = "更新周期";
            // 
            // lblGPIsSubscribed
            // 
            this.lblGPIsSubscribed.AutoSize = true;
            this.lblGPIsSubscribed.Location = new System.Drawing.Point(6, 81);
            this.lblGPIsSubscribed.Name = "lblGPIsSubscribed";
            this.lblGPIsSubscribed.Size = new System.Drawing.Size(53, 12);
            this.lblGPIsSubscribed.TabIndex = 7;
            this.lblGPIsSubscribed.Text = "订阅模式";
            // 
            // txtGPDeadBand
            // 
            this.txtGPDeadBand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGPDeadBand.Location = new System.Drawing.Point(65, 131);
            this.txtGPDeadBand.Name = "txtGPDeadBand";
            this.txtGPDeadBand.Size = new System.Drawing.Size(126, 21);
            this.txtGPDeadBand.TabIndex = 4;
            // 
            // txtGPUpdateRate
            // 
            this.txtGPUpdateRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGPUpdateRate.Location = new System.Drawing.Point(65, 104);
            this.txtGPUpdateRate.Name = "txtGPUpdateRate";
            this.txtGPUpdateRate.Size = new System.Drawing.Size(126, 21);
            this.txtGPUpdateRate.TabIndex = 3;
            // 
            // cbGPIsSubcribed
            // 
            this.cbGPIsSubcribed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGPIsSubcribed.FormattingEnabled = true;
            this.cbGPIsSubcribed.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cbGPIsSubcribed.Location = new System.Drawing.Point(65, 78);
            this.cbGPIsSubcribed.Name = "cbGPIsSubcribed";
            this.cbGPIsSubcribed.Size = new System.Drawing.Size(70, 20);
            this.cbGPIsSubcribed.TabIndex = 2;
            // 
            // lblGPIsActive
            // 
            this.lblGPIsActive.AutoSize = true;
            this.lblGPIsActive.Location = new System.Drawing.Point(6, 55);
            this.lblGPIsActive.Name = "lblGPIsActive";
            this.lblGPIsActive.Size = new System.Drawing.Size(53, 12);
            this.lblGPIsActive.TabIndex = 3;
            this.lblGPIsActive.Text = "是否激活";
            // 
            // cbGPIsActive
            // 
            this.cbGPIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGPIsActive.FormattingEnabled = true;
            this.cbGPIsActive.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cbGPIsActive.Location = new System.Drawing.Point(65, 52);
            this.cbGPIsActive.Name = "cbGPIsActive";
            this.cbGPIsActive.Size = new System.Drawing.Size(70, 20);
            this.cbGPIsActive.TabIndex = 1;
            // 
            // txtGPName
            // 
            this.txtGPName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGPName.Location = new System.Drawing.Point(65, 25);
            this.txtGPName.Name = "txtGPName";
            this.txtGPName.Size = new System.Drawing.Size(126, 21);
            this.txtGPName.TabIndex = 0;
            // 
            // lblGPName
            // 
            this.lblGPName.AutoSize = true;
            this.lblGPName.Location = new System.Drawing.Point(18, 28);
            this.lblGPName.Name = "lblGPName";
            this.lblGPName.Size = new System.Drawing.Size(29, 12);
            this.lblGPName.TabIndex = 0;
            this.lblGPName.Text = "名称";
            // 
            // gpGroups
            // 
            this.gpGroups.Controls.Add(this.btnRemoveGroup);
            this.gpGroups.Controls.Add(this.btnAddGroup);
            this.gpGroups.Controls.Add(this.lvGroups);
            this.gpGroups.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpGroups.Location = new System.Drawing.Point(0, 114);
            this.gpGroups.Name = "gpGroups";
            this.gpGroups.Size = new System.Drawing.Size(251, 131);
            this.gpGroups.TabIndex = 4;
            this.gpGroups.TabStop = false;
            this.gpGroups.Text = "组";
            // 
            // btnRemoveGroup
            // 
            this.btnRemoveGroup.Enabled = false;
            this.btnRemoveGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveGroup.Location = new System.Drawing.Point(197, 49);
            this.btnRemoveGroup.Name = "btnRemoveGroup";
            this.btnRemoveGroup.Size = new System.Drawing.Size(48, 23);
            this.btnRemoveGroup.TabIndex = 2;
            this.btnRemoveGroup.Text = "移除";
            this.btnRemoveGroup.UseVisualStyleBackColor = true;
            this.btnRemoveGroup.Click += new System.EventHandler(this.btnRemoveGroup_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Enabled = false;
            this.btnAddGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddGroup.Location = new System.Drawing.Point(197, 20);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(48, 23);
            this.btnAddGroup.TabIndex = 0;
            this.btnAddGroup.Text = "添加";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // lvGroups
            // 
            this.lvGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chGroupName});
            this.lvGroups.Dock = System.Windows.Forms.DockStyle.Left;
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.HideSelection = false;
            this.lvGroups.Location = new System.Drawing.Point(3, 17);
            this.lvGroups.MultiSelect = false;
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.Size = new System.Drawing.Size(188, 111);
            this.lvGroups.TabIndex = 1;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            this.lvGroups.SelectedIndexChanged += new System.EventHandler(this.lvGroups_SelectedIndexChanged);
            // 
            // chGroupName
            // 
            this.chGroupName.Text = "名称";
            this.chGroupName.Width = 150;
            // 
            // gpServers
            // 
            this.gpServers.Controls.Add(this.btnConnectServer);
            this.gpServers.Controls.Add(this.btnRefreshServers);
            this.gpServers.Controls.Add(this.lblServers);
            this.gpServers.Controls.Add(this.cbServers);
            this.gpServers.Controls.Add(this.lblHost);
            this.gpServers.Controls.Add(this.txtServerHost);
            this.gpServers.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpServers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gpServers.Location = new System.Drawing.Point(0, 0);
            this.gpServers.Name = "gpServers";
            this.gpServers.Size = new System.Drawing.Size(251, 114);
            this.gpServers.TabIndex = 3;
            this.gpServers.TabStop = false;
            this.gpServers.Text = "服务器";
            // 
            // btnConnectServer
            // 
            this.btnConnectServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnectServer.Location = new System.Drawing.Point(197, 73);
            this.btnConnectServer.Name = "btnConnectServer";
            this.btnConnectServer.Size = new System.Drawing.Size(48, 23);
            this.btnConnectServer.TabIndex = 3;
            this.btnConnectServer.Tag = "0";
            this.btnConnectServer.Text = "连接";
            this.btnConnectServer.UseVisualStyleBackColor = true;
            this.btnConnectServer.Click += new System.EventHandler(this.btnConnectServer_Click);
            // 
            // btnRefreshServers
            // 
            this.btnRefreshServers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshServers.Location = new System.Drawing.Point(197, 18);
            this.btnRefreshServers.Name = "btnRefreshServers";
            this.btnRefreshServers.Size = new System.Drawing.Size(48, 23);
            this.btnRefreshServers.TabIndex = 1;
            this.btnRefreshServers.Text = "刷新";
            this.btnRefreshServers.UseVisualStyleBackColor = true;
            this.btnRefreshServers.Click += new System.EventHandler(this.btnRefreshServers_Click);
            // 
            // lblServers
            // 
            this.lblServers.AutoSize = true;
            this.lblServers.Location = new System.Drawing.Point(6, 50);
            this.lblServers.Name = "lblServers";
            this.lblServers.Size = new System.Drawing.Size(59, 12);
            this.lblServers.TabIndex = 3;
            this.lblServers.Text = "OPC服务器";
            // 
            // cbServers
            // 
            this.cbServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServers.FormattingEnabled = true;
            this.cbServers.Location = new System.Drawing.Point(65, 47);
            this.cbServers.Name = "cbServers";
            this.cbServers.Size = new System.Drawing.Size(180, 20);
            this.cbServers.TabIndex = 2;
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(6, 23);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(53, 12);
            this.lblHost.TabIndex = 1;
            this.lblHost.Text = "计算机IP";
            // 
            // txtServerHost
            // 
            this.txtServerHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerHost.Location = new System.Drawing.Point(65, 20);
            this.txtServerHost.Name = "txtServerHost";
            this.txtServerHost.Size = new System.Drawing.Size(126, 21);
            this.txtServerHost.TabIndex = 0;
            // 
            // tmCheckGroupStatus
            // 
            this.tmCheckGroupStatus.Enabled = true;
            this.tmCheckGroupStatus.Interval = 10000;
            this.tmCheckGroupStatus.Tick += new System.EventHandler(this.tmCheckGroupStatus_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(894, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miSettings
            // 
            this.miSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadSettings,
            this.miSaveSettings});
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(68, 21);
            this.miSettings.Text = "系统设置";
            // 
            // miLoadSettings
            // 
            this.miLoadSettings.Name = "miLoadSettings";
            this.miLoadSettings.Size = new System.Drawing.Size(152, 22);
            this.miLoadSettings.Text = "读取设置";
            this.miLoadSettings.Click += new System.EventHandler(this.miLoadSettings_Click);
            // 
            // miSaveSettings
            // 
            this.miSaveSettings.Name = "miSaveSettings";
            this.miSaveSettings.Size = new System.Drawing.Size(152, 22);
            this.miSaveSettings.Text = "保存设置";
            this.miSaveSettings.Click += new System.EventHandler(this.miSaveSettings_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 581);
            this.Controls.Add(this.pnlBase);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OPC数据访问";
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.pnlBase.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gpDefineGrpItems.ResumeLayout(false);
            this.gpDefineGrpItems.PerformLayout();
            this.tsGroupItems.ResumeLayout(false);
            this.tsGroupItems.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.gpItems.ResumeLayout(false);
            this.gpBranches.ResumeLayout(false);
            this.tsBranches.ResumeLayout(false);
            this.tsBranches.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gpGroupProperty.ResumeLayout(false);
            this.gpGroupProperty.PerformLayout();
            this.pnlGroupPropCtrl.ResumeLayout(false);
            this.gpGroups.ResumeLayout(false);
            this.gpServers.ResumeLayout(false);
            this.gpServers.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.Panel pnlBase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gpGroupProperty;
        private System.Windows.Forms.GroupBox gpGroups;
        private System.Windows.Forms.Button btnRemoveGroup;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.ListView lvGroups;
        private System.Windows.Forms.ColumnHeader chGroupName;
        private System.Windows.Forms.GroupBox gpServers;
        private System.Windows.Forms.Button btnConnectServer;
        private System.Windows.Forms.Button btnRefreshServers;
        private System.Windows.Forms.Label lblServers;
        private System.Windows.Forms.ComboBox cbServers;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtServerHost;
        private System.Windows.Forms.Label lblDeadband;
        private System.Windows.Forms.Label lblGPUpdateRate;
        private System.Windows.Forms.Label lblGPIsSubscribed;
        private System.Windows.Forms.TextBox txtGPDeadBand;
        private System.Windows.Forms.TextBox txtGPUpdateRate;
        private System.Windows.Forms.ComboBox cbGPIsSubcribed;
        private System.Windows.Forms.Label lblGPIsActive;
        private System.Windows.Forms.ComboBox cbGPIsActive;
        private System.Windows.Forms.TextBox txtGPName;
        private System.Windows.Forms.Label lblGPName;
        private System.Windows.Forms.ToolStripStatusLabel tsslOPCServerState;
        private System.Windows.Forms.ToolStripStatusLabel tsslOPCServerStartTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gpDefineGrpItems;
        private System.Windows.Forms.ListView lvGroupItems;
        private System.Windows.Forms.ColumnHeader chItemID;
        private System.Windows.Forms.ColumnHeader chItemValue;
        private System.Windows.Forms.ColumnHeader chQuality;
        private System.Windows.Forms.ColumnHeader chTimeStamp;
        private System.Windows.Forms.ToolStrip tsGroupItems;
        private System.Windows.Forms.ToolStripTextBox tstbSelectedLeaf;
        private System.Windows.Forms.ToolStripButton tsbAddItem;
        private System.Windows.Forms.ToolStripButton tsbRemoveItem;
        private System.Windows.Forms.ToolStrip tsBranches;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ColumnHeader chServerHDL;
        private System.Windows.Forms.ColumnHeader chClientHDL;
        private System.Windows.Forms.ToolStripLabel tslItemID;
        private System.Windows.Forms.ToolStripButton tsbOPCBrowserTree;
        private System.Windows.Forms.GroupBox gpItems;
        private System.Windows.Forms.GroupBox gpBranches;
        private System.Windows.Forms.TreeView tvOPCBrowser;
        private System.Windows.Forms.ListBox lbItems;
        private System.Windows.Forms.ToolStripLabel tslBranchFilter;
        private System.Windows.Forms.ToolStripTextBox tstbBranchFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRefreshSync;
        private System.Windows.Forms.ToolStripStatusLabel tsslDBConnection;
        private System.Windows.Forms.ToolStripButton tsbSyncRead;
        private System.Windows.Forms.ToolStripButton tsbAsyncRead;
        private System.Windows.Forms.ToolStripButton tsbAsyncRefresh;
        private System.Windows.Forms.Timer tmCheckGroupStatus;
        private System.Windows.Forms.Panel pnlGroupPropCtrl;
        private System.Windows.Forms.Button btnGroupDBConnSetting;
        private System.Windows.Forms.Button btnGPApply;
        private System.Windows.Forms.Button btnGPCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGPUpdateTimeout;
        private System.Windows.Forms.CheckBox chkCheckTimeout;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miSettings;
        private System.Windows.Forms.ToolStripMenuItem miLoadSettings;
        private System.Windows.Forms.ToolStripMenuItem miSaveSettings;

    }
}


namespace GDDST.GIS.EsriDataConnection
{
    partial class FormSelectDatasets
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectDatasets));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddToMap = new System.Windows.Forms.Button();
            this.gbDatasets = new System.Windows.Forms.GroupBox();
            this.tvDatasets = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.gbDatasets.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnAddToMap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(353, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(117, 578);
            this.panel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(16, 46);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAddToMap
            // 
            this.btnAddToMap.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAddToMap.Location = new System.Drawing.Point(16, 17);
            this.btnAddToMap.Name = "btnAddToMap";
            this.btnAddToMap.Size = new System.Drawing.Size(75, 23);
            this.btnAddToMap.TabIndex = 4;
            this.btnAddToMap.Text = "加载";
            this.btnAddToMap.UseVisualStyleBackColor = true;
            // 
            // gbDatasets
            // 
            this.gbDatasets.Controls.Add(this.tvDatasets);
            this.gbDatasets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDatasets.Location = new System.Drawing.Point(0, 0);
            this.gbDatasets.Name = "gbDatasets";
            this.gbDatasets.Size = new System.Drawing.Size(353, 578);
            this.gbDatasets.TabIndex = 5;
            this.gbDatasets.TabStop = false;
            this.gbDatasets.Text = "数据集列表";
            // 
            // tvDatasets
            // 
            this.tvDatasets.CheckBoxes = true;
            this.tvDatasets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvDatasets.HideSelection = false;
            this.tvDatasets.ImageIndex = 0;
            this.tvDatasets.ImageList = this.imgList;
            this.tvDatasets.Location = new System.Drawing.Point(3, 17);
            this.tvDatasets.Name = "tvDatasets";
            this.tvDatasets.SelectedImageIndex = 0;
            this.tvDatasets.Size = new System.Drawing.Size(347, 558);
            this.tvDatasets.TabIndex = 1;
            this.tvDatasets.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvDatasets_AfterCheck);
            this.tvDatasets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvDatasets_MouseDown);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "GeodatabaseFeatureClassAnnotation16.png");
            this.imgList.Images.SetKeyName(1, "GeodatabaseFeatureClassDimension16.png");
            this.imgList.Images.SetKeyName(2, "GeodatabaseFeatureClassEmpty16.png");
            this.imgList.Images.SetKeyName(3, "GeodatabaseFeatureClassLine16.png");
            this.imgList.Images.SetKeyName(4, "GeodatabaseFeatureClassMultipatch16.png");
            this.imgList.Images.SetKeyName(5, "GeodatabaseFeatureClassMultipoint16.png");
            this.imgList.Images.SetKeyName(6, "GeodatabaseFeatureClassPoint16.png");
            this.imgList.Images.SetKeyName(7, "GeodatabaseFeatureClassPolygon16.png");
            this.imgList.Images.SetKeyName(8, "GeodatabaseFeatureClassRoute16.png");
            this.imgList.Images.SetKeyName(9, "GeodatabaseFeatureDataset16.png");
            this.imgList.Images.SetKeyName(10, "GeodatabaseHistoryArchiveAdd16.png");
            this.imgList.Images.SetKeyName(11, "GeodatabaseHistoryMarker16.png");
            this.imgList.Images.SetKeyName(12, "GeodatabaseHistoryMarkerManager16.png");
            this.imgList.Images.SetKeyName(13, "GeodatabaseHistoryViewer16.png");
            this.imgList.Images.SetKeyName(14, "GeodatabaseHome16.png");
            this.imgList.Images.SetKeyName(15, "GeodatabaseMosaicDataset16.png");
            this.imgList.Images.SetKeyName(16, "GeodatabaseNetworkDataset16.png");
            this.imgList.Images.SetKeyName(17, "GeodatabaseNetworkGeometric16.png");
            this.imgList.Images.SetKeyName(18, "GeodatabaseNew16.png");
            this.imgList.Images.SetKeyName(19, "GeodatabaseRaster16.png");
            this.imgList.Images.SetKeyName(20, "GeodatabaseRasterCatalog16.png");
            this.imgList.Images.SetKeyName(21, "GeodatabaseRasterGrid16.png");
            this.imgList.Images.SetKeyName(22, "GeodatabaseRasterGridBand16.png");
            this.imgList.Images.SetKeyName(23, "GeodatabaseRecords16.png");
            this.imgList.Images.SetKeyName(24, "GeodatabaseRelationship16.png");
            this.imgList.Images.SetKeyName(25, "GeodatabaseRelationshipSelect16.png");
            this.imgList.Images.SetKeyName(26, "GeodatabaseSmall_B_16.png");
            this.imgList.Images.SetKeyName(27, "GeodatabaseSmall16.png");
            this.imgList.Images.SetKeyName(28, "GeodatabaseTable_B_10.png");
            this.imgList.Images.SetKeyName(29, "GeodatabaseTable_B_12.png");
            this.imgList.Images.SetKeyName(30, "GeodatabaseTable_B_16.png");
            this.imgList.Images.SetKeyName(31, "GeodatabaseTable_C_16.png");
            this.imgList.Images.SetKeyName(32, "GeodatabaseTable10.png");
            this.imgList.Images.SetKeyName(33, "GeodatabaseTable12.png");
            this.imgList.Images.SetKeyName(34, "GeodatabaseTable16.png");
            this.imgList.Images.SetKeyName(35, "GeodatabaseTableMultiple_B_16.png");
            this.imgList.Images.SetKeyName(36, "GeodatabaseTableMultiple16.png");
            this.imgList.Images.SetKeyName(37, "GeodatabaseTableMultipleSmall_B_16.png");
            this.imgList.Images.SetKeyName(38, "GeodatabaseTableMultipleSmall16.png");
            this.imgList.Images.SetKeyName(39, "GeodatabaseTerrain16.png");
            this.imgList.Images.SetKeyName(40, "GeodatabaseTin16.png");
            this.imgList.Images.SetKeyName(41, "GeodatabaseTiny_B_16.png");
            this.imgList.Images.SetKeyName(42, "GeodatabaseTiny_B_WithPointer_16.png");
            this.imgList.Images.SetKeyName(43, "GeodatabaseTiny16.png");
            this.imgList.Images.SetKeyName(44, "GeodatabaseTopology_B_16.png");
            this.imgList.Images.SetKeyName(45, "GeodatabaseTopology16.png");
            this.imgList.Images.SetKeyName(46, "GeodatabaseTopologyChoose16.png");
            this.imgList.Images.SetKeyName(47, "GeodatabaseWithGeoprocessingTool16.png");
            this.imgList.Images.SetKeyName(48, "GeodatabaseWithHand16.png");
            this.imgList.Images.SetKeyName(49, "GeodatabaseWithPointer16.png");
            this.imgList.Images.SetKeyName(50, "GeodatabaseWithWrench16.png");
            this.imgList.Images.SetKeyName(51, "GeodatabaseXMLRecordSetExport16.png");
            this.imgList.Images.SetKeyName(52, "GeodatabaseXMLRecordSetImport16.png");
            this.imgList.Images.SetKeyName(53, "GeodatabaseXMLWorkspaceExport16.png");
            this.imgList.Images.SetKeyName(54, "GeodatabaseXMLWorkspaceImport16.png");
            // 
            // FormSelectDatasets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 578);
            this.Controls.Add(this.gbDatasets);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSelectDatasets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择数据集";
            this.panel1.ResumeLayout(false);
            this.gbDatasets.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAddToMap;
        private System.Windows.Forms.GroupBox gbDatasets;
        private System.Windows.Forms.TreeView tvDatasets;
        private System.Windows.Forms.ImageList imgList;
    }
}
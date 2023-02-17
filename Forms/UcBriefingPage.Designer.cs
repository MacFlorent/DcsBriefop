namespace DcsBriefop.Forms
{
	partial class UcBriefingPage
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.TcDetail = new System.Windows.Forms.TabControl();
			this.TpPartDetail = new System.Windows.Forms.TabPage();
			this.TpPreview = new System.Windows.Forms.TabPage();
			this.PnImage = new System.Windows.Forms.Panel();
			this.PbHtml = new System.Windows.Forms.PictureBox();
			this.TbHtmlSource = new System.Windows.Forms.TextBox();
			this.BtRefresh = new System.Windows.Forms.Button();
			this.TpMap = new System.Windows.Forms.TabPage();
			this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
			this.ScMain = new System.Windows.Forms.SplitContainer();
			this.BtPartRemove = new System.Windows.Forms.Button();
			this.BtPartAdd = new System.Windows.Forms.Button();
			this.LbParts = new System.Windows.Forms.Label();
			this.CkRenderMap = new System.Windows.Forms.CheckBox();
			this.CkRenderHtml = new System.Windows.Forms.CheckBox();
			this.LbRender = new System.Windows.Forms.Label();
			this.LbHeader = new System.Windows.Forms.Label();
			this.CkDisplayTitle = new System.Windows.Forms.CheckBox();
			this.TbTitle = new System.Windows.Forms.TextBox();
			this.LbTitle = new System.Windows.Forms.Label();
			this.DgvParts = new Zuby.ADGV.AdvancedDataGridView();
			this.TcDetail.SuspendLayout();
			this.TpPreview.SuspendLayout();
			this.PnImage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PbHtml)).BeginInit();
			this.TpMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
			this.ScMain.Panel1.SuspendLayout();
			this.ScMain.Panel2.SuspendLayout();
			this.ScMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvParts)).BeginInit();
			this.SuspendLayout();
			// 
			// TcDetail
			// 
			this.TcDetail.Controls.Add(this.TpPartDetail);
			this.TcDetail.Controls.Add(this.TpPreview);
			this.TcDetail.Controls.Add(this.TpMap);
			this.TcDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TcDetail.Location = new System.Drawing.Point(0, 0);
			this.TcDetail.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TcDetail.Name = "TcDetail";
			this.TcDetail.SelectedIndex = 0;
			this.TcDetail.Size = new System.Drawing.Size(665, 500);
			this.TcDetail.TabIndex = 0;
			// 
			// TpPartDetail
			// 
			this.TpPartDetail.Location = new System.Drawing.Point(4, 24);
			this.TpPartDetail.Name = "TpPartDetail";
			this.TpPartDetail.Size = new System.Drawing.Size(657, 472);
			this.TpPartDetail.TabIndex = 2;
			this.TpPartDetail.Text = "Part detail";
			this.TpPartDetail.UseVisualStyleBackColor = true;
			// 
			// TpPreview
			// 
			this.TpPreview.Controls.Add(this.PnImage);
			this.TpPreview.Controls.Add(this.TbHtmlSource);
			this.TpPreview.Controls.Add(this.BtRefresh);
			this.TpPreview.Location = new System.Drawing.Point(4, 24);
			this.TpPreview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpPreview.Name = "TpPreview";
			this.TpPreview.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpPreview.Size = new System.Drawing.Size(657, 472);
			this.TpPreview.TabIndex = 0;
			this.TpPreview.Text = "Preview";
			this.TpPreview.UseVisualStyleBackColor = true;
			// 
			// PnImage
			// 
			this.PnImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnImage.AutoScroll = true;
			this.PnImage.Controls.Add(this.PbHtml);
			this.PnImage.Location = new System.Drawing.Point(0, 3);
			this.PnImage.Name = "PnImage";
			this.PnImage.Size = new System.Drawing.Size(657, 440);
			this.PnImage.TabIndex = 5;
			// 
			// PbHtml
			// 
			this.PbHtml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PbHtml.Location = new System.Drawing.Point(2, 0);
			this.PbHtml.Name = "PbHtml";
			this.PbHtml.Size = new System.Drawing.Size(664, 680);
			this.PbHtml.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PbHtml.TabIndex = 1;
			this.PbHtml.TabStop = false;
			// 
			// TbHtmlSource
			// 
			this.TbHtmlSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbHtmlSource.Location = new System.Drawing.Point(78, 449);
			this.TbHtmlSource.Multiline = true;
			this.TbHtmlSource.Name = "TbHtmlSource";
			this.TbHtmlSource.Size = new System.Drawing.Size(579, 23);
			this.TbHtmlSource.TabIndex = 4;
			// 
			// BtRefresh
			// 
			this.BtRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtRefresh.Location = new System.Drawing.Point(2, 449);
			this.BtRefresh.Name = "BtRefresh";
			this.BtRefresh.Size = new System.Drawing.Size(73, 23);
			this.BtRefresh.TabIndex = 3;
			this.BtRefresh.Text = "Refresh";
			this.BtRefresh.UseVisualStyleBackColor = true;
			this.BtRefresh.Click += new System.EventHandler(this.BtRefresh_Click);
			// 
			// TpMap
			// 
			this.TpMap.Controls.Add(this.gMapControl1);
			this.TpMap.Location = new System.Drawing.Point(4, 24);
			this.TpMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpMap.Name = "TpMap";
			this.TpMap.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpMap.Size = new System.Drawing.Size(657, 472);
			this.TpMap.TabIndex = 1;
			this.TpMap.Text = "Map";
			this.TpMap.UseVisualStyleBackColor = true;
			// 
			// gMapControl1
			// 
			this.gMapControl1.Bearing = 0F;
			this.gMapControl1.CanDragMap = true;
			this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
			this.gMapControl1.GrayScaleMode = false;
			this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.gMapControl1.LevelsKeepInMemory = 5;
			this.gMapControl1.Location = new System.Drawing.Point(433, 276);
			this.gMapControl1.MarkersEnabled = true;
			this.gMapControl1.MaxZoom = 2;
			this.gMapControl1.MinZoom = 2;
			this.gMapControl1.MouseWheelZoomEnabled = true;
			this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.gMapControl1.Name = "gMapControl1";
			this.gMapControl1.NegativeMode = false;
			this.gMapControl1.PolygonsEnabled = true;
			this.gMapControl1.RetryLoadTile = 0;
			this.gMapControl1.RoutesEnabled = true;
			this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.gMapControl1.ShowTileGridLines = false;
			this.gMapControl1.Size = new System.Drawing.Size(150, 150);
			this.gMapControl1.TabIndex = 0;
			this.gMapControl1.Zoom = 0D;
			// 
			// ScMain
			// 
			this.ScMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScMain.Location = new System.Drawing.Point(0, 0);
			this.ScMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ScMain.Name = "ScMain";
			// 
			// ScMain.Panel1
			// 
			this.ScMain.Panel1.Controls.Add(this.BtPartRemove);
			this.ScMain.Panel1.Controls.Add(this.BtPartAdd);
			this.ScMain.Panel1.Controls.Add(this.LbParts);
			this.ScMain.Panel1.Controls.Add(this.CkRenderMap);
			this.ScMain.Panel1.Controls.Add(this.CkRenderHtml);
			this.ScMain.Panel1.Controls.Add(this.LbRender);
			this.ScMain.Panel1.Controls.Add(this.LbHeader);
			this.ScMain.Panel1.Controls.Add(this.CkDisplayTitle);
			this.ScMain.Panel1.Controls.Add(this.TbTitle);
			this.ScMain.Panel1.Controls.Add(this.LbTitle);
			this.ScMain.Panel1.Controls.Add(this.DgvParts);
			// 
			// ScMain.Panel2
			// 
			this.ScMain.Panel2.AutoScroll = true;
			this.ScMain.Panel2.Controls.Add(this.TcDetail);
			this.ScMain.Size = new System.Drawing.Size(921, 500);
			this.ScMain.SplitterDistance = 251;
			this.ScMain.SplitterWidth = 5;
			this.ScMain.TabIndex = 2;
			// 
			// BtPartRemove
			// 
			this.BtPartRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtPartRemove.Location = new System.Drawing.Point(172, 474);
			this.BtPartRemove.Name = "BtPartRemove";
			this.BtPartRemove.Size = new System.Drawing.Size(75, 23);
			this.BtPartRemove.TabIndex = 43;
			this.BtPartRemove.Text = "Remove";
			this.BtPartRemove.UseVisualStyleBackColor = true;
			// 
			// BtPartAdd
			// 
			this.BtPartAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtPartAdd.Location = new System.Drawing.Point(91, 474);
			this.BtPartAdd.Name = "BtPartAdd";
			this.BtPartAdd.Size = new System.Drawing.Size(75, 23);
			this.BtPartAdd.TabIndex = 42;
			this.BtPartAdd.Text = "Add";
			this.BtPartAdd.UseVisualStyleBackColor = true;
			this.BtPartAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtPartAdd_MouseDown);
			// 
			// LbParts
			// 
			this.LbParts.AutoSize = true;
			this.LbParts.Location = new System.Drawing.Point(11, 116);
			this.LbParts.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbParts.Name = "LbParts";
			this.LbParts.Size = new System.Drawing.Size(33, 15);
			this.LbParts.TabIndex = 8;
			this.LbParts.Text = "Parts";
			// 
			// CkRenderMap
			// 
			this.CkRenderMap.AutoSize = true;
			this.CkRenderMap.Location = new System.Drawing.Point(119, 90);
			this.CkRenderMap.Name = "CkRenderMap";
			this.CkRenderMap.Size = new System.Drawing.Size(50, 19);
			this.CkRenderMap.TabIndex = 7;
			this.CkRenderMap.Text = "Map";
			this.CkRenderMap.UseVisualStyleBackColor = true;
			// 
			// CkRenderHtml
			// 
			this.CkRenderHtml.AutoSize = true;
			this.CkRenderHtml.Location = new System.Drawing.Point(62, 90);
			this.CkRenderHtml.Name = "CkRenderHtml";
			this.CkRenderHtml.Size = new System.Drawing.Size(53, 19);
			this.CkRenderHtml.TabIndex = 6;
			this.CkRenderHtml.Text = "Html";
			this.CkRenderHtml.UseVisualStyleBackColor = true;
			// 
			// LbRender
			// 
			this.LbRender.AutoSize = true;
			this.LbRender.Location = new System.Drawing.Point(11, 90);
			this.LbRender.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbRender.Name = "LbRender";
			this.LbRender.Size = new System.Drawing.Size(44, 15);
			this.LbRender.TabIndex = 5;
			this.LbRender.Text = "Render";
			// 
			// LbHeader
			// 
			this.LbHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbHeader.Location = new System.Drawing.Point(0, 0);
			this.LbHeader.Name = "LbHeader";
			this.LbHeader.Size = new System.Drawing.Size(251, 33);
			this.LbHeader.TabIndex = 4;
			this.LbHeader.Text = "Page";
			this.LbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CkDisplayTitle
			// 
			this.CkDisplayTitle.AutoSize = true;
			this.CkDisplayTitle.Location = new System.Drawing.Point(62, 65);
			this.CkDisplayTitle.Name = "CkDisplayTitle";
			this.CkDisplayTitle.Size = new System.Drawing.Size(87, 19);
			this.CkDisplayTitle.TabIndex = 3;
			this.CkDisplayTitle.Text = "Display title";
			this.CkDisplayTitle.UseVisualStyleBackColor = true;
			// 
			// TbTitle
			// 
			this.TbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbTitle.Location = new System.Drawing.Point(62, 36);
			this.TbTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbTitle.Name = "TbTitle";
			this.TbTitle.Size = new System.Drawing.Size(185, 23);
			this.TbTitle.TabIndex = 2;
			// 
			// LbTitle
			// 
			this.LbTitle.AutoSize = true;
			this.LbTitle.Location = new System.Drawing.Point(11, 39);
			this.LbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbTitle.Name = "LbTitle";
			this.LbTitle.Size = new System.Drawing.Size(29, 15);
			this.LbTitle.TabIndex = 1;
			this.LbTitle.Text = "Title";
			// 
			// DgvParts
			// 
			this.DgvParts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvParts.FilterAndSortEnabled = true;
			this.DgvParts.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvParts.Location = new System.Drawing.Point(4, 134);
			this.DgvParts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DgvParts.Name = "DgvParts";
			this.DgvParts.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvParts.Size = new System.Drawing.Size(243, 338);
			this.DgvParts.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvParts.TabIndex = 0;
			// 
			// UcBriefingPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ScMain);
			this.Name = "UcBriefingPage";
			this.Size = new System.Drawing.Size(921, 500);
			this.TcDetail.ResumeLayout(false);
			this.TpPreview.ResumeLayout(false);
			this.TpPreview.PerformLayout();
			this.PnImage.ResumeLayout(false);
			this.PnImage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PbHtml)).EndInit();
			this.TpMap.ResumeLayout(false);
			this.ScMain.Panel1.ResumeLayout(false);
			this.ScMain.Panel1.PerformLayout();
			this.ScMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
			this.ScMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgvParts)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private TabControl TcDetail;
		private TabPage TpPreview;
		private Button BtRefresh;
		private PictureBox PbHtml;
		private TabPage TpMap;
		private SplitContainer ScMain;
		private TextBox TbTitle;
		private Label LbTitle;
		private Zuby.ADGV.AdvancedDataGridView DgvParts;
		private CheckBox CkDisplayTitle;
		private GMap.NET.WindowsForms.GMapControl gMapControl1;
		private TextBox TbHtmlSource;
		private Panel PnImage;
		private Label LbHeader;
		private TabPage TpPartDetail;
		private Label LbParts;
		private CheckBox CkRenderMap;
		private CheckBox CkRenderHtml;
		private Label LbRender;
		private Button BtPartRemove;
		private Button BtPartAdd;
	}
}

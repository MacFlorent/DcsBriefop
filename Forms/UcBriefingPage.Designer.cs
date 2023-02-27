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
			TcDetail = new TabControl();
			TpPartDetail = new TabPage();
			TpMapDetail = new TabPage();
			CkMapIncludeBaseOverlays = new CheckBox();
			PnMap = new Panel();
			TpHtmlPreview = new TabPage();
			PnImage = new Panel();
			PbHtmlPreview = new PictureBox();
			TbHtmlPreviewSource = new TextBox();
			BtHtmlPreviewRefresh = new Button();
			TpMapPreview = new TabPage();
			BtMapPreviewRefresh = new Button();
			PnMapPreview = new Panel();
			PbMapPreview = new PictureBox();
			ScMain = new SplitContainer();
			BtPartRemove = new Button();
			BtPartAdd = new Button();
			LbParts = new Label();
			CkRenderMap = new CheckBox();
			CkRenderHtml = new CheckBox();
			LbRender = new Label();
			LbHeader = new Label();
			CkDisplayTitle = new CheckBox();
			TbTitle = new TextBox();
			LbTitle = new Label();
			DgvParts = new Zuby.ADGV.AdvancedDataGridView();
			TcDetail.SuspendLayout();
			TpMapDetail.SuspendLayout();
			TpHtmlPreview.SuspendLayout();
			PnImage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)PbHtmlPreview).BeginInit();
			TpMapPreview.SuspendLayout();
			PnMapPreview.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)PbMapPreview).BeginInit();
			((System.ComponentModel.ISupportInitialize)ScMain).BeginInit();
			ScMain.Panel1.SuspendLayout();
			ScMain.Panel2.SuspendLayout();
			ScMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DgvParts).BeginInit();
			SuspendLayout();
			// 
			// TcDetail
			// 
			TcDetail.Controls.Add(TpPartDetail);
			TcDetail.Controls.Add(TpMapDetail);
			TcDetail.Controls.Add(TpHtmlPreview);
			TcDetail.Controls.Add(TpMapPreview);
			TcDetail.Dock = DockStyle.Fill;
			TcDetail.Location = new Point(0, 0);
			TcDetail.Margin = new Padding(4, 3, 4, 3);
			TcDetail.Name = "TcDetail";
			TcDetail.SelectedIndex = 0;
			TcDetail.Size = new Size(665, 500);
			TcDetail.TabIndex = 0;
			// 
			// TpPartDetail
			// 
			TpPartDetail.Location = new Point(4, 24);
			TpPartDetail.Name = "TpPartDetail";
			TpPartDetail.Size = new Size(657, 472);
			TpPartDetail.TabIndex = 2;
			TpPartDetail.Text = "Part configuration";
			TpPartDetail.UseVisualStyleBackColor = true;
			// 
			// TpMapDetail
			// 
			TpMapDetail.Controls.Add(CkMapIncludeBaseOverlays);
			TpMapDetail.Controls.Add(PnMap);
			TpMapDetail.Location = new Point(4, 24);
			TpMapDetail.Margin = new Padding(4, 3, 4, 3);
			TpMapDetail.Name = "TpMapDetail";
			TpMapDetail.Padding = new Padding(4, 3, 4, 3);
			TpMapDetail.Size = new Size(657, 472);
			TpMapDetail.TabIndex = 1;
			TpMapDetail.Text = "Map configuration";
			TpMapDetail.UseVisualStyleBackColor = true;
			// 
			// CkMapIncludeBaseOverlays
			// 
			CkMapIncludeBaseOverlays.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			CkMapIncludeBaseOverlays.AutoSize = true;
			CkMapIncludeBaseOverlays.Location = new Point(0, 453);
			CkMapIncludeBaseOverlays.Name = "CkMapIncludeBaseOverlays";
			CkMapIncludeBaseOverlays.Size = new Size(138, 19);
			CkMapIncludeBaseOverlays.TabIndex = 4;
			CkMapIncludeBaseOverlays.Text = "Include base overlays";
			CkMapIncludeBaseOverlays.UseVisualStyleBackColor = true;
			CkMapIncludeBaseOverlays.CheckedChanged += CkMapIncludeBaseOverlays_CheckedChanged;
			// 
			// PnMap
			// 
			PnMap.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			PnMap.Location = new Point(4, 3);
			PnMap.Name = "PnMap";
			PnMap.Size = new Size(650, 459);
			PnMap.TabIndex = 5;
			// 
			// TpHtmlPreview
			// 
			TpHtmlPreview.Controls.Add(PnImage);
			TpHtmlPreview.Controls.Add(TbHtmlPreviewSource);
			TpHtmlPreview.Controls.Add(BtHtmlPreviewRefresh);
			TpHtmlPreview.Location = new Point(4, 24);
			TpHtmlPreview.Margin = new Padding(4, 3, 4, 3);
			TpHtmlPreview.Name = "TpHtmlPreview";
			TpHtmlPreview.Padding = new Padding(4, 3, 4, 3);
			TpHtmlPreview.Size = new Size(657, 472);
			TpHtmlPreview.TabIndex = 0;
			TpHtmlPreview.Text = "Html preview";
			TpHtmlPreview.UseVisualStyleBackColor = true;
			// 
			// PnImage
			// 
			PnImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			PnImage.AutoScroll = true;
			PnImage.Controls.Add(PbHtmlPreview);
			PnImage.Location = new Point(0, 3);
			PnImage.Name = "PnImage";
			PnImage.Size = new Size(657, 440);
			PnImage.TabIndex = 5;
			// 
			// PbHtmlPreview
			// 
			PbHtmlPreview.BorderStyle = BorderStyle.FixedSingle;
			PbHtmlPreview.Location = new Point(2, 0);
			PbHtmlPreview.Name = "PbHtmlPreview";
			PbHtmlPreview.Size = new Size(664, 680);
			PbHtmlPreview.SizeMode = PictureBoxSizeMode.AutoSize;
			PbHtmlPreview.TabIndex = 1;
			PbHtmlPreview.TabStop = false;
			// 
			// TbHtmlPreviewSource
			// 
			TbHtmlPreviewSource.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TbHtmlPreviewSource.Location = new Point(78, 449);
			TbHtmlPreviewSource.Multiline = true;
			TbHtmlPreviewSource.Name = "TbHtmlPreviewSource";
			TbHtmlPreviewSource.Size = new Size(579, 23);
			TbHtmlPreviewSource.TabIndex = 4;
			// 
			// BtHtmlPreviewRefresh
			// 
			BtHtmlPreviewRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtHtmlPreviewRefresh.Location = new Point(2, 449);
			BtHtmlPreviewRefresh.Name = "BtHtmlPreviewRefresh";
			BtHtmlPreviewRefresh.Size = new Size(73, 23);
			BtHtmlPreviewRefresh.TabIndex = 3;
			BtHtmlPreviewRefresh.Text = "Refresh";
			BtHtmlPreviewRefresh.UseVisualStyleBackColor = true;
			BtHtmlPreviewRefresh.Click += BtHtmlPreviewRefresh_Click;
			// 
			// TpMapPreview
			// 
			TpMapPreview.Controls.Add(BtMapPreviewRefresh);
			TpMapPreview.Controls.Add(PnMapPreview);
			TpMapPreview.Location = new Point(4, 24);
			TpMapPreview.Name = "TpMapPreview";
			TpMapPreview.Size = new Size(657, 472);
			TpMapPreview.TabIndex = 3;
			TpMapPreview.Text = "Map preview";
			TpMapPreview.UseVisualStyleBackColor = true;
			// 
			// BtMapPreviewRefresh
			// 
			BtMapPreviewRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtMapPreviewRefresh.Location = new Point(2, 448);
			BtMapPreviewRefresh.Name = "BtMapPreviewRefresh";
			BtMapPreviewRefresh.Size = new Size(73, 23);
			BtMapPreviewRefresh.TabIndex = 6;
			BtMapPreviewRefresh.Text = "Refresh";
			BtMapPreviewRefresh.UseVisualStyleBackColor = true;
			BtMapPreviewRefresh.Click += BtMapPreviewRefresh_Click;
			// 
			// PnMapPreview
			// 
			PnMapPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			PnMapPreview.AutoScroll = true;
			PnMapPreview.Controls.Add(PbMapPreview);
			PnMapPreview.Location = new Point(0, 2);
			PnMapPreview.Name = "PnMapPreview";
			PnMapPreview.Size = new Size(657, 440);
			PnMapPreview.TabIndex = 7;
			// 
			// PbMapPreview
			// 
			PbMapPreview.BorderStyle = BorderStyle.FixedSingle;
			PbMapPreview.Location = new Point(2, 0);
			PbMapPreview.Name = "PbMapPreview";
			PbMapPreview.Size = new Size(664, 680);
			PbMapPreview.SizeMode = PictureBoxSizeMode.AutoSize;
			PbMapPreview.TabIndex = 1;
			PbMapPreview.TabStop = false;
			// 
			// ScMain
			// 
			ScMain.Dock = DockStyle.Fill;
			ScMain.Location = new Point(0, 0);
			ScMain.Margin = new Padding(4, 3, 4, 3);
			ScMain.Name = "ScMain";
			// 
			// ScMain.Panel1
			// 
			ScMain.Panel1.Controls.Add(BtPartRemove);
			ScMain.Panel1.Controls.Add(BtPartAdd);
			ScMain.Panel1.Controls.Add(LbParts);
			ScMain.Panel1.Controls.Add(CkRenderMap);
			ScMain.Panel1.Controls.Add(CkRenderHtml);
			ScMain.Panel1.Controls.Add(LbRender);
			ScMain.Panel1.Controls.Add(LbHeader);
			ScMain.Panel1.Controls.Add(CkDisplayTitle);
			ScMain.Panel1.Controls.Add(TbTitle);
			ScMain.Panel1.Controls.Add(LbTitle);
			ScMain.Panel1.Controls.Add(DgvParts);
			// 
			// ScMain.Panel2
			// 
			ScMain.Panel2.AutoScroll = true;
			ScMain.Panel2.Controls.Add(TcDetail);
			ScMain.Size = new Size(921, 500);
			ScMain.SplitterDistance = 251;
			ScMain.SplitterWidth = 5;
			ScMain.TabIndex = 2;
			// 
			// BtPartRemove
			// 
			BtPartRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtPartRemove.Location = new Point(172, 474);
			BtPartRemove.Name = "BtPartRemove";
			BtPartRemove.Size = new Size(75, 23);
			BtPartRemove.TabIndex = 43;
			BtPartRemove.Text = "Remove";
			BtPartRemove.UseVisualStyleBackColor = true;
			BtPartRemove.Click += BtPartRemove_Click;
			// 
			// BtPartAdd
			// 
			BtPartAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtPartAdd.Location = new Point(91, 474);
			BtPartAdd.Name = "BtPartAdd";
			BtPartAdd.Size = new Size(75, 23);
			BtPartAdd.TabIndex = 42;
			BtPartAdd.Text = "Add";
			BtPartAdd.UseVisualStyleBackColor = true;
			BtPartAdd.MouseDown += BtPartAdd_MouseDown;
			// 
			// LbParts
			// 
			LbParts.AutoSize = true;
			LbParts.Location = new Point(11, 116);
			LbParts.Margin = new Padding(4, 0, 4, 0);
			LbParts.Name = "LbParts";
			LbParts.Size = new Size(33, 15);
			LbParts.TabIndex = 8;
			LbParts.Text = "Parts";
			// 
			// CkRenderMap
			// 
			CkRenderMap.AutoSize = true;
			CkRenderMap.Location = new Point(119, 90);
			CkRenderMap.Name = "CkRenderMap";
			CkRenderMap.Size = new Size(50, 19);
			CkRenderMap.TabIndex = 7;
			CkRenderMap.Text = "Map";
			CkRenderMap.UseVisualStyleBackColor = true;
			CkRenderMap.CheckedChanged += CkRender_CheckedChanged;
			// 
			// CkRenderHtml
			// 
			CkRenderHtml.AutoSize = true;
			CkRenderHtml.Location = new Point(62, 90);
			CkRenderHtml.Name = "CkRenderHtml";
			CkRenderHtml.Size = new Size(53, 19);
			CkRenderHtml.TabIndex = 6;
			CkRenderHtml.Text = "Html";
			CkRenderHtml.UseVisualStyleBackColor = true;
			CkRenderHtml.CheckedChanged += CkRender_CheckedChanged;
			// 
			// LbRender
			// 
			LbRender.AutoSize = true;
			LbRender.Location = new Point(11, 90);
			LbRender.Margin = new Padding(4, 0, 4, 0);
			LbRender.Name = "LbRender";
			LbRender.Size = new Size(44, 15);
			LbRender.TabIndex = 5;
			LbRender.Text = "Render";
			// 
			// LbHeader
			// 
			LbHeader.Dock = DockStyle.Top;
			LbHeader.Location = new Point(0, 0);
			LbHeader.Name = "LbHeader";
			LbHeader.Size = new Size(251, 33);
			LbHeader.TabIndex = 4;
			LbHeader.Text = "Page";
			LbHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// CkDisplayTitle
			// 
			CkDisplayTitle.AutoSize = true;
			CkDisplayTitle.Location = new Point(62, 65);
			CkDisplayTitle.Name = "CkDisplayTitle";
			CkDisplayTitle.Size = new Size(87, 19);
			CkDisplayTitle.TabIndex = 3;
			CkDisplayTitle.Text = "Display title";
			CkDisplayTitle.UseVisualStyleBackColor = true;
			// 
			// TbTitle
			// 
			TbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbTitle.Location = new Point(62, 36);
			TbTitle.Margin = new Padding(4, 3, 4, 3);
			TbTitle.Name = "TbTitle";
			TbTitle.Size = new Size(185, 23);
			TbTitle.TabIndex = 2;
			// 
			// LbTitle
			// 
			LbTitle.AutoSize = true;
			LbTitle.Location = new Point(11, 39);
			LbTitle.Margin = new Padding(4, 0, 4, 0);
			LbTitle.Name = "LbTitle";
			LbTitle.Size = new Size(29, 15);
			LbTitle.TabIndex = 1;
			LbTitle.Text = "Title";
			// 
			// DgvParts
			// 
			DgvParts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvParts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvParts.FilterAndSortEnabled = true;
			DgvParts.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvParts.Location = new Point(4, 134);
			DgvParts.Margin = new Padding(4, 3, 4, 3);
			DgvParts.Name = "DgvParts";
			DgvParts.RightToLeft = RightToLeft.No;
			DgvParts.Size = new Size(243, 338);
			DgvParts.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvParts.TabIndex = 0;
			// 
			// UcBriefingPage
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(ScMain);
			Name = "UcBriefingPage";
			Size = new Size(921, 500);
			TcDetail.ResumeLayout(false);
			TpMapDetail.ResumeLayout(false);
			TpMapDetail.PerformLayout();
			TpHtmlPreview.ResumeLayout(false);
			TpHtmlPreview.PerformLayout();
			PnImage.ResumeLayout(false);
			PnImage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)PbHtmlPreview).EndInit();
			TpMapPreview.ResumeLayout(false);
			PnMapPreview.ResumeLayout(false);
			PnMapPreview.PerformLayout();
			((System.ComponentModel.ISupportInitialize)PbMapPreview).EndInit();
			ScMain.Panel1.ResumeLayout(false);
			ScMain.Panel1.PerformLayout();
			ScMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ScMain).EndInit();
			ScMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DgvParts).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private TabControl TcDetail;
		private TabPage TpHtmlPreview;
		private PictureBox PbHtmlPreview;
		private TabPage TpMapDetail;
		private SplitContainer ScMain;
		private TextBox TbTitle;
		private Label LbTitle;
		private Zuby.ADGV.AdvancedDataGridView DgvParts;
		private CheckBox CkDisplayTitle;
		private TextBox TbHtmlPreviewSource;
		private Panel PnImage;
		private Label LbHeader;
		private TabPage TpPartDetail;
		private Label LbParts;
		private CheckBox CkRenderMap;
		private CheckBox CkRenderHtml;
		private Label LbRender;
		private Button BtPartRemove;
		private Button BtPartAdd;
		private TabPage TpMapPreview;
		private CheckBox CkMapIncludeBaseOverlays;
		private Button BtHtmlPreviewRefresh;
		private Button BtMapPreviewRefresh;
		private Panel PnMapPreview;
		private PictureBox PbMapPreview;
		private Panel PnMap;
	}
}

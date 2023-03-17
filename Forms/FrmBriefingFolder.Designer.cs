namespace DcsBriefop.Forms
{
	partial class FrmBriefingFolder
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
			BtPageRemove = new Button();
			BtPageAdd = new Button();
			DgvPages = new Zuby.ADGV.AdvancedDataGridView();
			LbPages = new Label();
			LbBriefingImageSize = new Label();
			UcImageSize = new UcImageSize();
			CbMeasurementSystem = new ComboBox();
			LbMeasurementSystem = new Label();
			LstCoordinateDisplay = new CheckedListBox();
			CbWeatherDisplay = new ComboBox();
			LbCoordinateDisplay = new Label();
			LbWeatherDisplay = new Label();
			CbCoalition = new ComboBox();
			LbCoalition = new Label();
			LbUnitTypes = new Label();
			TbName = new TextBox();
			LbName = new Label();
			ScMain = new SplitContainer();
			TlGrids = new TableLayoutPanel();
			PnGridUnitTypes = new Panel();
			LstKneeboards = new CheckedListBox();
			PnGridsPages = new Panel();
			BtPageOrderDown = new Button();
			BtPageOrderUp = new Button();
			LbHeader = new Label();
			((System.ComponentModel.ISupportInitialize)DgvPages).BeginInit();
			((System.ComponentModel.ISupportInitialize)ScMain).BeginInit();
			ScMain.Panel1.SuspendLayout();
			ScMain.SuspendLayout();
			TlGrids.SuspendLayout();
			PnGridUnitTypes.SuspendLayout();
			PnGridsPages.SuspendLayout();
			SuspendLayout();
			// 
			// BtPageRemove
			// 
			BtPageRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtPageRemove.Location = new Point(234, 264);
			BtPageRemove.Name = "BtPageRemove";
			BtPageRemove.Size = new Size(75, 23);
			BtPageRemove.TabIndex = 41;
			BtPageRemove.Text = "Remove";
			BtPageRemove.UseVisualStyleBackColor = true;
			BtPageRemove.Click += BtPageRemove_Click;
			// 
			// BtPageAdd
			// 
			BtPageAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtPageAdd.Location = new Point(153, 264);
			BtPageAdd.Name = "BtPageAdd";
			BtPageAdd.Size = new Size(75, 23);
			BtPageAdd.TabIndex = 40;
			BtPageAdd.Text = "Add";
			BtPageAdd.UseVisualStyleBackColor = true;
			BtPageAdd.Click += BtPageAdd_Click;
			// 
			// DgvPages
			// 
			DgvPages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvPages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvPages.FilterAndSortEnabled = false;
			DgvPages.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvPages.Location = new Point(0, 19);
			DgvPages.Name = "DgvPages";
			DgvPages.RightToLeft = RightToLeft.No;
			DgvPages.RowTemplate.Height = 25;
			DgvPages.Size = new Size(310, 243);
			DgvPages.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvPages.TabIndex = 39;
			// 
			// LbPages
			// 
			LbPages.AutoSize = true;
			LbPages.Location = new Point(0, 0);
			LbPages.Margin = new Padding(4, 0, 4, 0);
			LbPages.Name = "LbPages";
			LbPages.Size = new Size(38, 15);
			LbPages.TabIndex = 38;
			LbPages.Text = "Pages";
			// 
			// LbBriefingImageSize
			// 
			LbBriefingImageSize.AutoSize = true;
			LbBriefingImageSize.Location = new Point(13, 231);
			LbBriefingImageSize.Margin = new Padding(4, 0, 4, 0);
			LbBriefingImageSize.Name = "LbBriefingImageSize";
			LbBriefingImageSize.Size = new Size(62, 15);
			LbBriefingImageSize.TabIndex = 37;
			LbBriefingImageSize.Text = "Image size";
			// 
			// UcImageSize
			// 
			UcImageSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			UcImageSize.Location = new Point(12, 249);
			UcImageSize.Margin = new Padding(4, 3, 4, 3);
			UcImageSize.Name = "UcImageSize";
			UcImageSize.SelectedSize = new Size(1, 1);
			UcImageSize.Size = new Size(299, 27);
			UcImageSize.TabIndex = 36;
			// 
			// CbMeasurementSystem
			// 
			CbMeasurementSystem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbMeasurementSystem.FormattingEnabled = true;
			CbMeasurementSystem.Location = new Point(139, 140);
			CbMeasurementSystem.Margin = new Padding(4, 3, 4, 3);
			CbMeasurementSystem.Name = "CbMeasurementSystem";
			CbMeasurementSystem.Size = new Size(174, 23);
			CbMeasurementSystem.TabIndex = 35;
			// 
			// LbMeasurementSystem
			// 
			LbMeasurementSystem.AutoSize = true;
			LbMeasurementSystem.Location = new Point(13, 144);
			LbMeasurementSystem.Margin = new Padding(4, 0, 4, 0);
			LbMeasurementSystem.Name = "LbMeasurementSystem";
			LbMeasurementSystem.Size = new Size(120, 15);
			LbMeasurementSystem.TabIndex = 34;
			LbMeasurementSystem.Text = "Measurement system";
			// 
			// LstCoordinateDisplay
			// 
			LstCoordinateDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			LstCoordinateDisplay.FormattingEnabled = true;
			LstCoordinateDisplay.Location = new Point(139, 169);
			LstCoordinateDisplay.Margin = new Padding(4, 3, 4, 3);
			LstCoordinateDisplay.Name = "LstCoordinateDisplay";
			LstCoordinateDisplay.Size = new Size(174, 58);
			LstCoordinateDisplay.TabIndex = 33;
			// 
			// CbWeatherDisplay
			// 
			CbWeatherDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbWeatherDisplay.FormattingEnabled = true;
			CbWeatherDisplay.Location = new Point(139, 111);
			CbWeatherDisplay.Margin = new Padding(4, 3, 4, 3);
			CbWeatherDisplay.Name = "CbWeatherDisplay";
			CbWeatherDisplay.Size = new Size(174, 23);
			CbWeatherDisplay.TabIndex = 32;
			// 
			// LbCoordinateDisplay
			// 
			LbCoordinateDisplay.AutoSize = true;
			LbCoordinateDisplay.Location = new Point(13, 169);
			LbCoordinateDisplay.Margin = new Padding(4, 0, 4, 0);
			LbCoordinateDisplay.Name = "LbCoordinateDisplay";
			LbCoordinateDisplay.Size = new Size(106, 15);
			LbCoordinateDisplay.TabIndex = 31;
			LbCoordinateDisplay.Text = "Coordinate display";
			// 
			// LbWeatherDisplay
			// 
			LbWeatherDisplay.AutoSize = true;
			LbWeatherDisplay.Location = new Point(12, 119);
			LbWeatherDisplay.Margin = new Padding(4, 0, 4, 0);
			LbWeatherDisplay.Name = "LbWeatherDisplay";
			LbWeatherDisplay.Size = new Size(91, 15);
			LbWeatherDisplay.TabIndex = 30;
			LbWeatherDisplay.Text = "Weather display";
			// 
			// CbCoalition
			// 
			CbCoalition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbCoalition.FormattingEnabled = true;
			CbCoalition.Location = new Point(139, 82);
			CbCoalition.Name = "CbCoalition";
			CbCoalition.Size = new Size(174, 23);
			CbCoalition.TabIndex = 5;
			CbCoalition.SelectedValueChanged += CbCoalition_SelectedValueChanged;
			// 
			// LbCoalition
			// 
			LbCoalition.AutoSize = true;
			LbCoalition.Location = new Point(13, 85);
			LbCoalition.Name = "LbCoalition";
			LbCoalition.Size = new Size(55, 15);
			LbCoalition.TabIndex = 4;
			LbCoalition.Text = "Coalition";
			// 
			// LbUnitTypes
			// 
			LbUnitTypes.AutoSize = true;
			LbUnitTypes.Location = new Point(0, 0);
			LbUnitTypes.Name = "LbUnitTypes";
			LbUnitTypes.Size = new Size(60, 15);
			LbUnitTypes.TabIndex = 2;
			LbUnitTypes.Text = "Unit types";
			// 
			// TbName
			// 
			TbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbName.Location = new Point(138, 36);
			TbName.Name = "TbName";
			TbName.Size = new Size(174, 23);
			TbName.TabIndex = 1;
			// 
			// LbName
			// 
			LbName.AutoSize = true;
			LbName.Location = new Point(11, 39);
			LbName.Name = "LbName";
			LbName.Size = new Size(39, 15);
			LbName.TabIndex = 0;
			LbName.Text = "Name";
			// 
			// ScMain
			// 
			ScMain.BorderStyle = BorderStyle.FixedSingle;
			ScMain.Dock = DockStyle.Fill;
			ScMain.Location = new Point(0, 0);
			ScMain.Margin = new Padding(4, 3, 4, 3);
			ScMain.Name = "ScMain";
			// 
			// ScMain.Panel1
			// 
			ScMain.Panel1.Controls.Add(TlGrids);
			ScMain.Panel1.Controls.Add(LbHeader);
			ScMain.Panel1.Controls.Add(LbBriefingImageSize);
			ScMain.Panel1.Controls.Add(UcImageSize);
			ScMain.Panel1.Controls.Add(CbMeasurementSystem);
			ScMain.Panel1.Controls.Add(LbMeasurementSystem);
			ScMain.Panel1.Controls.Add(LstCoordinateDisplay);
			ScMain.Panel1.Controls.Add(CbWeatherDisplay);
			ScMain.Panel1.Controls.Add(LbCoordinateDisplay);
			ScMain.Panel1.Controls.Add(LbWeatherDisplay);
			ScMain.Panel1.Controls.Add(CbCoalition);
			ScMain.Panel1.Controls.Add(LbCoalition);
			ScMain.Panel1.Controls.Add(TbName);
			ScMain.Panel1.Controls.Add(LbName);
			// 
			// ScMain.Panel2
			// 
			ScMain.Panel2.AutoScroll = true;
			ScMain.Size = new Size(1291, 764);
			ScMain.SplitterDistance = 318;
			ScMain.SplitterWidth = 5;
			ScMain.TabIndex = 2;
			// 
			// TlGrids
			// 
			TlGrids.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TlGrids.ColumnCount = 1;
			TlGrids.ColumnStyles.Add(new ColumnStyle());
			TlGrids.Controls.Add(PnGridUnitTypes, 0, 0);
			TlGrids.Controls.Add(PnGridsPages, 0, 1);
			TlGrids.Location = new Point(0, 276);
			TlGrids.Name = "TlGrids";
			TlGrids.RowCount = 2;
			TlGrids.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
			TlGrids.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
			TlGrids.Size = new Size(316, 487);
			TlGrids.TabIndex = 49;
			// 
			// PnGridUnitTypes
			// 
			PnGridUnitTypes.Controls.Add(LstKneeboards);
			PnGridUnitTypes.Controls.Add(LbUnitTypes);
			PnGridUnitTypes.Dock = DockStyle.Fill;
			PnGridUnitTypes.Location = new Point(3, 3);
			PnGridUnitTypes.Name = "PnGridUnitTypes";
			PnGridUnitTypes.Size = new Size(310, 188);
			PnGridUnitTypes.TabIndex = 0;
			// 
			// LstKneeboards
			// 
			LstKneeboards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			LstKneeboards.FormattingEnabled = true;
			LstKneeboards.Location = new Point(2, 33);
			LstKneeboards.Margin = new Padding(4, 3, 4, 3);
			LstKneeboards.Name = "LstKneeboards";
			LstKneeboards.Size = new Size(289, 148);
			LstKneeboards.TabIndex = 34;
			// 
			// PnGridsPages
			// 
			PnGridsPages.Controls.Add(LbPages);
			PnGridsPages.Controls.Add(BtPageOrderDown);
			PnGridsPages.Controls.Add(BtPageRemove);
			PnGridsPages.Controls.Add(BtPageOrderUp);
			PnGridsPages.Controls.Add(BtPageAdd);
			PnGridsPages.Controls.Add(DgvPages);
			PnGridsPages.Dock = DockStyle.Fill;
			PnGridsPages.Location = new Point(3, 197);
			PnGridsPages.Name = "PnGridsPages";
			PnGridsPages.Size = new Size(310, 287);
			PnGridsPages.TabIndex = 1;
			// 
			// BtPageOrderDown
			// 
			BtPageOrderDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtPageOrderDown.Location = new Point(34, 264);
			BtPageOrderDown.Name = "BtPageOrderDown";
			BtPageOrderDown.Size = new Size(26, 23);
			BtPageOrderDown.TabIndex = 47;
			BtPageOrderDown.Text = "v";
			BtPageOrderDown.UseVisualStyleBackColor = true;
			BtPageOrderDown.Click += BtPageOrderDown_Click;
			// 
			// BtPageOrderUp
			// 
			BtPageOrderUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtPageOrderUp.Location = new Point(2, 264);
			BtPageOrderUp.Name = "BtPageOrderUp";
			BtPageOrderUp.Size = new Size(26, 23);
			BtPageOrderUp.TabIndex = 46;
			BtPageOrderUp.Text = "^";
			BtPageOrderUp.UseVisualStyleBackColor = true;
			BtPageOrderUp.Click += BtPageOrderUp_Click;
			// 
			// LbHeader
			// 
			LbHeader.Dock = DockStyle.Top;
			LbHeader.Location = new Point(0, 0);
			LbHeader.Name = "LbHeader";
			LbHeader.Size = new Size(316, 33);
			LbHeader.TabIndex = 42;
			LbHeader.Text = "Folder";
			LbHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// FrmBriefingFolder
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1291, 764);
			Controls.Add(ScMain);
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			Name = "FrmBriefingFolder";
			ShowInTaskbar = false;
			Text = "Briefing folder";
			FormClosed += FrmBriefingFolder_FormClosed;
			Shown += FrmBriefingFolder_Shown;
			((System.ComponentModel.ISupportInitialize)DgvPages).EndInit();
			ScMain.Panel1.ResumeLayout(false);
			ScMain.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ScMain).EndInit();
			ScMain.ResumeLayout(false);
			TlGrids.ResumeLayout(false);
			PnGridUnitTypes.ResumeLayout(false);
			PnGridUnitTypes.PerformLayout();
			PnGridsPages.ResumeLayout(false);
			PnGridsPages.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Button BtPageRemove;
		private Button BtPageAdd;
		private Zuby.ADGV.AdvancedDataGridView DgvPages;
		private Label LbPages;
		private Label LbBriefingImageSize;
		private UcImageSize UcImageSize;
		private ComboBox CbMeasurementSystem;
		private Label LbMeasurementSystem;
		private CheckedListBox LstCoordinateDisplay;
		private ComboBox CbWeatherDisplay;
		private Label LbCoordinateDisplay;
		private Label LbWeatherDisplay;
		private ComboBox CbCoalition;
		private Label LbCoalition;
		private Label LbUnitTypes;
		private TextBox TbName;
		private Label LbName;
		private SplitContainer ScMain;
		private Label LbHeader;
		private Button BtPageOrderDown;
		private Button BtPageOrderUp;
		private TableLayoutPanel TlGrids;
		private Panel PnGridUnitTypes;
		private Panel PnGridsPages;
		private CheckedListBox LstKneeboards;
	}
}
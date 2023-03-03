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
			TbUnitTypes = new TextBox();
			LbUnitTypes = new Label();
			TbName = new TextBox();
			LbName = new Label();
			ScMain = new SplitContainer();
			LbHeader = new Label();
			BtPageOrderDown = new Button();
			BtPageOrderUp = new Button();
			((System.ComponentModel.ISupportInitialize)DgvPages).BeginInit();
			((System.ComponentModel.ISupportInitialize)ScMain).BeginInit();
			ScMain.Panel1.SuspendLayout();
			ScMain.SuspendLayout();
			SuspendLayout();
			// 
			// BtPageRemove
			// 
			BtPageRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtPageRemove.Location = new Point(210, 741);
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
			BtPageAdd.Location = new Point(129, 741);
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
			DgvPages.FilterAndSortEnabled = true;
			DgvPages.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvPages.Location = new Point(3, 309);
			DgvPages.Name = "DgvPages";
			DgvPages.RightToLeft = RightToLeft.No;
			DgvPages.RowTemplate.Height = 25;
			DgvPages.Size = new Size(282, 426);
			DgvPages.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvPages.TabIndex = 39;
			// 
			// LbPages
			// 
			LbPages.AutoSize = true;
			LbPages.Location = new Point(11, 291);
			LbPages.Margin = new Padding(4, 0, 4, 0);
			LbPages.Name = "LbPages";
			LbPages.Size = new Size(38, 15);
			LbPages.TabIndex = 38;
			LbPages.Text = "Pages";
			// 
			// LbBriefingImageSize
			// 
			LbBriefingImageSize.AutoSize = true;
			LbBriefingImageSize.Location = new Point(12, 243);
			LbBriefingImageSize.Margin = new Padding(4, 0, 4, 0);
			LbBriefingImageSize.Name = "LbBriefingImageSize";
			LbBriefingImageSize.Size = new Size(62, 15);
			LbBriefingImageSize.TabIndex = 37;
			LbBriefingImageSize.Text = "Image size";
			// 
			// UcImageSize
			// 
			UcImageSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			UcImageSize.Location = new Point(12, 261);
			UcImageSize.Margin = new Padding(4, 3, 4, 3);
			UcImageSize.Name = "UcImageSize";
			UcImageSize.SelectedSize = new Size(1, 1);
			UcImageSize.Size = new Size(272, 27);
			UcImageSize.TabIndex = 36;
			// 
			// CbMeasurementSystem
			// 
			CbMeasurementSystem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbMeasurementSystem.FormattingEnabled = true;
			CbMeasurementSystem.Location = new Point(138, 152);
			CbMeasurementSystem.Margin = new Padding(4, 3, 4, 3);
			CbMeasurementSystem.Name = "CbMeasurementSystem";
			CbMeasurementSystem.Size = new Size(147, 23);
			CbMeasurementSystem.TabIndex = 35;
			// 
			// LbMeasurementSystem
			// 
			LbMeasurementSystem.AutoSize = true;
			LbMeasurementSystem.Location = new Point(12, 156);
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
			LstCoordinateDisplay.Location = new Point(138, 181);
			LstCoordinateDisplay.Margin = new Padding(4, 3, 4, 3);
			LstCoordinateDisplay.Name = "LstCoordinateDisplay";
			LstCoordinateDisplay.Size = new Size(147, 58);
			LstCoordinateDisplay.TabIndex = 33;
			// 
			// CbWeatherDisplay
			// 
			CbWeatherDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbWeatherDisplay.FormattingEnabled = true;
			CbWeatherDisplay.Location = new Point(138, 123);
			CbWeatherDisplay.Margin = new Padding(4, 3, 4, 3);
			CbWeatherDisplay.Name = "CbWeatherDisplay";
			CbWeatherDisplay.Size = new Size(147, 23);
			CbWeatherDisplay.TabIndex = 32;
			// 
			// LbCoordinateDisplay
			// 
			LbCoordinateDisplay.AutoSize = true;
			LbCoordinateDisplay.Location = new Point(12, 181);
			LbCoordinateDisplay.Margin = new Padding(4, 0, 4, 0);
			LbCoordinateDisplay.Name = "LbCoordinateDisplay";
			LbCoordinateDisplay.Size = new Size(106, 15);
			LbCoordinateDisplay.TabIndex = 31;
			LbCoordinateDisplay.Text = "Coordinate display";
			// 
			// LbWeatherDisplay
			// 
			LbWeatherDisplay.AutoSize = true;
			LbWeatherDisplay.Location = new Point(11, 131);
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
			CbCoalition.Location = new Point(138, 94);
			CbCoalition.Name = "CbCoalition";
			CbCoalition.Size = new Size(147, 23);
			CbCoalition.TabIndex = 5;
			// 
			// LbCoalition
			// 
			LbCoalition.AutoSize = true;
			LbCoalition.Location = new Point(12, 97);
			LbCoalition.Name = "LbCoalition";
			LbCoalition.Size = new Size(55, 15);
			LbCoalition.TabIndex = 4;
			LbCoalition.Text = "Coalition";
			// 
			// TbUnitTypes
			// 
			TbUnitTypes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbUnitTypes.Location = new Point(138, 65);
			TbUnitTypes.Name = "TbUnitTypes";
			TbUnitTypes.Size = new Size(147, 23);
			TbUnitTypes.TabIndex = 3;
			// 
			// LbUnitTypes
			// 
			LbUnitTypes.AutoSize = true;
			LbUnitTypes.Location = new Point(11, 68);
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
			TbName.Size = new Size(147, 23);
			TbName.TabIndex = 1;
			// 
			// LbName
			// 
			LbName.AutoSize = true;
			LbName.Location = new Point(12, 39);
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
			ScMain.Panel1.Controls.Add(BtPageOrderDown);
			ScMain.Panel1.Controls.Add(BtPageOrderUp);
			ScMain.Panel1.Controls.Add(LbHeader);
			ScMain.Panel1.Controls.Add(BtPageRemove);
			ScMain.Panel1.Controls.Add(BtPageAdd);
			ScMain.Panel1.Controls.Add(DgvPages);
			ScMain.Panel1.Controls.Add(LbPages);
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
			ScMain.Panel1.Controls.Add(TbUnitTypes);
			ScMain.Panel1.Controls.Add(LbUnitTypes);
			ScMain.Panel1.Controls.Add(TbName);
			ScMain.Panel1.Controls.Add(LbName);
			// 
			// ScMain.Panel2
			// 
			ScMain.Panel2.AutoScroll = true;
			ScMain.Size = new Size(1179, 769);
			ScMain.SplitterDistance = 291;
			ScMain.SplitterWidth = 5;
			ScMain.TabIndex = 2;
			// 
			// LbHeader
			// 
			LbHeader.Dock = DockStyle.Top;
			LbHeader.Location = new Point(0, 0);
			LbHeader.Name = "LbHeader";
			LbHeader.Size = new Size(289, 33);
			LbHeader.TabIndex = 42;
			LbHeader.Text = "Folder";
			LbHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// BtPageOrderDown
			// 
			BtPageOrderDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtPageOrderDown.Location = new Point(35, 741);
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
			BtPageOrderUp.Location = new Point(3, 741);
			BtPageOrderUp.Name = "BtPageOrderUp";
			BtPageOrderUp.Size = new Size(26, 23);
			BtPageOrderUp.TabIndex = 46;
			BtPageOrderUp.Text = "^";
			BtPageOrderUp.UseVisualStyleBackColor = true;
			BtPageOrderUp.Click += BtPageOrderUp_Click;
			// 
			// FrmBriefingFolder
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1179, 769);
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
		private TextBox TbUnitTypes;
		private Label LbUnitTypes;
		private TextBox TbName;
		private Label LbName;
		private SplitContainer ScMain;
		private Label LbHeader;
		private Button BtPageOrderDown;
		private Button BtPageOrderUp;
	}
}
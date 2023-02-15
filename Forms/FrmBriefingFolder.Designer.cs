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
			this.BtPageRemove = new System.Windows.Forms.Button();
			this.BtPageAdd = new System.Windows.Forms.Button();
			this.DgvPages = new Zuby.ADGV.AdvancedDataGridView();
			this.LbPages = new System.Windows.Forms.Label();
			this.LbBriefingImageSize = new System.Windows.Forms.Label();
			this.UcImageSize = new DcsBriefop.Forms.UcImageSize();
			this.CbMeasurementSystem = new System.Windows.Forms.ComboBox();
			this.LbMeasurementSystem = new System.Windows.Forms.Label();
			this.LstCoordinateDisplay = new System.Windows.Forms.CheckedListBox();
			this.CbWeatherDisplay = new System.Windows.Forms.ComboBox();
			this.LbCoordinateDisplay = new System.Windows.Forms.Label();
			this.LbWeatherDisplay = new System.Windows.Forms.Label();
			this.CbCoalition = new System.Windows.Forms.ComboBox();
			this.LbCoalition = new System.Windows.Forms.Label();
			this.TbUnitTypes = new System.Windows.Forms.TextBox();
			this.LbUnitTypes = new System.Windows.Forms.Label();
			this.TbName = new System.Windows.Forms.TextBox();
			this.LbName = new System.Windows.Forms.Label();
			this.ScMain = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.DgvPages)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
			this.ScMain.Panel1.SuspendLayout();
			this.ScMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtPageRemove
			// 
			this.BtPageRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtPageRemove.Location = new System.Drawing.Point(293, 743);
			this.BtPageRemove.Name = "BtPageRemove";
			this.BtPageRemove.Size = new System.Drawing.Size(75, 23);
			this.BtPageRemove.TabIndex = 41;
			this.BtPageRemove.Text = "Remove";
			this.BtPageRemove.UseVisualStyleBackColor = true;
			// 
			// BtPageAdd
			// 
			this.BtPageAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtPageAdd.Location = new System.Drawing.Point(212, 743);
			this.BtPageAdd.Name = "BtPageAdd";
			this.BtPageAdd.Size = new System.Drawing.Size(75, 23);
			this.BtPageAdd.TabIndex = 40;
			this.BtPageAdd.Text = "Add";
			this.BtPageAdd.UseVisualStyleBackColor = true;
			// 
			// DgvPages
			// 
			this.DgvPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvPages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvPages.FilterAndSortEnabled = true;
			this.DgvPages.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvPages.Location = new System.Drawing.Point(12, 278);
			this.DgvPages.Name = "DgvPages";
			this.DgvPages.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvPages.RowTemplate.Height = 25;
			this.DgvPages.Size = new System.Drawing.Size(356, 459);
			this.DgvPages.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvPages.TabIndex = 39;
			// 
			// LbPages
			// 
			this.LbPages.AutoSize = true;
			this.LbPages.Location = new System.Drawing.Point(13, 260);
			this.LbPages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbPages.Name = "LbPages";
			this.LbPages.Size = new System.Drawing.Size(38, 15);
			this.LbPages.TabIndex = 38;
			this.LbPages.Text = "Pages";
			// 
			// LbBriefingImageSize
			// 
			this.LbBriefingImageSize.AutoSize = true;
			this.LbBriefingImageSize.Location = new System.Drawing.Point(13, 212);
			this.LbBriefingImageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbBriefingImageSize.Name = "LbBriefingImageSize";
			this.LbBriefingImageSize.Size = new System.Drawing.Size(62, 15);
			this.LbBriefingImageSize.TabIndex = 37;
			this.LbBriefingImageSize.Text = "Image size";
			// 
			// UcImageSize
			// 
			this.UcImageSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UcImageSize.Location = new System.Drawing.Point(13, 230);
			this.UcImageSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.UcImageSize.Name = "UcImageSize";
			this.UcImageSize.SelectedSize = new System.Drawing.Size(1, 1);
			this.UcImageSize.Size = new System.Drawing.Size(355, 27);
			this.UcImageSize.TabIndex = 36;
			// 
			// CbMeasurementSystem
			// 
			this.CbMeasurementSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CbMeasurementSystem.FormattingEnabled = true;
			this.CbMeasurementSystem.Location = new System.Drawing.Point(139, 121);
			this.CbMeasurementSystem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CbMeasurementSystem.Name = "CbMeasurementSystem";
			this.CbMeasurementSystem.Size = new System.Drawing.Size(230, 23);
			this.CbMeasurementSystem.TabIndex = 35;
			// 
			// LbMeasurementSystem
			// 
			this.LbMeasurementSystem.AutoSize = true;
			this.LbMeasurementSystem.Location = new System.Drawing.Point(13, 125);
			this.LbMeasurementSystem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbMeasurementSystem.Name = "LbMeasurementSystem";
			this.LbMeasurementSystem.Size = new System.Drawing.Size(120, 15);
			this.LbMeasurementSystem.TabIndex = 34;
			this.LbMeasurementSystem.Text = "Measurement system";
			// 
			// LstCoordinateDisplay
			// 
			this.LstCoordinateDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LstCoordinateDisplay.FormattingEnabled = true;
			this.LstCoordinateDisplay.Location = new System.Drawing.Point(139, 150);
			this.LstCoordinateDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LstCoordinateDisplay.Name = "LstCoordinateDisplay";
			this.LstCoordinateDisplay.Size = new System.Drawing.Size(230, 58);
			this.LstCoordinateDisplay.TabIndex = 33;
			// 
			// CbWeatherDisplay
			// 
			this.CbWeatherDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CbWeatherDisplay.FormattingEnabled = true;
			this.CbWeatherDisplay.Location = new System.Drawing.Point(139, 92);
			this.CbWeatherDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CbWeatherDisplay.Name = "CbWeatherDisplay";
			this.CbWeatherDisplay.Size = new System.Drawing.Size(230, 23);
			this.CbWeatherDisplay.TabIndex = 32;
			// 
			// LbCoordinateDisplay
			// 
			this.LbCoordinateDisplay.AutoSize = true;
			this.LbCoordinateDisplay.Location = new System.Drawing.Point(13, 150);
			this.LbCoordinateDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbCoordinateDisplay.Name = "LbCoordinateDisplay";
			this.LbCoordinateDisplay.Size = new System.Drawing.Size(106, 15);
			this.LbCoordinateDisplay.TabIndex = 31;
			this.LbCoordinateDisplay.Text = "Coordinate display";
			// 
			// LbWeatherDisplay
			// 
			this.LbWeatherDisplay.AutoSize = true;
			this.LbWeatherDisplay.Location = new System.Drawing.Point(12, 100);
			this.LbWeatherDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbWeatherDisplay.Name = "LbWeatherDisplay";
			this.LbWeatherDisplay.Size = new System.Drawing.Size(91, 15);
			this.LbWeatherDisplay.TabIndex = 30;
			this.LbWeatherDisplay.Text = "Weather display";
			// 
			// CbCoalition
			// 
			this.CbCoalition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CbCoalition.FormattingEnabled = true;
			this.CbCoalition.Location = new System.Drawing.Point(139, 63);
			this.CbCoalition.Name = "CbCoalition";
			this.CbCoalition.Size = new System.Drawing.Size(230, 23);
			this.CbCoalition.TabIndex = 5;
			// 
			// LbCoalition
			// 
			this.LbCoalition.AutoSize = true;
			this.LbCoalition.Location = new System.Drawing.Point(13, 66);
			this.LbCoalition.Name = "LbCoalition";
			this.LbCoalition.Size = new System.Drawing.Size(55, 15);
			this.LbCoalition.TabIndex = 4;
			this.LbCoalition.Text = "Coalition";
			// 
			// TbUnitTypes
			// 
			this.TbUnitTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbUnitTypes.Location = new System.Drawing.Point(139, 34);
			this.TbUnitTypes.Name = "TbUnitTypes";
			this.TbUnitTypes.Size = new System.Drawing.Size(230, 23);
			this.TbUnitTypes.TabIndex = 3;
			// 
			// LbUnitTypes
			// 
			this.LbUnitTypes.AutoSize = true;
			this.LbUnitTypes.Location = new System.Drawing.Point(12, 37);
			this.LbUnitTypes.Name = "LbUnitTypes";
			this.LbUnitTypes.Size = new System.Drawing.Size(60, 15);
			this.LbUnitTypes.TabIndex = 2;
			this.LbUnitTypes.Text = "Unit types";
			// 
			// TbName
			// 
			this.TbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbName.Location = new System.Drawing.Point(139, 5);
			this.TbName.Name = "TbName";
			this.TbName.Size = new System.Drawing.Size(230, 23);
			this.TbName.TabIndex = 1;
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(13, 8);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(39, 15);
			this.LbName.TabIndex = 0;
			this.LbName.Text = "Name";
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
			this.ScMain.Panel1.Controls.Add(this.BtPageRemove);
			this.ScMain.Panel1.Controls.Add(this.BtPageAdd);
			this.ScMain.Panel1.Controls.Add(this.DgvPages);
			this.ScMain.Panel1.Controls.Add(this.LbPages);
			this.ScMain.Panel1.Controls.Add(this.LbBriefingImageSize);
			this.ScMain.Panel1.Controls.Add(this.UcImageSize);
			this.ScMain.Panel1.Controls.Add(this.CbMeasurementSystem);
			this.ScMain.Panel1.Controls.Add(this.LbMeasurementSystem);
			this.ScMain.Panel1.Controls.Add(this.LstCoordinateDisplay);
			this.ScMain.Panel1.Controls.Add(this.CbWeatherDisplay);
			this.ScMain.Panel1.Controls.Add(this.LbCoordinateDisplay);
			this.ScMain.Panel1.Controls.Add(this.LbWeatherDisplay);
			this.ScMain.Panel1.Controls.Add(this.CbCoalition);
			this.ScMain.Panel1.Controls.Add(this.LbCoalition);
			this.ScMain.Panel1.Controls.Add(this.TbUnitTypes);
			this.ScMain.Panel1.Controls.Add(this.LbUnitTypes);
			this.ScMain.Panel1.Controls.Add(this.TbName);
			this.ScMain.Panel1.Controls.Add(this.LbName);
			// 
			// ScMain.Panel2
			// 
			this.ScMain.Panel2.AutoScroll = true;
			this.ScMain.Size = new System.Drawing.Size(1179, 769);
			this.ScMain.SplitterDistance = 372;
			this.ScMain.SplitterWidth = 5;
			this.ScMain.TabIndex = 2;
			// 
			// FrmBriefingFolder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1179, 769);
			this.Controls.Add(this.ScMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmBriefingFolder";
			this.Text = "Briefing folder";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBriefingFolder_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.DgvPages)).EndInit();
			this.ScMain.Panel1.ResumeLayout(false);
			this.ScMain.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
			this.ScMain.ResumeLayout(false);
			this.ResumeLayout(false);

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
	}
}
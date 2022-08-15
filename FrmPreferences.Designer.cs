
namespace DcsBriefop
{
	partial class FrmPreferences
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
			this.CbMapProvider = new System.Windows.Forms.ComboBox();
			this.LbMapDisplay = new System.Windows.Forms.Label();
			this.GbImage = new System.Windows.Forms.GroupBox();
			this.UcDefaultImageSize = new DcsBriefop.FormBop.UcImageSize();
			this.label1 = new System.Windows.Forms.Label();
			this.UcImageBackgroundColor = new DcsBriefop.FormBop.UcColor();
			this.LbImageBackgroundColor = new System.Windows.Forms.Label();
			this.GbMap = new System.Windows.Forms.GroupBox();
			this.UdMapDefaultZoom = new System.Windows.Forms.NumericUpDown();
			this.LbMapDefaultZoom = new System.Windows.Forms.Label();
			this.GbGeneration = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.CkGenerateOnSave = new System.Windows.Forms.CheckBox();
			this.DgvFileTypes = new System.Windows.Forms.DataGridView();
			this.CkLocalDirectoryHtml = new System.Windows.Forms.CheckBox();
			this.CkLocalDirectory = new System.Windows.Forms.CheckBox();
			this.CkMizFile = new System.Windows.Forms.CheckBox();
			this.BtSave = new System.Windows.Forms.Button();
			this.BtCancel = new System.Windows.Forms.Button();
			this.CkNoCallsignForPlayableFlights = new System.Windows.Forms.CheckBox();
			this.CkBackupBeforeOverwrite = new System.Windows.Forms.CheckBox();
			this.CkGenerateBatchCommandOnSave = new System.Windows.Forms.CheckBox();
			this.GbImage.SuspendLayout();
			this.GbMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.UdMapDefaultZoom)).BeginInit();
			this.GbGeneration.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvFileTypes)).BeginInit();
			this.SuspendLayout();
			// 
			// CbMapProvider
			// 
			this.CbMapProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CbMapProvider.FormattingEnabled = true;
			this.CbMapProvider.Location = new System.Drawing.Point(118, 19);
			this.CbMapProvider.Name = "CbMapProvider";
			this.CbMapProvider.Size = new System.Drawing.Size(181, 21);
			this.CbMapProvider.TabIndex = 19;
			// 
			// LbMapDisplay
			// 
			this.LbMapDisplay.AutoSize = true;
			this.LbMapDisplay.Location = new System.Drawing.Point(7, 22);
			this.LbMapDisplay.Name = "LbMapDisplay";
			this.LbMapDisplay.Size = new System.Drawing.Size(105, 13);
			this.LbMapDisplay.TabIndex = 20;
			this.LbMapDisplay.Text = "Default map provider";
			// 
			// GbImage
			// 
			this.GbImage.Controls.Add(this.UcDefaultImageSize);
			this.GbImage.Controls.Add(this.label1);
			this.GbImage.Controls.Add(this.UcImageBackgroundColor);
			this.GbImage.Controls.Add(this.LbImageBackgroundColor);
			this.GbImage.Location = new System.Drawing.Point(12, 388);
			this.GbImage.Name = "GbImage";
			this.GbImage.Size = new System.Drawing.Size(322, 100);
			this.GbImage.TabIndex = 39;
			this.GbImage.TabStop = false;
			this.GbImage.Text = "Image";
			// 
			// UcDefaultImageSize
			// 
			this.UcDefaultImageSize.Location = new System.Drawing.Point(36, 16);
			this.UcDefaultImageSize.Name = "UcDefaultImageSize";
			this.UcDefaultImageSize.SelectedSize = new System.Drawing.Size(1, 1);
			this.UcDefaultImageSize.Size = new System.Drawing.Size(280, 23);
			this.UcDefaultImageSize.TabIndex = 38;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(40, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(230, 13);
			this.label1.TabIndex = 37;
			this.label1.Text = "Recommended ratio for DCS kneeboard is 0.66";
			// 
			// UcImageBackgroundColor
			// 
			this.UcImageBackgroundColor.Location = new System.Drawing.Point(104, 66);
			this.UcImageBackgroundColor.Name = "UcImageBackgroundColor";
			this.UcImageBackgroundColor.SelectedColor = null;
			this.UcImageBackgroundColor.SelectedColorHtml = null;
			this.UcImageBackgroundColor.Size = new System.Drawing.Size(154, 23);
			this.UcImageBackgroundColor.TabIndex = 33;
			// 
			// LbImageBackgroundColor
			// 
			this.LbImageBackgroundColor.AutoSize = true;
			this.LbImageBackgroundColor.Location = new System.Drawing.Point(7, 72);
			this.LbImageBackgroundColor.Name = "LbImageBackgroundColor";
			this.LbImageBackgroundColor.Size = new System.Drawing.Size(91, 13);
			this.LbImageBackgroundColor.TabIndex = 29;
			this.LbImageBackgroundColor.Text = "Background color";
			// 
			// GbMap
			// 
			this.GbMap.Controls.Add(this.UdMapDefaultZoom);
			this.GbMap.Controls.Add(this.LbMapDefaultZoom);
			this.GbMap.Controls.Add(this.CbMapProvider);
			this.GbMap.Controls.Add(this.LbMapDisplay);
			this.GbMap.Location = new System.Drawing.Point(12, 80);
			this.GbMap.Name = "GbMap";
			this.GbMap.Size = new System.Drawing.Size(322, 78);
			this.GbMap.TabIndex = 40;
			this.GbMap.TabStop = false;
			this.GbMap.Text = "Map preferences";
			// 
			// UdMapDefaultZoom
			// 
			this.UdMapDefaultZoom.Location = new System.Drawing.Point(118, 46);
			this.UdMapDefaultZoom.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.UdMapDefaultZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdMapDefaultZoom.Name = "UdMapDefaultZoom";
			this.UdMapDefaultZoom.Size = new System.Drawing.Size(51, 20);
			this.UdMapDefaultZoom.TabIndex = 35;
			this.UdMapDefaultZoom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// LbMapDefaultZoom
			// 
			this.LbMapDefaultZoom.AutoSize = true;
			this.LbMapDefaultZoom.Location = new System.Drawing.Point(7, 48);
			this.LbMapDefaultZoom.Name = "LbMapDefaultZoom";
			this.LbMapDefaultZoom.Size = new System.Drawing.Size(92, 13);
			this.LbMapDefaultZoom.TabIndex = 21;
			this.LbMapDefaultZoom.Text = "Default map zoom";
			// 
			// GbGeneration
			// 
			this.GbGeneration.Controls.Add(this.label3);
			this.GbGeneration.Controls.Add(this.CkGenerateOnSave);
			this.GbGeneration.Controls.Add(this.DgvFileTypes);
			this.GbGeneration.Controls.Add(this.CkLocalDirectoryHtml);
			this.GbGeneration.Controls.Add(this.CkLocalDirectory);
			this.GbGeneration.Controls.Add(this.CkMizFile);
			this.GbGeneration.Location = new System.Drawing.Point(12, 164);
			this.GbGeneration.Name = "GbGeneration";
			this.GbGeneration.Size = new System.Drawing.Size(322, 218);
			this.GbGeneration.TabIndex = 41;
			this.GbGeneration.TabStop = false;
			this.GbGeneration.Text = "File generation preferences";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 13);
			this.label3.TabIndex = 45;
			this.label3.Text = "Files to generate";
			// 
			// CkGenerateOnSave
			// 
			this.CkGenerateOnSave.AutoSize = true;
			this.CkGenerateOnSave.Location = new System.Drawing.Point(10, 19);
			this.CkGenerateOnSave.Name = "CkGenerateOnSave";
			this.CkGenerateOnSave.Size = new System.Drawing.Size(111, 17);
			this.CkGenerateOnSave.TabIndex = 44;
			this.CkGenerateOnSave.Text = "Generate on save";
			this.CkGenerateOnSave.UseVisualStyleBackColor = true;
			// 
			// DgvFileTypes
			// 
			this.DgvFileTypes.AllowUserToAddRows = false;
			this.DgvFileTypes.AllowUserToDeleteRows = false;
			this.DgvFileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvFileTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvFileTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvFileTypes.Location = new System.Drawing.Point(10, 113);
			this.DgvFileTypes.Name = "DgvFileTypes";
			this.DgvFileTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvFileTypes.Size = new System.Drawing.Size(306, 99);
			this.DgvFileTypes.TabIndex = 43;
			// 
			// CkLocalDirectoryHtml
			// 
			this.CkLocalDirectoryHtml.AutoSize = true;
			this.CkLocalDirectoryHtml.Location = new System.Drawing.Point(165, 65);
			this.CkLocalDirectoryHtml.Name = "CkLocalDirectoryHtml";
			this.CkLocalDirectoryHtml.Size = new System.Drawing.Size(91, 17);
			this.CkLocalDirectoryHtml.TabIndex = 42;
			this.CkLocalDirectoryHtml.Text = "With html files";
			this.CkLocalDirectoryHtml.UseVisualStyleBackColor = true;
			// 
			// CkLocalDirectory
			// 
			this.CkLocalDirectory.AutoSize = true;
			this.CkLocalDirectory.Location = new System.Drawing.Point(10, 65);
			this.CkLocalDirectory.Name = "CkLocalDirectory";
			this.CkLocalDirectory.Size = new System.Drawing.Size(149, 17);
			this.CkLocalDirectory.TabIndex = 41;
			this.CkLocalDirectory.Text = "Generate in local directory";
			this.CkLocalDirectory.UseVisualStyleBackColor = true;
			// 
			// CkMizFile
			// 
			this.CkMizFile.AutoSize = true;
			this.CkMizFile.Location = new System.Drawing.Point(10, 42);
			this.CkMizFile.Name = "CkMizFile";
			this.CkMizFile.Size = new System.Drawing.Size(115, 17);
			this.CkMizFile.TabIndex = 40;
			this.CkMizFile.Text = "Generate in miz file";
			this.CkMizFile.UseVisualStyleBackColor = true;
			// 
			// BtSave
			// 
			this.BtSave.Location = new System.Drawing.Point(176, 494);
			this.BtSave.Name = "BtSave";
			this.BtSave.Size = new System.Drawing.Size(75, 23);
			this.BtSave.TabIndex = 42;
			this.BtSave.Text = "Save";
			this.BtSave.UseVisualStyleBackColor = true;
			this.BtSave.Click += new System.EventHandler(this.BtSave_Click);
			// 
			// BtCancel
			// 
			this.BtCancel.Location = new System.Drawing.Point(259, 494);
			this.BtCancel.Name = "BtCancel";
			this.BtCancel.Size = new System.Drawing.Size(75, 23);
			this.BtCancel.TabIndex = 43;
			this.BtCancel.Text = "Cancel";
			this.BtCancel.UseVisualStyleBackColor = true;
			this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// CkNoCallsignForPlayableFlights
			// 
			this.CkNoCallsignForPlayableFlights.AutoSize = true;
			this.CkNoCallsignForPlayableFlights.Location = new System.Drawing.Point(12, 12);
			this.CkNoCallsignForPlayableFlights.Name = "CkNoCallsignForPlayableFlights";
			this.CkNoCallsignForPlayableFlights.Size = new System.Drawing.Size(165, 17);
			this.CkNoCallsignForPlayableFlights.TabIndex = 46;
			this.CkNoCallsignForPlayableFlights.Text = "No callsign for playable flights";
			this.CkNoCallsignForPlayableFlights.UseVisualStyleBackColor = true;
			// 
			// CkBackupBeforeOverwrite
			// 
			this.CkBackupBeforeOverwrite.AutoSize = true;
			this.CkBackupBeforeOverwrite.Location = new System.Drawing.Point(12, 35);
			this.CkBackupBeforeOverwrite.Name = "CkBackupBeforeOverwrite";
			this.CkBackupBeforeOverwrite.Size = new System.Drawing.Size(209, 17);
			this.CkBackupBeforeOverwrite.TabIndex = 47;
			this.CkBackupBeforeOverwrite.Text = "Make backup when overwriting miz file";
			this.CkBackupBeforeOverwrite.UseVisualStyleBackColor = true;
			// 
			// CkGenerateBatchCommandOnSave
			// 
			this.CkGenerateBatchCommandOnSave.AutoSize = true;
			this.CkGenerateBatchCommandOnSave.Location = new System.Drawing.Point(12, 57);
			this.CkGenerateBatchCommandOnSave.Name = "CkGenerateBatchCommandOnSave";
			this.CkGenerateBatchCommandOnSave.Size = new System.Drawing.Size(190, 17);
			this.CkGenerateBatchCommandOnSave.TabIndex = 48;
			this.CkGenerateBatchCommandOnSave.Text = "Generate batch command on save";
			this.CkGenerateBatchCommandOnSave.UseVisualStyleBackColor = true;
			// 
			// FrmPreferences
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(349, 528);
			this.Controls.Add(this.CkGenerateBatchCommandOnSave);
			this.Controls.Add(this.CkBackupBeforeOverwrite);
			this.Controls.Add(this.CkNoCallsignForPlayableFlights);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.BtSave);
			this.Controls.Add(this.GbGeneration);
			this.Controls.Add(this.GbMap);
			this.Controls.Add(this.GbImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmPreferences";
			this.Text = "Preferences";
			this.GbImage.ResumeLayout(false);
			this.GbImage.PerformLayout();
			this.GbMap.ResumeLayout(false);
			this.GbMap.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.UdMapDefaultZoom)).EndInit();
			this.GbGeneration.ResumeLayout(false);
			this.GbGeneration.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvFileTypes)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox CbMapProvider;
		private System.Windows.Forms.Label LbMapDisplay;
		private System.Windows.Forms.GroupBox GbImage;
		private System.Windows.Forms.Label label1;
		private FormBop.UcColor UcImageBackgroundColor;
		private System.Windows.Forms.Label LbImageBackgroundColor;
		private System.Windows.Forms.GroupBox GbMap;
		private System.Windows.Forms.NumericUpDown UdMapDefaultZoom;
		private System.Windows.Forms.Label LbMapDefaultZoom;
		private System.Windows.Forms.GroupBox GbGeneration;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox CkGenerateOnSave;
		private System.Windows.Forms.DataGridView DgvFileTypes;
		private System.Windows.Forms.CheckBox CkLocalDirectoryHtml;
		private System.Windows.Forms.CheckBox CkLocalDirectory;
		private System.Windows.Forms.CheckBox CkMizFile;
		private System.Windows.Forms.Button BtSave;
		private System.Windows.Forms.Button BtCancel;
		private FormBop.UcImageSize UcDefaultImageSize;
		private System.Windows.Forms.CheckBox CkNoCallsignForPlayableFlights;
		private System.Windows.Forms.CheckBox CkBackupBeforeOverwrite;
		private System.Windows.Forms.CheckBox CkGenerateBatchCommandOnSave;
	}
}
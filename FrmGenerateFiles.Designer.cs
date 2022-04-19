
namespace DcsBriefop
{
	partial class FrmGenerateFiles
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
			this.CkMizFile = new System.Windows.Forms.CheckBox();
			this.CkLocalDirectory = new System.Windows.Forms.CheckBox();
			this.TbLocalDirectory = new System.Windows.Forms.TextBox();
			this.BtGenerate = new System.Windows.Forms.Button();
			this.BtLocalDirectoryBrowse = new System.Windows.Forms.Button();
			this.CkLocalDirectoryHtml = new System.Windows.Forms.CheckBox();
			this.BtLocalDirectoryReset = new System.Windows.Forms.Button();
			this.DgvFileTypes = new System.Windows.Forms.DataGridView();
			this.LbImageSize = new System.Windows.Forms.Label();
			this.LbImageBackgroundColor = new System.Windows.Forms.Label();
			this.LbImageSizeX = new System.Windows.Forms.Label();
			this.UcImageBackgroundColor = new DcsBriefop.UcBriefing.UcColor();
			this.UdImageWidth = new System.Windows.Forms.NumericUpDown();
			this.UdImageHeight = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.DgvFileTypes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UdImageWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UdImageHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// CkMizFile
			// 
			this.CkMizFile.AutoSize = true;
			this.CkMizFile.Location = new System.Drawing.Point(12, 12);
			this.CkMizFile.Name = "CkMizFile";
			this.CkMizFile.Size = new System.Drawing.Size(115, 17);
			this.CkMizFile.TabIndex = 0;
			this.CkMizFile.Text = "Generate in miz file";
			this.CkMizFile.UseVisualStyleBackColor = true;
			// 
			// CkLocalDirectory
			// 
			this.CkLocalDirectory.AutoSize = true;
			this.CkLocalDirectory.Location = new System.Drawing.Point(12, 31);
			this.CkLocalDirectory.Name = "CkLocalDirectory";
			this.CkLocalDirectory.Size = new System.Drawing.Size(149, 17);
			this.CkLocalDirectory.TabIndex = 1;
			this.CkLocalDirectory.Text = "Generate in local directory";
			this.CkLocalDirectory.UseVisualStyleBackColor = true;
			this.CkLocalDirectory.CheckedChanged += new System.EventHandler(this.CkLocalDirectory_CheckedChanged);
			// 
			// TbLocalDirectory
			// 
			this.TbLocalDirectory.Location = new System.Drawing.Point(260, 29);
			this.TbLocalDirectory.Name = "TbLocalDirectory";
			this.TbLocalDirectory.Size = new System.Drawing.Size(220, 20);
			this.TbLocalDirectory.TabIndex = 2;
			// 
			// BtGenerate
			// 
			this.BtGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtGenerate.Location = new System.Drawing.Point(12, 229);
			this.BtGenerate.Name = "BtGenerate";
			this.BtGenerate.Size = new System.Drawing.Size(544, 23);
			this.BtGenerate.TabIndex = 3;
			this.BtGenerate.Text = "Generate";
			this.BtGenerate.UseVisualStyleBackColor = true;
			this.BtGenerate.Click += new System.EventHandler(this.BtGenerate_Click);
			// 
			// BtLocalDirectoryBrowse
			// 
			this.BtLocalDirectoryBrowse.Location = new System.Drawing.Point(486, 27);
			this.BtLocalDirectoryBrowse.Name = "BtLocalDirectoryBrowse";
			this.BtLocalDirectoryBrowse.Size = new System.Drawing.Size(32, 23);
			this.BtLocalDirectoryBrowse.TabIndex = 4;
			this.BtLocalDirectoryBrowse.Text = "...";
			this.BtLocalDirectoryBrowse.UseVisualStyleBackColor = true;
			this.BtLocalDirectoryBrowse.Click += new System.EventHandler(this.BtDirectoryBrowse_Click);
			// 
			// CkLocalDirectoryHtml
			// 
			this.CkLocalDirectoryHtml.AutoSize = true;
			this.CkLocalDirectoryHtml.Location = new System.Drawing.Point(167, 31);
			this.CkLocalDirectoryHtml.Name = "CkLocalDirectoryHtml";
			this.CkLocalDirectoryHtml.Size = new System.Drawing.Size(91, 17);
			this.CkLocalDirectoryHtml.TabIndex = 5;
			this.CkLocalDirectoryHtml.Text = "With html files";
			this.CkLocalDirectoryHtml.UseVisualStyleBackColor = true;
			// 
			// BtLocalDirectoryReset
			// 
			this.BtLocalDirectoryReset.Location = new System.Drawing.Point(524, 27);
			this.BtLocalDirectoryReset.Name = "BtLocalDirectoryReset";
			this.BtLocalDirectoryReset.Size = new System.Drawing.Size(32, 23);
			this.BtLocalDirectoryReset.TabIndex = 6;
			this.BtLocalDirectoryReset.Text = "R";
			this.BtLocalDirectoryReset.UseVisualStyleBackColor = true;
			this.BtLocalDirectoryReset.Click += new System.EventHandler(this.BtDirectoryReset_Click);
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
			this.DgvFileTypes.Location = new System.Drawing.Point(12, 80);
			this.DgvFileTypes.Name = "DgvFileTypes";
			this.DgvFileTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvFileTypes.Size = new System.Drawing.Size(544, 143);
			this.DgvFileTypes.TabIndex = 25;
			// 
			// LbImageSize
			// 
			this.LbImageSize.AutoSize = true;
			this.LbImageSize.Location = new System.Drawing.Point(9, 51);
			this.LbImageSize.Name = "LbImageSize";
			this.LbImageSize.Size = new System.Drawing.Size(57, 13);
			this.LbImageSize.TabIndex = 28;
			this.LbImageSize.Text = "Image size";
			// 
			// LbImageBackgroundColor
			// 
			this.LbImageBackgroundColor.AutoSize = true;
			this.LbImageBackgroundColor.Location = new System.Drawing.Point(257, 55);
			this.LbImageBackgroundColor.Name = "LbImageBackgroundColor";
			this.LbImageBackgroundColor.Size = new System.Drawing.Size(91, 13);
			this.LbImageBackgroundColor.TabIndex = 29;
			this.LbImageBackgroundColor.Text = "Background color";
			// 
			// LbImageSizeX
			// 
			this.LbImageSizeX.AutoSize = true;
			this.LbImageSizeX.Location = new System.Drawing.Point(130, 51);
			this.LbImageSizeX.Name = "LbImageSizeX";
			this.LbImageSizeX.Size = new System.Drawing.Size(12, 13);
			this.LbImageSizeX.TabIndex = 32;
			this.LbImageSizeX.Text = "x";
			// 
			// UcImageBackgroundColor
			// 
			this.UcImageBackgroundColor.Location = new System.Drawing.Point(364, 51);
			this.UcImageBackgroundColor.Name = "UcImageBackgroundColor";
			this.UcImageBackgroundColor.SelectedColor = null;
			this.UcImageBackgroundColor.Size = new System.Drawing.Size(154, 23);
			this.UcImageBackgroundColor.TabIndex = 33;
			// 
			// UdImageWidth
			// 
			this.UdImageWidth.Location = new System.Drawing.Point(73, 51);
			this.UdImageWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.UdImageWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdImageWidth.Name = "UdImageWidth";
			this.UdImageWidth.Size = new System.Drawing.Size(51, 20);
			this.UdImageWidth.TabIndex = 34;
			this.UdImageWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// UdImageHeight
			// 
			this.UdImageHeight.Location = new System.Drawing.Point(148, 51);
			this.UdImageHeight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.UdImageHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdImageHeight.Name = "UdImageHeight";
			this.UdImageHeight.Size = new System.Drawing.Size(51, 20);
			this.UdImageHeight.TabIndex = 35;
			this.UdImageHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// FrmGenerateFiles
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 259);
			this.Controls.Add(this.UdImageHeight);
			this.Controls.Add(this.UdImageWidth);
			this.Controls.Add(this.UcImageBackgroundColor);
			this.Controls.Add(this.LbImageSizeX);
			this.Controls.Add(this.LbImageBackgroundColor);
			this.Controls.Add(this.LbImageSize);
			this.Controls.Add(this.DgvFileTypes);
			this.Controls.Add(this.BtLocalDirectoryReset);
			this.Controls.Add(this.CkLocalDirectoryHtml);
			this.Controls.Add(this.BtLocalDirectoryBrowse);
			this.Controls.Add(this.BtGenerate);
			this.Controls.Add(this.TbLocalDirectory);
			this.Controls.Add(this.CkLocalDirectory);
			this.Controls.Add(this.CkMizFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmGenerateFiles";
			this.ShowInTaskbar = false;
			this.Text = "FrmGenerateFiles";
			((System.ComponentModel.ISupportInitialize)(this.DgvFileTypes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UdImageWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UdImageHeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox CkMizFile;
		private System.Windows.Forms.CheckBox CkLocalDirectory;
		private System.Windows.Forms.TextBox TbLocalDirectory;
		private System.Windows.Forms.Button BtGenerate;
		private System.Windows.Forms.Button BtLocalDirectoryBrowse;
		private System.Windows.Forms.CheckBox CkLocalDirectoryHtml;
		private System.Windows.Forms.Button BtLocalDirectoryReset;
		private System.Windows.Forms.DataGridView DgvFileTypes;
		private System.Windows.Forms.Label LbImageSize;
		private System.Windows.Forms.Label LbImageBackgroundColor;
		private System.Windows.Forms.Label LbImageSizeX;
		private UcBriefing.UcColor UcImageBackgroundColor;
		private System.Windows.Forms.NumericUpDown UdImageWidth;
		private System.Windows.Forms.NumericUpDown UdImageHeight;
	}
}
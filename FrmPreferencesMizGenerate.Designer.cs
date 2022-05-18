
namespace DcsBriefop
{
	partial class FrmPreferencesMizGenerate
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
			this.LbImageBackgroundColor = new System.Windows.Forms.Label();
			this.CkGenerateOnSave = new System.Windows.Forms.CheckBox();
			this.GbImage = new System.Windows.Forms.GroupBox();
			this.UcExportImageSize = new DcsBriefop.UcBriefing.UcImageSize();
			this.label1 = new System.Windows.Forms.Label();
			this.UcImageBackgroundColor = new DcsBriefop.UcBriefing.UcColor();
			this.label2 = new System.Windows.Forms.Label();
			this.BtClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.DgvFileTypes)).BeginInit();
			this.GbImage.SuspendLayout();
			this.SuspendLayout();
			// 
			// CkMizFile
			// 
			this.CkMizFile.AutoSize = true;
			this.CkMizFile.Location = new System.Drawing.Point(12, 35);
			this.CkMizFile.Name = "CkMizFile";
			this.CkMizFile.Size = new System.Drawing.Size(115, 17);
			this.CkMizFile.TabIndex = 0;
			this.CkMizFile.Text = "Generate in miz file";
			this.CkMizFile.UseVisualStyleBackColor = true;
			// 
			// CkLocalDirectory
			// 
			this.CkLocalDirectory.AutoSize = true;
			this.CkLocalDirectory.Location = new System.Drawing.Point(12, 58);
			this.CkLocalDirectory.Name = "CkLocalDirectory";
			this.CkLocalDirectory.Size = new System.Drawing.Size(149, 17);
			this.CkLocalDirectory.TabIndex = 1;
			this.CkLocalDirectory.Text = "Generate in local directory";
			this.CkLocalDirectory.UseVisualStyleBackColor = true;
			this.CkLocalDirectory.CheckedChanged += new System.EventHandler(this.CkLocalDirectory_CheckedChanged);
			// 
			// TbLocalDirectory
			// 
			this.TbLocalDirectory.Location = new System.Drawing.Point(158, 56);
			this.TbLocalDirectory.Name = "TbLocalDirectory";
			this.TbLocalDirectory.Size = new System.Drawing.Size(110, 20);
			this.TbLocalDirectory.TabIndex = 2;
			// 
			// BtGenerate
			// 
			this.BtGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtGenerate.Location = new System.Drawing.Point(341, 211);
			this.BtGenerate.Name = "BtGenerate";
			this.BtGenerate.Size = new System.Drawing.Size(82, 23);
			this.BtGenerate.TabIndex = 3;
			this.BtGenerate.Text = "Generate";
			this.BtGenerate.UseVisualStyleBackColor = true;
			this.BtGenerate.Click += new System.EventHandler(this.BtGenerate_Click);
			// 
			// BtLocalDirectoryBrowse
			// 
			this.BtLocalDirectoryBrowse.Location = new System.Drawing.Point(269, 54);
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
			this.CkLocalDirectoryHtml.Location = new System.Drawing.Point(29, 81);
			this.CkLocalDirectoryHtml.Name = "CkLocalDirectoryHtml";
			this.CkLocalDirectoryHtml.Size = new System.Drawing.Size(91, 17);
			this.CkLocalDirectoryHtml.TabIndex = 5;
			this.CkLocalDirectoryHtml.Text = "With html files";
			this.CkLocalDirectoryHtml.UseVisualStyleBackColor = true;
			// 
			// BtLocalDirectoryReset
			// 
			this.BtLocalDirectoryReset.Location = new System.Drawing.Point(302, 54);
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
			this.DgvFileTypes.Location = new System.Drawing.Point(340, 35);
			this.DgvFileTypes.Name = "DgvFileTypes";
			this.DgvFileTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvFileTypes.Size = new System.Drawing.Size(174, 170);
			this.DgvFileTypes.TabIndex = 25;
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
			// CkGenerateOnSave
			// 
			this.CkGenerateOnSave.AutoSize = true;
			this.CkGenerateOnSave.Location = new System.Drawing.Point(12, 12);
			this.CkGenerateOnSave.Name = "CkGenerateOnSave";
			this.CkGenerateOnSave.Size = new System.Drawing.Size(111, 17);
			this.CkGenerateOnSave.TabIndex = 36;
			this.CkGenerateOnSave.Text = "Generate on save";
			this.CkGenerateOnSave.UseVisualStyleBackColor = true;
			// 
			// GbImage
			// 
			this.GbImage.Controls.Add(this.UcExportImageSize);
			this.GbImage.Controls.Add(this.label1);
			this.GbImage.Controls.Add(this.UcImageBackgroundColor);
			this.GbImage.Controls.Add(this.LbImageBackgroundColor);
			this.GbImage.Location = new System.Drawing.Point(12, 106);
			this.GbImage.Name = "GbImage";
			this.GbImage.Size = new System.Drawing.Size(322, 100);
			this.GbImage.TabIndex = 38;
			this.GbImage.TabStop = false;
			this.GbImage.Text = "Image";
			// 
			// UcExportImageSize
			// 
			this.UcExportImageSize.Location = new System.Drawing.Point(17, 16);
			this.UcExportImageSize.Name = "UcExportImageSize";
			this.UcExportImageSize.SelectedSize = new System.Drawing.Size(1, 1);
			this.UcExportImageSize.Size = new System.Drawing.Size(280, 23);
			this.UcExportImageSize.TabIndex = 38;
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(337, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 39;
			this.label2.Text = "Files to generate";
			// 
			// BtClose
			// 
			this.BtClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtClose.Location = new System.Drawing.Point(432, 211);
			this.BtClose.Name = "BtClose";
			this.BtClose.Size = new System.Drawing.Size(82, 23);
			this.BtClose.TabIndex = 40;
			this.BtClose.Text = "Close";
			this.BtClose.UseVisualStyleBackColor = true;
			this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
			// 
			// FrmPreferencesMizGenerate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 241);
			this.Controls.Add(this.BtClose);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.GbImage);
			this.Controls.Add(this.CkGenerateOnSave);
			this.Controls.Add(this.DgvFileTypes);
			this.Controls.Add(this.BtLocalDirectoryReset);
			this.Controls.Add(this.CkLocalDirectoryHtml);
			this.Controls.Add(this.BtLocalDirectoryBrowse);
			this.Controls.Add(this.BtGenerate);
			this.Controls.Add(this.TbLocalDirectory);
			this.Controls.Add(this.CkLocalDirectory);
			this.Controls.Add(this.CkMizFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmPreferencesMizGenerate";
			this.ShowInTaskbar = false;
			this.Text = "Mission briefing generation preferences";
			((System.ComponentModel.ISupportInitialize)(this.DgvFileTypes)).EndInit();
			this.GbImage.ResumeLayout(false);
			this.GbImage.PerformLayout();
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
		private System.Windows.Forms.Label LbImageBackgroundColor;
		private UcBriefing.UcColor UcImageBackgroundColor;
		private System.Windows.Forms.CheckBox CkGenerateOnSave;
		private System.Windows.Forms.GroupBox GbImage;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BtClose;
		private UcBriefing.UcImageSize UcExportImageSize;
	}
}

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
			this.CkLocalDirectoryHtmlBitmaps = new System.Windows.Forms.CheckBox();
			this.BtLocalDirectoryReset = new System.Windows.Forms.Button();
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
			this.BtGenerate.Location = new System.Drawing.Point(12, 54);
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
			// CkLocalDirectoryHtmlBitmaps
			// 
			this.CkLocalDirectoryHtmlBitmaps.AutoSize = true;
			this.CkLocalDirectoryHtmlBitmaps.Location = new System.Drawing.Point(167, 31);
			this.CkLocalDirectoryHtmlBitmaps.Name = "CkLocalDirectoryHtmlBitmaps";
			this.CkLocalDirectoryHtmlBitmaps.Size = new System.Drawing.Size(87, 17);
			this.CkLocalDirectoryHtmlBitmaps.TabIndex = 5;
			this.CkLocalDirectoryHtmlBitmaps.Text = "With bitmaps";
			this.CkLocalDirectoryHtmlBitmaps.UseVisualStyleBackColor = true;
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
			// FrmGenerateFiles
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 86);
			this.Controls.Add(this.BtLocalDirectoryReset);
			this.Controls.Add(this.CkLocalDirectoryHtmlBitmaps);
			this.Controls.Add(this.BtLocalDirectoryBrowse);
			this.Controls.Add(this.BtGenerate);
			this.Controls.Add(this.TbLocalDirectory);
			this.Controls.Add(this.CkLocalDirectory);
			this.Controls.Add(this.CkMizFile);
			this.Name = "FrmGenerateFiles";
			this.ShowInTaskbar = false;
			this.Text = "FrmGenerateFiles";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox CkMizFile;
		private System.Windows.Forms.CheckBox CkLocalDirectory;
		private System.Windows.Forms.TextBox TbLocalDirectory;
		private System.Windows.Forms.Button BtGenerate;
		private System.Windows.Forms.Button BtLocalDirectoryBrowse;
		private System.Windows.Forms.CheckBox CkLocalDirectoryHtmlBitmaps;
		private System.Windows.Forms.Button BtLocalDirectoryReset;
	}
}
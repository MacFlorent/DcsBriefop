namespace DcsBriefop.Forms
{
	partial class UcBriefingPartImage
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
			TbImageFullPath = new TextBox();
			BtImagePath = new Button();
			TbImagePath = new TextBox();
			LbImagePath = new Label();
			PbImage = new PictureBox();
			LbImageFullPath = new Label();
			TbHeader = new TextBox();
			LbHeader = new Label();
			((System.ComponentModel.ISupportInitialize)PbImage).BeginInit();
			SuspendLayout();
			// 
			// TbImageFullPath
			// 
			TbImageFullPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbImageFullPath.Location = new Point(66, 64);
			TbImageFullPath.Margin = new Padding(4, 3, 4, 3);
			TbImageFullPath.Name = "TbImageFullPath";
			TbImageFullPath.ReadOnly = true;
			TbImageFullPath.Size = new Size(908, 23);
			TbImageFullPath.TabIndex = 23;
			// 
			// BtImagePath
			// 
			BtImagePath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtImagePath.Location = new Point(982, 32);
			BtImagePath.Margin = new Padding(4, 3, 4, 3);
			BtImagePath.Name = "BtImagePath";
			BtImagePath.Size = new Size(40, 27);
			BtImagePath.TabIndex = 22;
			BtImagePath.Text = "...";
			BtImagePath.UseVisualStyleBackColor = true;
			BtImagePath.Click += BtImagePath_Click;
			// 
			// TbImagePath
			// 
			TbImagePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbImagePath.Location = new Point(66, 34);
			TbImagePath.Margin = new Padding(4, 3, 4, 3);
			TbImagePath.Name = "TbImagePath";
			TbImagePath.ReadOnly = true;
			TbImagePath.Size = new Size(908, 23);
			TbImagePath.TabIndex = 21;
			// 
			// LbImagePath
			// 
			LbImagePath.AutoSize = true;
			LbImagePath.Location = new Point(5, 38);
			LbImagePath.Margin = new Padding(4, 0, 4, 0);
			LbImagePath.Name = "LbImagePath";
			LbImagePath.Size = new Size(40, 15);
			LbImagePath.TabIndex = 20;
			LbImagePath.Text = "Image";
			// 
			// PbImage
			// 
			PbImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			PbImage.Location = new Point(66, 97);
			PbImage.Name = "PbImage";
			PbImage.Size = new Size(908, 307);
			PbImage.SizeMode = PictureBoxSizeMode.StretchImage;
			PbImage.TabIndex = 24;
			PbImage.TabStop = false;
			// 
			// LbImageFullPath
			// 
			LbImageFullPath.AutoSize = true;
			LbImageFullPath.Location = new Point(5, 67);
			LbImageFullPath.Margin = new Padding(4, 0, 4, 0);
			LbImageFullPath.Name = "LbImageFullPath";
			LbImageFullPath.Size = new Size(53, 15);
			LbImageFullPath.TabIndex = 25;
			LbImageFullPath.Text = "Full path";
			// 
			// TbHeader
			// 
			TbHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbHeader.Location = new Point(66, 5);
			TbHeader.Name = "TbHeader";
			TbHeader.Size = new Size(908, 23);
			TbHeader.TabIndex = 27;
			// 
			// LbHeader
			// 
			LbHeader.AutoSize = true;
			LbHeader.Location = new Point(5, 8);
			LbHeader.Name = "LbHeader";
			LbHeader.Size = new Size(45, 15);
			LbHeader.TabIndex = 26;
			LbHeader.Text = "Header";
			// 
			// UcBriefingPartImage
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(TbHeader);
			Controls.Add(LbHeader);
			Controls.Add(LbImageFullPath);
			Controls.Add(PbImage);
			Controls.Add(TbImageFullPath);
			Controls.Add(BtImagePath);
			Controls.Add(TbImagePath);
			Controls.Add(LbImagePath);
			Name = "UcBriefingPartImage";
			Size = new Size(1026, 407);
			((System.ComponentModel.ISupportInitialize)PbImage).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox TbImageFullPath;
		private Button BtImagePath;
		private TextBox TbImagePath;
		private Label LbImagePath;
		private PictureBox PbImage;
		private Label LbImageFullPath;
		private TextBox TbHeader;
		private Label LbHeader;
	}
}

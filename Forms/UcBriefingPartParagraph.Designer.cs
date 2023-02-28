namespace DcsBriefop.Forms
{
	partial class UcBriefingPartParagraph
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
			LbHeader = new Label();
			LbText = new Label();
			TbHeader = new TextBox();
			TbText = new TextBox();
			SuspendLayout();
			// 
			// LbHeader
			// 
			LbHeader.AutoSize = true;
			LbHeader.Location = new Point(3, 7);
			LbHeader.Name = "LbHeader";
			LbHeader.Size = new Size(45, 15);
			LbHeader.TabIndex = 0;
			LbHeader.Text = "Header";
			// 
			// LbText
			// 
			LbText.AutoSize = true;
			LbText.Location = new Point(3, 33);
			LbText.Name = "LbText";
			LbText.Size = new Size(28, 15);
			LbText.TabIndex = 1;
			LbText.Text = "Text";
			// 
			// TbHeader
			// 
			TbHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbHeader.Location = new Point(74, 4);
			TbHeader.Name = "TbHeader";
			TbHeader.Size = new Size(244, 23);
			TbHeader.TabIndex = 2;
			// 
			// TbText
			// 
			TbText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TbText.Location = new Point(74, 33);
			TbText.Multiline = true;
			TbText.Name = "TbText";
			TbText.ScrollBars = ScrollBars.Vertical;
			TbText.Size = new Size(244, 106);
			TbText.TabIndex = 3;
			// 
			// UcBriefingPartParagraph
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(TbText);
			Controls.Add(TbHeader);
			Controls.Add(LbText);
			Controls.Add(LbHeader);
			Name = "UcBriefingPartParagraph";
			Size = new Size(321, 142);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label LbHeader;
		private Label LbText;
		private TextBox TbHeader;
		private TextBox TbText;
	}
}

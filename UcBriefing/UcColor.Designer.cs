namespace DcsBriefop.UcBriefing
{
	partial class UcColor
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
			this.TbColor = new System.Windows.Forms.TextBox();
			this.BtColor = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TbColor
			// 
			this.TbColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbColor.Location = new System.Drawing.Point(0, 0);
			this.TbColor.Name = "TbColor";
			this.TbColor.Size = new System.Drawing.Size(166, 20);
			this.TbColor.TabIndex = 5;
			this.TbColor.TextChanged += new System.EventHandler(this.TbColor_TextChanged);
			// 
			// BtColor
			// 
			this.BtColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtColor.Location = new System.Drawing.Point(167, -1);
			this.BtColor.Name = "BtColor";
			this.BtColor.Size = new System.Drawing.Size(27, 22);
			this.BtColor.TabIndex = 6;
			this.BtColor.Text = "...";
			this.BtColor.UseVisualStyleBackColor = true;
			this.BtColor.Click += new System.EventHandler(this.BtColor_Click);
			// 
			// UcColor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TbColor);
			this.Controls.Add(this.BtColor);
			this.Name = "UcColor";
			this.Size = new System.Drawing.Size(194, 23);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TbColor;
		private System.Windows.Forms.Button BtColor;
	}
}

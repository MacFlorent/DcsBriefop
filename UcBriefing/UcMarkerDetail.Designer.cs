
namespace DcsBriefop.UcBriefing
{
	partial class UcMarkerDetail
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
			this.LbLabel = new System.Windows.Forms.Label();
			this.TbLabel = new System.Windows.Forms.TextBox();
			this.CbMarkerType = new System.Windows.Forms.ComboBox();
			this.LbType = new System.Windows.Forms.Label();
			this.BtColor = new System.Windows.Forms.Button();
			this.LbColor = new System.Windows.Forms.Label();
			this.TbColor = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// LbLabel
			// 
			this.LbLabel.AutoSize = true;
			this.LbLabel.Location = new System.Drawing.Point(45, 67);
			this.LbLabel.Name = "LbLabel";
			this.LbLabel.Size = new System.Drawing.Size(33, 13);
			this.LbLabel.TabIndex = 5;
			this.LbLabel.Text = "Label";
			// 
			// TbLabel
			// 
			this.TbLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbLabel.Location = new System.Drawing.Point(84, 64);
			this.TbLabel.Name = "TbLabel";
			this.TbLabel.Size = new System.Drawing.Size(142, 20);
			this.TbLabel.TabIndex = 6;
			this.TbLabel.Validated += new System.EventHandler(this.TbLabel_Validated);
			// 
			// CbMarkerType
			// 
			this.CbMarkerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CbMarkerType.FormattingEnabled = true;
			this.CbMarkerType.Location = new System.Drawing.Point(84, 11);
			this.CbMarkerType.Name = "CbMarkerType";
			this.CbMarkerType.Size = new System.Drawing.Size(142, 21);
			this.CbMarkerType.TabIndex = 1;
			this.CbMarkerType.Validated += new System.EventHandler(this.CbMarkerType_Validated);
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(13, 14);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(63, 13);
			this.LbType.TabIndex = 0;
			this.LbType.Text = "Marker type";
			// 
			// BtColor
			// 
			this.BtColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtColor.Location = new System.Drawing.Point(232, 36);
			this.BtColor.Name = "BtColor";
			this.BtColor.Size = new System.Drawing.Size(32, 23);
			this.BtColor.TabIndex = 4;
			this.BtColor.Text = "...";
			this.BtColor.UseVisualStyleBackColor = true;
			this.BtColor.Click += new System.EventHandler(this.BtColor_Click);
			// 
			// LbColor
			// 
			this.LbColor.AutoSize = true;
			this.LbColor.Location = new System.Drawing.Point(47, 41);
			this.LbColor.Name = "LbColor";
			this.LbColor.Size = new System.Drawing.Size(31, 13);
			this.LbColor.TabIndex = 2;
			this.LbColor.Text = "Color";
			// 
			// TbColor
			// 
			this.TbColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbColor.Location = new System.Drawing.Point(84, 38);
			this.TbColor.Name = "TbColor";
			this.TbColor.Size = new System.Drawing.Size(142, 20);
			this.TbColor.TabIndex = 3;
			this.TbColor.Validating += new System.ComponentModel.CancelEventHandler(this.TbColor_Validating);
			this.TbColor.Validated += new System.EventHandler(this.TbColor_Validated);
			// 
			// UcMarkerDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TbColor);
			this.Controls.Add(this.LbColor);
			this.Controls.Add(this.BtColor);
			this.Controls.Add(this.LbType);
			this.Controls.Add(this.CbMarkerType);
			this.Controls.Add(this.TbLabel);
			this.Controls.Add(this.LbLabel);
			this.Name = "UcMarkerDetail";
			this.Size = new System.Drawing.Size(267, 99);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LbLabel;
		private System.Windows.Forms.TextBox TbLabel;
		private System.Windows.Forms.ComboBox CbMarkerType;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.Button BtColor;
		private System.Windows.Forms.Label LbColor;
		private System.Windows.Forms.TextBox TbColor;
	}
}

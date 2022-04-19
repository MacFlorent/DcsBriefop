
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
			this.LbColor = new System.Windows.Forms.Label();
			this.LbScale = new System.Windows.Forms.Label();
			this.UdScale = new System.Windows.Forms.NumericUpDown();
			this.UdAngle = new System.Windows.Forms.NumericUpDown();
			this.LbAngle = new System.Windows.Forms.Label();
			this.UcTintColor = new DcsBriefop.UcBriefing.UcColor();
			((System.ComponentModel.ISupportInitialize)(this.UdScale)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UdAngle)).BeginInit();
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
			this.TbLabel.TextChanged += new System.EventHandler(this.TbLabel_TextChanged);
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
			this.CbMarkerType.SelectionChangeCommitted += new System.EventHandler(this.CbMarkerType_SelectionChangeCommitted);
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
			// LbColor
			// 
			this.LbColor.AutoSize = true;
			this.LbColor.Location = new System.Drawing.Point(47, 41);
			this.LbColor.Name = "LbColor";
			this.LbColor.Size = new System.Drawing.Size(31, 13);
			this.LbColor.TabIndex = 2;
			this.LbColor.Text = "Color";
			// 
			// LbScale
			// 
			this.LbScale.AutoSize = true;
			this.LbScale.Location = new System.Drawing.Point(43, 94);
			this.LbScale.Name = "LbScale";
			this.LbScale.Size = new System.Drawing.Size(34, 13);
			this.LbScale.TabIndex = 7;
			this.LbScale.Text = "Scale";
			// 
			// UdScale
			// 
			this.UdScale.Location = new System.Drawing.Point(84, 90);
			this.UdScale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.UdScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdScale.Name = "UdScale";
			this.UdScale.Size = new System.Drawing.Size(142, 20);
			this.UdScale.TabIndex = 8;
			this.UdScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdScale.ValueChanged += new System.EventHandler(this.UdScale_ValueChanged);
			// 
			// UdAngle
			// 
			this.UdAngle.Location = new System.Drawing.Point(84, 116);
			this.UdAngle.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
			this.UdAngle.Name = "UdAngle";
			this.UdAngle.Size = new System.Drawing.Size(142, 20);
			this.UdAngle.TabIndex = 10;
			this.UdAngle.ValueChanged += new System.EventHandler(this.UdAngle_ValueChanged);
			// 
			// LbAngle
			// 
			this.LbAngle.AutoSize = true;
			this.LbAngle.Location = new System.Drawing.Point(43, 120);
			this.LbAngle.Name = "LbAngle";
			this.LbAngle.Size = new System.Drawing.Size(34, 13);
			this.LbAngle.TabIndex = 9;
			this.LbAngle.Text = "Angle";
			// 
			// UcTintColor
			// 
			this.UcTintColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UcTintColor.Location = new System.Drawing.Point(84, 35);
			this.UcTintColor.Name = "UcTintColor";
			this.UcTintColor.SelectedColor = null;
			this.UcTintColor.Size = new System.Drawing.Size(180, 23);
			this.UcTintColor.TabIndex = 11;
			this.UcTintColor.ColorChanged += new System.EventHandler(this.UcTintColor_ColorChanged);
			// 
			// UcMarkerDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.UcTintColor);
			this.Controls.Add(this.UdAngle);
			this.Controls.Add(this.LbAngle);
			this.Controls.Add(this.UdScale);
			this.Controls.Add(this.LbScale);
			this.Controls.Add(this.LbColor);
			this.Controls.Add(this.LbType);
			this.Controls.Add(this.CbMarkerType);
			this.Controls.Add(this.TbLabel);
			this.Controls.Add(this.LbLabel);
			this.Name = "UcMarkerDetail";
			this.Size = new System.Drawing.Size(267, 143);
			((System.ComponentModel.ISupportInitialize)(this.UdScale)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UdAngle)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LbLabel;
		private System.Windows.Forms.TextBox TbLabel;
		private System.Windows.Forms.ComboBox CbMarkerType;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.Label LbColor;
		private System.Windows.Forms.Label LbScale;
		private System.Windows.Forms.NumericUpDown UdScale;
		private System.Windows.Forms.NumericUpDown UdAngle;
		private System.Windows.Forms.Label LbAngle;
		private UcColor UcTintColor;
	}
}

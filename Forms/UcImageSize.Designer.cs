namespace DcsBriefop.Forms
{
	partial class UcImageSize
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
			this.CkRatioLock = new System.Windows.Forms.CheckBox();
			this.UdRatio = new System.Windows.Forms.NumericUpDown();
			this.UdWidth = new System.Windows.Forms.NumericUpDown();
			this.UdHeight = new System.Windows.Forms.NumericUpDown();
			this.LbX = new System.Windows.Forms.Label();
			this.LbRatio = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.UdRatio)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UdWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UdHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// CkRatioLock
			// 
			this.CkRatioLock.AutoSize = true;
			this.CkRatioLock.Location = new System.Drawing.Point(202, 4);
			this.CkRatioLock.Name = "CkRatioLock";
			this.CkRatioLock.Size = new System.Drawing.Size(73, 17);
			this.CkRatioLock.TabIndex = 44;
			this.CkRatioLock.Text = "Lock ratio";
			this.CkRatioLock.UseVisualStyleBackColor = true;
			// 
			// UdRatio
			// 
			this.UdRatio.DecimalPlaces = 2;
			this.UdRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.UdRatio.Location = new System.Drawing.Point(144, 1);
			this.UdRatio.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.UdRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.UdRatio.Name = "UdRatio";
			this.UdRatio.Size = new System.Drawing.Size(51, 20);
			this.UdRatio.TabIndex = 43;
			this.UdRatio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdRatio.ValueChanged += new System.EventHandler(this.UdImageRatio_ValueChanged);
			// 
			// UdWidth
			// 
			this.UdWidth.Location = new System.Drawing.Point(1, 1);
			this.UdWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.UdWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdWidth.Name = "UdWidth";
			this.UdWidth.Size = new System.Drawing.Size(51, 20);
			this.UdWidth.TabIndex = 41;
			this.UdWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdWidth.ValueChanged += new System.EventHandler(this.UdImageWidth_ValueChanged);
			// 
			// UdHeight
			// 
			this.UdHeight.Location = new System.Drawing.Point(71, 1);
			this.UdHeight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.UdHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdHeight.Name = "UdHeight";
			this.UdHeight.Size = new System.Drawing.Size(51, 20);
			this.UdHeight.TabIndex = 42;
			this.UdHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UdHeight.ValueChanged += new System.EventHandler(this.UdImageHeight_ValueChanged);
			// 
			// LbX
			// 
			this.LbX.AutoSize = true;
			this.LbX.Location = new System.Drawing.Point(56, 3);
			this.LbX.Name = "LbX";
			this.LbX.Size = new System.Drawing.Size(12, 13);
			this.LbX.TabIndex = 40;
			this.LbX.Text = "x";
			// 
			// LbRatio
			// 
			this.LbRatio.AutoSize = true;
			this.LbRatio.Location = new System.Drawing.Point(128, 3);
			this.LbRatio.Name = "LbRatio";
			this.LbRatio.Size = new System.Drawing.Size(15, 13);
			this.LbRatio.TabIndex = 39;
			this.LbRatio.Text = "R";
			// 
			// UcImageSize
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CkRatioLock);
			this.Controls.Add(this.UdRatio);
			this.Controls.Add(this.UdWidth);
			this.Controls.Add(this.LbRatio);
			this.Controls.Add(this.UdHeight);
			this.Controls.Add(this.LbX);
			this.Name = "UcImageSize";
			this.Size = new System.Drawing.Size(280, 23);
			((System.ComponentModel.ISupportInitialize)(this.UdRatio)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UdWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UdHeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox CkRatioLock;
		private System.Windows.Forms.NumericUpDown UdRatio;
		private System.Windows.Forms.NumericUpDown UdWidth;
		private System.Windows.Forms.NumericUpDown UdHeight;
		private System.Windows.Forms.Label LbX;
		private System.Windows.Forms.Label LbRatio;
	}
}

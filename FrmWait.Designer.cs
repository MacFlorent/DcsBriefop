
namespace DcsBriefop
{
	partial class FrmWait
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
			this.PbWait = new System.Windows.Forms.PictureBox();
			this.LbElapsed = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.PbWait)).BeginInit();
			this.SuspendLayout();
			// 
			// PbWait
			// 
			this.PbWait.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PbWait.Location = new System.Drawing.Point(12, 12);
			this.PbWait.Name = "PbWait";
			this.PbWait.Size = new System.Drawing.Size(101, 86);
			this.PbWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.PbWait.TabIndex = 0;
			this.PbWait.TabStop = false;
			this.PbWait.UseWaitCursor = true;
			// 
			// LbElapsed
			// 
			this.LbElapsed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbElapsed.Location = new System.Drawing.Point(9, 101);
			this.LbElapsed.Name = "LbElapsed";
			this.LbElapsed.Size = new System.Drawing.Size(104, 19);
			this.LbElapsed.TabIndex = 1;
			this.LbElapsed.Text = "Elspased";
			this.LbElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.LbElapsed.UseWaitCursor = true;
			// 
			// FrmWait
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(125, 129);
			this.ControlBox = false;
			this.Controls.Add(this.LbElapsed);
			this.Controls.Add(this.PbWait);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmWait";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmWait2";
			this.UseWaitCursor = true;
			this.Load += new System.EventHandler(this.FrmWait_Load);
			((System.ComponentModel.ISupportInitialize)(this.PbWait)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox PbWait;
		private System.Windows.Forms.Label LbElapsed;
	}
}
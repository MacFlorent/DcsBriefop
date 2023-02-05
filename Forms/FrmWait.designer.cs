
namespace DcsBriefop.Forms
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
			this.PnMain = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.PbWait)).BeginInit();
			this.PnMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// PbWait
			// 
			this.PbWait.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PbWait.Location = new System.Drawing.Point(0, 0);
			this.PbWait.Name = "PbWait";
			this.PbWait.Size = new System.Drawing.Size(131, 128);
			this.PbWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.PbWait.TabIndex = 0;
			this.PbWait.TabStop = false;
			this.PbWait.UseWaitCursor = true;
			// 
			// LbElapsed
			// 
			this.LbElapsed.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LbElapsed.Location = new System.Drawing.Point(0, 128);
			this.LbElapsed.Name = "LbElapsed";
			this.LbElapsed.Size = new System.Drawing.Size(131, 19);
			this.LbElapsed.TabIndex = 1;
			this.LbElapsed.Text = "Elapsed";
			this.LbElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.LbElapsed.UseWaitCursor = true;
			// 
			// PnMain
			// 
			this.PnMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnMain.Controls.Add(this.PbWait);
			this.PnMain.Controls.Add(this.LbElapsed);
			this.PnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnMain.Location = new System.Drawing.Point(0, 0);
			this.PnMain.Name = "PnMain";
			this.PnMain.Size = new System.Drawing.Size(133, 149);
			this.PnMain.TabIndex = 2;
			this.PnMain.UseWaitCursor = true;
			// 
			// FrmWait
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(133, 149);
			this.ControlBox = false;
			this.Controls.Add(this.PnMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmWait";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmWait";
			this.UseWaitCursor = true;
			this.Load += new System.EventHandler(this.FrmWait_Load);
			((System.ComponentModel.ISupportInitialize)(this.PbWait)).EndInit();
			this.PnMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox PbWait;
		private System.Windows.Forms.Label LbElapsed;
        private System.Windows.Forms.Panel PnMain;
    }
}

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
			PbWait = new PictureBox();
			LbElapsed = new Label();
			PnMain = new Panel();
			((System.ComponentModel.ISupportInitialize)PbWait).BeginInit();
			PnMain.SuspendLayout();
			SuspendLayout();
			// 
			// PbWait
			// 
			PbWait.Dock = DockStyle.Fill;
			PbWait.Location = new Point(0, 0);
			PbWait.Margin = new Padding(4, 3, 4, 3);
			PbWait.Name = "PbWait";
			PbWait.Size = new Size(153, 148);
			PbWait.SizeMode = PictureBoxSizeMode.StretchImage;
			PbWait.TabIndex = 0;
			PbWait.TabStop = false;
			PbWait.UseWaitCursor = true;
			// 
			// LbElapsed
			// 
			LbElapsed.Dock = DockStyle.Bottom;
			LbElapsed.Location = new Point(0, 148);
			LbElapsed.Margin = new Padding(4, 0, 4, 0);
			LbElapsed.Name = "LbElapsed";
			LbElapsed.Size = new Size(153, 22);
			LbElapsed.TabIndex = 1;
			LbElapsed.Text = "Elapsed";
			LbElapsed.TextAlign = ContentAlignment.MiddleCenter;
			LbElapsed.UseWaitCursor = true;
			// 
			// PnMain
			// 
			PnMain.BorderStyle = BorderStyle.FixedSingle;
			PnMain.Controls.Add(PbWait);
			PnMain.Controls.Add(LbElapsed);
			PnMain.Dock = DockStyle.Fill;
			PnMain.Location = new Point(0, 0);
			PnMain.Margin = new Padding(4, 3, 4, 3);
			PnMain.Name = "PnMain";
			PnMain.Size = new Size(155, 172);
			PnMain.TabIndex = 2;
			PnMain.UseWaitCursor = true;
			// 
			// FrmWait
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(155, 172);
			ControlBox = false;
			Controls.Add(PnMain);
			FormBorderStyle = FormBorderStyle.None;
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmWait";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "FrmWait";
			UseWaitCursor = true;
			Load += FrmWait_Load;
			((System.ComponentModel.ISupportInitialize)PbWait).EndInit();
			PnMain.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.PictureBox PbWait;
		private System.Windows.Forms.Label LbElapsed;
		private System.Windows.Forms.Panel PnMain;
	}
}

namespace DcsBriefop.Forms
{
	partial class FrmMain
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
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.PnMain = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// StatusStrip
			// 
			this.StatusStrip.Location = new System.Drawing.Point(0, 801);
			this.StatusStrip.Name = "StatusStrip";
			this.StatusStrip.Size = new System.Drawing.Size(1691, 22);
			this.StatusStrip.TabIndex = 0;
			this.StatusStrip.Text = "statusStrip1";
			// 
			// MainMenu
			// 
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(1691, 24);
			this.MainMenu.TabIndex = 1;
			this.MainMenu.Text = "menuStrip1";
			// 
			// PnMain
			// 
			this.PnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnMain.Location = new System.Drawing.Point(0, 24);
			this.PnMain.Name = "PnMain";
			this.PnMain.Size = new System.Drawing.Size(1691, 777);
			this.PnMain.TabIndex = 2;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1691, 823);
			this.Controls.Add(this.PnMain);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.MainMenu);
			this.MainMenuStrip = this.MainMenu;
			this.Name = "FrmMain";
			this.Text = "Dcs:BriefOp";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.Panel PnMain;
	}
}



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
			StatusStrip = new StatusStrip();
			MainMenu = new MenuStrip();
			PnMain = new Panel();
			SuspendLayout();
			// 
			// StatusStrip
			// 
			StatusStrip.Location = new Point(0, 650);
			StatusStrip.Name = "StatusStrip";
			StatusStrip.Padding = new Padding(1, 0, 16, 0);
			StatusStrip.Size = new Size(1416, 22);
			StatusStrip.TabIndex = 0;
			StatusStrip.Text = "statusStrip1";
			// 
			// MainMenu
			// 
			MainMenu.Location = new Point(0, 0);
			MainMenu.Name = "MainMenu";
			MainMenu.Padding = new Padding(7, 2, 0, 2);
			MainMenu.Size = new Size(1416, 24);
			MainMenu.TabIndex = 1;
			MainMenu.Text = "menuStrip1";
			// 
			// PnMain
			// 
			PnMain.Dock = DockStyle.Fill;
			PnMain.Location = new Point(0, 24);
			PnMain.Margin = new Padding(4, 3, 4, 3);
			PnMain.Name = "PnMain";
			PnMain.Size = new Size(1416, 626);
			PnMain.TabIndex = 2;
			// 
			// FrmMain
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1416, 672);
			Controls.Add(PnMain);
			Controls.Add(StatusStrip);
			Controls.Add(MainMenu);
			MainMenuStrip = MainMenu;
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmMain";
			Text = "Dcs:BriefOp";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.Panel PnMain;
	}
}


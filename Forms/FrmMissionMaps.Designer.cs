namespace DcsBriefop.Forms
{
	partial class FrmMissionMaps
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
			PnTop = new Panel();
			LbMapProvider = new Label();
			CbMapProvider = new ComboBox();
			PnMapSelection = new Panel();
			LbMaps = new Label();
			RbMapSelectionNeutral = new RadioButton();
			RbMapSelectionGlobal = new RadioButton();
			RbMapSelectionRed = new RadioButton();
			RbMapSelectionBlue = new RadioButton();
			PnMap = new Panel();
			PnTop.SuspendLayout();
			PnMapSelection.SuspendLayout();
			SuspendLayout();
			// 
			// PnTop
			// 
			PnTop.Controls.Add(LbMapProvider);
			PnTop.Controls.Add(CbMapProvider);
			PnTop.Controls.Add(PnMapSelection);
			PnTop.Dock = DockStyle.Top;
			PnTop.Location = new Point(0, 0);
			PnTop.Margin = new Padding(4, 3, 4, 3);
			PnTop.Name = "PnTop";
			PnTop.Size = new Size(1637, 46);
			PnTop.TabIndex = 0;
			// 
			// LbMapProvider
			// 
			LbMapProvider.AutoSize = true;
			LbMapProvider.Location = new Point(15, 21);
			LbMapProvider.Margin = new Padding(4, 0, 4, 0);
			LbMapProvider.Name = "LbMapProvider";
			LbMapProvider.Size = new Size(78, 15);
			LbMapProvider.TabIndex = 6;
			LbMapProvider.Text = "Map provider";
			// 
			// CbMapProvider
			// 
			CbMapProvider.FormattingEnabled = true;
			CbMapProvider.Location = new Point(103, 15);
			CbMapProvider.Margin = new Padding(4, 3, 4, 3);
			CbMapProvider.Name = "CbMapProvider";
			CbMapProvider.Size = new Size(140, 23);
			CbMapProvider.TabIndex = 5;
			CbMapProvider.SelectedValueChanged += CbMapProvider_SelectedValueChanged;
			// 
			// PnMapSelection
			// 
			PnMapSelection.AutoSize = true;
			PnMapSelection.BackColor = Color.Transparent;
			PnMapSelection.Controls.Add(LbMaps);
			PnMapSelection.Controls.Add(RbMapSelectionNeutral);
			PnMapSelection.Controls.Add(RbMapSelectionGlobal);
			PnMapSelection.Controls.Add(RbMapSelectionRed);
			PnMapSelection.Controls.Add(RbMapSelectionBlue);
			PnMapSelection.Location = new Point(251, 8);
			PnMapSelection.Margin = new Padding(4, 3, 4, 3);
			PnMapSelection.Name = "PnMapSelection";
			PnMapSelection.Size = new Size(609, 38);
			PnMapSelection.TabIndex = 4;
			// 
			// LbMaps
			// 
			LbMaps.AutoSize = true;
			LbMaps.Location = new Point(43, 14);
			LbMaps.Margin = new Padding(4, 0, 4, 0);
			LbMaps.Name = "LbMaps";
			LbMaps.Size = new Size(36, 15);
			LbMaps.TabIndex = 7;
			LbMaps.Text = "Maps";
			// 
			// RbMapSelectionNeutral
			// 
			RbMapSelectionNeutral.Appearance = Appearance.Button;
			RbMapSelectionNeutral.Location = new Point(481, 7);
			RbMapSelectionNeutral.Margin = new Padding(4, 3, 4, 3);
			RbMapSelectionNeutral.Name = "RbMapSelectionNeutral";
			RbMapSelectionNeutral.Size = new Size(124, 27);
			RbMapSelectionNeutral.TabIndex = 1;
			RbMapSelectionNeutral.Text = "Neutral";
			RbMapSelectionNeutral.TextAlign = ContentAlignment.MiddleCenter;
			RbMapSelectionNeutral.UseVisualStyleBackColor = true;
			RbMapSelectionNeutral.CheckedChanged += RbMapSelection_CheckedChanged;
			// 
			// RbMapSelectionGlobal
			// 
			RbMapSelectionGlobal.Appearance = Appearance.Button;
			RbMapSelectionGlobal.Checked = true;
			RbMapSelectionGlobal.Location = new Point(89, 7);
			RbMapSelectionGlobal.Margin = new Padding(4, 3, 4, 3);
			RbMapSelectionGlobal.Name = "RbMapSelectionGlobal";
			RbMapSelectionGlobal.Size = new Size(124, 27);
			RbMapSelectionGlobal.TabIndex = 3;
			RbMapSelectionGlobal.TabStop = true;
			RbMapSelectionGlobal.Text = "Global";
			RbMapSelectionGlobal.TextAlign = ContentAlignment.MiddleCenter;
			RbMapSelectionGlobal.UseVisualStyleBackColor = true;
			RbMapSelectionGlobal.CheckedChanged += RbMapSelection_CheckedChanged;
			// 
			// RbMapSelectionRed
			// 
			RbMapSelectionRed.Appearance = Appearance.Button;
			RbMapSelectionRed.Location = new Point(219, 7);
			RbMapSelectionRed.Margin = new Padding(4, 3, 4, 3);
			RbMapSelectionRed.Name = "RbMapSelectionRed";
			RbMapSelectionRed.Size = new Size(124, 28);
			RbMapSelectionRed.TabIndex = 0;
			RbMapSelectionRed.Text = "Red";
			RbMapSelectionRed.TextAlign = ContentAlignment.MiddleCenter;
			RbMapSelectionRed.UseVisualStyleBackColor = true;
			RbMapSelectionRed.CheckedChanged += RbMapSelection_CheckedChanged;
			// 
			// RbMapSelectionBlue
			// 
			RbMapSelectionBlue.Appearance = Appearance.Button;
			RbMapSelectionBlue.Location = new Point(350, 7);
			RbMapSelectionBlue.Margin = new Padding(4, 3, 4, 3);
			RbMapSelectionBlue.Name = "RbMapSelectionBlue";
			RbMapSelectionBlue.Size = new Size(124, 27);
			RbMapSelectionBlue.TabIndex = 2;
			RbMapSelectionBlue.Text = "Blue";
			RbMapSelectionBlue.TextAlign = ContentAlignment.MiddleCenter;
			RbMapSelectionBlue.UseVisualStyleBackColor = true;
			RbMapSelectionBlue.CheckedChanged += RbMapSelection_CheckedChanged;
			// 
			// PnMap
			// 
			PnMap.Dock = DockStyle.Fill;
			PnMap.Location = new Point(0, 46);
			PnMap.Margin = new Padding(4, 3, 4, 3);
			PnMap.Name = "PnMap";
			PnMap.Size = new Size(1637, 689);
			PnMap.TabIndex = 1;
			// 
			// FrmMissionMaps
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1637, 735);
			Controls.Add(PnMap);
			Controls.Add(PnTop);
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmMissionMaps";
			ShowInTaskbar = false;
			Text = "Mission maps";
			FormClosed += FrmMissionMaps_FormClosed;
			Shown += FrmMissionMaps_Shown;
			PnTop.ResumeLayout(false);
			PnTop.PerformLayout();
			PnMapSelection.ResumeLayout(false);
			PnMapSelection.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel PnTop;
		private System.Windows.Forms.Panel PnMapSelection;
		private System.Windows.Forms.RadioButton RbMapSelectionNeutral;
		private System.Windows.Forms.RadioButton RbMapSelectionGlobal;
		private System.Windows.Forms.RadioButton RbMapSelectionRed;
		private System.Windows.Forms.RadioButton RbMapSelectionBlue;
		private System.Windows.Forms.Panel PnMap;
		private System.Windows.Forms.Label LbMapProvider;
		private System.Windows.Forms.ComboBox CbMapProvider;
		private System.Windows.Forms.Label LbMaps;
	}
}
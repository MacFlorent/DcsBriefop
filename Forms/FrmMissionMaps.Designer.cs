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
			this.PnTop = new System.Windows.Forms.Panel();
			this.LbMapProvider = new System.Windows.Forms.Label();
			this.CbMapProvider = new System.Windows.Forms.ComboBox();
			this.PnMapSelection = new System.Windows.Forms.Panel();
			this.LbMaps = new System.Windows.Forms.Label();
			this.RbMapSelectionNeutral = new System.Windows.Forms.RadioButton();
			this.RbMapSelectionGlobal = new System.Windows.Forms.RadioButton();
			this.RbMapSelectionRed = new System.Windows.Forms.RadioButton();
			this.RbMapSelectionBlue = new System.Windows.Forms.RadioButton();
			this.PnMap = new System.Windows.Forms.Panel();
			this.PnTop.SuspendLayout();
			this.PnMapSelection.SuspendLayout();
			this.SuspendLayout();
			// 
			// PnTop
			// 
			this.PnTop.Controls.Add(this.LbMapProvider);
			this.PnTop.Controls.Add(this.CbMapProvider);
			this.PnTop.Controls.Add(this.PnMapSelection);
			this.PnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.PnTop.Location = new System.Drawing.Point(0, 0);
			this.PnTop.Name = "PnTop";
			this.PnTop.Size = new System.Drawing.Size(1403, 40);
			this.PnTop.TabIndex = 0;
			// 
			// LbMapProvider
			// 
			this.LbMapProvider.AutoSize = true;
			this.LbMapProvider.Location = new System.Drawing.Point(13, 18);
			this.LbMapProvider.Name = "LbMapProvider";
			this.LbMapProvider.Size = new System.Drawing.Size(69, 13);
			this.LbMapProvider.TabIndex = 6;
			this.LbMapProvider.Text = "Map provider";
			// 
			// CbMapProvider
			// 
			this.CbMapProvider.FormattingEnabled = true;
			this.CbMapProvider.Location = new System.Drawing.Point(88, 13);
			this.CbMapProvider.Name = "CbMapProvider";
			this.CbMapProvider.Size = new System.Drawing.Size(121, 21);
			this.CbMapProvider.TabIndex = 5;
			this.CbMapProvider.SelectedValueChanged += new System.EventHandler(this.CbMapProvider_SelectedValueChanged);
			// 
			// PnMapSelection
			// 
			this.PnMapSelection.AutoSize = true;
			this.PnMapSelection.BackColor = System.Drawing.Color.Transparent;
			this.PnMapSelection.Controls.Add(this.LbMaps);
			this.PnMapSelection.Controls.Add(this.RbMapSelectionNeutral);
			this.PnMapSelection.Controls.Add(this.RbMapSelectionGlobal);
			this.PnMapSelection.Controls.Add(this.RbMapSelectionRed);
			this.PnMapSelection.Controls.Add(this.RbMapSelectionBlue);
			this.PnMapSelection.Location = new System.Drawing.Point(215, 7);
			this.PnMapSelection.Name = "PnMapSelection";
			this.PnMapSelection.Size = new System.Drawing.Size(521, 33);
			this.PnMapSelection.TabIndex = 4;
			// 
			// LbMaps
			// 
			this.LbMaps.AutoSize = true;
			this.LbMaps.Location = new System.Drawing.Point(37, 12);
			this.LbMaps.Name = "LbMaps";
			this.LbMaps.Size = new System.Drawing.Size(33, 13);
			this.LbMaps.TabIndex = 7;
			this.LbMaps.Text = "Maps";
			// 
			// RbMapSelectionNeutral
			// 
			this.RbMapSelectionNeutral.Appearance = System.Windows.Forms.Appearance.Button;
			this.RbMapSelectionNeutral.Location = new System.Drawing.Point(412, 6);
			this.RbMapSelectionNeutral.Name = "RbMapSelectionNeutral";
			this.RbMapSelectionNeutral.Size = new System.Drawing.Size(106, 23);
			this.RbMapSelectionNeutral.TabIndex = 1;
			this.RbMapSelectionNeutral.Text = "Neutral";
			this.RbMapSelectionNeutral.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.RbMapSelectionNeutral.UseVisualStyleBackColor = true;
			this.RbMapSelectionNeutral.CheckedChanged += new System.EventHandler(this.RbMapSelection_CheckedChanged);
			// 
			// RbMapSelectionGlobal
			// 
			this.RbMapSelectionGlobal.Appearance = System.Windows.Forms.Appearance.Button;
			this.RbMapSelectionGlobal.Checked = true;
			this.RbMapSelectionGlobal.Location = new System.Drawing.Point(76, 6);
			this.RbMapSelectionGlobal.Name = "RbMapSelectionGlobal";
			this.RbMapSelectionGlobal.Size = new System.Drawing.Size(106, 23);
			this.RbMapSelectionGlobal.TabIndex = 3;
			this.RbMapSelectionGlobal.TabStop = true;
			this.RbMapSelectionGlobal.Text = "Global";
			this.RbMapSelectionGlobal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.RbMapSelectionGlobal.UseVisualStyleBackColor = true;
			this.RbMapSelectionGlobal.CheckedChanged += new System.EventHandler(this.RbMapSelection_CheckedChanged);
			// 
			// RbMapSelectionRed
			// 
			this.RbMapSelectionRed.Appearance = System.Windows.Forms.Appearance.Button;
			this.RbMapSelectionRed.Location = new System.Drawing.Point(188, 6);
			this.RbMapSelectionRed.Name = "RbMapSelectionRed";
			this.RbMapSelectionRed.Size = new System.Drawing.Size(106, 24);
			this.RbMapSelectionRed.TabIndex = 0;
			this.RbMapSelectionRed.Text = "Red";
			this.RbMapSelectionRed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.RbMapSelectionRed.UseVisualStyleBackColor = true;
			this.RbMapSelectionRed.CheckedChanged += new System.EventHandler(this.RbMapSelection_CheckedChanged);
			// 
			// RbMapSelectionBlue
			// 
			this.RbMapSelectionBlue.Appearance = System.Windows.Forms.Appearance.Button;
			this.RbMapSelectionBlue.Location = new System.Drawing.Point(300, 6);
			this.RbMapSelectionBlue.Name = "RbMapSelectionBlue";
			this.RbMapSelectionBlue.Size = new System.Drawing.Size(106, 23);
			this.RbMapSelectionBlue.TabIndex = 2;
			this.RbMapSelectionBlue.Text = "Blue";
			this.RbMapSelectionBlue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.RbMapSelectionBlue.UseVisualStyleBackColor = true;
			this.RbMapSelectionBlue.CheckedChanged += new System.EventHandler(this.RbMapSelection_CheckedChanged);
			// 
			// PnMap
			// 
			this.PnMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnMap.Location = new System.Drawing.Point(0, 40);
			this.PnMap.Name = "PnMap";
			this.PnMap.Size = new System.Drawing.Size(1403, 597);
			this.PnMap.TabIndex = 1;
			// 
			// FrmMissionMaps
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1403, 637);
			this.Controls.Add(this.PnMap);
			this.Controls.Add(this.PnTop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmMissionMaps";
			this.ShowInTaskbar = false;
			this.Text = "Mission maps";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMissionMaps_FormClosed);
			this.PnTop.ResumeLayout(false);
			this.PnTop.PerformLayout();
			this.PnMapSelection.ResumeLayout(false);
			this.PnMapSelection.PerformLayout();
			this.ResumeLayout(false);

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
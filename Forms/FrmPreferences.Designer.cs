namespace DcsBriefop.Forms
{
	partial class FrmPreferences
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
			this.BtCancel = new System.Windows.Forms.Button();
			this.BtOk = new System.Windows.Forms.Button();
			this.LbTitleApplication = new System.Windows.Forms.Label();
			this.PnApplication = new System.Windows.Forms.Panel();
			this.CkApplicationGenerateBatch = new System.Windows.Forms.CheckBox();
			this.CkApplicationMizBackup = new System.Windows.Forms.CheckBox();
			this.BtApplicationRecentMiz = new System.Windows.Forms.Button();
			this.TbApplicationRecentMiz = new System.Windows.Forms.TextBox();
			this.LbApplicationRecentMiz = new System.Windows.Forms.Label();
			this.BtApplicationWorkingDirectoryReset = new System.Windows.Forms.Button();
			this.BtApplicationWorkingDirectorySelect = new System.Windows.Forms.Button();
			this.TbApplicationWorkingDirectory = new System.Windows.Forms.TextBox();
			this.LbApplicationWorkingDirectory = new System.Windows.Forms.Label();
			this.PnMission = new System.Windows.Forms.Panel();
			this.CbMissionWeatherDisplay = new System.Windows.Forms.ComboBox();
			this.LbCoodinateDisplay = new System.Windows.Forms.Label();
			this.LbMissionWeatherDisplay = new System.Windows.Forms.Label();
			this.LbTitleMission = new System.Windows.Forms.Label();
			this.PnMap = new System.Windows.Forms.Panel();
			this.CbMapProvider = new System.Windows.Forms.ComboBox();
			this.LbMapZoom = new System.Windows.Forms.Label();
			this.LbMapProvider = new System.Windows.Forms.Label();
			this.LbTitleMap = new System.Windows.Forms.Label();
			this.BtDefaults = new System.Windows.Forms.Button();
			this.NudMapZoom = new System.Windows.Forms.NumericUpDown();
			this.LstCoordinateDisplay = new System.Windows.Forms.CheckedListBox();
			this.PnApplication.SuspendLayout();
			this.PnMission.SuspendLayout();
			this.PnMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NudMapZoom)).BeginInit();
			this.SuspendLayout();
			// 
			// BtCancel
			// 
			this.BtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtCancel.Location = new System.Drawing.Point(405, 354);
			this.BtCancel.Name = "BtCancel";
			this.BtCancel.Size = new System.Drawing.Size(75, 23);
			this.BtCancel.TabIndex = 12;
			this.BtCancel.Text = "Cancel";
			this.BtCancel.UseVisualStyleBackColor = true;
			this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// BtOk
			// 
			this.BtOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtOk.Location = new System.Drawing.Point(324, 354);
			this.BtOk.Name = "BtOk";
			this.BtOk.Size = new System.Drawing.Size(75, 23);
			this.BtOk.TabIndex = 11;
			this.BtOk.Text = "OK";
			this.BtOk.UseVisualStyleBackColor = true;
			this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
			// 
			// LbTitleApplication
			// 
			this.LbTitleApplication.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTitleApplication.Location = new System.Drawing.Point(0, 0);
			this.LbTitleApplication.Name = "LbTitleApplication";
			this.LbTitleApplication.Size = new System.Drawing.Size(466, 28);
			this.LbTitleApplication.TabIndex = 13;
			this.LbTitleApplication.Text = "Application";
			this.LbTitleApplication.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PnApplication
			// 
			this.PnApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnApplication.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnApplication.Controls.Add(this.CkApplicationGenerateBatch);
			this.PnApplication.Controls.Add(this.CkApplicationMizBackup);
			this.PnApplication.Controls.Add(this.BtApplicationRecentMiz);
			this.PnApplication.Controls.Add(this.TbApplicationRecentMiz);
			this.PnApplication.Controls.Add(this.LbApplicationRecentMiz);
			this.PnApplication.Controls.Add(this.BtApplicationWorkingDirectoryReset);
			this.PnApplication.Controls.Add(this.BtApplicationWorkingDirectorySelect);
			this.PnApplication.Controls.Add(this.TbApplicationWorkingDirectory);
			this.PnApplication.Controls.Add(this.LbApplicationWorkingDirectory);
			this.PnApplication.Controls.Add(this.LbTitleApplication);
			this.PnApplication.Location = new System.Drawing.Point(12, 12);
			this.PnApplication.Name = "PnApplication";
			this.PnApplication.Size = new System.Drawing.Size(468, 117);
			this.PnApplication.TabIndex = 14;
			// 
			// CkApplicationGenerateBatch
			// 
			this.CkApplicationGenerateBatch.AutoSize = true;
			this.CkApplicationGenerateBatch.Location = new System.Drawing.Point(227, 87);
			this.CkApplicationGenerateBatch.Name = "CkApplicationGenerateBatch";
			this.CkApplicationGenerateBatch.Size = new System.Drawing.Size(190, 17);
			this.CkApplicationGenerateBatch.TabIndex = 22;
			this.CkApplicationGenerateBatch.Text = "Generate batch command on save";
			this.CkApplicationGenerateBatch.UseVisualStyleBackColor = true;
			// 
			// CkApplicationMizBackup
			// 
			this.CkApplicationMizBackup.AutoSize = true;
			this.CkApplicationMizBackup.Location = new System.Drawing.Point(36, 87);
			this.CkApplicationMizBackup.Name = "CkApplicationMizBackup";
			this.CkApplicationMizBackup.Size = new System.Drawing.Size(169, 17);
			this.CkApplicationMizBackup.TabIndex = 21;
			this.CkApplicationMizBackup.Text = "Backup Miz before overwriting";
			this.CkApplicationMizBackup.UseVisualStyleBackColor = true;
			// 
			// BtApplicationRecentMiz
			// 
			this.BtApplicationRecentMiz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtApplicationRecentMiz.Location = new System.Drawing.Point(384, 53);
			this.BtApplicationRecentMiz.Name = "BtApplicationRecentMiz";
			this.BtApplicationRecentMiz.Size = new System.Drawing.Size(74, 23);
			this.BtApplicationRecentMiz.TabIndex = 20;
			this.BtApplicationRecentMiz.Text = "Clear";
			this.BtApplicationRecentMiz.UseVisualStyleBackColor = true;
			this.BtApplicationRecentMiz.Click += new System.EventHandler(this.BtApplicationRecentMiz_Click);
			// 
			// TbApplicationRecentMiz
			// 
			this.TbApplicationRecentMiz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbApplicationRecentMiz.Location = new System.Drawing.Point(112, 55);
			this.TbApplicationRecentMiz.Name = "TbApplicationRecentMiz";
			this.TbApplicationRecentMiz.ReadOnly = true;
			this.TbApplicationRecentMiz.Size = new System.Drawing.Size(266, 20);
			this.TbApplicationRecentMiz.TabIndex = 19;
			// 
			// LbApplicationRecentMiz
			// 
			this.LbApplicationRecentMiz.AutoSize = true;
			this.LbApplicationRecentMiz.Location = new System.Drawing.Point(4, 58);
			this.LbApplicationRecentMiz.Name = "LbApplicationRecentMiz";
			this.LbApplicationRecentMiz.Size = new System.Drawing.Size(61, 13);
			this.LbApplicationRecentMiz.TabIndex = 18;
			this.LbApplicationRecentMiz.Text = "Recent Miz";
			// 
			// BtApplicationWorkingDirectoryReset
			// 
			this.BtApplicationWorkingDirectoryReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtApplicationWorkingDirectoryReset.Location = new System.Drawing.Point(424, 27);
			this.BtApplicationWorkingDirectoryReset.Name = "BtApplicationWorkingDirectoryReset";
			this.BtApplicationWorkingDirectoryReset.Size = new System.Drawing.Size(34, 23);
			this.BtApplicationWorkingDirectoryReset.TabIndex = 17;
			this.BtApplicationWorkingDirectoryReset.Text = "R";
			this.BtApplicationWorkingDirectoryReset.UseVisualStyleBackColor = true;
			this.BtApplicationWorkingDirectoryReset.Click += new System.EventHandler(this.BtApplicationWorkingDirectoryReset_Click);
			// 
			// BtApplicationWorkingDirectorySelect
			// 
			this.BtApplicationWorkingDirectorySelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtApplicationWorkingDirectorySelect.Location = new System.Drawing.Point(384, 27);
			this.BtApplicationWorkingDirectorySelect.Name = "BtApplicationWorkingDirectorySelect";
			this.BtApplicationWorkingDirectorySelect.Size = new System.Drawing.Size(34, 23);
			this.BtApplicationWorkingDirectorySelect.TabIndex = 16;
			this.BtApplicationWorkingDirectorySelect.Text = "...";
			this.BtApplicationWorkingDirectorySelect.UseVisualStyleBackColor = true;
			this.BtApplicationWorkingDirectorySelect.Click += new System.EventHandler(this.BtApplicationWorkingDirectorySelect_Click);
			// 
			// TbApplicationWorkingDirectory
			// 
			this.TbApplicationWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbApplicationWorkingDirectory.Location = new System.Drawing.Point(112, 29);
			this.TbApplicationWorkingDirectory.Name = "TbApplicationWorkingDirectory";
			this.TbApplicationWorkingDirectory.ReadOnly = true;
			this.TbApplicationWorkingDirectory.Size = new System.Drawing.Size(266, 20);
			this.TbApplicationWorkingDirectory.TabIndex = 15;
			// 
			// LbApplicationWorkingDirectory
			// 
			this.LbApplicationWorkingDirectory.AutoSize = true;
			this.LbApplicationWorkingDirectory.Location = new System.Drawing.Point(4, 32);
			this.LbApplicationWorkingDirectory.Name = "LbApplicationWorkingDirectory";
			this.LbApplicationWorkingDirectory.Size = new System.Drawing.Size(90, 13);
			this.LbApplicationWorkingDirectory.TabIndex = 14;
			this.LbApplicationWorkingDirectory.Text = "Working directory";
			// 
			// PnMission
			// 
			this.PnMission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnMission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnMission.Controls.Add(this.LstCoordinateDisplay);
			this.PnMission.Controls.Add(this.CbMissionWeatherDisplay);
			this.PnMission.Controls.Add(this.LbCoodinateDisplay);
			this.PnMission.Controls.Add(this.LbMissionWeatherDisplay);
			this.PnMission.Controls.Add(this.LbTitleMission);
			this.PnMission.Location = new System.Drawing.Point(12, 135);
			this.PnMission.Name = "PnMission";
			this.PnMission.Size = new System.Drawing.Size(468, 121);
			this.PnMission.TabIndex = 15;
			// 
			// CbMissionWeatherDisplay
			// 
			this.CbMissionWeatherDisplay.FormattingEnabled = true;
			this.CbMissionWeatherDisplay.Location = new System.Drawing.Point(112, 28);
			this.CbMissionWeatherDisplay.Name = "CbMissionWeatherDisplay";
			this.CbMissionWeatherDisplay.Size = new System.Drawing.Size(268, 21);
			this.CbMissionWeatherDisplay.TabIndex = 23;
			// 
			// LbCoodinateDisplay
			// 
			this.LbCoodinateDisplay.AutoSize = true;
			this.LbCoodinateDisplay.Location = new System.Drawing.Point(4, 58);
			this.LbCoodinateDisplay.Name = "LbCoodinateDisplay";
			this.LbCoodinateDisplay.Size = new System.Drawing.Size(93, 13);
			this.LbCoodinateDisplay.TabIndex = 18;
			this.LbCoodinateDisplay.Text = "Coordinate display";
			// 
			// LbMissionWeatherDisplay
			// 
			this.LbMissionWeatherDisplay.AutoSize = true;
			this.LbMissionWeatherDisplay.Location = new System.Drawing.Point(4, 31);
			this.LbMissionWeatherDisplay.Name = "LbMissionWeatherDisplay";
			this.LbMissionWeatherDisplay.Size = new System.Drawing.Size(83, 13);
			this.LbMissionWeatherDisplay.TabIndex = 14;
			this.LbMissionWeatherDisplay.Text = "Weather display";
			// 
			// LbTitleMission
			// 
			this.LbTitleMission.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTitleMission.Location = new System.Drawing.Point(0, 0);
			this.LbTitleMission.Name = "LbTitleMission";
			this.LbTitleMission.Size = new System.Drawing.Size(466, 28);
			this.LbTitleMission.TabIndex = 13;
			this.LbTitleMission.Text = "Mission";
			this.LbTitleMission.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PnMap
			// 
			this.PnMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnMap.Controls.Add(this.NudMapZoom);
			this.PnMap.Controls.Add(this.CbMapProvider);
			this.PnMap.Controls.Add(this.LbMapZoom);
			this.PnMap.Controls.Add(this.LbMapProvider);
			this.PnMap.Controls.Add(this.LbTitleMap);
			this.PnMap.Location = new System.Drawing.Point(11, 262);
			this.PnMap.Name = "PnMap";
			this.PnMap.Size = new System.Drawing.Size(468, 86);
			this.PnMap.TabIndex = 16;
			// 
			// CbMapProvider
			// 
			this.CbMapProvider.FormattingEnabled = true;
			this.CbMapProvider.Location = new System.Drawing.Point(112, 28);
			this.CbMapProvider.Name = "CbMapProvider";
			this.CbMapProvider.Size = new System.Drawing.Size(268, 21);
			this.CbMapProvider.TabIndex = 23;
			// 
			// LbMapZoom
			// 
			this.LbMapZoom.AutoSize = true;
			this.LbMapZoom.Location = new System.Drawing.Point(4, 58);
			this.LbMapZoom.Name = "LbMapZoom";
			this.LbMapZoom.Size = new System.Drawing.Size(69, 13);
			this.LbMapZoom.TabIndex = 18;
			this.LbMapZoom.Text = "Default zoom";
			// 
			// LbMapProvider
			// 
			this.LbMapProvider.AutoSize = true;
			this.LbMapProvider.Location = new System.Drawing.Point(4, 31);
			this.LbMapProvider.Name = "LbMapProvider";
			this.LbMapProvider.Size = new System.Drawing.Size(82, 13);
			this.LbMapProvider.TabIndex = 14;
			this.LbMapProvider.Text = "Default provider";
			// 
			// LbTitleMap
			// 
			this.LbTitleMap.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTitleMap.Location = new System.Drawing.Point(0, 0);
			this.LbTitleMap.Name = "LbTitleMap";
			this.LbTitleMap.Size = new System.Drawing.Size(466, 28);
			this.LbTitleMap.TabIndex = 13;
			this.LbTitleMap.Text = "Map";
			this.LbTitleMap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BtDefaults
			// 
			this.BtDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtDefaults.Location = new System.Drawing.Point(12, 354);
			this.BtDefaults.Name = "BtDefaults";
			this.BtDefaults.Size = new System.Drawing.Size(106, 23);
			this.BtDefaults.TabIndex = 17;
			this.BtDefaults.Text = "Reset defaults";
			this.BtDefaults.UseVisualStyleBackColor = true;
			this.BtDefaults.Click += new System.EventHandler(this.BtDefaults_Click);
			// 
			// NudMapZoom
			// 
			this.NudMapZoom.Location = new System.Drawing.Point(112, 56);
			this.NudMapZoom.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.NudMapZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.NudMapZoom.Name = "NudMapZoom";
			this.NudMapZoom.Size = new System.Drawing.Size(73, 20);
			this.NudMapZoom.TabIndex = 24;
			this.NudMapZoom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// LstCoordinateDisplay
			// 
			this.LstCoordinateDisplay.FormattingEnabled = true;
			this.LstCoordinateDisplay.Location = new System.Drawing.Point(112, 57);
			this.LstCoordinateDisplay.Name = "LstCoordinateDisplay";
			this.LstCoordinateDisplay.Size = new System.Drawing.Size(268, 49);
			this.LstCoordinateDisplay.TabIndex = 24;
			// 
			// FrmPreferences
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 384);
			this.Controls.Add(this.BtDefaults);
			this.Controls.Add(this.PnMap);
			this.Controls.Add(this.PnMission);
			this.Controls.Add(this.PnApplication);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.BtOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmPreferences";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Preferences";
			this.PnApplication.ResumeLayout(false);
			this.PnApplication.PerformLayout();
			this.PnMission.ResumeLayout(false);
			this.PnMission.PerformLayout();
			this.PnMap.ResumeLayout(false);
			this.PnMap.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NudMapZoom)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.Button BtOk;
		private System.Windows.Forms.Label LbTitleApplication;
		private System.Windows.Forms.Panel PnApplication;
		private System.Windows.Forms.Button BtApplicationWorkingDirectorySelect;
		private System.Windows.Forms.TextBox TbApplicationWorkingDirectory;
		private System.Windows.Forms.Label LbApplicationWorkingDirectory;
		private System.Windows.Forms.Button BtApplicationRecentMiz;
		private System.Windows.Forms.TextBox TbApplicationRecentMiz;
		private System.Windows.Forms.Label LbApplicationRecentMiz;
		private System.Windows.Forms.CheckBox CkApplicationGenerateBatch;
		private System.Windows.Forms.CheckBox CkApplicationMizBackup;
		private System.Windows.Forms.Panel PnMission;
		private System.Windows.Forms.Label LbCoodinateDisplay;
		private System.Windows.Forms.Label LbMissionWeatherDisplay;
		private System.Windows.Forms.Label LbTitleMission;
		private System.Windows.Forms.ComboBox CbMissionWeatherDisplay;
		private System.Windows.Forms.Panel PnMap;
		private System.Windows.Forms.ComboBox CbMapProvider;
		private System.Windows.Forms.Label LbMapZoom;
		private System.Windows.Forms.Label LbMapProvider;
		private System.Windows.Forms.Label LbTitleMap;
		private System.Windows.Forms.Button BtDefaults;
		private System.Windows.Forms.Button BtApplicationWorkingDirectoryReset;
		private System.Windows.Forms.NumericUpDown NudMapZoom;
		private System.Windows.Forms.CheckedListBox LstCoordinateDisplay;
	}
}
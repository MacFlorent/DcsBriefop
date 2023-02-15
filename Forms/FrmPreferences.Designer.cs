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
			this.CkMissionBullseyeWaypoint = new System.Windows.Forms.CheckBox();
			this.CkMissionNoCallsignForPlayable = new System.Windows.Forms.CheckBox();
			this.LbTitleMission = new System.Windows.Forms.Label();
			this.PnMap = new System.Windows.Forms.Panel();
			this.NudMapZoom = new System.Windows.Forms.NumericUpDown();
			this.CbMapProvider = new System.Windows.Forms.ComboBox();
			this.LbMapZoom = new System.Windows.Forms.Label();
			this.LbMapProvider = new System.Windows.Forms.Label();
			this.LbTitleMap = new System.Windows.Forms.Label();
			this.BtDefaults = new System.Windows.Forms.Button();
			this.PnBriefing = new System.Windows.Forms.Panel();
			this.TbBriefingDirectoryName = new System.Windows.Forms.TextBox();
			this.LbBriefingDirectoryName = new System.Windows.Forms.Label();
			this.CkBriefingGenerateDirectoryHtml = new System.Windows.Forms.CheckBox();
			this.CkBriefingGenerateDirectory = new System.Windows.Forms.CheckBox();
			this.CkBriefingGenerateMiz = new System.Windows.Forms.CheckBox();
			this.LbBriefingImageSize = new System.Windows.Forms.Label();
			this.UcBriefingImageSize = new DcsBriefop.Forms.UcImageSize();
			this.CkBriefingGenerateOnSave = new System.Windows.Forms.CheckBox();
			this.CbBriefingMeasurementSystem = new System.Windows.Forms.ComboBox();
			this.LbBriefingMeasurementSystem = new System.Windows.Forms.Label();
			this.LstBriefingCoordinateDisplay = new System.Windows.Forms.CheckedListBox();
			this.CbBriefingWeatherDisplay = new System.Windows.Forms.ComboBox();
			this.LbBriefingCoordinateDisplay = new System.Windows.Forms.Label();
			this.LbBriefingWeatherDisplay = new System.Windows.Forms.Label();
			this.LbTitleBriefing = new System.Windows.Forms.Label();
			this.PnApplication.SuspendLayout();
			this.PnMission.SuspendLayout();
			this.PnMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NudMapZoom)).BeginInit();
			this.PnBriefing.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtCancel
			// 
			this.BtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtCancel.Location = new System.Drawing.Point(472, 638);
			this.BtCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BtCancel.Name = "BtCancel";
			this.BtCancel.Size = new System.Drawing.Size(88, 27);
			this.BtCancel.TabIndex = 12;
			this.BtCancel.Text = "Cancel";
			this.BtCancel.UseVisualStyleBackColor = true;
			this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// BtOk
			// 
			this.BtOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtOk.Location = new System.Drawing.Point(378, 638);
			this.BtOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BtOk.Name = "BtOk";
			this.BtOk.Size = new System.Drawing.Size(88, 27);
			this.BtOk.TabIndex = 11;
			this.BtOk.Text = "OK";
			this.BtOk.UseVisualStyleBackColor = true;
			this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
			// 
			// LbTitleApplication
			// 
			this.LbTitleApplication.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTitleApplication.Location = new System.Drawing.Point(0, 0);
			this.LbTitleApplication.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbTitleApplication.Name = "LbTitleApplication";
			this.LbTitleApplication.Size = new System.Drawing.Size(544, 32);
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
			this.PnApplication.Location = new System.Drawing.Point(14, 14);
			this.PnApplication.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PnApplication.Name = "PnApplication";
			this.PnApplication.Size = new System.Drawing.Size(546, 135);
			this.PnApplication.TabIndex = 14;
			// 
			// CkApplicationGenerateBatch
			// 
			this.CkApplicationGenerateBatch.AutoSize = true;
			this.CkApplicationGenerateBatch.Location = new System.Drawing.Point(259, 100);
			this.CkApplicationGenerateBatch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkApplicationGenerateBatch.Name = "CkApplicationGenerateBatch";
			this.CkApplicationGenerateBatch.Size = new System.Drawing.Size(207, 19);
			this.CkApplicationGenerateBatch.TabIndex = 22;
			this.CkApplicationGenerateBatch.Text = "Generate batch command on save";
			this.CkApplicationGenerateBatch.UseVisualStyleBackColor = true;
			// 
			// CkApplicationMizBackup
			// 
			this.CkApplicationMizBackup.AutoSize = true;
			this.CkApplicationMizBackup.Location = new System.Drawing.Point(42, 100);
			this.CkApplicationMizBackup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkApplicationMizBackup.Name = "CkApplicationMizBackup";
			this.CkApplicationMizBackup.Size = new System.Drawing.Size(187, 19);
			this.CkApplicationMizBackup.TabIndex = 21;
			this.CkApplicationMizBackup.Text = "Backup Miz before overwriting";
			this.CkApplicationMizBackup.UseVisualStyleBackColor = true;
			// 
			// BtApplicationRecentMiz
			// 
			this.BtApplicationRecentMiz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtApplicationRecentMiz.Location = new System.Drawing.Point(448, 61);
			this.BtApplicationRecentMiz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BtApplicationRecentMiz.Name = "BtApplicationRecentMiz";
			this.BtApplicationRecentMiz.Size = new System.Drawing.Size(86, 27);
			this.BtApplicationRecentMiz.TabIndex = 20;
			this.BtApplicationRecentMiz.Text = "Clear";
			this.BtApplicationRecentMiz.UseVisualStyleBackColor = true;
			this.BtApplicationRecentMiz.Click += new System.EventHandler(this.BtApplicationRecentMiz_Click);
			// 
			// TbApplicationRecentMiz
			// 
			this.TbApplicationRecentMiz.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbApplicationRecentMiz.Location = new System.Drawing.Point(131, 63);
			this.TbApplicationRecentMiz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbApplicationRecentMiz.Name = "TbApplicationRecentMiz";
			this.TbApplicationRecentMiz.ReadOnly = true;
			this.TbApplicationRecentMiz.Size = new System.Drawing.Size(310, 23);
			this.TbApplicationRecentMiz.TabIndex = 19;
			// 
			// LbApplicationRecentMiz
			// 
			this.LbApplicationRecentMiz.AutoSize = true;
			this.LbApplicationRecentMiz.Location = new System.Drawing.Point(5, 67);
			this.LbApplicationRecentMiz.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbApplicationRecentMiz.Name = "LbApplicationRecentMiz";
			this.LbApplicationRecentMiz.Size = new System.Drawing.Size(65, 15);
			this.LbApplicationRecentMiz.TabIndex = 18;
			this.LbApplicationRecentMiz.Text = "Recent Miz";
			// 
			// BtApplicationWorkingDirectoryReset
			// 
			this.BtApplicationWorkingDirectoryReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtApplicationWorkingDirectoryReset.Location = new System.Drawing.Point(495, 31);
			this.BtApplicationWorkingDirectoryReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BtApplicationWorkingDirectoryReset.Name = "BtApplicationWorkingDirectoryReset";
			this.BtApplicationWorkingDirectoryReset.Size = new System.Drawing.Size(40, 27);
			this.BtApplicationWorkingDirectoryReset.TabIndex = 17;
			this.BtApplicationWorkingDirectoryReset.Text = "R";
			this.BtApplicationWorkingDirectoryReset.UseVisualStyleBackColor = true;
			this.BtApplicationWorkingDirectoryReset.Click += new System.EventHandler(this.BtApplicationWorkingDirectoryReset_Click);
			// 
			// BtApplicationWorkingDirectorySelect
			// 
			this.BtApplicationWorkingDirectorySelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtApplicationWorkingDirectorySelect.Location = new System.Drawing.Point(448, 31);
			this.BtApplicationWorkingDirectorySelect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BtApplicationWorkingDirectorySelect.Name = "BtApplicationWorkingDirectorySelect";
			this.BtApplicationWorkingDirectorySelect.Size = new System.Drawing.Size(40, 27);
			this.BtApplicationWorkingDirectorySelect.TabIndex = 16;
			this.BtApplicationWorkingDirectorySelect.Text = "...";
			this.BtApplicationWorkingDirectorySelect.UseVisualStyleBackColor = true;
			this.BtApplicationWorkingDirectorySelect.Click += new System.EventHandler(this.BtApplicationWorkingDirectorySelect_Click);
			// 
			// TbApplicationWorkingDirectory
			// 
			this.TbApplicationWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbApplicationWorkingDirectory.Location = new System.Drawing.Point(131, 33);
			this.TbApplicationWorkingDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbApplicationWorkingDirectory.Name = "TbApplicationWorkingDirectory";
			this.TbApplicationWorkingDirectory.ReadOnly = true;
			this.TbApplicationWorkingDirectory.Size = new System.Drawing.Size(310, 23);
			this.TbApplicationWorkingDirectory.TabIndex = 15;
			// 
			// LbApplicationWorkingDirectory
			// 
			this.LbApplicationWorkingDirectory.AutoSize = true;
			this.LbApplicationWorkingDirectory.Location = new System.Drawing.Point(5, 37);
			this.LbApplicationWorkingDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbApplicationWorkingDirectory.Name = "LbApplicationWorkingDirectory";
			this.LbApplicationWorkingDirectory.Size = new System.Drawing.Size(102, 15);
			this.LbApplicationWorkingDirectory.TabIndex = 14;
			this.LbApplicationWorkingDirectory.Text = "Working directory";
			// 
			// PnMission
			// 
			this.PnMission.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnMission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnMission.Controls.Add(this.CkMissionBullseyeWaypoint);
			this.PnMission.Controls.Add(this.CkMissionNoCallsignForPlayable);
			this.PnMission.Controls.Add(this.LbTitleMission);
			this.PnMission.Location = new System.Drawing.Point(14, 156);
			this.PnMission.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PnMission.Name = "PnMission";
			this.PnMission.Size = new System.Drawing.Size(546, 65);
			this.PnMission.TabIndex = 15;
			// 
			// CkMissionBullseyeWaypoint
			// 
			this.CkMissionBullseyeWaypoint.AutoSize = true;
			this.CkMissionBullseyeWaypoint.Location = new System.Drawing.Point(259, 35);
			this.CkMissionBullseyeWaypoint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkMissionBullseyeWaypoint.Name = "CkMissionBullseyeWaypoint";
			this.CkMissionBullseyeWaypoint.Size = new System.Drawing.Size(281, 19);
			this.CkMissionBullseyeWaypoint.TabIndex = 26;
			this.CkMissionBullseyeWaypoint.Text = "Add Bulls as first waypoint for all playable flights";
			this.CkMissionBullseyeWaypoint.UseVisualStyleBackColor = true;
			// 
			// CkMissionNoCallsignForPlayable
			// 
			this.CkMissionNoCallsignForPlayable.AutoSize = true;
			this.CkMissionNoCallsignForPlayable.Location = new System.Drawing.Point(42, 35);
			this.CkMissionNoCallsignForPlayable.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkMissionNoCallsignForPlayable.Name = "CkMissionNoCallsignForPlayable";
			this.CkMissionNoCallsignForPlayable.Size = new System.Drawing.Size(186, 19);
			this.CkMissionNoCallsignForPlayable.TabIndex = 25;
			this.CkMissionNoCallsignForPlayable.Text = "No callsign for playable flights";
			this.CkMissionNoCallsignForPlayable.UseVisualStyleBackColor = true;
			// 
			// LbTitleMission
			// 
			this.LbTitleMission.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTitleMission.Location = new System.Drawing.Point(0, 0);
			this.LbTitleMission.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbTitleMission.Name = "LbTitleMission";
			this.LbTitleMission.Size = new System.Drawing.Size(544, 32);
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
			this.PnMap.Location = new System.Drawing.Point(14, 227);
			this.PnMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PnMap.Name = "PnMap";
			this.PnMap.Size = new System.Drawing.Size(546, 99);
			this.PnMap.TabIndex = 16;
			// 
			// NudMapZoom
			// 
			this.NudMapZoom.Location = new System.Drawing.Point(131, 65);
			this.NudMapZoom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
			this.NudMapZoom.Size = new System.Drawing.Size(85, 23);
			this.NudMapZoom.TabIndex = 24;
			this.NudMapZoom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// CbMapProvider
			// 
			this.CbMapProvider.FormattingEnabled = true;
			this.CbMapProvider.Location = new System.Drawing.Point(131, 32);
			this.CbMapProvider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CbMapProvider.Name = "CbMapProvider";
			this.CbMapProvider.Size = new System.Drawing.Size(310, 23);
			this.CbMapProvider.TabIndex = 23;
			// 
			// LbMapZoom
			// 
			this.LbMapZoom.AutoSize = true;
			this.LbMapZoom.Location = new System.Drawing.Point(5, 67);
			this.LbMapZoom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbMapZoom.Name = "LbMapZoom";
			this.LbMapZoom.Size = new System.Drawing.Size(78, 15);
			this.LbMapZoom.TabIndex = 18;
			this.LbMapZoom.Text = "Default zoom";
			// 
			// LbMapProvider
			// 
			this.LbMapProvider.AutoSize = true;
			this.LbMapProvider.Location = new System.Drawing.Point(5, 36);
			this.LbMapProvider.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbMapProvider.Name = "LbMapProvider";
			this.LbMapProvider.Size = new System.Drawing.Size(92, 15);
			this.LbMapProvider.TabIndex = 14;
			this.LbMapProvider.Text = "Default provider";
			// 
			// LbTitleMap
			// 
			this.LbTitleMap.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTitleMap.Location = new System.Drawing.Point(0, 0);
			this.LbTitleMap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbTitleMap.Name = "LbTitleMap";
			this.LbTitleMap.Size = new System.Drawing.Size(544, 32);
			this.LbTitleMap.TabIndex = 13;
			this.LbTitleMap.Text = "Map";
			this.LbTitleMap.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// BtDefaults
			// 
			this.BtDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtDefaults.Location = new System.Drawing.Point(14, 638);
			this.BtDefaults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BtDefaults.Name = "BtDefaults";
			this.BtDefaults.Size = new System.Drawing.Size(124, 27);
			this.BtDefaults.TabIndex = 17;
			this.BtDefaults.Text = "Reset defaults";
			this.BtDefaults.UseVisualStyleBackColor = true;
			this.BtDefaults.Click += new System.EventHandler(this.BtDefaults_Click);
			// 
			// PnBriefing
			// 
			this.PnBriefing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnBriefing.Controls.Add(this.TbBriefingDirectoryName);
			this.PnBriefing.Controls.Add(this.LbBriefingDirectoryName);
			this.PnBriefing.Controls.Add(this.CkBriefingGenerateDirectoryHtml);
			this.PnBriefing.Controls.Add(this.CkBriefingGenerateDirectory);
			this.PnBriefing.Controls.Add(this.CkBriefingGenerateMiz);
			this.PnBriefing.Controls.Add(this.LbBriefingImageSize);
			this.PnBriefing.Controls.Add(this.UcBriefingImageSize);
			this.PnBriefing.Controls.Add(this.CkBriefingGenerateOnSave);
			this.PnBriefing.Controls.Add(this.CbBriefingMeasurementSystem);
			this.PnBriefing.Controls.Add(this.LbBriefingMeasurementSystem);
			this.PnBriefing.Controls.Add(this.LstBriefingCoordinateDisplay);
			this.PnBriefing.Controls.Add(this.CbBriefingWeatherDisplay);
			this.PnBriefing.Controls.Add(this.LbBriefingCoordinateDisplay);
			this.PnBriefing.Controls.Add(this.LbBriefingWeatherDisplay);
			this.PnBriefing.Controls.Add(this.LbTitleBriefing);
			this.PnBriefing.Location = new System.Drawing.Point(14, 332);
			this.PnBriefing.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.PnBriefing.Name = "PnBriefing";
			this.PnBriefing.Size = new System.Drawing.Size(546, 301);
			this.PnBriefing.TabIndex = 18;
			// 
			// TbBriefingDirectoryName
			// 
			this.TbBriefingDirectoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbBriefingDirectoryName.Location = new System.Drawing.Point(213, 262);
			this.TbBriefingDirectoryName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbBriefingDirectoryName.Name = "TbBriefingDirectoryName";
			this.TbBriefingDirectoryName.Size = new System.Drawing.Size(226, 23);
			this.TbBriefingDirectoryName.TabIndex = 34;
			// 
			// LbBriefingDirectoryName
			// 
			this.LbBriefingDirectoryName.AutoSize = true;
			this.LbBriefingDirectoryName.Location = new System.Drawing.Point(150, 265);
			this.LbBriefingDirectoryName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbBriefingDirectoryName.Name = "LbBriefingDirectoryName";
			this.LbBriefingDirectoryName.Size = new System.Drawing.Size(55, 15);
			this.LbBriefingDirectoryName.TabIndex = 33;
			this.LbBriefingDirectoryName.Text = "Directory";
			// 
			// CkBriefingGenerateDirectoryHtml
			// 
			this.CkBriefingGenerateDirectoryHtml.AutoSize = true;
			this.CkBriefingGenerateDirectoryHtml.Location = new System.Drawing.Point(360, 237);
			this.CkBriefingGenerateDirectoryHtml.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkBriefingGenerateDirectoryHtml.Name = "CkBriefingGenerateDirectoryHtml";
			this.CkBriefingGenerateDirectoryHtml.Size = new System.Drawing.Size(79, 19);
			this.CkBriefingGenerateDirectoryHtml.TabIndex = 32;
			this.CkBriefingGenerateDirectoryHtml.Text = "With html";
			this.CkBriefingGenerateDirectoryHtml.UseVisualStyleBackColor = true;
			// 
			// CkBriefingGenerateDirectory
			// 
			this.CkBriefingGenerateDirectory.AutoSize = true;
			this.CkBriefingGenerateDirectory.Location = new System.Drawing.Point(131, 237);
			this.CkBriefingGenerateDirectory.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkBriefingGenerateDirectory.Name = "CkBriefingGenerateDirectory";
			this.CkBriefingGenerateDirectory.Size = new System.Drawing.Size(221, 19);
			this.CkBriefingGenerateDirectory.TabIndex = 31;
			this.CkBriefingGenerateDirectory.Text = "Generate in directory (relative to miz)";
			this.CkBriefingGenerateDirectory.UseVisualStyleBackColor = true;
			this.CkBriefingGenerateDirectory.CheckedChanged += new System.EventHandler(this.CkBriefingGenerateDirectory_CheckedChanged);
			// 
			// CkBriefingGenerateMiz
			// 
			this.CkBriefingGenerateMiz.AutoSize = true;
			this.CkBriefingGenerateMiz.Location = new System.Drawing.Point(131, 212);
			this.CkBriefingGenerateMiz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkBriefingGenerateMiz.Name = "CkBriefingGenerateMiz";
			this.CkBriefingGenerateMiz.Size = new System.Drawing.Size(127, 19);
			this.CkBriefingGenerateMiz.TabIndex = 30;
			this.CkBriefingGenerateMiz.Text = "Generate in miz file";
			this.CkBriefingGenerateMiz.UseVisualStyleBackColor = true;
			// 
			// LbBriefingImageSize
			// 
			this.LbBriefingImageSize.AutoSize = true;
			this.LbBriefingImageSize.Location = new System.Drawing.Point(5, 159);
			this.LbBriefingImageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbBriefingImageSize.Name = "LbBriefingImageSize";
			this.LbBriefingImageSize.Size = new System.Drawing.Size(62, 15);
			this.LbBriefingImageSize.TabIndex = 29;
			this.LbBriefingImageSize.Text = "Image size";
			// 
			// UcBriefingImageSize
			// 
			this.UcBriefingImageSize.Location = new System.Drawing.Point(131, 154);
			this.UcBriefingImageSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.UcBriefingImageSize.Name = "UcBriefingImageSize";
			this.UcBriefingImageSize.SelectedSize = new System.Drawing.Size(1, 1);
			this.UcBriefingImageSize.Size = new System.Drawing.Size(327, 27);
			this.UcBriefingImageSize.TabIndex = 28;
			// 
			// CkBriefingGenerateOnSave
			// 
			this.CkBriefingGenerateOnSave.AutoSize = true;
			this.CkBriefingGenerateOnSave.Location = new System.Drawing.Point(131, 187);
			this.CkBriefingGenerateOnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkBriefingGenerateOnSave.Name = "CkBriefingGenerateOnSave";
			this.CkBriefingGenerateOnSave.Size = new System.Drawing.Size(250, 19);
			this.CkBriefingGenerateOnSave.TabIndex = 27;
			this.CkBriefingGenerateOnSave.Text = "Generate briefing when saving the mission";
			this.CkBriefingGenerateOnSave.UseVisualStyleBackColor = true;
			// 
			// CbBriefingMeasurementSystem
			// 
			this.CbBriefingMeasurementSystem.FormattingEnabled = true;
			this.CbBriefingMeasurementSystem.Location = new System.Drawing.Point(131, 61);
			this.CbBriefingMeasurementSystem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CbBriefingMeasurementSystem.Name = "CbBriefingMeasurementSystem";
			this.CbBriefingMeasurementSystem.Size = new System.Drawing.Size(312, 23);
			this.CbBriefingMeasurementSystem.TabIndex = 26;
			// 
			// LbBriefingMeasurementSystem
			// 
			this.LbBriefingMeasurementSystem.AutoSize = true;
			this.LbBriefingMeasurementSystem.Location = new System.Drawing.Point(5, 65);
			this.LbBriefingMeasurementSystem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbBriefingMeasurementSystem.Name = "LbBriefingMeasurementSystem";
			this.LbBriefingMeasurementSystem.Size = new System.Drawing.Size(120, 15);
			this.LbBriefingMeasurementSystem.TabIndex = 25;
			this.LbBriefingMeasurementSystem.Text = "Measurement system";
			// 
			// LstBriefingCoordinateDisplay
			// 
			this.LstBriefingCoordinateDisplay.FormattingEnabled = true;
			this.LstBriefingCoordinateDisplay.Location = new System.Drawing.Point(131, 90);
			this.LstBriefingCoordinateDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.LstBriefingCoordinateDisplay.Name = "LstBriefingCoordinateDisplay";
			this.LstBriefingCoordinateDisplay.Size = new System.Drawing.Size(312, 58);
			this.LstBriefingCoordinateDisplay.TabIndex = 24;
			// 
			// CbBriefingWeatherDisplay
			// 
			this.CbBriefingWeatherDisplay.FormattingEnabled = true;
			this.CbBriefingWeatherDisplay.Location = new System.Drawing.Point(131, 32);
			this.CbBriefingWeatherDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CbBriefingWeatherDisplay.Name = "CbBriefingWeatherDisplay";
			this.CbBriefingWeatherDisplay.Size = new System.Drawing.Size(312, 23);
			this.CbBriefingWeatherDisplay.TabIndex = 23;
			// 
			// LbBriefingCoordinateDisplay
			// 
			this.LbBriefingCoordinateDisplay.AutoSize = true;
			this.LbBriefingCoordinateDisplay.Location = new System.Drawing.Point(5, 90);
			this.LbBriefingCoordinateDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbBriefingCoordinateDisplay.Name = "LbBriefingCoordinateDisplay";
			this.LbBriefingCoordinateDisplay.Size = new System.Drawing.Size(106, 15);
			this.LbBriefingCoordinateDisplay.TabIndex = 18;
			this.LbBriefingCoordinateDisplay.Text = "Coordinate display";
			// 
			// LbBriefingWeatherDisplay
			// 
			this.LbBriefingWeatherDisplay.AutoSize = true;
			this.LbBriefingWeatherDisplay.Location = new System.Drawing.Point(5, 36);
			this.LbBriefingWeatherDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbBriefingWeatherDisplay.Name = "LbBriefingWeatherDisplay";
			this.LbBriefingWeatherDisplay.Size = new System.Drawing.Size(91, 15);
			this.LbBriefingWeatherDisplay.TabIndex = 14;
			this.LbBriefingWeatherDisplay.Text = "Weather display";
			// 
			// LbTitleBriefing
			// 
			this.LbTitleBriefing.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTitleBriefing.Location = new System.Drawing.Point(0, 0);
			this.LbTitleBriefing.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbTitleBriefing.Name = "LbTitleBriefing";
			this.LbTitleBriefing.Size = new System.Drawing.Size(544, 32);
			this.LbTitleBriefing.TabIndex = 13;
			this.LbTitleBriefing.Text = "Briefing";
			this.LbTitleBriefing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FrmPreferences
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 673);
			this.Controls.Add(this.PnBriefing);
			this.Controls.Add(this.BtDefaults);
			this.Controls.Add(this.PnMap);
			this.Controls.Add(this.PnMission);
			this.Controls.Add(this.PnApplication);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.BtOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
			this.PnBriefing.ResumeLayout(false);
			this.PnBriefing.PerformLayout();
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
		private System.Windows.Forms.Label LbTitleMission;
		private System.Windows.Forms.Panel PnMap;
		private System.Windows.Forms.ComboBox CbMapProvider;
		private System.Windows.Forms.Label LbMapZoom;
		private System.Windows.Forms.Label LbMapProvider;
		private System.Windows.Forms.Label LbTitleMap;
		private System.Windows.Forms.Button BtDefaults;
		private System.Windows.Forms.Button BtApplicationWorkingDirectoryReset;
		private System.Windows.Forms.NumericUpDown NudMapZoom;
		private CheckBox CkMissionNoCallsignForPlayable;
		private Panel PnBriefing;
		private CheckedListBox LstBriefingCoordinateDisplay;
		private ComboBox CbBriefingWeatherDisplay;
		private Label LbBriefingCoordinateDisplay;
		private Label LbBriefingWeatherDisplay;
		private Label LbTitleBriefing;
		private CheckBox CkMissionBullseyeWaypoint;
		private ComboBox CbBriefingMeasurementSystem;
		private Label LbBriefingMeasurementSystem;
		private CheckBox CkBriefingGenerateOnSave;
		private Label LbBriefingImageSize;
		private UcImageSize UcBriefingImageSize;
		private CheckBox CkBriefingGenerateDirectoryHtml;
		private CheckBox CkBriefingGenerateDirectory;
		private CheckBox CkBriefingGenerateMiz;
		private TextBox TbBriefingDirectoryName;
		private Label LbBriefingDirectoryName;
	}
}
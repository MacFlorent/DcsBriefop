﻿namespace DcsBriefop.Forms
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
			BtCancel = new Button();
			BtOk = new Button();
			LbTitleApplication = new Label();
			PnApplication = new Panel();
			CkApplicationGenerateBatch = new CheckBox();
			CkApplicationMizBackup = new CheckBox();
			BtApplicationRecentMiz = new Button();
			TbApplicationRecentMiz = new TextBox();
			LbApplicationRecentMiz = new Label();
			BtApplicationWorkingDirectoryReset = new Button();
			BtApplicationWorkingDirectorySelect = new Button();
			TbApplicationWorkingDirectory = new TextBox();
			LbApplicationWorkingDirectory = new Label();
			PnMission = new Panel();
			CkMissionBullseyeWaypoint = new CheckBox();
			CkMissionNoCallsignForPlayable = new CheckBox();
			LbTitleMission = new Label();
			PnMap = new Panel();
			NudMapZoom = new NumericUpDown();
			CbMapProvider = new ComboBox();
			LbMapZoom = new Label();
			LbMapProvider = new Label();
			LbTitleMap = new Label();
			BtDefaults = new Button();
			PnBriefing = new Panel();
			CkBriefingGenerateDirectoryHtml = new CheckBox();
			LbBriefingImageSize = new Label();
			UcBriefingImageSize = new UcImageSize();
			CkBriefingGenerateOnSave = new CheckBox();
			CbBriefingMeasurementSystem = new ComboBox();
			LbBriefingMeasurementSystem = new Label();
			LstBriefingCoordinateDisplay = new CheckedListBox();
			CbBriefingWeatherDisplay = new ComboBox();
			LbBriefingCoordinateDisplay = new Label();
			LbBriefingWeatherDisplay = new Label();
			LbTitleBriefing = new Label();
			PnApplication.SuspendLayout();
			PnMission.SuspendLayout();
			PnMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)NudMapZoom).BeginInit();
			PnBriefing.SuspendLayout();
			SuspendLayout();
			// 
			// BtCancel
			// 
			BtCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtCancel.Location = new Point(472, 586);
			BtCancel.Margin = new Padding(4, 3, 4, 3);
			BtCancel.Name = "BtCancel";
			BtCancel.Size = new Size(88, 27);
			BtCancel.TabIndex = 12;
			BtCancel.Text = "Cancel";
			BtCancel.UseVisualStyleBackColor = true;
			BtCancel.Click += BtCancel_Click;
			// 
			// BtOk
			// 
			BtOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtOk.Location = new Point(378, 586);
			BtOk.Margin = new Padding(4, 3, 4, 3);
			BtOk.Name = "BtOk";
			BtOk.Size = new Size(88, 27);
			BtOk.TabIndex = 11;
			BtOk.Text = "OK";
			BtOk.UseVisualStyleBackColor = true;
			BtOk.Click += BtOk_Click;
			// 
			// LbTitleApplication
			// 
			LbTitleApplication.Dock = DockStyle.Top;
			LbTitleApplication.Location = new Point(0, 0);
			LbTitleApplication.Margin = new Padding(4, 0, 4, 0);
			LbTitleApplication.Name = "LbTitleApplication";
			LbTitleApplication.Size = new Size(544, 32);
			LbTitleApplication.TabIndex = 13;
			LbTitleApplication.Text = "Application";
			LbTitleApplication.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// PnApplication
			// 
			PnApplication.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PnApplication.BorderStyle = BorderStyle.FixedSingle;
			PnApplication.Controls.Add(CkApplicationGenerateBatch);
			PnApplication.Controls.Add(CkApplicationMizBackup);
			PnApplication.Controls.Add(BtApplicationRecentMiz);
			PnApplication.Controls.Add(TbApplicationRecentMiz);
			PnApplication.Controls.Add(LbApplicationRecentMiz);
			PnApplication.Controls.Add(BtApplicationWorkingDirectoryReset);
			PnApplication.Controls.Add(BtApplicationWorkingDirectorySelect);
			PnApplication.Controls.Add(TbApplicationWorkingDirectory);
			PnApplication.Controls.Add(LbApplicationWorkingDirectory);
			PnApplication.Controls.Add(LbTitleApplication);
			PnApplication.Location = new Point(14, 14);
			PnApplication.Margin = new Padding(4, 3, 4, 3);
			PnApplication.Name = "PnApplication";
			PnApplication.Size = new Size(546, 135);
			PnApplication.TabIndex = 14;
			// 
			// CkApplicationGenerateBatch
			// 
			CkApplicationGenerateBatch.AutoSize = true;
			CkApplicationGenerateBatch.Location = new Point(259, 100);
			CkApplicationGenerateBatch.Margin = new Padding(4, 3, 4, 3);
			CkApplicationGenerateBatch.Name = "CkApplicationGenerateBatch";
			CkApplicationGenerateBatch.Size = new Size(207, 19);
			CkApplicationGenerateBatch.TabIndex = 22;
			CkApplicationGenerateBatch.Text = "Generate batch command on save";
			CkApplicationGenerateBatch.UseVisualStyleBackColor = true;
			// 
			// CkApplicationMizBackup
			// 
			CkApplicationMizBackup.AutoSize = true;
			CkApplicationMizBackup.Location = new Point(42, 100);
			CkApplicationMizBackup.Margin = new Padding(4, 3, 4, 3);
			CkApplicationMizBackup.Name = "CkApplicationMizBackup";
			CkApplicationMizBackup.Size = new Size(187, 19);
			CkApplicationMizBackup.TabIndex = 21;
			CkApplicationMizBackup.Text = "Backup Miz before overwriting";
			CkApplicationMizBackup.UseVisualStyleBackColor = true;
			// 
			// BtApplicationRecentMiz
			// 
			BtApplicationRecentMiz.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtApplicationRecentMiz.Location = new Point(448, 61);
			BtApplicationRecentMiz.Margin = new Padding(4, 3, 4, 3);
			BtApplicationRecentMiz.Name = "BtApplicationRecentMiz";
			BtApplicationRecentMiz.Size = new Size(86, 27);
			BtApplicationRecentMiz.TabIndex = 20;
			BtApplicationRecentMiz.Text = "Clear";
			BtApplicationRecentMiz.UseVisualStyleBackColor = true;
			BtApplicationRecentMiz.Click += BtApplicationRecentMiz_Click;
			// 
			// TbApplicationRecentMiz
			// 
			TbApplicationRecentMiz.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbApplicationRecentMiz.Location = new Point(131, 63);
			TbApplicationRecentMiz.Margin = new Padding(4, 3, 4, 3);
			TbApplicationRecentMiz.Name = "TbApplicationRecentMiz";
			TbApplicationRecentMiz.ReadOnly = true;
			TbApplicationRecentMiz.Size = new Size(310, 23);
			TbApplicationRecentMiz.TabIndex = 19;
			// 
			// LbApplicationRecentMiz
			// 
			LbApplicationRecentMiz.AutoSize = true;
			LbApplicationRecentMiz.Location = new Point(5, 67);
			LbApplicationRecentMiz.Margin = new Padding(4, 0, 4, 0);
			LbApplicationRecentMiz.Name = "LbApplicationRecentMiz";
			LbApplicationRecentMiz.Size = new Size(65, 15);
			LbApplicationRecentMiz.TabIndex = 18;
			LbApplicationRecentMiz.Text = "Recent Miz";
			// 
			// BtApplicationWorkingDirectoryReset
			// 
			BtApplicationWorkingDirectoryReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtApplicationWorkingDirectoryReset.Location = new Point(495, 31);
			BtApplicationWorkingDirectoryReset.Margin = new Padding(4, 3, 4, 3);
			BtApplicationWorkingDirectoryReset.Name = "BtApplicationWorkingDirectoryReset";
			BtApplicationWorkingDirectoryReset.Size = new Size(40, 27);
			BtApplicationWorkingDirectoryReset.TabIndex = 17;
			BtApplicationWorkingDirectoryReset.Text = "R";
			BtApplicationWorkingDirectoryReset.UseVisualStyleBackColor = true;
			BtApplicationWorkingDirectoryReset.Click += BtApplicationWorkingDirectoryReset_Click;
			// 
			// BtApplicationWorkingDirectorySelect
			// 
			BtApplicationWorkingDirectorySelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtApplicationWorkingDirectorySelect.Location = new Point(448, 31);
			BtApplicationWorkingDirectorySelect.Margin = new Padding(4, 3, 4, 3);
			BtApplicationWorkingDirectorySelect.Name = "BtApplicationWorkingDirectorySelect";
			BtApplicationWorkingDirectorySelect.Size = new Size(40, 27);
			BtApplicationWorkingDirectorySelect.TabIndex = 16;
			BtApplicationWorkingDirectorySelect.Text = "...";
			BtApplicationWorkingDirectorySelect.UseVisualStyleBackColor = true;
			BtApplicationWorkingDirectorySelect.Click += BtApplicationWorkingDirectorySelect_Click;
			// 
			// TbApplicationWorkingDirectory
			// 
			TbApplicationWorkingDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbApplicationWorkingDirectory.Location = new Point(131, 33);
			TbApplicationWorkingDirectory.Margin = new Padding(4, 3, 4, 3);
			TbApplicationWorkingDirectory.Name = "TbApplicationWorkingDirectory";
			TbApplicationWorkingDirectory.ReadOnly = true;
			TbApplicationWorkingDirectory.Size = new Size(310, 23);
			TbApplicationWorkingDirectory.TabIndex = 15;
			// 
			// LbApplicationWorkingDirectory
			// 
			LbApplicationWorkingDirectory.AutoSize = true;
			LbApplicationWorkingDirectory.Location = new Point(5, 37);
			LbApplicationWorkingDirectory.Margin = new Padding(4, 0, 4, 0);
			LbApplicationWorkingDirectory.Name = "LbApplicationWorkingDirectory";
			LbApplicationWorkingDirectory.Size = new Size(102, 15);
			LbApplicationWorkingDirectory.TabIndex = 14;
			LbApplicationWorkingDirectory.Text = "Working directory";
			// 
			// PnMission
			// 
			PnMission.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PnMission.BorderStyle = BorderStyle.FixedSingle;
			PnMission.Controls.Add(CkMissionBullseyeWaypoint);
			PnMission.Controls.Add(CkMissionNoCallsignForPlayable);
			PnMission.Controls.Add(LbTitleMission);
			PnMission.Location = new Point(14, 156);
			PnMission.Margin = new Padding(4, 3, 4, 3);
			PnMission.Name = "PnMission";
			PnMission.Size = new Size(546, 65);
			PnMission.TabIndex = 15;
			// 
			// CkMissionBullseyeWaypoint
			// 
			CkMissionBullseyeWaypoint.AutoSize = true;
			CkMissionBullseyeWaypoint.Location = new Point(259, 35);
			CkMissionBullseyeWaypoint.Margin = new Padding(4, 3, 4, 3);
			CkMissionBullseyeWaypoint.Name = "CkMissionBullseyeWaypoint";
			CkMissionBullseyeWaypoint.Size = new Size(281, 19);
			CkMissionBullseyeWaypoint.TabIndex = 26;
			CkMissionBullseyeWaypoint.Text = "Add Bulls as first waypoint for all playable flights";
			CkMissionBullseyeWaypoint.UseVisualStyleBackColor = true;
			// 
			// CkMissionNoCallsignForPlayable
			// 
			CkMissionNoCallsignForPlayable.AutoSize = true;
			CkMissionNoCallsignForPlayable.Location = new Point(42, 35);
			CkMissionNoCallsignForPlayable.Margin = new Padding(4, 3, 4, 3);
			CkMissionNoCallsignForPlayable.Name = "CkMissionNoCallsignForPlayable";
			CkMissionNoCallsignForPlayable.Size = new Size(186, 19);
			CkMissionNoCallsignForPlayable.TabIndex = 25;
			CkMissionNoCallsignForPlayable.Text = "No callsign for playable flights";
			CkMissionNoCallsignForPlayable.UseVisualStyleBackColor = true;
			// 
			// LbTitleMission
			// 
			LbTitleMission.Dock = DockStyle.Top;
			LbTitleMission.Location = new Point(0, 0);
			LbTitleMission.Margin = new Padding(4, 0, 4, 0);
			LbTitleMission.Name = "LbTitleMission";
			LbTitleMission.Size = new Size(544, 32);
			LbTitleMission.TabIndex = 13;
			LbTitleMission.Text = "Mission";
			LbTitleMission.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// PnMap
			// 
			PnMap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PnMap.BorderStyle = BorderStyle.FixedSingle;
			PnMap.Controls.Add(NudMapZoom);
			PnMap.Controls.Add(CbMapProvider);
			PnMap.Controls.Add(LbMapZoom);
			PnMap.Controls.Add(LbMapProvider);
			PnMap.Controls.Add(LbTitleMap);
			PnMap.Location = new Point(14, 227);
			PnMap.Margin = new Padding(4, 3, 4, 3);
			PnMap.Name = "PnMap";
			PnMap.Size = new Size(546, 99);
			PnMap.TabIndex = 16;
			// 
			// NudMapZoom
			// 
			NudMapZoom.Location = new Point(131, 65);
			NudMapZoom.Margin = new Padding(4, 3, 4, 3);
			NudMapZoom.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
			NudMapZoom.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			NudMapZoom.Name = "NudMapZoom";
			NudMapZoom.Size = new Size(85, 23);
			NudMapZoom.TabIndex = 24;
			NudMapZoom.Value = new decimal(new int[] { 1, 0, 0, 0 });
			// 
			// CbMapProvider
			// 
			CbMapProvider.FormattingEnabled = true;
			CbMapProvider.Location = new Point(131, 32);
			CbMapProvider.Margin = new Padding(4, 3, 4, 3);
			CbMapProvider.Name = "CbMapProvider";
			CbMapProvider.Size = new Size(310, 23);
			CbMapProvider.TabIndex = 23;
			// 
			// LbMapZoom
			// 
			LbMapZoom.AutoSize = true;
			LbMapZoom.Location = new Point(5, 67);
			LbMapZoom.Margin = new Padding(4, 0, 4, 0);
			LbMapZoom.Name = "LbMapZoom";
			LbMapZoom.Size = new Size(78, 15);
			LbMapZoom.TabIndex = 18;
			LbMapZoom.Text = "Default zoom";
			// 
			// LbMapProvider
			// 
			LbMapProvider.AutoSize = true;
			LbMapProvider.Location = new Point(5, 36);
			LbMapProvider.Margin = new Padding(4, 0, 4, 0);
			LbMapProvider.Name = "LbMapProvider";
			LbMapProvider.Size = new Size(92, 15);
			LbMapProvider.TabIndex = 14;
			LbMapProvider.Text = "Default provider";
			// 
			// LbTitleMap
			// 
			LbTitleMap.Dock = DockStyle.Top;
			LbTitleMap.Location = new Point(0, 0);
			LbTitleMap.Margin = new Padding(4, 0, 4, 0);
			LbTitleMap.Name = "LbTitleMap";
			LbTitleMap.Size = new Size(544, 32);
			LbTitleMap.TabIndex = 13;
			LbTitleMap.Text = "Map";
			LbTitleMap.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// BtDefaults
			// 
			BtDefaults.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtDefaults.Location = new Point(14, 586);
			BtDefaults.Margin = new Padding(4, 3, 4, 3);
			BtDefaults.Name = "BtDefaults";
			BtDefaults.Size = new Size(124, 27);
			BtDefaults.TabIndex = 17;
			BtDefaults.Text = "Reset defaults";
			BtDefaults.UseVisualStyleBackColor = true;
			BtDefaults.Click += BtDefaults_Click;
			// 
			// PnBriefing
			// 
			PnBriefing.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PnBriefing.BorderStyle = BorderStyle.FixedSingle;
			PnBriefing.Controls.Add(CkBriefingGenerateDirectoryHtml);
			PnBriefing.Controls.Add(LbBriefingImageSize);
			PnBriefing.Controls.Add(UcBriefingImageSize);
			PnBriefing.Controls.Add(CkBriefingGenerateOnSave);
			PnBriefing.Controls.Add(CbBriefingMeasurementSystem);
			PnBriefing.Controls.Add(LbBriefingMeasurementSystem);
			PnBriefing.Controls.Add(LstBriefingCoordinateDisplay);
			PnBriefing.Controls.Add(CbBriefingWeatherDisplay);
			PnBriefing.Controls.Add(LbBriefingCoordinateDisplay);
			PnBriefing.Controls.Add(LbBriefingWeatherDisplay);
			PnBriefing.Controls.Add(LbTitleBriefing);
			PnBriefing.Location = new Point(14, 332);
			PnBriefing.Margin = new Padding(4, 3, 4, 3);
			PnBriefing.Name = "PnBriefing";
			PnBriefing.Size = new Size(546, 246);
			PnBriefing.TabIndex = 18;
			// 
			// CkBriefingGenerateDirectoryHtml
			// 
			CkBriefingGenerateDirectoryHtml.AutoSize = true;
			CkBriefingGenerateDirectoryHtml.Location = new Point(131, 212);
			CkBriefingGenerateDirectoryHtml.Margin = new Padding(4, 3, 4, 3);
			CkBriefingGenerateDirectoryHtml.Name = "CkBriefingGenerateDirectoryHtml";
			CkBriefingGenerateDirectoryHtml.Size = new Size(164, 19);
			CkBriefingGenerateDirectoryHtml.TabIndex = 35;
			CkBriefingGenerateDirectoryHtml.Text = "Generate html in directory";
			CkBriefingGenerateDirectoryHtml.UseVisualStyleBackColor = true;
			// 
			// LbBriefingImageSize
			// 
			LbBriefingImageSize.AutoSize = true;
			LbBriefingImageSize.Location = new Point(5, 159);
			LbBriefingImageSize.Margin = new Padding(4, 0, 4, 0);
			LbBriefingImageSize.Name = "LbBriefingImageSize";
			LbBriefingImageSize.Size = new Size(62, 15);
			LbBriefingImageSize.TabIndex = 29;
			LbBriefingImageSize.Text = "Image size";
			// 
			// UcBriefingImageSize
			// 
			UcBriefingImageSize.Location = new Point(131, 154);
			UcBriefingImageSize.Margin = new Padding(4, 3, 4, 3);
			UcBriefingImageSize.Name = "UcBriefingImageSize";
			UcBriefingImageSize.SelectedSize = new Size(1, 1);
			UcBriefingImageSize.Size = new Size(327, 27);
			UcBriefingImageSize.TabIndex = 28;
			// 
			// CkBriefingGenerateOnSave
			// 
			CkBriefingGenerateOnSave.AutoSize = true;
			CkBriefingGenerateOnSave.Location = new Point(131, 187);
			CkBriefingGenerateOnSave.Margin = new Padding(4, 3, 4, 3);
			CkBriefingGenerateOnSave.Name = "CkBriefingGenerateOnSave";
			CkBriefingGenerateOnSave.Size = new Size(250, 19);
			CkBriefingGenerateOnSave.TabIndex = 27;
			CkBriefingGenerateOnSave.Text = "Generate briefing when saving the mission";
			CkBriefingGenerateOnSave.UseVisualStyleBackColor = true;
			// 
			// CbBriefingMeasurementSystem
			// 
			CbBriefingMeasurementSystem.FormattingEnabled = true;
			CbBriefingMeasurementSystem.Location = new Point(131, 61);
			CbBriefingMeasurementSystem.Margin = new Padding(4, 3, 4, 3);
			CbBriefingMeasurementSystem.Name = "CbBriefingMeasurementSystem";
			CbBriefingMeasurementSystem.Size = new Size(312, 23);
			CbBriefingMeasurementSystem.TabIndex = 26;
			// 
			// LbBriefingMeasurementSystem
			// 
			LbBriefingMeasurementSystem.AutoSize = true;
			LbBriefingMeasurementSystem.Location = new Point(5, 65);
			LbBriefingMeasurementSystem.Margin = new Padding(4, 0, 4, 0);
			LbBriefingMeasurementSystem.Name = "LbBriefingMeasurementSystem";
			LbBriefingMeasurementSystem.Size = new Size(120, 15);
			LbBriefingMeasurementSystem.TabIndex = 25;
			LbBriefingMeasurementSystem.Text = "Measurement system";
			// 
			// LstBriefingCoordinateDisplay
			// 
			LstBriefingCoordinateDisplay.FormattingEnabled = true;
			LstBriefingCoordinateDisplay.Location = new Point(131, 90);
			LstBriefingCoordinateDisplay.Margin = new Padding(4, 3, 4, 3);
			LstBriefingCoordinateDisplay.Name = "LstBriefingCoordinateDisplay";
			LstBriefingCoordinateDisplay.Size = new Size(312, 58);
			LstBriefingCoordinateDisplay.TabIndex = 24;
			// 
			// CbBriefingWeatherDisplay
			// 
			CbBriefingWeatherDisplay.FormattingEnabled = true;
			CbBriefingWeatherDisplay.Location = new Point(131, 32);
			CbBriefingWeatherDisplay.Margin = new Padding(4, 3, 4, 3);
			CbBriefingWeatherDisplay.Name = "CbBriefingWeatherDisplay";
			CbBriefingWeatherDisplay.Size = new Size(312, 23);
			CbBriefingWeatherDisplay.TabIndex = 23;
			// 
			// LbBriefingCoordinateDisplay
			// 
			LbBriefingCoordinateDisplay.AutoSize = true;
			LbBriefingCoordinateDisplay.Location = new Point(5, 90);
			LbBriefingCoordinateDisplay.Margin = new Padding(4, 0, 4, 0);
			LbBriefingCoordinateDisplay.Name = "LbBriefingCoordinateDisplay";
			LbBriefingCoordinateDisplay.Size = new Size(106, 15);
			LbBriefingCoordinateDisplay.TabIndex = 18;
			LbBriefingCoordinateDisplay.Text = "Coordinate display";
			// 
			// LbBriefingWeatherDisplay
			// 
			LbBriefingWeatherDisplay.AutoSize = true;
			LbBriefingWeatherDisplay.Location = new Point(5, 36);
			LbBriefingWeatherDisplay.Margin = new Padding(4, 0, 4, 0);
			LbBriefingWeatherDisplay.Name = "LbBriefingWeatherDisplay";
			LbBriefingWeatherDisplay.Size = new Size(91, 15);
			LbBriefingWeatherDisplay.TabIndex = 14;
			LbBriefingWeatherDisplay.Text = "Weather display";
			// 
			// LbTitleBriefing
			// 
			LbTitleBriefing.Dock = DockStyle.Top;
			LbTitleBriefing.Location = new Point(0, 0);
			LbTitleBriefing.Margin = new Padding(4, 0, 4, 0);
			LbTitleBriefing.Name = "LbTitleBriefing";
			LbTitleBriefing.Size = new Size(544, 32);
			LbTitleBriefing.TabIndex = 13;
			LbTitleBriefing.Text = "Briefing";
			LbTitleBriefing.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// FrmPreferences
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(574, 621);
			Controls.Add(PnBriefing);
			Controls.Add(BtDefaults);
			Controls.Add(PnMap);
			Controls.Add(PnMission);
			Controls.Add(PnApplication);
			Controls.Add(BtCancel);
			Controls.Add(BtOk);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmPreferences";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Preferences";
			PnApplication.ResumeLayout(false);
			PnApplication.PerformLayout();
			PnMission.ResumeLayout(false);
			PnMission.PerformLayout();
			PnMap.ResumeLayout(false);
			PnMap.PerformLayout();
			((System.ComponentModel.ISupportInitialize)NudMapZoom).EndInit();
			PnBriefing.ResumeLayout(false);
			PnBriefing.PerformLayout();
			ResumeLayout(false);
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
	}
}
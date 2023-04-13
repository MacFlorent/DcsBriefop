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
			BtCancel = new Button();
			BtOk = new Button();
			LbApplicationProxy = new Label();
			PnApplication = new Panel();
			TbApplicationProxyPassword = new TextBox();
			TbApplicationProxyHost = new TextBox();
			TbApplicationProxyUser = new TextBox();
			LbApplicationProxyHost = new Label();
			TbApplicationProxyPort = new TextBox();
			LbApplicationProxyUser = new Label();
			CkApplicationGenerateBatch = new CheckBox();
			CkApplicationMizBackup = new CheckBox();
			BtApplicationRecentMiz = new Button();
			TbApplicationRecentMiz = new TextBox();
			LbApplicationRecentMiz = new Label();
			BtApplicationWorkingDirectoryReset = new Button();
			BtApplicationWorkingDirectorySelect = new Button();
			TbApplicationWorkingDirectory = new TextBox();
			LbApplicationWorkingDirectory = new Label();
			LbMissionBullseyeWaypoint = new Label();
			CbMissionBullseyeWaypoint = new ComboBox();
			CkMissionNoCallsignForPlayable = new CheckBox();
			PnMap = new Panel();
			NudMapZoom = new NumericUpDown();
			CbMapProvider = new ComboBox();
			LbMapZoom = new Label();
			LbMapProvider = new Label();
			LbTitleMap = new Label();
			BtDefaults = new Button();
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
			TcMain = new TabControl();
			TpApplication = new TabPage();
			TpMission = new TabPage();
			TpBriefing = new TabPage();
			BtBriefingGenerationDirectoryReset = new Button();
			BtBriefingGenerationDirectory = new Button();
			LbBriefingGenerationDirectory = new Label();
			TbBriefingGenerationDirectory = new TextBox();
			LbBriefingHtmlCss = new Label();
			CbBriefingHtmlCss = new ComboBox();
			PnApplication.SuspendLayout();
			PnMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)NudMapZoom).BeginInit();
			TcMain.SuspendLayout();
			TpApplication.SuspendLayout();
			TpMission.SuspendLayout();
			TpBriefing.SuspendLayout();
			SuspendLayout();
			// 
			// BtCancel
			// 
			BtCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtCancel.Location = new Point(466, 307);
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
			BtOk.Location = new Point(372, 307);
			BtOk.Margin = new Padding(4, 3, 4, 3);
			BtOk.Name = "BtOk";
			BtOk.Size = new Size(88, 27);
			BtOk.TabIndex = 11;
			BtOk.Text = "OK";
			BtOk.UseVisualStyleBackColor = true;
			BtOk.Click += BtOk_Click;
			// 
			// LbApplicationProxy
			// 
			LbApplicationProxy.Dock = DockStyle.Top;
			LbApplicationProxy.Location = new Point(0, 0);
			LbApplicationProxy.Margin = new Padding(4, 0, 4, 0);
			LbApplicationProxy.Name = "LbApplicationProxy";
			LbApplicationProxy.Size = new Size(544, 23);
			LbApplicationProxy.TabIndex = 13;
			LbApplicationProxy.Text = "Internet proxy";
			LbApplicationProxy.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// PnApplication
			// 
			PnApplication.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PnApplication.BorderStyle = BorderStyle.FixedSingle;
			PnApplication.Controls.Add(TbApplicationProxyPassword);
			PnApplication.Controls.Add(LbApplicationProxy);
			PnApplication.Controls.Add(TbApplicationProxyHost);
			PnApplication.Controls.Add(TbApplicationProxyUser);
			PnApplication.Controls.Add(LbApplicationProxyHost);
			PnApplication.Controls.Add(TbApplicationProxyPort);
			PnApplication.Controls.Add(LbApplicationProxyUser);
			PnApplication.Location = new Point(7, 95);
			PnApplication.Margin = new Padding(4, 3, 4, 3);
			PnApplication.Name = "PnApplication";
			PnApplication.Size = new Size(546, 87);
			PnApplication.TabIndex = 14;
			// 
			// TbApplicationProxyPassword
			// 
			TbApplicationProxyPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbApplicationProxyPassword.Location = new Point(384, 52);
			TbApplicationProxyPassword.Margin = new Padding(4, 3, 4, 3);
			TbApplicationProxyPassword.Name = "TbApplicationProxyPassword";
			TbApplicationProxyPassword.Size = new Size(156, 23);
			TbApplicationProxyPassword.TabIndex = 28;
			// 
			// TbApplicationProxyHost
			// 
			TbApplicationProxyHost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbApplicationProxyHost.Location = new Point(126, 26);
			TbApplicationProxyHost.Margin = new Padding(4, 3, 4, 3);
			TbApplicationProxyHost.Name = "TbApplicationProxyHost";
			TbApplicationProxyHost.Size = new Size(305, 23);
			TbApplicationProxyHost.TabIndex = 24;
			// 
			// TbApplicationProxyUser
			// 
			TbApplicationProxyUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbApplicationProxyUser.Location = new Point(126, 52);
			TbApplicationProxyUser.Margin = new Padding(4, 3, 4, 3);
			TbApplicationProxyUser.Name = "TbApplicationProxyUser";
			TbApplicationProxyUser.Size = new Size(151, 23);
			TbApplicationProxyUser.TabIndex = 27;
			// 
			// LbApplicationProxyHost
			// 
			LbApplicationProxyHost.AutoSize = true;
			LbApplicationProxyHost.Location = new Point(22, 26);
			LbApplicationProxyHost.Margin = new Padding(4, 0, 4, 0);
			LbApplicationProxyHost.Name = "LbApplicationProxyHost";
			LbApplicationProxyHost.Size = new Size(59, 15);
			LbApplicationProxyHost.TabIndex = 23;
			LbApplicationProxyHost.Text = "Host/port";
			// 
			// TbApplicationProxyPort
			// 
			TbApplicationProxyPort.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			TbApplicationProxyPort.Location = new Point(439, 26);
			TbApplicationProxyPort.Margin = new Padding(4, 3, 4, 3);
			TbApplicationProxyPort.Name = "TbApplicationProxyPort";
			TbApplicationProxyPort.Size = new Size(101, 23);
			TbApplicationProxyPort.TabIndex = 25;
			TbApplicationProxyPort.Validated += TbApplicationProxyPort_Validated;
			// 
			// LbApplicationProxyUser
			// 
			LbApplicationProxyUser.AutoSize = true;
			LbApplicationProxyUser.Location = new Point(22, 55);
			LbApplicationProxyUser.Margin = new Padding(4, 0, 4, 0);
			LbApplicationProxyUser.Name = "LbApplicationProxyUser";
			LbApplicationProxyUser.Size = new Size(85, 15);
			LbApplicationProxyUser.TabIndex = 26;
			LbApplicationProxyUser.Text = "User/password";
			// 
			// CkApplicationGenerateBatch
			// 
			CkApplicationGenerateBatch.AutoSize = true;
			CkApplicationGenerateBatch.Location = new Point(329, 68);
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
			CkApplicationMizBackup.Location = new Point(134, 68);
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
			BtApplicationRecentMiz.Location = new Point(469, 37);
			BtApplicationRecentMiz.Margin = new Padding(4, 3, 4, 3);
			BtApplicationRecentMiz.Name = "BtApplicationRecentMiz";
			BtApplicationRecentMiz.Size = new Size(84, 27);
			BtApplicationRecentMiz.TabIndex = 20;
			BtApplicationRecentMiz.Text = "Clear";
			BtApplicationRecentMiz.UseVisualStyleBackColor = true;
			BtApplicationRecentMiz.Click += BtApplicationRecentMiz_Click;
			// 
			// TbApplicationRecentMiz
			// 
			TbApplicationRecentMiz.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbApplicationRecentMiz.Location = new Point(134, 39);
			TbApplicationRecentMiz.Margin = new Padding(4, 3, 4, 3);
			TbApplicationRecentMiz.Name = "TbApplicationRecentMiz";
			TbApplicationRecentMiz.ReadOnly = true;
			TbApplicationRecentMiz.Size = new Size(328, 23);
			TbApplicationRecentMiz.TabIndex = 19;
			// 
			// LbApplicationRecentMiz
			// 
			LbApplicationRecentMiz.AutoSize = true;
			LbApplicationRecentMiz.Location = new Point(8, 43);
			LbApplicationRecentMiz.Margin = new Padding(4, 0, 4, 0);
			LbApplicationRecentMiz.Name = "LbApplicationRecentMiz";
			LbApplicationRecentMiz.Size = new Size(65, 15);
			LbApplicationRecentMiz.TabIndex = 18;
			LbApplicationRecentMiz.Text = "Recent Miz";
			// 
			// BtApplicationWorkingDirectoryReset
			// 
			BtApplicationWorkingDirectoryReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtApplicationWorkingDirectoryReset.Location = new Point(513, 6);
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
			BtApplicationWorkingDirectorySelect.Location = new Point(469, 6);
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
			TbApplicationWorkingDirectory.Location = new Point(134, 9);
			TbApplicationWorkingDirectory.Margin = new Padding(4, 3, 4, 3);
			TbApplicationWorkingDirectory.Name = "TbApplicationWorkingDirectory";
			TbApplicationWorkingDirectory.ReadOnly = true;
			TbApplicationWorkingDirectory.Size = new Size(328, 23);
			TbApplicationWorkingDirectory.TabIndex = 15;
			// 
			// LbApplicationWorkingDirectory
			// 
			LbApplicationWorkingDirectory.AutoSize = true;
			LbApplicationWorkingDirectory.Location = new Point(8, 13);
			LbApplicationWorkingDirectory.Margin = new Padding(4, 0, 4, 0);
			LbApplicationWorkingDirectory.Name = "LbApplicationWorkingDirectory";
			LbApplicationWorkingDirectory.Size = new Size(102, 15);
			LbApplicationWorkingDirectory.TabIndex = 14;
			LbApplicationWorkingDirectory.Text = "Working directory";
			// 
			// LbMissionBullseyeWaypoint
			// 
			LbMissionBullseyeWaypoint.AutoSize = true;
			LbMissionBullseyeWaypoint.Location = new Point(9, 12);
			LbMissionBullseyeWaypoint.Margin = new Padding(4, 0, 4, 0);
			LbMissionBullseyeWaypoint.Name = "LbMissionBullseyeWaypoint";
			LbMissionBullseyeWaypoint.Size = new Size(102, 15);
			LbMissionBullseyeWaypoint.TabIndex = 28;
			LbMissionBullseyeWaypoint.Text = "Bullseye waypoint";
			// 
			// CbMissionBullseyeWaypoint
			// 
			CbMissionBullseyeWaypoint.FormattingEnabled = true;
			CbMissionBullseyeWaypoint.Location = new Point(119, 9);
			CbMissionBullseyeWaypoint.Margin = new Padding(4, 3, 4, 3);
			CbMissionBullseyeWaypoint.Name = "CbMissionBullseyeWaypoint";
			CbMissionBullseyeWaypoint.Size = new Size(310, 23);
			CbMissionBullseyeWaypoint.TabIndex = 27;
			// 
			// CkMissionNoCallsignForPlayable
			// 
			CkMissionNoCallsignForPlayable.AutoSize = true;
			CkMissionNoCallsignForPlayable.Location = new Point(119, 37);
			CkMissionNoCallsignForPlayable.Margin = new Padding(4, 3, 4, 3);
			CkMissionNoCallsignForPlayable.Name = "CkMissionNoCallsignForPlayable";
			CkMissionNoCallsignForPlayable.Size = new Size(186, 19);
			CkMissionNoCallsignForPlayable.TabIndex = 25;
			CkMissionNoCallsignForPlayable.Text = "No callsign for playable flights";
			CkMissionNoCallsignForPlayable.UseVisualStyleBackColor = true;
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
			PnMap.Location = new Point(4, 62);
			PnMap.Margin = new Padding(4, 3, 4, 3);
			PnMap.Name = "PnMap";
			PnMap.Size = new Size(546, 99);
			PnMap.TabIndex = 16;
			// 
			// NudMapZoom
			// 
			NudMapZoom.Location = new Point(114, 57);
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
			CbMapProvider.Location = new Point(114, 28);
			CbMapProvider.Margin = new Padding(4, 3, 4, 3);
			CbMapProvider.Name = "CbMapProvider";
			CbMapProvider.Size = new Size(310, 23);
			CbMapProvider.TabIndex = 23;
			// 
			// LbMapZoom
			// 
			LbMapZoom.AutoSize = true;
			LbMapZoom.Location = new Point(14, 59);
			LbMapZoom.Margin = new Padding(4, 0, 4, 0);
			LbMapZoom.Name = "LbMapZoom";
			LbMapZoom.Size = new Size(78, 15);
			LbMapZoom.TabIndex = 18;
			LbMapZoom.Text = "Default zoom";
			// 
			// LbMapProvider
			// 
			LbMapProvider.AutoSize = true;
			LbMapProvider.Location = new Point(14, 36);
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
			LbTitleMap.Size = new Size(544, 23);
			LbTitleMap.TabIndex = 13;
			LbTitleMap.Text = "Map";
			LbTitleMap.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// BtDefaults
			// 
			BtDefaults.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtDefaults.Location = new Point(4, 307);
			BtDefaults.Margin = new Padding(4, 3, 4, 3);
			BtDefaults.Name = "BtDefaults";
			BtDefaults.Size = new Size(124, 27);
			BtDefaults.TabIndex = 17;
			BtDefaults.Text = "Reset defaults";
			BtDefaults.UseVisualStyleBackColor = true;
			BtDefaults.Click += BtDefaults_Click;
			// 
			// CkBriefingGenerateDirectoryHtml
			// 
			CkBriefingGenerateDirectoryHtml.AutoSize = true;
			CkBriefingGenerateDirectoryHtml.Location = new Point(140, 250);
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
			LbBriefingImageSize.Location = new Point(9, 163);
			LbBriefingImageSize.Margin = new Padding(4, 0, 4, 0);
			LbBriefingImageSize.Name = "LbBriefingImageSize";
			LbBriefingImageSize.Size = new Size(62, 15);
			LbBriefingImageSize.TabIndex = 29;
			LbBriefingImageSize.Text = "Image size";
			// 
			// UcBriefingImageSize
			// 
			UcBriefingImageSize.Location = new Point(140, 163);
			UcBriefingImageSize.Margin = new Padding(4, 3, 4, 3);
			UcBriefingImageSize.Name = "UcBriefingImageSize";
			UcBriefingImageSize.SelectedSize = new Size(1, 1);
			UcBriefingImageSize.Size = new Size(327, 27);
			UcBriefingImageSize.TabIndex = 28;
			// 
			// CkBriefingGenerateOnSave
			// 
			CkBriefingGenerateOnSave.AutoSize = true;
			CkBriefingGenerateOnSave.Location = new Point(140, 225);
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
			CbBriefingMeasurementSystem.Location = new Point(140, 43);
			CbBriefingMeasurementSystem.Margin = new Padding(4, 3, 4, 3);
			CbBriefingMeasurementSystem.Name = "CbBriefingMeasurementSystem";
			CbBriefingMeasurementSystem.Size = new Size(411, 23);
			CbBriefingMeasurementSystem.TabIndex = 26;
			// 
			// LbBriefingMeasurementSystem
			// 
			LbBriefingMeasurementSystem.AutoSize = true;
			LbBriefingMeasurementSystem.Location = new Point(9, 46);
			LbBriefingMeasurementSystem.Margin = new Padding(4, 0, 4, 0);
			LbBriefingMeasurementSystem.Name = "LbBriefingMeasurementSystem";
			LbBriefingMeasurementSystem.Size = new Size(120, 15);
			LbBriefingMeasurementSystem.TabIndex = 25;
			LbBriefingMeasurementSystem.Text = "Measurement system";
			// 
			// LstBriefingCoordinateDisplay
			// 
			LstBriefingCoordinateDisplay.FormattingEnabled = true;
			LstBriefingCoordinateDisplay.Location = new Point(140, 72);
			LstBriefingCoordinateDisplay.Margin = new Padding(4, 3, 4, 3);
			LstBriefingCoordinateDisplay.Name = "LstBriefingCoordinateDisplay";
			LstBriefingCoordinateDisplay.Size = new Size(411, 58);
			LstBriefingCoordinateDisplay.TabIndex = 24;
			// 
			// CbBriefingWeatherDisplay
			// 
			CbBriefingWeatherDisplay.FormattingEnabled = true;
			CbBriefingWeatherDisplay.Location = new Point(140, 14);
			CbBriefingWeatherDisplay.Margin = new Padding(4, 3, 4, 3);
			CbBriefingWeatherDisplay.Name = "CbBriefingWeatherDisplay";
			CbBriefingWeatherDisplay.Size = new Size(410, 23);
			CbBriefingWeatherDisplay.TabIndex = 23;
			// 
			// LbBriefingCoordinateDisplay
			// 
			LbBriefingCoordinateDisplay.AutoSize = true;
			LbBriefingCoordinateDisplay.Location = new Point(9, 72);
			LbBriefingCoordinateDisplay.Margin = new Padding(4, 0, 4, 0);
			LbBriefingCoordinateDisplay.Name = "LbBriefingCoordinateDisplay";
			LbBriefingCoordinateDisplay.Size = new Size(106, 15);
			LbBriefingCoordinateDisplay.TabIndex = 18;
			LbBriefingCoordinateDisplay.Text = "Coordinate display";
			// 
			// LbBriefingWeatherDisplay
			// 
			LbBriefingWeatherDisplay.AutoSize = true;
			LbBriefingWeatherDisplay.Location = new Point(9, 17);
			LbBriefingWeatherDisplay.Margin = new Padding(4, 0, 4, 0);
			LbBriefingWeatherDisplay.Name = "LbBriefingWeatherDisplay";
			LbBriefingWeatherDisplay.Size = new Size(91, 15);
			LbBriefingWeatherDisplay.TabIndex = 14;
			LbBriefingWeatherDisplay.Text = "Weather display";
			// 
			// TcMain
			// 
			TcMain.Controls.Add(TpApplication);
			TcMain.Controls.Add(TpMission);
			TcMain.Controls.Add(TpBriefing);
			TcMain.Dock = DockStyle.Top;
			TcMain.Location = new Point(0, 0);
			TcMain.Name = "TcMain";
			TcMain.SelectedIndex = 0;
			TcMain.Size = new Size(568, 301);
			TcMain.TabIndex = 19;
			// 
			// TpApplication
			// 
			TpApplication.Controls.Add(LbApplicationWorkingDirectory);
			TpApplication.Controls.Add(TbApplicationWorkingDirectory);
			TpApplication.Controls.Add(BtApplicationWorkingDirectorySelect);
			TpApplication.Controls.Add(BtApplicationWorkingDirectoryReset);
			TpApplication.Controls.Add(LbApplicationRecentMiz);
			TpApplication.Controls.Add(PnApplication);
			TpApplication.Controls.Add(TbApplicationRecentMiz);
			TpApplication.Controls.Add(CkApplicationGenerateBatch);
			TpApplication.Controls.Add(BtApplicationRecentMiz);
			TpApplication.Controls.Add(CkApplicationMizBackup);
			TpApplication.Location = new Point(4, 24);
			TpApplication.Name = "TpApplication";
			TpApplication.Padding = new Padding(3);
			TpApplication.Size = new Size(560, 251);
			TpApplication.TabIndex = 0;
			TpApplication.Text = "Application";
			TpApplication.UseVisualStyleBackColor = true;
			// 
			// TpMission
			// 
			TpMission.Controls.Add(LbMissionBullseyeWaypoint);
			TpMission.Controls.Add(CkMissionNoCallsignForPlayable);
			TpMission.Controls.Add(CbMissionBullseyeWaypoint);
			TpMission.Controls.Add(PnMap);
			TpMission.Location = new Point(4, 24);
			TpMission.Name = "TpMission";
			TpMission.Padding = new Padding(3);
			TpMission.Size = new Size(560, 251);
			TpMission.TabIndex = 1;
			TpMission.Text = "Mission";
			TpMission.UseVisualStyleBackColor = true;
			// 
			// TpBriefing
			// 
			TpBriefing.Controls.Add(LbBriefingHtmlCss);
			TpBriefing.Controls.Add(CbBriefingHtmlCss);
			TpBriefing.Controls.Add(BtBriefingGenerationDirectoryReset);
			TpBriefing.Controls.Add(BtBriefingGenerationDirectory);
			TpBriefing.Controls.Add(LbBriefingGenerationDirectory);
			TpBriefing.Controls.Add(TbBriefingGenerationDirectory);
			TpBriefing.Controls.Add(LbBriefingMeasurementSystem);
			TpBriefing.Controls.Add(CkBriefingGenerateDirectoryHtml);
			TpBriefing.Controls.Add(LbBriefingWeatherDisplay);
			TpBriefing.Controls.Add(LbBriefingImageSize);
			TpBriefing.Controls.Add(LbBriefingCoordinateDisplay);
			TpBriefing.Controls.Add(UcBriefingImageSize);
			TpBriefing.Controls.Add(CbBriefingWeatherDisplay);
			TpBriefing.Controls.Add(CkBriefingGenerateOnSave);
			TpBriefing.Controls.Add(LstBriefingCoordinateDisplay);
			TpBriefing.Controls.Add(CbBriefingMeasurementSystem);
			TpBriefing.Location = new Point(4, 24);
			TpBriefing.Name = "TpBriefing";
			TpBriefing.Size = new Size(560, 273);
			TpBriefing.TabIndex = 3;
			TpBriefing.Text = "Briefing";
			TpBriefing.UseVisualStyleBackColor = true;
			// 
			// BtBriefingGenerationDirectoryReset
			// 
			BtBriefingGenerationDirectoryReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtBriefingGenerationDirectoryReset.Location = new Point(511, 194);
			BtBriefingGenerationDirectoryReset.Margin = new Padding(4, 3, 4, 3);
			BtBriefingGenerationDirectoryReset.Name = "BtBriefingGenerationDirectoryReset";
			BtBriefingGenerationDirectoryReset.Size = new Size(40, 27);
			BtBriefingGenerationDirectoryReset.TabIndex = 39;
			BtBriefingGenerationDirectoryReset.Text = "R";
			BtBriefingGenerationDirectoryReset.UseVisualStyleBackColor = true;
			BtBriefingGenerationDirectoryReset.MouseDown += BtBriefingGenerationDirectoryReset_MouseDown;
			// 
			// BtBriefingGenerationDirectory
			// 
			BtBriefingGenerationDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtBriefingGenerationDirectory.Location = new Point(463, 194);
			BtBriefingGenerationDirectory.Margin = new Padding(4, 3, 4, 3);
			BtBriefingGenerationDirectory.Name = "BtBriefingGenerationDirectory";
			BtBriefingGenerationDirectory.Size = new Size(40, 27);
			BtBriefingGenerationDirectory.TabIndex = 38;
			BtBriefingGenerationDirectory.Text = "...";
			BtBriefingGenerationDirectory.UseVisualStyleBackColor = true;
			BtBriefingGenerationDirectory.Click += BtBriefingGenerationDirectory_Click;
			// 
			// LbBriefingGenerationDirectory
			// 
			LbBriefingGenerationDirectory.AutoSize = true;
			LbBriefingGenerationDirectory.Location = new Point(14, 200);
			LbBriefingGenerationDirectory.Margin = new Padding(4, 0, 4, 0);
			LbBriefingGenerationDirectory.Name = "LbBriefingGenerationDirectory";
			LbBriefingGenerationDirectory.Size = new Size(115, 15);
			LbBriefingGenerationDirectory.TabIndex = 36;
			LbBriefingGenerationDirectory.Text = "Generation directory";
			// 
			// TbBriefingGenerationDirectory
			// 
			TbBriefingGenerationDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbBriefingGenerationDirectory.Location = new Point(140, 196);
			TbBriefingGenerationDirectory.Margin = new Padding(4, 3, 4, 3);
			TbBriefingGenerationDirectory.Name = "TbBriefingGenerationDirectory";
			TbBriefingGenerationDirectory.Size = new Size(316, 23);
			TbBriefingGenerationDirectory.TabIndex = 37;
			// 
			// LbBriefingHtmlCss
			// 
			LbBriefingHtmlCss.AutoSize = true;
			LbBriefingHtmlCss.Location = new Point(9, 139);
			LbBriefingHtmlCss.Margin = new Padding(4, 0, 4, 0);
			LbBriefingHtmlCss.Name = "LbBriefingHtmlCss";
			LbBriefingHtmlCss.Size = new Size(52, 15);
			LbBriefingHtmlCss.TabIndex = 54;
			LbBriefingHtmlCss.Text = "Css style";
			// 
			// CbBriefingHtmlCss
			// 
			CbBriefingHtmlCss.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbBriefingHtmlCss.FormattingEnabled = true;
			CbBriefingHtmlCss.Location = new Point(140, 136);
			CbBriefingHtmlCss.Name = "CbBriefingHtmlCss";
			CbBriefingHtmlCss.Size = new Size(410, 23);
			CbBriefingHtmlCss.TabIndex = 53;
			// 
			// FrmPreferences
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(568, 342);
			Controls.Add(TcMain);
			Controls.Add(BtDefaults);
			Controls.Add(BtCancel);
			Controls.Add(BtOk);
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmPreferences";
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Preferences";
			Shown += FrmPreferences_Shown;
			PnApplication.ResumeLayout(false);
			PnApplication.PerformLayout();
			PnMap.ResumeLayout(false);
			PnMap.PerformLayout();
			((System.ComponentModel.ISupportInitialize)NudMapZoom).EndInit();
			TcMain.ResumeLayout(false);
			TpApplication.ResumeLayout(false);
			TpApplication.PerformLayout();
			TpMission.ResumeLayout(false);
			TpMission.PerformLayout();
			TpBriefing.ResumeLayout(false);
			TpBriefing.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.Button BtOk;
		private System.Windows.Forms.Label LbApplicationProxy;
		private System.Windows.Forms.Panel PnApplication;
		private System.Windows.Forms.Button BtApplicationWorkingDirectorySelect;
		private System.Windows.Forms.TextBox TbApplicationWorkingDirectory;
		private System.Windows.Forms.Label LbApplicationWorkingDirectory;
		private System.Windows.Forms.Button BtApplicationRecentMiz;
		private System.Windows.Forms.TextBox TbApplicationRecentMiz;
		private System.Windows.Forms.Label LbApplicationRecentMiz;
		private System.Windows.Forms.CheckBox CkApplicationGenerateBatch;
		private System.Windows.Forms.CheckBox CkApplicationMizBackup;
		private System.Windows.Forms.Panel PnMap;
		private System.Windows.Forms.ComboBox CbMapProvider;
		private System.Windows.Forms.Label LbMapZoom;
		private System.Windows.Forms.Label LbMapProvider;
		private System.Windows.Forms.Label LbTitleMap;
		private System.Windows.Forms.Button BtDefaults;
		private System.Windows.Forms.Button BtApplicationWorkingDirectoryReset;
		private System.Windows.Forms.NumericUpDown NudMapZoom;
		private CheckBox CkMissionNoCallsignForPlayable;
		private CheckedListBox LstBriefingCoordinateDisplay;
		private ComboBox CbBriefingWeatherDisplay;
		private Label LbBriefingCoordinateDisplay;
		private Label LbBriefingWeatherDisplay;
		private ComboBox CbBriefingMeasurementSystem;
		private Label LbBriefingMeasurementSystem;
		private CheckBox CkBriefingGenerateOnSave;
		private Label LbBriefingImageSize;
		private UcImageSize UcBriefingImageSize;
		private CheckBox CkBriefingGenerateDirectoryHtml;
		private Label LbMissionBullseyeWaypoint;
		private ComboBox CbMissionBullseyeWaypoint;
		private TextBox TbApplicationProxyHost;
		private Label LbApplicationProxyHost;
		private TextBox TbApplicationProxyPassword;
		private TextBox TbApplicationProxyUser;
		private Label LbApplicationProxyUser;
		private TextBox TbApplicationProxyPort;
		private TabControl TcMain;
		private TabPage TpApplication;
		private TabPage TpMission;
		private TabPage TpBriefing;
		private Button BtBriefingGenerationDirectory;
		private Label LbBriefingGenerationDirectory;
		private TextBox TbBriefingGenerationDirectory;
		private Button BtBriefingGenerationDirectoryReset;
		private Label LbBriefingHtmlCss;
		private ComboBox CbBriefingHtmlCss;
	}
}
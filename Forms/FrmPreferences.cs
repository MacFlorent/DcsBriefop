using DcsBriefop.Data;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;

namespace DcsBriefop.Forms
{
	internal partial class FrmPreferences : Form
	{
		#region Fields
		private Preferences m_preferences;
		#endregion

		#region CTOR
		public FrmPreferences()
		{
			m_preferences = PreferencesManager.Preferences.CloneJson();
			DialogResult = DialogResult.Cancel;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelHeader(LbApplicationProxy);
			ToolsStyle.LabelHeader(LbTitleMap);
			ToolsStyle.ButtonOk(BtOk);
			ToolsStyle.ButtonCancel(BtCancel);

			MapProviders.FillCombo(CbMapProvider, null);
			MasterDataRepository.FillCombo(MasterDataType.WeatherDisplay, CbBriefingWeatherDisplay, null);
			MasterDataRepository.FillCombo(MasterDataType.MeasurementSystem, CbBriefingMeasurementSystem, null);
			MasterDataRepository.FillCombo(MasterDataType.BullseyeWaypoint, CbMissionBullseyeWaypoint, null);
			MasterDataRepository.FillCheckedListBox(MasterDataType.CoordinateDisplay, LstBriefingCoordinateDisplay);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			TbApplicationWorkingDirectory.Text = m_preferences.Application.WorkingDirectory;
			TbApplicationRecentMiz.Text = string.Join(",", m_preferences.Application.RecentMiz);
			CkApplicationMizBackup.Checked = m_preferences.Application.BackupBeforeOverwrite;
			CkApplicationGenerateBatch.Checked = m_preferences.Application.GenerateBatchCommandOnSave;

			TbApplicationProxyHost.Text = m_preferences.Application.InternetProxyHost;
			TbApplicationProxyPort.Text = m_preferences.Application.InternetProxyPort.ToString();
			TbApplicationProxyUser.Text = m_preferences.Application.InternetProxyUser;
			TbApplicationProxyPassword.Text = m_preferences.Application.InternetProxyPassword;

			CbMissionBullseyeWaypoint.SelectedValue = (int)m_preferences.Mission.BullseyeWaypoint;
			CkMissionNoCallsignForPlayable.Checked = m_preferences.Mission.NoCallsignForPlayableFlights;

			CbMapProvider.SelectedItem = GMapProviders.TryGetProvider(m_preferences.Map.ProviderName);
			NudMapZoom.Value = (decimal)m_preferences.Map.Zoom;

			CbBriefingWeatherDisplay.SelectedValue = (int)m_preferences.Briefing.WeatherDisplay;
			CbBriefingMeasurementSystem.SelectedValue = (int)m_preferences.Briefing.MeasurementSystem;
			MasterDataRepository.SetFlagCheckedListbox((int)m_preferences.Briefing.CoordinateDisplay, LstBriefingCoordinateDisplay);
			UcBriefingImageSize.SelectedSize = m_preferences.Briefing.ImageSize;
			CkBriefingGenerateOnSave.Checked = m_preferences.Briefing.GenerateOnSave;
			CkBriefingGenerateDirectoryHtml.Checked = m_preferences.Briefing.GenerateDirectoryHtml;
			TbBriefingGenerationDirectory.Text = m_preferences.Briefing.GenerateDirectoryName;
		}

		private void ScreenToData()
		{
			m_preferences.Application.WorkingDirectory = TbApplicationWorkingDirectory.Text;

			if (string.IsNullOrEmpty(TbApplicationRecentMiz.Text))
				m_preferences.Application.RecentMiz.Clear();

			m_preferences.Application.BackupBeforeOverwrite = CkApplicationMizBackup.Checked;
			m_preferences.Application.GenerateBatchCommandOnSave = CkApplicationGenerateBatch.Checked;

			m_preferences.Application.InternetProxyHost = TbApplicationProxyHost.Text;
			m_preferences.Application.InternetProxyPort = null;
			if (int.TryParse(TbApplicationProxyPort.Text, out int iPort))
				m_preferences.Application.InternetProxyPort = iPort;
			m_preferences.Application.InternetProxyUser = TbApplicationProxyUser.Text;
			m_preferences.Application.InternetProxyPassword = TbApplicationProxyPassword.Text;

			m_preferences.Mission.BullseyeWaypoint = (ElementBullseyeWaypoint)CbMissionBullseyeWaypoint.SelectedValue;
			m_preferences.Mission.NoCallsignForPlayableFlights = CkMissionNoCallsignForPlayable.Checked;

			m_preferences.Map.ProviderName = (CbMapProvider.SelectedItem as GMapProvider)?.Name;
			m_preferences.Map.Zoom = (double)NudMapZoom.Value;

			m_preferences.Briefing.WeatherDisplay = (ElementWeatherDisplay)CbBriefingWeatherDisplay.SelectedValue;
			m_preferences.Briefing.MeasurementSystem = (ElementMeasurementSystem)CbBriefingMeasurementSystem.SelectedValue;
			m_preferences.Briefing.CoordinateDisplay = (ElementCoordinateDisplay)MasterDataRepository.GetFlagCheckedListbox(LstBriefingCoordinateDisplay);
			m_preferences.Briefing.ImageSize = UcBriefingImageSize.SelectedSize;
			m_preferences.Briefing.GenerateOnSave = CkBriefingGenerateOnSave.Checked;
			m_preferences.Briefing.GenerateDirectoryHtml = CkBriefingGenerateDirectoryHtml.Checked;
			m_preferences.Briefing.GenerateDirectoryName = TbBriefingGenerationDirectory.Text;
		}
		#endregion

		#region Events
		private void FrmPreferences_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void BtOk_Click(object sender, EventArgs e)
		{
			ScreenToData();
			PreferencesManager.Preferences = m_preferences.CloneJson();
			PreferencesManager.Save();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void BtCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BtDefaults_Click(object sender, EventArgs e)
		{
			m_preferences = new Preferences();
			m_preferences.InitializeDefault();
			DataToScreen();
		}

		private void BtApplicationWorkingDirectorySelect_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = TbApplicationWorkingDirectory.Text;

				if (fbd.ShowDialog() == DialogResult.OK)
				{
					TbApplicationWorkingDirectory.Text = fbd.SelectedPath;
				}
			}
		}

		private void BtApplicationWorkingDirectoryReset_Click(object sender, EventArgs e)
		{
			PreferencesApplication defaultApplication = new PreferencesApplication();
			defaultApplication.InitializeDefault();
			TbApplicationWorkingDirectory.Text = defaultApplication.WorkingDirectory;
		}

		private void BtApplicationRecentMiz_Click(object sender, EventArgs e)
		{
			TbApplicationRecentMiz.Text = null;
		}

		private void TbApplicationProxyPort_Validated(object sender, EventArgs e)
		{
			if (!int.TryParse(TbApplicationProxyPort.Text, out int iPort))
				TbApplicationProxyPort.Text = null;
		}

		private void BtBriefingGenerationDirectory_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = TbBriefingGenerationDirectory.Text;

				if (fbd.ShowDialog() == DialogResult.OK)
				{
					TbBriefingGenerationDirectory.Text = fbd.SelectedPath;
				}
			}
		}

		private void BtBriefingGenerationDirectoryReset_MouseDown(object sender, MouseEventArgs e)
		{
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.Clear();

			menu.Items.AddMenuItem("Default", (object _sender, EventArgs _e) => { TbBriefingGenerationDirectory.Text = ElementGlobalData.GenerateDirectoryNameDefault; });
			menu.Items.AddMenuItem("VEAF", (object _sender, EventArgs _e) => { TbBriefingGenerationDirectory.Text = ElementGlobalData.GenerateDirectoryNameVeaf; });

			if (menu.Items.Count > 0)
			{
				menu.Show(BtBriefingGenerationDirectoryReset, new Point(0, BtBriefingGenerationDirectoryReset.Height));
			}
		}
		#endregion
	}
}

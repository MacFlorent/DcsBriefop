using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using System;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	public partial class FrmPreferences : Form
	{
		#region Fields
		private Preferences m_preferences;
		#endregion

		#region CTOR
		public FrmPreferences()
		{
			m_preferences = PreferencesManager.Preferences.CloneJson();

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelHeader(LbTitleApplication);
			ToolsStyle.LabelHeader(LbTitleMission);
			ToolsStyle.LabelHeader(LbTitleMap);
			ToolsStyle.ButtonOk(BtOk);
			ToolsStyle.ButtonCancel(BtCancel);

			CbMapProvider.ValueMember = "Name";
			CbMapProvider.DataSource = GMapProviders.List;
			MasterDataRepository.FillCombo(MasterDataType.WeatherDisplay, CbMissionWeatherDisplay);
			MasterDataRepository.FillListBox(MasterDataType.CoordinateDisplay, LstCoordinateDisplay);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			TbApplicationWorkingDirectory.Text = m_preferences.Application.WorkingDirectory;
			TbApplicationRecentMiz.Text = string.Join(",", m_preferences.Application.RecentMiz);
			CkApplicationMizBackup.Checked = m_preferences.Application.BackupBeforeOverwrite;
			CkApplicationGenerateBatch.Checked = m_preferences.Application.GenerateBatchCommandOnSave;

			CbMissionWeatherDisplay.SelectedValue = (int)m_preferences.Mission.WeatherDisplay;
			MasterDataRepository.SetFlagCheckedListbox((int)m_preferences.Mission.CoordinateDisplay, LstCoordinateDisplay);

			CbMapProvider.SelectedItem = GMapProviders.TryGetProvider(m_preferences.Map.DefaultProvider);
			NudMapZoom.Value = (decimal)m_preferences.Map.DefaultZoom;
		}

		private void ScreenToData()
		{
			m_preferences.Application.WorkingDirectory = TbApplicationWorkingDirectory.Text;

			if (string.IsNullOrEmpty(TbApplicationRecentMiz.Text))
				m_preferences.Application.RecentMiz.Clear();

			m_preferences.Application.BackupBeforeOverwrite = CkApplicationMizBackup.Checked;
			m_preferences.Application.GenerateBatchCommandOnSave = CkApplicationGenerateBatch.Checked;

			m_preferences.Mission.WeatherDisplay = (ElementWeatherDisplay)CbMissionWeatherDisplay.SelectedValue;
			m_preferences.Mission.CoordinateDisplay = (ElementCoordinateDisplay)MasterDataRepository.GetFlagCheckedListbox(LstCoordinateDisplay);

			m_preferences.Map.DefaultProvider = (CbMapProvider.SelectedItem as GMapProvider)?.Name;
			m_preferences.Map.DefaultZoom = (double)NudMapZoom.Value;
		}
		#endregion

		#region Events
		private void BtOk_Click(object sender, EventArgs e)
		{
			ScreenToData();
			PreferencesManager.Preferences = m_preferences.CloneJson();
			PreferencesManager.Save();
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
		#endregion


	}
}

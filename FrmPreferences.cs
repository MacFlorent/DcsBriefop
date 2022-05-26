using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using GMap.NET.MapProviders;
using System;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmPreferences : Form
	{
		#region Fields
		private GridFileTypeManager m_gridFileTypeManager;
		#endregion

		#region CTOR
		public FrmPreferences()
		{
			InitializeComponent();

			ToolsStyle.ApplyStyle(this);
			ToolsStyle.ButtonOk(BtSave);
			ToolsStyle.ButtonCancel(BtCancel);

			ToolsControls.SetDataGridViewProperties(DgvFileTypes);

			CbMapProvider.ValueMember = "Name";
			CbMapProvider.DataSource = GMapProviders.List;

			m_gridFileTypeManager = new GridFileTypeManager(DgvFileTypes);
			m_gridFileTypeManager.Initialize();

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CkNoCallsignForPlayableFlights.Checked = Preferences.PreferencesManager.Preferences.General.NoCallsignForPlayableFlights;
			CkBackupBeforeOverwrite.Checked = Preferences.PreferencesManager.Preferences.General.BackupBeforeOverwrite;
			CkGenerateBatchCommandOnSave.Checked = Preferences.PreferencesManager.Preferences.General.GenerateBatchCommandOnSave;

			CbMapProvider.SelectedItem = GMapProviders.TryGetProvider(Preferences.PreferencesManager.Preferences.Map.DefaultProvider);
			UdMapDefaultZoom.Value = (decimal)Preferences.PreferencesManager.Preferences.Map.DefaultZoom;
			
			CkGenerateOnSave.Checked = Preferences.PreferencesManager.Preferences.Generation.ExportOnSave;
			CkMizFile.Checked = Preferences.PreferencesManager.Preferences.Generation.ExportMiz;
			CkLocalDirectory.Checked = Preferences.PreferencesManager.Preferences.Generation.ExportLocalDirectory;
			CkLocalDirectoryHtml.Checked = Preferences.PreferencesManager.Preferences.Generation.ExportLocalDirectoryHtml;

			m_gridFileTypeManager.SelectedExportFileTypes = Preferences.PreferencesManager.Preferences.Generation.ExportFileTypes;

			UcDefaultImageSize.SelectedSize = Preferences.PreferencesManager.Preferences.Generation.ExportImageSize;
			UcImageBackgroundColor.SelectedColorHtml = Preferences.PreferencesManager.Preferences.Generation.ExportImageBackgroundColor;
		}

		private void ScreenToData()
		{
			Preferences.PreferencesManager.Preferences.General.NoCallsignForPlayableFlights = CkNoCallsignForPlayableFlights.Checked;
			Preferences.PreferencesManager.Preferences.General.BackupBeforeOverwrite = CkBackupBeforeOverwrite.Checked;
			Preferences.PreferencesManager.Preferences.General.GenerateBatchCommandOnSave = CkGenerateBatchCommandOnSave.Checked;

			Preferences.PreferencesManager.Preferences.Map.DefaultProvider = (CbMapProvider.SelectedItem as GMapProvider)?.Name;
			Preferences.PreferencesManager.Preferences.Map.DefaultZoom = (double)UdMapDefaultZoom.Value;
			
			Preferences.PreferencesManager.Preferences.Generation.ExportOnSave = CkGenerateOnSave.Checked;
			Preferences.PreferencesManager.Preferences.Generation.ExportMiz = CkMizFile.Checked;
			Preferences.PreferencesManager.Preferences.Generation.ExportLocalDirectory = CkLocalDirectory.Checked;
			Preferences.PreferencesManager.Preferences.Generation.ExportLocalDirectoryHtml = CkLocalDirectoryHtml.Checked;

			Preferences.PreferencesManager.Preferences.Generation.ExportFileTypes = m_gridFileTypeManager.SelectedExportFileTypes;

			Preferences.PreferencesManager.Preferences.Generation.ExportImageSize = UcDefaultImageSize.SelectedSize;
			Preferences.PreferencesManager.Preferences.Generation.ExportImageBackgroundColor = UcImageBackgroundColor.SelectedColorHtml;
		}
		#endregion

		#region Events
		private void BtSave_Click(object sender, EventArgs e)
		{
			ScreenToData();
			Preferences.PreferencesManager.Save();
			Close();
		}

		private void BtCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}

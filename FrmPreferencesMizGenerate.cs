using DcsBriefop.Data;
using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmPreferencesMizGenerate : Form
	{
		#region Fields
		private GridFileTypeManager m_gridFileTypeManager;
		private BriefingContainer m_briefingContainer;
		private MissionManager m_missionManager;
		#endregion

		#region CTOR
		public FrmPreferencesMizGenerate(BriefingContainer briefingContainer, MissionManager missionManager)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.ButtonOk(BtGenerate);

			m_briefingContainer = briefingContainer;
			m_missionManager = missionManager;

			ToolsMisc.SetDataGridViewProperties(DgvFileTypes);
			m_gridFileTypeManager = new GridFileTypeManager(DgvFileTypes);
			m_gridFileTypeManager.Initialize();

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CkGenerateOnSave.Checked = m_missionManager.Miz.BriefopCustomData.ExportOnSave;
			CkMizFile.Checked = m_missionManager.Miz.BriefopCustomData.ExportMiz;
			CkLocalDirectory.Checked = m_missionManager.Miz.BriefopCustomData.ExportLocalDirectory;
			CkLocalDirectoryHtml.Checked = m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryHtml;

			if (string.IsNullOrEmpty(m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryPath))
				TbLocalDirectory.Text = m_missionManager.MizFileDirectory;
			else
				TbLocalDirectory.Text = m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryPath;

			UcExportImageSize.SelectedSize = m_missionManager.Miz.BriefopCustomData.ExportImageSize;
			UcImageBackgroundColor.SelectedColorHtml = m_missionManager.Miz.BriefopCustomData.ExportImageBackgroundColor;

			m_gridFileTypeManager.SelectedExportFileTypes = m_missionManager.Miz.BriefopCustomData.ExportFileTypes;

			DisplayCurrentLocaDirectory();
		}

		private void ScreenToData()
		{
			m_missionManager.Miz.BriefopCustomData.ExportOnSave = CkGenerateOnSave.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportMiz = CkMizFile.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportLocalDirectory = CkLocalDirectory.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryHtml = CkLocalDirectoryHtml.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryPath = TbLocalDirectory.Text;

			m_missionManager.Miz.BriefopCustomData.ExportImageSize = UcExportImageSize.SelectedSize;
			m_missionManager.Miz.BriefopCustomData.ExportImageBackgroundColor = UcImageBackgroundColor.SelectedColorHtml;

			m_missionManager.Miz.BriefopCustomData.ExportFileTypes = m_gridFileTypeManager.SelectedExportFileTypes;
		}

		private void DisplayCurrentLocaDirectory()
		{
			CkLocalDirectoryHtml.Visible = TbLocalDirectory.Visible = BtLocalDirectoryBrowse.Visible = BtLocalDirectoryReset.Visible = CkLocalDirectory.Checked;
		}
		#endregion

		#region Events
		private void BtGenerate_Click(object sender, System.EventArgs e)
		{
			ScreenToData();

			using (new WaitDialog(this))
			using (BriefingFilesBuilder builder = new BriefingFilesBuilder(m_briefingContainer, m_missionManager))
			{
				builder.Generate();
			}
		}

		private void BtClose_Click(object sender, System.EventArgs e)
		{
			ScreenToData();
			Close();
		}

		private void BtDirectoryBrowse_Click(object sender, System.EventArgs e)
		{
			using (var fbd = new FolderBrowserDialog())
			{
				fbd.SelectedPath = TbLocalDirectory.Text;
				DialogResult result = fbd.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
				{
					TbLocalDirectory.Text = fbd.SelectedPath;
				}
			}
		}

		private void BtDirectoryReset_Click(object sender, System.EventArgs e)
		{
			TbLocalDirectory.Text = m_missionManager.MizFileDirectory;
		}

		private void CkLocalDirectory_CheckedChanged(object sender, System.EventArgs e)
		{
			DisplayCurrentLocaDirectory();
		}
		#endregion
	}
}

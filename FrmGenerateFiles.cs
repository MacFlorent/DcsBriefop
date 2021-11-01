using DcsBriefop.Briefing;
using DcsBriefop.Tools;
using System.IO;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmGenerateFiles : Form
	{
		#region Fields
		BriefingPack m_briefingPack;
		MissionManager m_missionManager;
		#endregion

		#region CTOR
		public FrmGenerateFiles(BriefingPack briefingPack, MissionManager missionManager)
		{
			InitializeComponent();

			m_briefingPack = briefingPack;
			m_missionManager = missionManager;

			if (string.IsNullOrEmpty(m_missionManager.ExportLocalDirectoryPath))
				m_missionManager.ExportLocalDirectoryPath = m_missionManager.MizFileDirectory;

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CkMizFile.Checked = m_missionManager.ExportMizActive;
			CkLocalDirectory.Checked = m_missionManager.ExportLocalDirectoryActive;
			CkLocalDirectoryHtmlBitmaps.Checked = m_missionManager.ExportLocalDirectoryBitmaps;
			TbLocalDirectory.Text = m_missionManager.ExportLocalDirectoryPath;

			DisplayCurrentLocaDirectory();
		}

		private void ScreenToData()
		{
			m_missionManager.ExportMizActive = CkMizFile.Checked;
			m_missionManager.ExportLocalDirectoryActive = CkLocalDirectory.Checked;
			m_missionManager.ExportLocalDirectoryBitmaps = CkLocalDirectoryHtmlBitmaps.Checked;
			m_missionManager.ExportLocalDirectoryPath = TbLocalDirectory.Text;
		}

		private void DisplayCurrentLocaDirectory()
		{
			CkLocalDirectoryHtmlBitmaps.Visible = TbLocalDirectory.Visible = BtLocalDirectoryBrowse.Visible = BtLocalDirectoryReset.Visible = CkLocalDirectory.Checked;
		}
		#endregion

		#region Events
		private void BtGenerate_Click(object sender, System.EventArgs e)
		{
			using (BriefingFilesBuilder builder = new BriefingFilesBuilder(m_briefingPack, m_missionManager))
			{
				builder.Generate(CkMizFile.Checked, CkLocalDirectory.Checked, CkLocalDirectoryHtmlBitmaps.Checked, TbLocalDirectory.Text);
			}

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

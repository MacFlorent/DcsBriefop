using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmGenerateFiles : Form
	{
		private static class GridColumn
		{
			public static readonly string Selected = "Selected";
			public static readonly string FileType = "FileType";
			public static readonly string Data = "Data";
		}

		#region Fields
		BriefingContainer m_briefingContainer;
		MissionManager m_missionManager;
		#endregion

		#region CTOR
		public FrmGenerateFiles(BriefingContainer briefingContainer, MissionManager missionManager)
		{
			InitializeComponent();

			m_briefingContainer = briefingContainer;
			m_missionManager = missionManager;

			if (string.IsNullOrEmpty(m_missionManager.ExportLocalDirectoryPath))
				m_missionManager.ExportLocalDirectoryPath = m_missionManager.MizFileDirectory;

			ToolsMisc.SetDataGridViewProperties(DgvFileTypes);
			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CkMizFile.Checked = m_missionManager.ExportMizActive;
			CkLocalDirectory.Checked = m_missionManager.ExportLocalDirectoryActive;
			CkLocalDirectoryHtml.Checked = m_missionManager.ExportLocalDirectoryBitmaps;
			TbLocalDirectory.Text = m_missionManager.ExportLocalDirectoryPath;

			DataToScreenFileTypes();
			DisplayCurrentLocaDirectory();
		}

		private void DataToScreenFileTypes()
		{
			DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn() { Name = GridColumn.Selected, HeaderText = "Selected" };
			DgvFileTypes.Columns.Add(col);
			DgvFileTypes.Columns.Add(GridColumn.FileType, "Biefop file");
			DgvFileTypes.Columns.Add(GridColumn.Data, "Data");

			DgvFileTypes.Columns[GridColumn.Selected].ValueType = typeof(bool);
			DgvFileTypes.Columns[GridColumn.FileType].ReadOnly = true;
			DgvFileTypes.Columns[GridColumn.Data].Visible = false;

			foreach (MasterDataObject fileType in MasterDataRepository.GetMasterDataList(MasterDataType.ExportFileType))
			{
				RefreshGridRowFileTypes(fileType);
			}
		}

		private void RefreshGridRowFileTypes(MasterDataObject fileType)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in DgvFileTypes.Rows)
			{
				if ((int)existingRow.Cells[GridColumn.Data].Value == fileType.Id)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvFileTypes.Rows.Add();
				dgvr = DgvFileTypes.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = fileType.Id;
			}

			dgvr.Cells[GridColumn.Selected].Value = m_missionManager.ExportFileTypes.Contains((ElementExportFileType)fileType.Id);
			dgvr.Cells[GridColumn.FileType].Value = fileType.Label;
		}

		private void ScreenToData()
		{
			m_missionManager.ExportMizActive = CkMizFile.Checked;
			m_missionManager.ExportLocalDirectoryActive = CkLocalDirectory.Checked;
			m_missionManager.ExportLocalDirectoryBitmaps = CkLocalDirectoryHtml.Checked;
			m_missionManager.ExportLocalDirectoryPath = TbLocalDirectory.Text;

			m_missionManager.ExportFileTypes = GetListSelectedFileTypes();
		}

		private List<ElementExportFileType> GetListSelectedFileTypes()
		{
			List<ElementExportFileType> list = new List<ElementExportFileType>();
			foreach (DataGridViewRow dgvr in DgvFileTypes.Rows)
			{
				if ((bool)dgvr.Cells[GridColumn.Selected].Value)
				{
					list.Add((ElementExportFileType)dgvr.Cells[GridColumn.Data].Value);
				}
			}

			return list;
		}

		private void DisplayCurrentLocaDirectory()
		{
			CkLocalDirectoryHtml.Visible = TbLocalDirectory.Visible = BtLocalDirectoryBrowse.Visible = BtLocalDirectoryReset.Visible = CkLocalDirectory.Checked;
		}
		#endregion

		#region Events
		private void BtGenerate_Click(object sender, System.EventArgs e)
		{
			using (BriefingFilesBuilder builder = new BriefingFilesBuilder(m_briefingContainer, m_missionManager))
			{
				builder.Generate(GetListSelectedFileTypes(), CkMizFile.Checked, CkLocalDirectory.Checked, CkLocalDirectoryHtml.Checked, TbLocalDirectory.Text);
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

using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Drawing;
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
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.ButtonOk(BtGenerate);

			m_briefingContainer = briefingContainer;
			m_missionManager = missionManager;

			ToolsMisc.SetDataGridViewProperties(DgvFileTypes);
			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			UdImageWidth.ValueChanged -= UdImageWidth_ValueChanged;
			UdImageHeight.ValueChanged -= UdImageHeight_ValueChanged;
			UdImageRatio.ValueChanged -= UdImageRatio_ValueChanged;

			CkGenerateOnSave.Checked = m_missionManager.Miz.BriefopCustomData.ExportOnSave;
			CkMizFile.Checked = m_missionManager.Miz.BriefopCustomData.ExportMiz;
			CkLocalDirectory.Checked = m_missionManager.Miz.BriefopCustomData.ExportLocalDirectory;
			CkLocalDirectoryHtml.Checked = m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryHtml;

			if (string.IsNullOrEmpty(m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryPath))
				TbLocalDirectory.Text = m_missionManager.MizFileDirectory;
			else
				TbLocalDirectory.Text = m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryPath;

			UdImageWidth.Value = m_missionManager.Miz.BriefopCustomData.ExportImageSize.Width;
			UdImageHeight.Value = m_missionManager.Miz.BriefopCustomData.ExportImageSize.Height;
			UdImageRatio.Value = (decimal)m_missionManager.Miz.BriefopCustomData.ExportImageSize.Width / (decimal)m_missionManager.Miz.BriefopCustomData.ExportImageSize.Height;
			CkImageRatioLock.Checked = true;
			UcImageBackgroundColor.SelectedColor = ColorTranslator.FromHtml(m_missionManager.Miz.BriefopCustomData.ExportImageBackgroundColor);

			DataToScreenFileTypes();
			DisplayCurrentLocaDirectory();

			UdImageWidth.ValueChanged += UdImageWidth_ValueChanged;
			UdImageHeight.ValueChanged += UdImageHeight_ValueChanged;
			UdImageRatio.ValueChanged += UdImageRatio_ValueChanged;
		}

		private void DataToScreenFileTypes()
		{
			DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn() { Name = GridColumn.Selected, HeaderText = "Selected" };
			DgvFileTypes.Columns.Add(col);
			DgvFileTypes.Columns.Add(GridColumn.FileType, "Briefop file");
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

			dgvr.Cells[GridColumn.Selected].Value = m_missionManager.Miz.BriefopCustomData.ExportFileTypes.Contains((ElementExportFileType)fileType.Id);
			dgvr.Cells[GridColumn.FileType].Value = fileType.Label;
		}

		private void ScreenToData()
		{
			m_missionManager.Miz.BriefopCustomData.ExportOnSave = CkGenerateOnSave.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportMiz = CkMizFile.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportLocalDirectory = CkLocalDirectory.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryHtml = CkLocalDirectoryHtml.Checked;
			m_missionManager.Miz.BriefopCustomData.ExportLocalDirectoryPath = TbLocalDirectory.Text;

			m_missionManager.Miz.BriefopCustomData.ExportImageSize = new Size((int)UdImageWidth.Value, (int)UdImageHeight.Value);

			if (UcImageBackgroundColor.SelectedColor is object)
				m_missionManager.Miz.BriefopCustomData.ExportImageBackgroundColor = ColorTranslator.ToHtml(UcImageBackgroundColor.SelectedColor.Value);
			else
				m_missionManager.Miz.BriefopCustomData.ExportImageBackgroundColor = null;

			m_missionManager.Miz.BriefopCustomData.ExportFileTypes = GetListSelectedFileTypes();
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

		private void UdImageWidth_ValueChanged(object sender, System.EventArgs e)
		{
			if (CkImageRatioLock.Checked)
			{
				decimal dRatio = (UdImageRatio.Value as decimal?).GetValueOrDefault(1);
				decimal dWidth = (UdImageWidth.Value as decimal?).GetValueOrDefault(0);
				decimal dHeight = dWidth / dRatio;
				UdImageHeight.Value = dHeight;
			}
			else
			{
				decimal dWidth = (UdImageWidth.Value as decimal?).GetValueOrDefault(0);
				decimal dHeight = (UdImageHeight.Value as decimal?).GetValueOrDefault(1);
				decimal dRatio = dWidth / dHeight;
				UdImageRatio.Value = dRatio;
			}
		}

		private void UdImageHeight_ValueChanged(object sender, System.EventArgs e)
		{
			if (CkImageRatioLock.Checked)
			{
				decimal dRatio = (UdImageRatio.Value as decimal?).GetValueOrDefault(1);
				decimal dHeight = (UdImageHeight.Value as decimal?).GetValueOrDefault(0);
				decimal dWidth = dHeight * dRatio;
				UdImageWidth.Value = dWidth;
			}
			else
			{
				decimal dWidth = (UdImageWidth.Value as decimal?).GetValueOrDefault(0);
				decimal dHeight = (UdImageHeight.Value as decimal?).GetValueOrDefault(1);
				decimal dRatio = dWidth / dHeight;
				UdImageRatio.Value = dRatio;
			}
		}

		private void UdImageRatio_ValueChanged(object sender, System.EventArgs e)
		{
			decimal dRatio = (UdImageRatio.Value as decimal?).GetValueOrDefault(1);
			decimal dWidth = (UdImageWidth.Value as decimal?).GetValueOrDefault(0);
			decimal dHeight = dWidth / dRatio;
			UdImageHeight.Value = dHeight;
		}
		#endregion
	}
}

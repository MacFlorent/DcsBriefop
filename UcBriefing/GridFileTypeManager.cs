using DcsBriefop.Data;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class GridFileTypeManager
	{
		private static class GridColumn
		{
			public static readonly string Selected = "Selected";
			public static readonly string FileType = "FileType";
			public static readonly string Data = "Data";
		}

		#region Fields
		private DataGridView m_dgv;
		#endregion

		#region Properties
		private List<ElementExportFileType> m_selectedExportFileTypes = new List<ElementExportFileType>();
		public List<ElementExportFileType> SelectedExportFileTypes
		{
			get
			{
				m_selectedExportFileTypes = new List<ElementExportFileType>();
				foreach (DataGridViewRow dgvr in m_dgv.Rows)
				{
					if ((bool)dgvr.Cells[GridColumn.Selected].Value)
					{
						m_selectedExportFileTypes.Add((ElementExportFileType)dgvr.Cells[GridColumn.Data].Value);
					}
				}

				return m_selectedExportFileTypes;
			}
			set
			{
				m_selectedExportFileTypes = value;
				Fill();
			}
		}
		#endregion

		#region CTOR
		public GridFileTypeManager(DataGridView dgvFileType)
		{
			m_dgv = dgvFileType;
		}
		#endregion

		#region Methods
		public void Initialize()
		{
			InitializeColumns();
			Fill();
		}

		private void InitializeColumns()
		{
			m_dgv.Columns.Clear();

			DataGridViewCheckBoxColumn colSelected = new DataGridViewCheckBoxColumn() { Name = GridColumn.Selected, HeaderText = "Selected" };
			colSelected.ValueType = typeof(bool);

			m_dgv.Columns.Add(colSelected);
			m_dgv.Columns.Add(GridColumn.FileType, "Briefop file");
			m_dgv.Columns.Add(GridColumn.Data, "Data");

			m_dgv.Columns[GridColumn.FileType].ReadOnly = true;
			m_dgv.Columns[GridColumn.Data].Visible = false;
		}

		private void Fill()
		{
			if (m_dgv.Columns.Count <= 0)
				return;

			m_dgv.Rows.Clear();

			foreach (MasterDataObject fileType in MasterDataRepository.GetMasterDataList(MasterDataType.ExportFileType))
			{
				RefreshRow(fileType);
			}
		}
		private void RefreshRow(MasterDataObject fileType)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in m_dgv.Rows)
			{
				if ((int)existingRow.Cells[GridColumn.Data].Value == fileType.Id)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = m_dgv.Rows.Add();
				dgvr = m_dgv.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = fileType.Id;
			}

			dgvr.Cells[GridColumn.Selected].Value = m_selectedExportFileTypes.Contains((ElementExportFileType)fileType.Id);
			dgvr.Cells[GridColumn.FileType].Value = fileType.Label;
		}
		#endregion

		#region Events
		#endregion
	}
}

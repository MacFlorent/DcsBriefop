using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmComs : Form
	{
		private static class GridColumn
		{
			public static readonly string PresetNumber = "PresetNumber";
			public static readonly string Frequency = "Frequency";
			public static readonly string Modulation = "Modulation";
			public static readonly string Group = "Group";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}

		#region Fields
		private BriefingCoalition m_briefingCoalition;
		private ListComPreset m_listComPresets;
		#endregion

		#region CTOR
		public FrmComs(BriefingCoalition briefingCoalition)
		{
			InitializeComponent();

			m_briefingCoalition = briefingCoalition;
			if (m_briefingCoalition.ComPresets is object && m_briefingCoalition.ComPresets.Count > 0)
				m_listComPresets = m_briefingCoalition.ComPresets.GetCopy();
			else
			{
				m_listComPresets = new ListComPreset();
				m_listComPresets.InitializeDefault();
			}

			ToolsMisc.SetDataGridViewProperties(DgvRadio1);
			DgvRadio1.CellEndEdit += DgvCellEndEdit;
			ToolsMisc.SetDataGridViewProperties(DgvRadio2);
			DgvRadio2.CellEndEdit += DgvCellEndEdit;

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			DataToScreenGrid(DgvRadio1, 1);
			DataToScreenGrid(DgvRadio2, 2);
		}

		private void DataToScreenGrid(DataGridView dgv, int iRadio)
		{
			DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn() { Name = GridColumn.Modulation, HeaderText = "Mod." };
			col.DataSource = MasterDataRepository.GetMasterDataList(MasterDataType.RadioModulation);
			col.ValueMember = "Id";
			col.DisplayMember = "Label";

			dgv.Columns.Add(GridColumn.PresetNumber, "Num.");
			dgv.Columns.Add(GridColumn.Frequency, "Frequency");
			dgv.Columns.Add(col);
			dgv.Columns.Add(GridColumn.Group, "Group ID");
			dgv.Columns.Add(GridColumn.Notes, "Notes");
			dgv.Columns.Add(GridColumn.Data, null);

			dgv.Columns[GridColumn.PresetNumber].ReadOnly = true;
			dgv.Columns[GridColumn.Frequency].ValueType = typeof(decimal);
			dgv.Columns[GridColumn.Group].ValueType = typeof(int);
			dgv.Columns[GridColumn.Data].Visible = false;

			foreach (ComPreset preset in m_listComPresets.Where(_p => _p.PresetRadio == iRadio))
			{
				RefreshGridRow(dgv, preset);
			}
		}

		private void RefreshGridRow(DataGridView dgv, ComPreset preset)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in dgv.Rows)
			{
				if ((ComPreset)existingRow.Cells[GridColumn.Data].Value == preset)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = dgv.Rows.Add();
				dgvr = dgv.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = preset;
			}

			dgvr.Cells[GridColumn.PresetNumber].Value = preset.PresetNumber;
			dgvr.Cells[GridColumn.Frequency].Value = preset.Radio.Frequency;
			(dgvr.Cells[GridColumn.Modulation] as DataGridViewComboBoxCell).Value = preset.Radio.Modulation;
		}

		private void ScreenToData()
		{
			m_briefingCoalition.ComPresets = m_listComPresets;
		}
		#endregion

		#region Events
		private void DgvCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView grid = (sender as DataGridView);
			if (grid is null)
				return;

			ComPreset preset = grid.Rows[e.RowIndex].Cells[GridColumn.Data].Value as ComPreset;

			DataGridViewColumn column = grid.Columns[e.ColumnIndex];
			DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];

			if (column.Name == GridColumn.Frequency)
				preset.Radio.Frequency = (decimal)cell.Value;
			else if (column.Name == GridColumn.Modulation)
				preset.Radio.Modulation = (int)(cell as DataGridViewComboBoxCell).Value;
		}


		private void BtCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void BtOk_Click(object sender, System.EventArgs e)
		{
			ScreenToData();
			Close();
		}
		#endregion
	}
}

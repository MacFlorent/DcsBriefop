using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
			public static readonly string Mode = "Mode";
			public static readonly string Asset = "Asset";
			public static readonly string Label = "Notes";
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
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelHeader(LbRadio1);
			ToolsStyle.LabelHeader(LbRadio2);
			ToolsStyle.ButtonOk(BtOk);
			ToolsStyle.ButtonCancel(BtCancel);

			m_briefingCoalition = briefingCoalition;
			if (m_briefingCoalition is object && m_briefingCoalition.ComPresets is object && m_briefingCoalition.ComPresets.Count > 0)
				m_listComPresets = m_briefingCoalition.ComPresets.GetCopy();
			else
			{
				m_listComPresets = new ListComPreset();
				m_listComPresets.InitializeEmpty();
			}

			ToolsControls.SetDataGridViewProperties(DgvRadio1);
			DgvRadio1.CellEndEdit += DgvCellEndEdit;
			InitializeContextMenu(DgvRadio1);
			ToolsControls.SetDataGridViewProperties(DgvRadio2);
			DgvRadio2.CellEndEdit += DgvCellEndEdit;
			InitializeContextMenu(DgvRadio2);

			StringBuilder sb = new StringBuilder();
			sb.AppendLine("For compatibility with all playable airframes use the following:");
			sb.AppendLine("  Radio 1 is UHF AM only");
			sb.AppendLine("  Radio 2 is UHF/VHF AM only");
			sb.AppendLine("Usable ranges are the following with 0.025 Mhz increments:");
			sb.AppendLine("  VHF FM range from: 30.000 MHz to 87.975 MHz");
			sb.AppendLine("  VHF AM range from: 118.000 MHz to 135.975 MHz");
			sb.AppendLine("  VHF AM / FM range from: 136.000 MHz to 155.975 MHz");
			sb.AppendLine("  VHF FM range from: 156.000 MHz to 173.975 MHz");
			sb.AppendLine("  UHF AM range from: 225.000 MHz to 399.975 MHz");
			TbLegend.Text = sb.ToString();

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
			dgv.Columns.Clear();

			DataGridViewComboBoxColumn colModulation = new DataGridViewComboBoxColumn() { Name = GridColumn.Modulation, HeaderText = "Mod." };
			colModulation.DataSource = MasterDataRepository.GetMasterDataList(MasterDataType.RadioModulation);
			colModulation.ValueMember = MasterDataColumn.Id;
			colModulation.DisplayMember = MasterDataColumn.Label;

			dgv.Columns.Add(GridColumn.PresetNumber, "Num.");
			dgv.Columns.Add(GridColumn.Mode, "Mode");
			dgv.Columns.Add(GridColumn.Asset, "Asset ID");
			dgv.Columns.Add(GridColumn.Label, "Label");
			dgv.Columns.Add(GridColumn.Frequency, "Frequency");
			dgv.Columns.Add(colModulation);
			dgv.Columns.Add(GridColumn.Data, null);

			dgv.Columns[GridColumn.PresetNumber].ReadOnly = true;
			dgv.Columns[GridColumn.Mode].ReadOnly = true;
			dgv.Columns[GridColumn.Asset].ValueType = typeof(int);
			dgv.Columns[GridColumn.Frequency].ValueType = typeof(decimal);
			dgv.Columns[GridColumn.Label].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
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

			RefreshGridRowContent(dgvr, GridColumn.PresetNumber, preset.PresetNumber);
			RefreshGridRowContent(dgvr, GridColumn.Mode, MasterDataRepository.GetById(MasterDataType.ComPresetMode, (int)preset.Mode)?.Label);
			RefreshGridRowContent(dgvr, GridColumn.Asset, preset.AssetId);
			RefreshGridRowContent(dgvr, GridColumn.Label, preset.Label);
			RefreshGridRowContent(dgvr, GridColumn.Frequency, preset.Radio?.Frequency);
			RefreshGridRowContent(dgvr, GridColumn.Modulation, preset.Radio?.Modulation);

			if (preset.Mode == ElementComPresetMode.Free)
			{
				dgvr.Cells[GridColumn.Asset].ReadOnly = true;
				dgvr.Cells[GridColumn.Label].ReadOnly = false;
				dgvr.Cells[GridColumn.Frequency].ReadOnly = false;
				dgvr.Cells[GridColumn.Modulation].ReadOnly = false;
			}
			else
			{
				dgvr.Cells[GridColumn.Asset].ReadOnly = false;
				dgvr.Cells[GridColumn.Label].ReadOnly = true;

				Asset asset = preset.GetAsset(m_briefingCoalition);
				if (asset is AssetAirdrome airdrome && (airdrome.Radios is null || airdrome.Radios.Count <= 0))
				{
					dgvr.Cells[GridColumn.Frequency].ReadOnly = false;
					dgvr.Cells[GridColumn.Modulation].ReadOnly = false;
				}
				else
				{
					dgvr.Cells[GridColumn.Frequency].ReadOnly = true;
					dgvr.Cells[GridColumn.Modulation].ReadOnly = true;
				}
			}
		}

		private void RefreshGridRowContent(DataGridViewRow dgvr, string sColumn, object value)
		{
			if (dgvr.DataGridView.Columns.Contains(sColumn))
			{
				if (dgvr.Cells[sColumn] is DataGridViewComboBoxCell dgvcCombo)
					dgvcCombo.Value = value;
				else
					dgvr.Cells[sColumn].Value = value;
			}
		}

		private void ScreenToData()
		{
			m_briefingCoalition.ComPresets = m_listComPresets;
			m_briefingCoalition.ComPresets?.Compute(m_briefingCoalition);
		}

		private void SetMode(List<ComPreset> presets, ElementComPresetMode mode, DataGridView dgv)
		{
			foreach (ComPreset preset in presets)
			{
				if (preset.Mode != mode)
				{
					preset.Mode = mode;
					preset.Compute(m_briefingCoalition);
					RefreshGridRow(dgv, preset);
				}
			}
		}

		private void SetRadio(ComPreset preset, Radio radio, DataGridView dgv)
		{
			preset.Radio = radio.GetCopy();
			RefreshGridRow(dgv, preset);
		}
		#endregion

		#region Menus
		private void InitializeContextMenu(DataGridView dgv)
		{
			dgv.ContextMenuStrip = new ContextMenuStrip();
			dgv.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) => { ContextMenuOpening(sender as ContextMenuStrip, dgv, e); };
		}

		private void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			List<ComPreset> selectedPresets = new List<ComPreset>();
			foreach (DataGridViewRow row in dgv.SelectedCells.Cast<DataGridViewCell>().Select(_dgvc => _dgvc.OwningRow).Distinct())
			{
				if (row.Cells[GridColumn.Data].Value is ComPreset preset)
				{
					selectedPresets.Add(preset);
				}
			}
			ComPreset singleSelected = selectedPresets.Count == 1 ? selectedPresets[0] as ComPreset : null;

			menu.Items.Clear();

			if (selectedPresets.Count > 0)
			{
				menu.Items.AddMenuItem("Free", (object _sender, EventArgs _e) => { SetMode(selectedPresets, ElementComPresetMode.Free, dgv); });
				menu.Items.AddMenuItem("Airdrome", (object _sender, EventArgs _e) => { SetMode(selectedPresets, ElementComPresetMode.Airdrome, dgv); });
				menu.Items.AddMenuItem("Group", (object _sender, EventArgs _e) => { SetMode(selectedPresets, ElementComPresetMode.Group, dgv); });

				if (singleSelected is object && singleSelected.GetAsset(m_briefingCoalition) is AssetAirdrome airdrome && airdrome.Radios is object && airdrome.Radios.Count() > 1)
				{
					menu.Items.AddMenuSeparator();
					foreach (Radio radio in airdrome.Radios)
					{
						menu.Items.AddMenuItem($"Set radio {radio}", (object _sender, EventArgs _e) => { SetRadio(singleSelected, radio, dgv); });
					}
				}
			}

			if (menu.Items.Count <= 0)
				e.Cancel = true;

		}
		#endregion

		#region Events
		private void DgvCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			ComPreset preset = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as ComPreset;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

			if (column.Name == GridColumn.Frequency)
				preset.RadioFrequency = cell.Value as decimal?;
			else if (column.Name == GridColumn.Modulation)
				preset.RadioModulation = (cell as DataGridViewComboBoxCell).Value as int?;
			else if (column.Name == GridColumn.Asset)
				preset.AssetId = (int)cell.Value;
			else if (column.Name == GridColumn.Label)
				preset.Label = cell.Value as string;

			preset.Compute(m_briefingCoalition);
			RefreshGridRow(dgv, preset);
		}


		private void BtCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void BtOk_Click(object sender, System.EventArgs e)
		{
			ScreenToData();
			DialogResult = DialogResult.OK;
			Close();
		}
		#endregion

		private void BtClear_Click(object sender, EventArgs e)
		{
			m_listComPresets.InitializeEmpty();
			DataToScreen();
		}

		private void BtAuto_Click(object sender, EventArgs e)
		{
			m_listComPresets.InitializeCoalition(m_briefingCoalition);
			DataToScreen();
		}
	}
}

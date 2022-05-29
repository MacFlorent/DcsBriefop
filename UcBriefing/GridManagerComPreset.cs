using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class GridManagerComPreset : GridManager
	{
		#region Columns
		private static class GridColumn
		{
			public static readonly string PresetNumber = "PresetNumber";
			public static readonly string Frequency = "Frequency";
			public static readonly string Modulation = "Modulation";
			public static readonly string Mode = "Mode";
			public static readonly string Asset = "Asset";
			public static readonly string Label = "Label";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private List<ComPreset> m_comPresets;
		private BriefingCoalition m_briefingCoalition;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerComPreset(DataGridView dgv, List<ComPreset> comPresets, BriefingCoalition briefingCoalition) : base(dgv)
		{
			m_comPresets = comPresets;
			m_briefingCoalition = briefingCoalition;

			m_dgv.CellEndEdit += CellEndEdit;
			//m_dgv.DataError += CellDataError;
			m_dgv.EditingControlShowing += CellEditingControlShowing;
			m_dgv.DataBindingComplete += (sender, e) => { RefreshGridRowsReadOnly(); };
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();

			m_dtSource.Columns.Add(GridColumn.PresetNumber, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Mode, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Asset, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Label, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Frequency, typeof(decimal));
			m_dtSource.Columns.Add(GridColumn.Modulation, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(ComPreset));

			foreach (ComPreset preset in m_comPresets)
				RefreshDataSourceRow(preset);
		}

		private void RefreshDataSourceRow(ComPreset preset)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<ComPreset>(GridColumn.Data) == preset).FirstOrDefault();

			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, preset);
				m_dtSource.Rows.Add(dr);
			}

			dr.ClearErrors();

			dr.SetField(GridColumn.PresetNumber, preset.PresetNumber);
			dr.SetField(GridColumn.Mode, MasterDataRepository.GetById(MasterDataType.ComPresetMode, (int)preset.Mode)?.Label);
			dr.SetField(GridColumn.Asset, preset.AssetId);
			dr.SetField(GridColumn.Label, preset.Label);
			dr.SetField(GridColumn.Frequency, preset.Radio?.Frequency);
			dr.SetField(GridColumn.Modulation, preset.Radio?.Modulation);
		}

		private void RefreshGridRowsReadOnly()
		{
			foreach (DataGridViewRow dgvr in m_dgv.Rows)
				RefreshGridRowReadOnly(dgvr);
		}

		private void RefreshGridRowReadOnly(DataGridViewRow dgvr)
		{
			ComPreset preset = dgvr.Cells[GridColumn.Data].Value as ComPreset;
			if (preset is null)
			{
				dgvr.Cells[GridColumn.Asset].ReadOnly = true;
				dgvr.Cells[GridColumn.Label].ReadOnly = true;
				dgvr.Cells[GridColumn.Frequency].ReadOnly = true;
				dgvr.Cells[GridColumn.Modulation].ReadOnly = true;
			}
			else if (preset.Mode == ElementComPresetMode.Free)
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

		private void ReplaceColumnWithComboBox(string sColumnName, string sHeaderText, object dataSource, string sValueMember, string sDisplayMember)
		{
			if (!m_dgv.Columns.Contains(sColumnName))
				return;

			DataGridViewColumn dgvc = m_dgv.Columns[sColumnName];
			int iDisplayIndex = dgvc.DisplayIndex;
			m_dgv.Columns.Remove(dgvc);

			DataGridViewComboBoxColumn dgvcComboBox = new DataGridViewComboBoxColumn() { Name = sColumnName, DataPropertyName = sColumnName, HeaderText = sHeaderText };
			if (dataSource is object)
			{
				dgvcComboBox.DataSource = dataSource;
				dgvcComboBox.ValueMember = sValueMember;
				dgvcComboBox.DisplayMember = sDisplayMember;
			}

			dgvcComboBox.DisplayIndex = iDisplayIndex;
			m_dgv.Columns.Add(dgvcComboBox);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			ReplaceColumnWithComboBox(GridColumn.Modulation, "Mod.", MasterDataRepository.GetMasterDataList(MasterDataType.RadioModulation), MasterDataColumn.Id, MasterDataColumn.Label);
		//https://stackoverflow.com/questions/27201551/datagridview-comboboxcolumn-different-values-for-each-row
			ReplaceColumnWithComboBox(GridColumn.Asset, "Asset", null, "Id", "Name");
			//m_dgv.Columns.Remove(m_dgv.Columns[GridColumn.Modulation]);
			//DataGridViewComboBoxColumn colModulation = new DataGridViewComboBoxColumn() { Name = GridColumn.Modulation, DataPropertyName = GridColumn.Modulation, HeaderText = "Mod." };
			//colModulation.DataSource = MasterDataRepository.GetMasterDataList(MasterDataType.RadioModulation);
			//colModulation.ValueMember = MasterDataColumn.Id;
			//colModulation.DisplayMember = MasterDataColumn.Label;
			//m_dgv.Columns.Add(colModulation);

			m_dgv.Columns[GridColumn.PresetNumber].ReadOnly = true;
			m_dgv.Columns[GridColumn.Mode].ReadOnly = true;
			m_dgv.Columns[GridColumn.Data].Visible = false;
			m_dgv.Columns[GridColumn.Label].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			m_dgv.Columns[GridColumn.Frequency].DefaultCellStyle.Format = "##0.000";

			m_dgv.Columns[GridColumn.PresetNumber].HeaderText = "Num.";
			//m_dgv.Columns[GridColumn.Asset].HeaderText = "Asset ID";
		}

		private void SetMode(List<ComPreset> presets, ElementComPresetMode mode, DataGridView dgv)
		{
			foreach (ComPreset preset in presets)
			{
				if (preset.Mode != mode)
				{
					preset.Mode = mode;
					preset.Compute(m_briefingCoalition);
					RefreshDataSourceRow(preset);
				}
			}
		}

		private void SetRadio(ComPreset preset, Radio radio, DataGridView dgv)
		{
			preset.Radio = radio.GetCopy();
			RefreshDataSourceRow(preset);
		}
		#endregion

		#region Menus
		protected override void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			List<ComPreset> selectedPresets = new List<ComPreset>();
			foreach (DataGridViewRow row in GetSelectedRows())
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
		public class EventArgsComPreset : EventArgs
		{
			public ComPreset ComPreset { get; set; }
		}
		public event EventHandler<EventArgsComPreset> ComPresetModified;

		private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			ComPreset preset = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as ComPreset;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

			if (column.DataPropertyName == GridColumn.Frequency)
				preset.RadioFrequency = cell.Value as decimal?;
			else if (column.DataPropertyName == GridColumn.Modulation)
				preset.RadioModulation = (cell as DataGridViewComboBoxCell).Value as int?;
			else if (column.DataPropertyName == GridColumn.Asset)
				preset.AssetId = (int)cell.Value;
			else if (column.DataPropertyName == GridColumn.Label)
				preset.Label = cell.Value as string;

			preset.Compute(m_briefingCoalition);
			RefreshDataSourceRow(preset);
			RefreshGridRowReadOnly(cell.OwningRow);
			ComPresetModified?.Invoke(this, new EventArgsComPreset() { ComPreset = preset });
		}

		//private void CellDataError(object sender, DataGridViewDataErrorEventArgs e)
		//{
		//	DataGridView dgv = (sender as DataGridView); 
		//	DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
		//	DataGridViewRow row = dgv.Rows[e.RowIndex];
		//	DataRow drSource = (row.DataBoundItem as DataRowView)?.Row;

		//	if (column.DataPropertyName == GridColumn.Asset)
		//		drSource.SetColumnError(column.DataPropertyName, "invalid asset");
		//	else if (column.DataPropertyName == GridColumn.Modulation)
		//		drSource.SetColumnError(column.DataPropertyName, "invalid modulation");
		//	else
		//		e.ThrowException = true;
		//}

		private void CellEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			DataGridView dgv = (sender as DataGridView);
			DataGridViewCell cell = dgv.CurrentCell;
			DataGridViewColumn column = cell.OwningColumn;
			DataGridViewRow row = cell.OwningRow;
			DataRow drSource = (row.DataBoundItem as DataRowView)?.Row;
			ComPreset preset = row.Cells[GridColumn.Data].Value as ComPreset;

			if (column.DataPropertyName == GridColumn.Asset && e.Control is ComboBox cb)
			{
				cb.DropDownStyle = ComboBoxStyle.DropDownList;
				if (preset.Mode == ElementComPresetMode.Airdrome)
				{
					cb.DataSource = m_briefingCoalition.Airdromes;
				}
				if (preset.Mode == ElementComPresetMode.Group)
				{
					cb.DataSource = m_briefingCoalition.OwnAssets;
				}
			}
		}

		#endregion
	}
}

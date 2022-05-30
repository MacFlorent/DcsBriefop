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
		private List<MasterDataObject> AssetEmpty = new List<MasterDataObject>() { new MasterDataObject() { Id = 0, Label = "None" } };
		private List<MasterDataObject> AssetAirdromes = new List<MasterDataObject>();
		private List<MasterDataObject> AssetGroups = new List<MasterDataObject>();
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerComPreset(DataGridView dgv, List<ComPreset> comPresets, BriefingCoalition briefingCoalition) : base(dgv)
		{
			m_comPresets = comPresets;
			m_briefingCoalition = briefingCoalition;

			AssetAirdromes.Add(new MasterDataObject() { Id = 0, Label = "-invalid airdrome-" });
			foreach (AssetAirdrome airdrome in m_briefingCoalition.Airdromes)
				AssetAirdromes.Add(new MasterDataObject() { Id = airdrome.Id, Label = airdrome.Name });

			AssetGroups.Add(new MasterDataObject() { Id = 0, Label = "-invalid group-" });
			foreach (AssetGroup group in m_briefingCoalition.OwnAssets.OfType<AssetGroup>().Where(_g => !string.IsNullOrEmpty (_g.GetRadioString())))
				AssetGroups.Add(new MasterDataObject() { Id = group.Id, Label = group.Name });

			m_dgv.CellEndEdit += CellEndEdit;
			m_dgv.CellValueChanged += CellValueChangedEvent;
			m_dgv.DataError += CellDataError;
			m_dgv.DataBindingComplete += (sender, e) => { RefreshGridRows(); };
		}

		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();

			m_dtSource.Columns.Add(GridColumn.PresetNumber, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Mode, typeof(ElementComPresetMode));
			m_dtSource.Columns.Add(GridColumn.Asset, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Label, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Frequency, typeof(decimal));
			m_dtSource.Columns.Add(GridColumn.Modulation, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(ComPreset));

			foreach (ComPreset preset in m_comPresets)
				InitializeDataSourceAddRow(preset);
		}

		private void InitializeDataSourceAddRow(ComPreset preset)
		{
			DataRow dr = m_dtSource.NewRow();
			dr.SetField(GridColumn.Data, preset);
			m_dtSource.Rows.Add(dr);
			ObjectToDataRow(dr);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			ReplaceColumnWithComboBox(GridColumn.Modulation, "Mod.", MasterDataRepository.GetMasterDataList(MasterDataType.RadioModulation), MasterDataColumn.Id, MasterDataColumn.Label);
			ReplaceColumnWithComboBox(GridColumn.Mode, "Mode", MasterDataRepository.GetMasterDataList(MasterDataType.ComPresetMode), MasterDataColumn.Id, MasterDataColumn.Label);
			ReplaceColumnWithComboBox(GridColumn.Asset, "Asset", null, "Id", "Name");
			//m_dgv.Columns.Remove(m_dgv.Columns[GridColumn.Modulation]);
			//DataGridViewComboBoxColumn colModulation = new DataGridViewComboBoxColumn() { Name = GridColumn.Modulation, DataPropertyName = GridColumn.Modulation, HeaderText = "Mod." };
			//colModulation.DataSource = MasterDataRepository.GetMasterDataList(MasterDataType.RadioModulation);
			//colModulation.ValueMember = MasterDataColumn.Id;
			//colModulation.DisplayMember = MasterDataColumn.Label;
			//m_dgv.Columns.Add(colModulation);

			m_dgv.Columns[GridColumn.PresetNumber].ReadOnly = true;
			//m_dgv.Columns[GridColumn.Mode].ReadOnly = true;
			m_dgv.Columns[GridColumn.Data].Visible = false;
			m_dgv.Columns[GridColumn.Label].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			m_dgv.Columns[GridColumn.Frequency].DefaultCellStyle.Format = "##0.000";

			m_dgv.Columns[GridColumn.PresetNumber].HeaderText = "Num.";
			//m_dgv.Columns[GridColumn.Asset].HeaderText = "Asset ID";
		}

		private void ObjectToDataRow(DataRow dr)
		{
			ComPreset preset = dr.Field<ComPreset>(GridColumn.Data);

			dr.SetField(GridColumn.PresetNumber, preset.PresetNumber);
			dr.SetField(GridColumn.Mode,preset.Mode);
			dr.SetField(GridColumn.Asset, preset.AssetId);
			dr.SetField(GridColumn.Label, preset.Label);
			dr.SetField(GridColumn.Frequency, preset.RadioFrequency);
			dr.SetField(GridColumn.Modulation, preset.RadioModulation);
		}

		private void DataRowToObject(DataRow dr)
		{
			ComPreset preset = dr.Field<ComPreset>(GridColumn.Data);

			preset.PresetNumber = dr.Field<int>(GridColumn.PresetNumber);
			preset.Mode = dr.Field<ElementComPresetMode>(GridColumn.Mode);
			preset.AssetId = dr.Field<int>(GridColumn.Asset);
			preset.Label = dr.Field<string>(GridColumn.Label);
			preset.RadioFrequency = dr.Field<decimal?>(GridColumn.Frequency);
			preset.RadioModulation = dr.Field<int?>(GridColumn.Modulation);
			preset.Compute(m_briefingCoalition);
		}

		private void RefreshGridRows()
		{
			foreach (DataGridViewRow dgvr in m_dgv.Rows)
				RefreshGridRow(dgvr);
		}
		
		private void RefreshGridRow(DataGridViewRow dgvr)
		{
			ComPreset preset = dgvr.Cells[GridColumn.Data].Value as ComPreset;

			DataGridViewComboBoxCell cbAsset = dgvr.Cells[GridColumn.Asset] as DataGridViewComboBoxCell;
			if (cbAsset is null)
				return;

			dgvr.Cells[GridColumn.Asset].ReadOnly = true;
			dgvr.Cells[GridColumn.Label].ReadOnly = true;
			dgvr.Cells[GridColumn.Frequency].ReadOnly = true;
			dgvr.Cells[GridColumn.Modulation].ReadOnly = true;

			if (preset is object)
			{
				if (preset.Mode == ElementComPresetMode.Free)
				{
					cbAsset.DataSource = AssetEmpty;
					cbAsset.ValueMember = MasterDataColumn.Id;
					cbAsset.DisplayMember = MasterDataColumn.Label;

					dgvr.Cells[GridColumn.Asset].ReadOnly = true;
					dgvr.Cells[GridColumn.Label].ReadOnly = false;
					dgvr.Cells[GridColumn.Frequency].ReadOnly = false;
					dgvr.Cells[GridColumn.Modulation].ReadOnly = false;
				}
				else if (preset.Mode == ElementComPresetMode.Airdrome)
				{
					cbAsset.DataSource = AssetAirdromes;
					cbAsset.ValueMember = MasterDataColumn.Id;
					cbAsset.DisplayMember = MasterDataColumn.Label;

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
				else if (preset.Mode == ElementComPresetMode.Group)
				{
					cbAsset.DataSource = AssetGroups;
					cbAsset.ValueMember = MasterDataColumn.Id;
					cbAsset.DisplayMember = MasterDataColumn.Label;

					dgvr.Cells[GridColumn.Asset].ReadOnly = false;
					dgvr.Cells[GridColumn.Label].ReadOnly = true;
					dgvr.Cells[GridColumn.Frequency].ReadOnly = true;
					dgvr.Cells[GridColumn.Modulation].ReadOnly = true;
				}
			}
		}

		private void SetMode(IEnumerable<DataGridViewRow> dgvrl, ElementComPresetMode mode)
		{
			foreach (DataGridViewRow dgvr in dgvrl)
			{
				DataRow dr = (dgvr.DataBoundItem as DataRowView)?.Row;
				ComPreset preset = dr.Field<ComPreset>(GridColumn.Data);
				if (preset.Mode != mode)
				{
					preset.Mode = mode;
					preset.Compute(m_briefingCoalition);
					ObjectToDataRow(dr);
					RefreshGridRow(dgvr);
				}
			}
		}

		private void SetRadio(DataGridViewRow dgvr, Radio radio)
		{
			DataRow dr = (dgvr.DataBoundItem as DataRowView)?.Row;
			ComPreset preset = dr.Field<ComPreset>(GridColumn.Data);
			preset.Radio = radio.GetCopy();
			ObjectToDataRow(dr);
			RefreshGridRow(dgvr);
		}
		#endregion

		#region Menus
		protected override void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			IEnumerable<DataGridViewRow> dgvrlSelected = GetSelectedRows();
			DataGridViewRow dgvrSingleSelected = dgvrlSelected.Count() == 1 ? dgvrlSelected.First() : null;

			menu.Items.Clear();

			if (dgvrlSelected.Count() > 0)
			{
				menu.Items.AddMenuItem("Free", (object _sender, EventArgs _e) => { SetMode(dgvrlSelected, ElementComPresetMode.Free); });
				menu.Items.AddMenuItem("Airdrome", (object _sender, EventArgs _e) => { SetMode(dgvrlSelected, ElementComPresetMode.Airdrome); });
				menu.Items.AddMenuItem("Group", (object _sender, EventArgs _e) => { SetMode(dgvrlSelected, ElementComPresetMode.Group); });

				if (dgvrSingleSelected is object)
				{
					DataRow dr = (dgvrSingleSelected.DataBoundItem as DataRowView)?.Row;
					ComPreset preset = dr.Field<ComPreset>(GridColumn.Data);
					if (preset.GetAsset(m_briefingCoalition) is AssetAirdrome airdrome && airdrome.Radios is object && airdrome.Radios.Count() > 1)
					{
						menu.Items.AddMenuSeparator();
						foreach (Radio radio in airdrome.Radios)
						{
							menu.Items.AddMenuItem($"Set radio {radio}", (object _sender, EventArgs _e) => { SetRadio(dgvrSingleSelected, radio); });
						}
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

			DataGridViewRow dgvr = dgv.Rows[e.RowIndex];
			DataRow dr = (dgvr.DataBoundItem as DataRowView)?.Row;

			if (dr is object)
			{
				DataRowToObject(dr);
				ObjectToDataRow(dr);
				RefreshGridRow(dgvr);

				ComPresetModified?.Invoke(this, new EventArgsComPreset() { ComPreset = dr.Field<ComPreset>(GridColumn.Data) });
			}
		}

		private void CellValueChangedEvent(object sender, DataGridViewCellEventArgs e)
		{
			//if (e.RowIndex < 0)
			//	return;

			//DataGridView dgv = (sender as DataGridView);
			//if (dgv is null)
			//	return;

			//DataGridViewRow dgvr = dgv.Rows[e.RowIndex];
			//DataRow dr = (dgvr.DataBoundItem as DataRowView)?.Row;

			//if (dr is object)
			//{
			//	DataRowToObject(dr);
			//	ObjectToDataRow(dr);
			//	RefreshGridRow(dgvr);

			//	ComPresetModified?.Invoke(this, new EventArgsComPreset() { ComPreset = dr.Field<ComPreset>(GridColumn.Data) });
			//}
		}

		private void CellDataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			//DataGridView dgv = (sender as DataGridView);
			//DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			//DataGridViewRow row = dgv.Rows[e.RowIndex];
			//DataRow drSource = (row.DataBoundItem as DataRowView)?.Row;

			//if (column.DataPropertyName == GridColumn.Asset)
			//	drSource.SetColumnError(column.DataPropertyName, "invalid asset");
			//else if (column.DataPropertyName == GridColumn.Modulation)
			//	drSource.SetColumnError(column.DataPropertyName, "invalid modulation");
			//else
			//	e.ThrowException = true;
		}
		#endregion
	}
}

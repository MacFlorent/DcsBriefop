//using DcsBriefop.Data;
//using DcsBriefop.Tools;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;

//namespace DcsBriefop.UcBriefing
//{
//	internal class GridManagerComPreset : GridManager
//	{
//		#region Columns
//		private static class GridColumn
//		{
//			public static readonly string PresetNumber = "PresetNumber";
//			public static readonly string Frequency = "Frequency";
//			public static readonly string Modulation = "Modulation";
//			public static readonly string Mode = "Mode";
//			public static readonly string Asset = "Asset";
//			public static readonly string Label = "Notes";
//			public static readonly string Data = "Data";
//		}
//		#endregion

//		#region Fields
//		private ListComPreset m_listComPresets;
//		#endregion

//		#region Properties
//		#endregion

//		#region CTOR
//		public GridManagerComPreset(DataGridView dgv, ListComPreset listComPresets) : base(dgv)
//		{
//			m_listComPresets = listComPresets;

//			m_dgv.CellEndEdit += CellEndEdit;
//			m_dgv.CellMouseUp += CellMouseUp;
//		}
//		#endregion

//		#region Methods
//		public override void Initialize()
//		{
//			base.Initialize();
//			InitializeContextMenu();
//		}

//		protected override void InitializeDataSource()
//		{
//			m_dtSource = new DataTable();

//			m_dtSource.Columns.Add(GridColumn.PresetNumber, typeof(bool));
//			m_dtSource.Columns.Add(GridColumn.Mode, typeof(int));
//			m_dtSource.Columns.Add(GridColumn.Asset, typeof(string));
//			m_dtSource.Columns.Add(GridColumn.Label, typeof(string));
//			m_dtSource.Columns.Add(GridColumn.Frequency, typeof(string));
//			m_dtSource.Columns.Add(GridColumn.Modulation, typeof(string));
//			m_dtSource.Columns.Add(GridColumn.Data, typeof(AssetUnit));

//			foreach (ComPreset preset in m_listComPresets)
//				RefreshDataSourceRow(preset);
//		}

//		private void RefreshDataSourceRow(ComPreset preset)
//		{
//			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<ComPreset>(GridColumn.Data) == preset).FirstOrDefault();
//			if (dr is null)
//			{
//				dr = m_dtSource.NewRow();
//				dr.SetField(GridColumn.Data, preset);
//				m_dtSource.Rows.Add(dr);
//			}

//			dr.SetField(GridColumn.PresetNumber, preset.PresetNumber);
//			dr.SetField(GridColumn.Mode, MasterDataRepository.GetById(MasterDataType.ComPresetMode, (int)preset.Mode)?.Label);
//			dr.SetField(GridColumn.Asset, preset.AssetId);
//			dr.SetField(GridColumn.Label, preset.Label);
//			dr.SetField(GridColumn.Frequency, preset.Radio?.Frequency);
//			dr.SetField(GridColumn.Modulation, preset.Radio?.Modulation);
//		}

//		protected override void PostInitializeColumns()
//		{
//			base.PostInitializeColumns();

//			DataGridViewColumn colModulation = m_dgv.Columns[GridColumn.Modulation];
//			colModulation.
//			colModulation.DataSource = MasterDataRepository.GetMasterDataList(MasterDataType.RadioModulation);
//			colModulation.ValueMember = MasterDataColumn.Id;
//			colModulation.DisplayMember = MasterDataColumn.Label;

			
//			m_dgv.Columns[GridColumn.PresetNumber].ReadOnly = true;
//			m_dgv.Columns[GridColumn.Mode].ReadOnly = true;
//			m_dgv.Columns[GridColumn.Data].Visible = false;
//		}

//		private bool UnitIncluded(AssetUnit unit)
//		{
//			if (m_missionData is object)
//			{
//				return m_missionData.IsThreatIncluded(unit.Id);
//			}
//			else
//			{
//				return unit.Included;
//			}
//		}

//		private void SetIncluded(AssetUnit unit, bool bIncluded)
//		{
//			if (m_missionData is object)
//			{
//				if (m_missionData.IsThreatIncluded(unit.Id) != bIncluded)
//				{
//					m_missionData.IncludeThreat(unit.Id, bIncluded);
//					RefreshDataSourceRow(unit);
//					(m_dgv.DataSource as BindingSource).EndEdit();
//				}
//			}
//			else
//			{
//				if (unit.Included != bIncluded)
//				{
//					unit.Included = bIncluded;
//					RefreshDataSourceRow(unit);
//					(m_dgv.DataSource as BindingSource).EndEdit();
//				}
//			}
//		}

//		private void SetIncluded(List<AssetUnit> units, bool bIncluded)
//		{
//			foreach (AssetUnit unit in units)
//			{
//				SetIncluded(unit, bIncluded);
//			}
//		}

//		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
//		{
//			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

//			DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
//			AssetUnit unit = dgvc.DataGridView.Rows[dgvc.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;

//			if (column.DataPropertyName == GridColumn.Included)
//			{
//				if ((unit is object && UnitIncluded(unit)))
//				{
//					cellStyle.BackColor = Color.LightGreen;
//				}
//			}

//			return cellStyle;
//		}
//		#endregion

//		#region Menus
//		private void InitializeContextMenu()
//		{
//			m_dgv.ContextMenuStrip = new ContextMenuStrip();
//			m_dgv.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) => { ContextMenuOpening(sender as ContextMenuStrip, m_dgv, e); };
//		}

//		private void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
//		{
//			List<AssetUnit> selectedUnits = new List<AssetUnit>();
//			foreach (DataGridViewRow row in GetSelectedRows())
//			{
//				if (row.Cells[GridColumn.Data].Value is AssetUnit unit)
//				{
//					selectedUnits.Add(unit);
//				}
//			}

//			menu.Items.Clear();

//			if (m_dgv.Columns[GridColumn.Included].Visible && selectedUnits.Count > 0)
//			{
//				menu.Items.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetIncluded(selectedUnits, false); });
//				menu.Items.AddMenuItem("Included", (object _sender, EventArgs _e) => { SetIncluded(selectedUnits, true); });
//			}

//			if (menu.Items.Count <= 0)
//				e.Cancel = true;

//		}
//		#endregion

//		#region Events
//		public class EventArgsUnit : EventArgs
//		{
//			public AssetUnit Unit { get; set; }
//		}
//		public event EventHandler<EventArgsUnit> UnitModified;

//		private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
//		{
//			if (e.RowIndex < 0)
//				return;

//			DataGridView dgv = (sender as DataGridView);
//			if (dgv is null)
//				return;

//			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
//			DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
//			AssetUnit unit = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;

//			if (column.Name == GridColumn.Included)
//			{
//				if (unit is object)
//				{
//					SetIncluded(unit, (bool)cell.Value);
//					UnitModified?.Invoke(this, new EventArgsUnit() { Unit = unit });
//				}
//			}
//		}

//		private void CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
//		{
//			if (e.RowIndex < 0)
//				return;

//			DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
//			if (column.Name == GridColumn.Included)
//			{
//				(sender as DataGridView).EndEdit();
//			}
//		}
//		#endregion
//	}
//}

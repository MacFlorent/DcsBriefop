using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class GridManagerUnit : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Included = "Included";
			public static readonly string Id = "Id";
			public static readonly string Side = "Side";
			public static readonly string Class = "Class";
			public static readonly string Description = "Description";
			public static readonly string Localisation = "Localisation";
			public static readonly string Function = "Function";
			public static readonly string Asset = "Asset";
			public static readonly string Type = "Type";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}
		public static List<string> ColumnsDisplayedUnit = new List<string>() { GridColumn.Notes, GridColumn.Included, GridColumn.Id, GridColumn.Asset, GridColumn.Description, GridColumn.Localisation };
		#endregion

		#region Fields
		private List<AssetUnit> m_units;
		private AssetFlightMission m_missionData;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerUnit(DataGridView dgv, List<AssetUnit> units, AssetFlightMission missionData) : base(dgv)
		{
			m_units = units;
			m_missionData = missionData;

			m_dgv.CellEndEdit += CellEndEdit;
			m_dgv.CellMouseUp += CellMouseUp;
		}

		public GridManagerUnit(DataGridView dgv, List<Asset> assets, AssetFlightMission missionData) : this(dgv, null as List<AssetUnit>, missionData)
		{
			m_units = assets.OfType<AssetGroup>().Select(_g => _g.Units).Aggregate((aggregated, toAggregate) => { return aggregated.Concat(toAggregate).ToList(); });
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Included, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Side, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Class, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Description, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Localisation, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Notes, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(AssetUnit));

			foreach (AssetUnit unit in m_units)
				RefreshDataSourceRow(unit);
		}

		private void RefreshDataSourceRow(AssetUnit unit)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<AssetUnit>(GridColumn.Data) == unit).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, unit);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Included, UnitIncluded(unit));
			dr.SetField(GridColumn.Id, unit.Id);
			dr.SetField(GridColumn.Side, unit.AssetGroup.Side);
			dr.SetField(GridColumn.Class, unit.AssetGroup.Class);
			dr.SetField(GridColumn.Description, unit.Description);
			dr.SetField(GridColumn.Localisation, unit.GetLocalisation());
			dr.SetField(GridColumn.Type, unit.Type);
			dr.SetField(GridColumn.Notes, unit.Information);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Included].ReadOnly = false;
			m_dgv.Columns[GridColumn.Data].Visible = false;
		}

		private bool UnitIncluded(AssetUnit unit)
		{
			if (m_missionData is object)
			{
				return m_missionData.IsThreatIncluded(unit.Id);
			}
			else
			{
				return unit.Included;
			}
		}

		private void SetIncluded(AssetUnit unit, bool bIncluded)
		{
			if (m_missionData is object)
			{
				if (m_missionData.IsThreatIncluded(unit.Id) != bIncluded)
				{
					m_missionData.IncludeThreat(unit.Id, bIncluded);
					RefreshDataSourceRow(unit);
					(m_dgv.DataSource as BindingSource).EndEdit();
				}
			}
			else
			{
				if (unit.Included != bIncluded)
				{
					unit.Included = bIncluded;
					RefreshDataSourceRow(unit);
					(m_dgv.DataSource as BindingSource).EndEdit();
				}
			}
		}

		private void SetIncluded(List<AssetUnit> units, bool bIncluded)
		{
			foreach (AssetUnit unit in units)
			{
				SetIncluded(unit, bIncluded);
			}
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

			DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
			AssetUnit unit = dgvc.DataGridView.Rows[dgvc.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;

			if (column.DataPropertyName == GridColumn.Included)
			{
				if ((unit is object && UnitIncluded(unit)))
				{
					cellStyle.BackColor = Color.LightGreen;
				}
			}

			return cellStyle;
		}
		#endregion

		#region Menus
		protected override void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			List<AssetUnit> selectedUnits = new List<AssetUnit>();
			foreach (DataGridViewRow row in GetSelectedRows())
			{
				if (row.Cells[GridColumn.Data].Value is AssetUnit unit)
				{
					selectedUnits.Add(unit);
				}
			}

			menu.Items.Clear();

			if (m_dgv.Columns[GridColumn.Included].Visible && selectedUnits.Count > 0)
			{
				menu.Items.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetIncluded(selectedUnits, false); });
				menu.Items.AddMenuItem("Included", (object _sender, EventArgs _e) => { SetIncluded(selectedUnits, true); });
			}

			if (menu.Items.Count <= 0)
				e.Cancel = true;

		}
		#endregion

		#region Events
		public class EventArgsUnit : EventArgs
		{
			public AssetUnit Unit { get; set; }
		}
		public event EventHandler<EventArgsUnit> UnitModified;

		private void CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			AssetUnit unit = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;

			if (column.Name == GridColumn.Included)
			{
				if (unit is object)
				{
					SetIncluded(unit, (bool)cell.Value);
					UnitModified?.Invoke(this, new EventArgsUnit() { Unit = unit });
				}
			}
		}

		private void CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
			if (column.Name == GridColumn.Included)
			{
				(sender as DataGridView).EndEdit();
			}
		}
		#endregion
	}
}

using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.FgControls
{
	internal class GridManagerUnit : GridManager
	{
		#region Columns
		public static class Column
		{
			public static readonly string Included = "Included";
			public static readonly string Id = "Id";
			public static readonly string Side = "Side";
			public static readonly string Class = "Class";
			public static readonly string Attributes = "Attributes";
			public static readonly string DisplayName = "Description";
			public static readonly string Localisation = "Localisation";
			public static readonly string Function = "Function";
			public static readonly string Asset = "Asset";
			public static readonly string Type = "Type";
			public static readonly string Information = "Notes";
			public static readonly string Data = "Data";
		}
		public static List<string> ColumnsDisplayedUnit = new List<string>() { Column.Included, Column.Id, Column.DisplayName, Column.Attributes, Column.Localisation, Column.Information };
		#endregion

		#region Fields
		private List<AssetUnit> m_units;
		private FlightMission m_flightMission;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerUnit(FgDataGridView dgv, List<string> columnsDisplayed, List<AssetUnit> units, FlightMission flightMission) : base(dgv, columnsDisplayed)
		{
			m_units = units;
			m_flightMission = flightMission;
		}

		public static GridManagerUnit CreateManager(FgDataGridView dgv, List<string> columnsDisplayed, List<AssetUnit> units, FlightMission flightMission)
		{
			GridManagerUnit gm = new GridManagerUnit(dgv, columnsDisplayed, units, flightMission);
			gm.Initialize();
			return gm;
		}

		public static GridManagerUnit CreateManager(FgDataGridView dgv, List<string> columnsDisplayed, List<Asset> assets, FlightMission flightMission)
		{
			List<AssetUnit> units = assets.OfType<AssetGroup>().Select(_g => _g.Units).Aggregate((aggregated, toAggregate) => { return aggregated.Concat(toAggregate).ToList(); });
			return CreateManager(dgv, columnsDisplayed, units, flightMission);
		}
		#endregion

		#region Methods
		protected override void InitializeGridSource()
		{
			m_dtSource = new DataTable();

			m_dtSource.Columns.Add(Column.Included, typeof(bool));
			m_dtSource.Columns.Add(Column.Id, typeof(int));
			m_dtSource.Columns.Add(Column.Side, typeof(ElementAssetSide));
			m_dtSource.Columns.Add(Column.Class, typeof(ElementDcsObjectClass));
			m_dtSource.Columns.Add(Column.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(Column.DisplayName, typeof(string));
			m_dtSource.Columns.Add(Column.Localisation, typeof(string));
			m_dtSource.Columns.Add(Column.Type, typeof(string));
			m_dtSource.Columns.Add(Column.Information, typeof(string));
			m_dtSource.Columns.Add(Column.Data, typeof(AssetUnit));

			foreach (AssetUnit unit in m_units)
			{
				DataRow dr = m_dtSource.NewRow();
				dr.SetField(Column.Data, unit);
				ObjectToDataRow(dr);
				m_dtSource.Rows.Add(dr);
			}
		}

		public void ObjectToDataRow(DataRow dr)
		{
			AssetUnit unit = dr.Field<AssetUnit>(Column.Data);

			dr.SetField(Column.Included, Included(dr));
			dr.SetField(Column.Id, unit.Id);
			dr.SetField(Column.Side, unit.AssetGroup.Side);
			dr.SetField(Column.Class, unit.Class);
			dr.SetField(Column.Attributes, unit.Attributes);
			dr.SetField(Column.DisplayName, unit.DisplayName);
			dr.SetField(Column.Localisation, unit.GetLocalisation());
			dr.SetField(Column.Type, unit.Type);
			dr.SetField(Column.Information, unit.Information);
		}

		protected override void InitializeGridColumns()
		{
			m_dgv.Columns.Clear();

			m_dgv.AddColumn<bool>(Column.Included, "Incl.");
			m_dgv.AddColumn<int>(Column.Id, "Id").ReadOnly = true;
			m_dgv.AddColumn<ElementAssetSide>(Column.Side, "Side").ReadOnly = true;
			m_dgv.AddColumn<ElementDcsObjectClass>(Column.Class, "Class").ReadOnly = true;
			m_dgv.AddColumn<ElementDcsObjectAttribute>(Column.Attributes, "Attr.").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.DisplayName, "Name").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Localisation, "Localisation").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Type, "Type").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Information, "Information").ReadOnly = true;
		}

		private bool Included(DataRow dr)
		{
			if (dr is null)
				return false;

			AssetUnit unit = dr.Field<AssetUnit>(Column.Data);

			if (m_flightMission is object)
			{
				return m_flightMission.OpposingUnitIds.Contains(unit.Id);
			}
			else
			{
				return unit.Included;
			}
		}

		private void SetIncluded(DataRow dr, bool bIncluded)
		{
			if (dr is null)
				return;

			AssetUnit unit = dr.Field<AssetUnit>(Column.Data);

			if (m_flightMission is object)
			{
				m_flightMission.IncludeOpposingUnit(unit.Id, bIncluded);
			}
			else
			{
				if (unit.Included != bIncluded)
				{
					unit.Included = bIncluded;
				}
			}
			ObjectToDataRow(dr);
			(m_dgv.DataSource as BindingSource).EndEdit();

		}

		private void SetIncluded(IEnumerable<DataRow> drl, bool bIncluded)
		{
			foreach (DataRow dr in drl)
			{
				SetIncluded(dr, bIncluded);
			}
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

			DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
			DataRow dr = (dgvc.OwningRow.DataBoundItem as DataRowView)?.Row;

			if (column.DataPropertyName == Column.Included)
			{
				if (Included(dr))
				{
					cellStyle.BackColor = Color.LightGreen;
				}
			}

			return cellStyle;
		}

		protected override void CellValueChanged(string sColumnName, DataRow dr)
		{
			AssetUnit unit = dr.Field<AssetUnit>(Column.Data);

			if (sColumnName == Column.Included)
			{
				SetIncluded(dr, dr.Field<bool>(Column.Included));
				UnitModified?.Invoke(this, new EventArgsUnit() { Unit = unit });
			}
		}
		#endregion

		#region Menus
		protected override void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e)
		{
			IEnumerable<DataRow> selectedRows = m_dgv.GetSelectedDataRows();

			menu.Items.Clear();

			if (m_dgv.Columns[Column.Included].Visible && selectedRows.Count() > 0)
			{
				menu.Items.AddMenuItem("Excluded", (object _sender, EventArgs _e) => { SetIncluded(selectedRows, false); });
				menu.Items.AddMenuItem("Included", (object _sender, EventArgs _e) => { SetIncluded(selectedRows, true); });
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
		#endregion
	}
}

using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal class GridManagerGroups : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Coalition = "Coalition";
			public static readonly string Country = "Country";
			public static readonly string DisplayName = "Display name";
			public static readonly string GroupType = "Group type";
			public static readonly string ObjectClass = "Class";
			public static readonly string Type = "Type";
			public static readonly string Attributes = "Attributes";
			public static readonly string Playable = "Playable";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopGroup> m_groups;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerGroups(DataGridView dgv, IEnumerable<BopGroup> groups) : base(dgv)
		{
			m_groups = groups;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Country, typeof(string));
			m_dtSource.Columns.Add(GridColumn.DisplayName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.GroupType, typeof(string));
			m_dtSource.Columns.Add(GridColumn.ObjectClass, typeof(ElementDcsObjectClass));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(GridColumn.Playable, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopGroup));

			foreach (BopGroup bopGroup in m_groups)
				RefreshDataSourceRow(bopGroup);
		}

		private void RefreshDataSourceRow(BopGroup bopGroup)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopGroup>(GridColumn.Data) == bopGroup).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, bopGroup);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Id, bopGroup.Id);
			dr.SetField(GridColumn.Coalition, bopGroup.CoalitionName);
			dr.SetField(GridColumn.Country, bopGroup.CountryName);
			dr.SetField(GridColumn.DisplayName, bopGroup.ToStringDisplayName());
			dr.SetField(GridColumn.GroupType, bopGroup.DcsGroupType);
			dr.SetField(GridColumn.ObjectClass, bopGroup.ObjectClass);
			dr.SetField(GridColumn.Type, bopGroup.Type);
			dr.SetField(GridColumn.Attributes, bopGroup.Attributes);
			dr.SetField(GridColumn.Playable, bopGroup.Playable);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Data].Visible = false;
		}

		public IEnumerable<BopGroup> GetSelectedBopGroups()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopGroup>(GridColumn.Data)).ToList();
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

			//DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
			//Asset asset = dgvc.DataGridView.Rows[dgvc.RowIndex].Cells[GridColumn.Data].Value as Asset;

			//if (column.DataPropertyName == GridColumn.Included)
			//{
			//	if ((asset is object && AssetOrUnitIncluded(asset)))
			//	{
			//		cellStyle.BackColor = Color.LightGreen;
			//	}
			//}
			//else if (column.DataPropertyName == GridColumn.Mission)
			//{
			//	if (asset is AssetFlight flight && flight.MissionData is object)
			//	{
			//		cellStyle.BackColor = Color.LightGreen;
			//	}
			//}

			return cellStyle;
		}

		protected override void SelectionChanged()
		{
			SelectionChangedBopGroups?.Invoke(this, new EventArgsBopGroup() { BopGroups = GetSelectedBopGroups() });
		}
		#endregion

		#region Events
		public class EventArgsBopGroup : EventArgs
		{
			public IEnumerable<BopGroup> BopGroups { get; set; }
		}
		public event EventHandler<EventArgsBopGroup> SelectionChangedBopGroups;
		#endregion
	}
}

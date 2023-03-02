using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerGroups : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Checked = "Checked";
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
		public List<int> CheckedElements = null;
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
			m_dtSource.Columns.Add(GridColumn.Checked, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Country, typeof(string));
			m_dtSource.Columns.Add(GridColumn.DisplayName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.GroupType, typeof(string));
			m_dtSource.Columns.Add(GridColumn.ObjectClass, typeof(ElementGroupClass));
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

			dr.SetField(GridColumn.Checked, CheckedElements is not null && CheckedElements.Contains(bopGroup.Id));
			dr.SetField(GridColumn.Id, bopGroup.Id);
			dr.SetField(GridColumn.Coalition, bopGroup.CoalitionName);
			dr.SetField(GridColumn.Country, bopGroup.CountryName);
			dr.SetField(GridColumn.DisplayName, bopGroup.ToStringDisplayName());
			dr.SetField(GridColumn.GroupType, bopGroup.DcsGroupType);
			dr.SetField(GridColumn.ObjectClass, bopGroup.GroupClass);
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

			m_dgv.Columns[GridColumn.Checked].ReadOnly = false;
			m_dgv.Columns[GridColumn.Checked].Visible = CheckedElements is not null;
			m_dgv.Columns[GridColumn.Data].Visible = false;
		}

		public IEnumerable<BopGroup> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopGroup>(GridColumn.Data)).ToList();
		}

		//protected override void SelectionChanged()
		//{
		//	SelectionChangedTyped?.Invoke(this, new EventArgsBopGroups() { BopGroups = GetSelected() });
		//}

		protected override void CellEndEditInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			BopGroup element = dgvc.OwningRow.Cells[GridColumn.Data].Value as BopGroup;
			if (element is not null && CheckedElements is not null && dgvc.OwningColumn.Name == GridColumn.Checked)
			{
				if ((bool)dgvc.Value)
				{
					if (!CheckedElements.Contains(element.Id))
						CheckedElements.Add(element.Id);
				}
				else
				{
					while (CheckedElements.Remove(element.Id)) ;
				}
			}
		}

		protected override void CellMouseUpInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			if (dgvc.OwningColumn.Name == GridColumn.Checked)
				dgv.EndEdit();
		}
		#endregion

		#region Events
		//public class EventArgsBopGroups : EventArgs
		//{
		//	public IEnumerable<BopGroup> BopGroups { get; set; }
		//}
		//public event EventHandler<EventArgsBopGroups> SelectionChangedTyped;
		#endregion
	}
}

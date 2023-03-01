using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerUnits : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Coalition = "Coalition";
			public static readonly string Country = "Country";
			public static readonly string Group = "Group";
			public static readonly string DisplayName = "Display name";
			public static readonly string ObjectClass = "Class";
			public static readonly string Type = "Type";
			public static readonly string Attributes = "Attributes";
			public static readonly string Playable = "Playable";
			public static readonly string Data = "Data";
		}
		public static List<string> ColumnsDisplayedGroup = new List<string>() { GridColumn.Id, GridColumn.DisplayName, GridColumn.ObjectClass, GridColumn.Type, GridColumn.Attributes, GridColumn.Playable };
		#endregion

		#region Fields
		private IEnumerable<BopUnit> m_units;
		#endregion

		#region Properties
		public IEnumerable<BopUnit> Units
		{
			get { return m_units; }
			set { m_units = value; }
		}
		#endregion

		#region CTOR
		public GridManagerUnits(DataGridView dgv, IEnumerable<BopUnit> units) : base(dgv)
		{
			m_units = units;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Country, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Group, typeof(string));
			m_dtSource.Columns.Add(GridColumn.DisplayName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.ObjectClass, typeof(ElementGroupClass));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(GridColumn.Playable, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopUnit));

			foreach (BopUnit bopUnit in m_units)
				RefreshDataSourceRow(bopUnit);
		}

		private void RefreshDataSourceRow(BopUnit bopUnit)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopUnit>(GridColumn.Data) == bopUnit).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, bopUnit);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Id, bopUnit.Id);
			dr.SetField(GridColumn.Coalition, bopUnit.BopGroup.CoalitionName);
			dr.SetField(GridColumn.Country, bopUnit.BopGroup.CountryName);
			dr.SetField(GridColumn.DisplayName, bopUnit.ToStringDisplayName());
			dr.SetField(GridColumn.ObjectClass, bopUnit.GroupClass);
			dr.SetField(GridColumn.Type, bopUnit.Type);
			dr.SetField(GridColumn.Attributes, bopUnit.Attributes);
			dr.SetField(GridColumn.Playable, bopUnit.Playable);
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

		public IEnumerable<BopUnit> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopUnit>(GridColumn.Data)).ToList();
		}

		//protected override void SelectionChanged()
		//{
		//	SelectionChangedTyped?.Invoke(this, new EventArgsBopUnits() { BopUnits = GetSelectedElements() });
		//}
		#endregion

		#region Events
		//public class EventArgsBopUnits : EventArgs
		//{
		//	public IEnumerable<BopUnit> BopUnits { get; set; }
		//}
		//public event EventHandler<EventArgsBopUnits> SelectionChangedTyped;
		#endregion
	}
}

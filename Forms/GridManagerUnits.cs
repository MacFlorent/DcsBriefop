using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;
using Zuby.ADGV;

namespace DcsBriefop.Forms
{
	internal class GridManagerUnits : GridManagerBase<BopUnit>
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
		}
		public static List<string> ColumnsDisplayedGroup { get; private set; } = new List<string>() { GridColumn.Id, GridColumn.DisplayName, GridColumn.ObjectClass, GridColumn.Type, GridColumn.Attributes, GridColumn.Playable };
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerUnits(AdvancedDataGridView dgv, IEnumerable<BopUnit> units) : base(dgv, units) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Country, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Group, typeof(string));
			m_dtSource.Columns.Add(GridColumn.DisplayName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.ObjectClass, typeof(ElementGroupClass));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(GridColumn.Playable, typeof(bool));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopUnit element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Id, element.Id);
			dr.SetField(GridColumn.Coalition, element.BopGroup.CoalitionName);
			dr.SetField(GridColumn.Country, element.BopGroup.CountryName);
			dr.SetField(GridColumn.DisplayName, element.ToStringDisplayName());
			dr.SetField(GridColumn.ObjectClass, element.GroupClass);
			dr.SetField(GridColumn.Type, element.Type);
			dr.SetField(GridColumn.Attributes, element.Attributes);
			dr.SetField(GridColumn.Playable, element.Playable);
		}
		#endregion

		#region Events
		#endregion
	}
}

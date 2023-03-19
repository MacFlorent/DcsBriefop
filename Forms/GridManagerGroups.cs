using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using System.Data;
using Zuby.ADGV;

namespace DcsBriefop.Forms
{
	internal class GridManagerGroups : GridManagerBase<BopGroup>
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
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerGroups(AdvancedDataGridView dgv, IEnumerable<BopGroup> groups) : base(dgv, groups) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Country, typeof(string));
			m_dtSource.Columns.Add(GridColumn.DisplayName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.GroupType, typeof(string));
			m_dtSource.Columns.Add(GridColumn.ObjectClass, typeof(ElementGroupClass));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(GridColumn.Playable, typeof(bool));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopGroup element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Id, element.Id);
			dr.SetField(GridColumn.Coalition, element.CoalitionName);
			dr.SetField(GridColumn.Country, element.CountryName);
			dr.SetField(GridColumn.DisplayName, element.ToStringDisplayName());
			dr.SetField(GridColumn.GroupType, element.DcsGroupType);
			dr.SetField(GridColumn.ObjectClass, element.GroupClass);
			dr.SetField(GridColumn.Type, element.Type);
			dr.SetField(GridColumn.Attributes, element.Attributes);
			dr.SetField(GridColumn.Playable, element.Playable);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.Id].Width = GridWidth.Small;
			m_dgv.Columns[GridColumn.Coalition].Width = GridWidth.Small;
			m_dgv.Columns[GridColumn.Country].Width = GridWidth.Small;
			m_dgv.Columns[GridColumn.DisplayName].Width = GridWidth.Large;
			m_dgv.Columns[GridColumn.Playable].Width = GridWidth.Small;
		}

		protected override DataGridViewCellStyle CellFormattingInternal(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormattingInternal(dgvc);

			DataGridViewColumn column = dgvc.OwningColumn;
			BopGroup element = GetBoundElement(dgvc.OwningRow);

			cellStyle.ForeColor = ToolsBriefop.GetCoalitionColor(element.CoalitionName);

			return cellStyle;
		}
		#endregion

		#region Events
		#endregion
	}
}

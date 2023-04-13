using DcsBriefop.DataBopBriefing;
using System.Data;
using Zuby.ADGV;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingFolders : GridManagerBase<BopBriefingFolder>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Name = "Name";
			public static readonly string Coalition = "Coalition";
			public static readonly string UnitTypes = "UnitTypes";
			public static readonly string PageCount = "PageCount";
			public static readonly string Inactive = "Inactive";
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerBriefingFolders(AdvancedDataGridView dgv, IEnumerable<BopBriefingFolder> briefingFolders) : base(dgv, briefingFolders) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Id, typeof(Guid));
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.UnitTypes, typeof(string));
			m_dtSource.Columns.Add(GridColumn.PageCount, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Inactive, typeof(bool));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopBriefingFolder element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Id, element.Guid);
			dr.SetField(GridColumn.Name, element.Name);
			dr.SetField(GridColumn.Coalition, element.CoalitionName);
			dr.SetField(GridColumn.UnitTypes, string.Join(",", element.Kneeboards));
			dr.SetField(GridColumn.PageCount, element.Pages.Count);
			dr.SetField(GridColumn.Inactive, element.Inactive);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.UnitTypes].HeaderText = "Unit types";
			m_dgv.Columns[GridColumn.PageCount].HeaderText = "Pages count";

			m_dgv.Columns[GridColumn.Id].Width = GridWidth.Small;
			m_dgv.Columns[GridColumn.UnitTypes].Width = GridWidth.Large;
		}

		protected override DataGridViewCellStyle CellFormattingInternal(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormattingInternal(dgvc);

			DataGridViewColumn column = dgvc.OwningColumn;
			BopBriefingFolder element = GetBoundElement(dgvc.OwningRow);

			if (element.Inactive)
				cellStyle.ForeColor = Color.Gray;

			return cellStyle;
		}
		#endregion

		#region Events
		#endregion
	}
}

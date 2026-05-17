using BrightIdeasSoftware;
using DcsBriefop.DataBopBriefing;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingFolders(FastObjectListView dgv, IEnumerable<BopBriefingFolder> briefingFolders) : GridManagerBase<BopBriefingFolder>(dgv, briefingFolders)
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

		#region Methods
		protected override void InitializeColumns()
		{
			m_grid.MultiSelect = false;
			m_grid.AllColumns.AddRange(
			[
				new() { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopBriefingFolder)obj).Guid },
				new() { Text = "Name", Name = GridColumn.Name, Width = GridWidth.Large, AspectGetter = obj => ((BopBriefingFolder)obj).Name },
				new() { Text = "Coalition", Name = GridColumn.Coalition, Width = GridWidth.Medium, AspectGetter = obj => ((BopBriefingFolder)obj).CoalitionName },
				new() { Text = "Unit types", Name = GridColumn.UnitTypes, Width = GridWidth.Large, AspectGetter = obj => string.Join(",", ((BopBriefingFolder)obj).Kneeboards) },
				new() { Text = "Pages count", Name = GridColumn.PageCount, Width = GridWidth.Medium, AspectGetter = obj => ((BopBriefingFolder)obj).Pages.Count },
				new() { Text = "Inactive", Name = GridColumn.Inactive, Width = GridWidth.Small, AspectGetter = obj => ((BopBriefingFolder)obj).Inactive },
			]);
			m_grid.RebuildColumns();
		}

		protected override void FormatRowInternal(FormatRowEventArgs e)
		{
			BopBriefingFolder element = (BopBriefingFolder)e.Model;
			if (element.Inactive)
				e.Item.ForeColor = Color.Gray;
		}
		#endregion
	}
}

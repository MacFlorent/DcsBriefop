using BrightIdeasSoftware;
using DcsBriefop.DataBopBriefing;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingPages(FastObjectListView dgv, IEnumerable<BopBriefingPage> briefingPages) : GridManagerBase<BopBriefingPage>(dgv, briefingPages)
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Title = "Title";
			public static readonly string Render = "Render";
		}

		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_grid.AllColumns.AddRange(
			[
				new() { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopBriefingPage)obj).Guid },
				new() { Text = "Title", Name = GridColumn.Title, Width = GridWidth.Large, AspectGetter = obj => ((BopBriefingPage)obj).Title },
				new() { Text = "Render", Name = GridColumn.Render, Width = GridWidth.Medium, AspectGetter = obj => ((BopBriefingPage)obj).Render },
			]);
			m_grid.RebuildColumns();
		}
		#endregion
	}
}

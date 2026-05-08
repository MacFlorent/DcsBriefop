using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingPages : GridManagerBase<BopBriefingPage>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Title = "Title";
			public static readonly string Render = "Render";
		}
		#endregion

		#region CTOR
		public GridManagerBriefingPages(FastObjectListView dgv, IEnumerable<BopBriefingPage> briefingPages) : base(dgv, briefingPages) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_dgv.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopBriefingPage)obj).Guid },
				new OLVColumn { Text = "Title", Name = GridColumn.Title, Width = GridWidth.Large, AspectGetter = obj => ((BopBriefingPage)obj).Title },
				new OLVColumn { Text = "Render", Name = GridColumn.Render, Width = GridWidth.Medium, AspectGetter = obj => ((BopBriefingPage)obj).Render },
			});
			m_dgv.RebuildColumns();
		}
		#endregion
	}
}

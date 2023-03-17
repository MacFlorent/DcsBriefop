using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using System.Data;
using Zuby.ADGV;

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

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerBriefingPages(AdvancedDataGridView dgv, IEnumerable<BopBriefingPage> briefingPages) : base(dgv, briefingPages) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Id, typeof(Guid));
			m_dtSource.Columns.Add(GridColumn.Title, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Render, typeof(ElementBriefingPageRender));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopBriefingPage element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Id, element.Guid);
			dr.SetField(GridColumn.Title, element.Title);
			dr.SetField(GridColumn.Render, element.Render);
		}
		#endregion

		#region Events
		#endregion
	}
}

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
			public static readonly string Kneeboards = "Kneeboards";
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
			m_dtSource.Columns.Add(GridColumn.Kneeboards, typeof(string));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopBriefingFolder element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Id, element.Guid);
			dr.SetField(GridColumn.Name, element.Name);
			dr.SetField(GridColumn.Coalition, element.CoalitionName);
			dr.SetField(GridColumn.Kneeboards, string.Join(",", element.Kneeboards));
		}
		#endregion

		#region Events
		#endregion
	}
}

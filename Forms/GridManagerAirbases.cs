using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerAirbases : GridManagerBase<BopAirbase>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string AirbaseType = "AirbaseType"; 
			public static readonly string Name = "Name";
			public static readonly string Additional = "Additional";
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerAirbases(DataGridView dgv, IEnumerable<BopAirbase> airbases) : base(dgv, airbases) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.AirbaseType, typeof(ElementAirbaseType));
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Additional, typeof(string));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopAirbase element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Id, element.Id);
			dr.SetField(GridColumn.AirbaseType, element.AirbaseType);
			dr.SetField(GridColumn.Name, element.Name);
			dr.SetField(GridColumn.Additional, element.ToStringAdditional());
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.AirbaseType].HeaderText = "Type";
		}
		#endregion

		#region Events
		#endregion
	}
}

using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using System.Data;
using Zuby.ADGV;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingParts : GridManagerBase<BaseBopBriefingPart>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string PartName = "PartName";
			public static readonly string Information = "Information";
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerBriefingParts(AdvancedDataGridView dgv, IEnumerable<BaseBopBriefingPart> briefingParts) : base(dgv, briefingParts) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Id, typeof(Guid));
			m_dtSource.Columns.Add(GridColumn.PartName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Information, typeof(string));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BaseBopBriefingPart element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			MasterData partType = MasterDataRepository.GetById(MasterDataType.BriefingPartType, (int)element.PartType);
			string sPartName = partType?.Label ?? element.PartType.ToString();

			dr.SetField(GridColumn.Id, element.Guid);
			dr.SetField(GridColumn.PartName, sPartName);
			dr.SetField(GridColumn.Information, element.ToStringAdditional());
		}


		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.PartName].HeaderText = "Part name";

			m_dgv.Columns[GridColumn.Id].Width = GridWidth.Small;
		}
		#endregion

		#region Events
		#endregion
	}
}

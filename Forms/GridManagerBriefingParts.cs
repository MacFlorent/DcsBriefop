using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;

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

		#region CTOR
		public GridManagerBriefingParts(FastObjectListView dgv, IEnumerable<BaseBopBriefingPart> briefingParts) : base(dgv, briefingParts) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_dgv.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BaseBopBriefingPart)obj).Guid },
				new OLVColumn { Text = "Part name", Name = GridColumn.PartName, Width = GridWidth.Medium, AspectGetter = obj =>
				{
					BaseBopBriefingPart part = (BaseBopBriefingPart)obj;
					MasterData partType = MasterDataRepository.GetById(MasterDataType.BriefingPartType, (int)part.PartType);
					return partType?.Label ?? part.PartType.ToString();
				}},
				new OLVColumn { Text = "Information", Name = GridColumn.Information, Width = GridWidth.ExtraLarge, AspectGetter = obj => ((BaseBopBriefingPart)obj).ToStringAdditional() },
			});
			m_dgv.RebuildColumns();
		}
		#endregion
	}
}

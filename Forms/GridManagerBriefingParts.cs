using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingParts(FastObjectListView dgv, IEnumerable<BaseBopBriefingPart> briefingParts) : GridManagerBase<BaseBopBriefingPart>(dgv, briefingParts)
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string PartName = "PartName";
			public static readonly string Information = "Information";
		}

		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_grid.AllColumns.AddRange(
			[
				new() { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BaseBopBriefingPart)obj).Guid },
				new() { Text = "Part name", Name = GridColumn.PartName, Width = GridWidth.Medium, AspectGetter = obj =>
				{
					BaseBopBriefingPart part = (BaseBopBriefingPart)obj;
					MasterData partType = MasterDataRepository.GetById(MasterDataType.BriefingPartType, (int)part.PartType);
					return partType?.Label ?? part.PartType.ToString();
				}},
				new() { Text = "Information", Name = GridColumn.Information, Width = GridWidth.ExtraLarge, AspectGetter = obj => ((BaseBopBriefingPart)obj).ToStringAdditional() },
			]);
			m_grid.RebuildColumns();
		}
		#endregion
	}
}

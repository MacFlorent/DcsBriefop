using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal class GridManagerGroups : GridManagerBase<BopGroup>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Coalition = "Coalition";
			public static readonly string Country = "Country";
			public static readonly string DisplayName = "DisplayName";
			public static readonly string GroupType = "GroupType";
			public static readonly string ObjectClass = "ObjectClass";
			public static readonly string Type = "Type";
			public static readonly string Attributes = "Attributes";
			public static readonly string Playable = "Playable";
		}
		#endregion

		#region CTOR
		public GridManagerGroups(FastObjectListView dgv, IEnumerable<BopGroup> groups) : base(dgv, groups) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_dgv.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopGroup)obj).Id },
				new OLVColumn { Text = "Coalition", Name = GridColumn.Coalition, Width = GridWidth.Small, AspectGetter = obj => ((BopGroup)obj).CoalitionName },
				new OLVColumn { Text = "Country", Name = GridColumn.Country, Width = GridWidth.Small, AspectGetter = obj => ((BopGroup)obj).CountryName },
				new OLVColumn { Text = "Display name", Name = GridColumn.DisplayName, Width = GridWidth.Large, AspectGetter = obj => ((BopGroup)obj).ToStringDisplayName() },
				new OLVColumn { Text = "Group type", Name = GridColumn.GroupType, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroup)obj).DcsGroupType },
				new OLVColumn { Text = "Class", Name = GridColumn.ObjectClass, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroup)obj).GroupClass },
				new OLVColumn { Text = "Type", Name = GridColumn.Type, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroup)obj).Type },
				new OLVColumn { Text = "Attributes", Name = GridColumn.Attributes, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroup)obj).Attributes },
				new OLVColumn { Text = "Playable", Name = GridColumn.Playable, Width = GridWidth.Small, CheckBoxes = true, AspectGetter = obj => ((BopGroup)obj).Playable },
			});
			m_dgv.RebuildColumns();
		}

		protected override void FormatRowInternal(FormatRowEventArgs e)
		{
			BopGroup element = (BopGroup)e.Model;
			e.Item.ForeColor = ToolsBriefop.GetCoalitionColor(element.CoalitionName);
		}
		#endregion
	}
}

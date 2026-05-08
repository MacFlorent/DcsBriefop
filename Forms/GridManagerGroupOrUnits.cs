using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal class GridManagerGroupOrUnits : GridManagerBase<BopGroupOrUnit>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string DisplayName = "DisplayName";
			public static readonly string Coalition = "Coalition";
			public static readonly string Country = "Country";
			public static readonly string GroupOrUnit = "GroupOrUnit";
			public static readonly string Group = "Group";
			public static readonly string Type = "Type";
			public static readonly string ObjectClass = "ObjectClass";
			public static readonly string Attributes = "Attributes";
			public static readonly string Radio = "Radio";
			public static readonly string Additional = "Additional";
		}
		#endregion

		#region CTOR
		public GridManagerGroupOrUnits(FastObjectListView dgv, IEnumerable<BopGroupOrUnit> elements) : base(dgv, elements) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_dgv.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopGroupOrUnit)obj).Id },
				new OLVColumn { Text = "Display name", Name = GridColumn.DisplayName, Width = GridWidth.Large, AspectGetter = obj => ((BopGroupOrUnit)obj).DisplayName },
				new OLVColumn { Text = "Coalition", Name = GridColumn.Coalition, Width = GridWidth.Small, AspectGetter = obj => ((BopGroupOrUnit)obj).Coalition },
				new OLVColumn { Text = "Country", Name = GridColumn.Country, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroupOrUnit)obj).Country },
				new OLVColumn { Text = "Object", Name = GridColumn.GroupOrUnit, Width = GridWidth.Small, AspectGetter = obj => ((BopGroupOrUnit)obj).GroupOrUnit },
				new OLVColumn { Text = "Group", Name = GridColumn.Group, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroupOrUnit)obj).Group },
				new OLVColumn { Text = "Type", Name = GridColumn.Type, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroupOrUnit)obj).Type },
				new OLVColumn { Text = "Class", Name = GridColumn.ObjectClass, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroupOrUnit)obj).GroupClass },
				new OLVColumn { Text = "Attributes", Name = GridColumn.Attributes, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroupOrUnit)obj).Attributes },
				new OLVColumn { Text = "Radio", Name = GridColumn.Radio, Width = GridWidth.Medium, AspectGetter = obj => ((BopGroupOrUnit)obj).Radio },
				new OLVColumn { Text = "Additional", Name = GridColumn.Additional, Width = GridWidth.ExtraLarge, AspectGetter = obj => ((BopGroupOrUnit)obj).Additional },
			});
			m_dgv.RebuildColumns();
		}

		protected override void FormatRowInternal(FormatRowEventArgs e)
		{
			BopGroupOrUnit element = (BopGroupOrUnit)e.Model;
			e.Item.ForeColor = ToolsBriefop.GetCoalitionColor(element.Coalition);
			if (element.GroupOrUnit == ElementGroupOrUnit.Group)
				e.Item.BackColor = Color.LightGray;
		}
		#endregion
	}
}

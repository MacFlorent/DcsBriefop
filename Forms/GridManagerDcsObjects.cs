using BrightIdeasSoftware;
using DcsBriefop.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerDcsObjects : GridManagerBase<DcsObject>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string TypeName = "TypeName";
			public static readonly string DisplayName = "DisplayName";
			public static readonly string GroupClass = "GroupClass";
			public static readonly string Attributes = "Attributes";
			public static readonly string MapMarker = "MapMarker";
			public static readonly string Information = "Information";
			public static readonly string MainInGroup = "MainInGroup";
		}
		#endregion

		#region CTOR
		public GridManagerDcsObjects(FastObjectListView dgv, IEnumerable<DcsObject> elements) : base(dgv, elements) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_dgv.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Type", Name = GridColumn.TypeName, Width = GridWidth.Medium, AspectGetter = obj => ((DcsObject)obj).TypeName },
				new OLVColumn { Text = "Name", Name = GridColumn.DisplayName, Width = GridWidth.Large, AspectGetter = obj => ((DcsObject)obj).DisplayName },
				new OLVColumn { Text = "Class", Name = GridColumn.GroupClass, Width = GridWidth.Medium, AspectGetter = obj => ((DcsObject)obj).GroupClass },
				new OLVColumn { Text = "Attributes", Name = GridColumn.Attributes, Width = GridWidth.Medium, AspectGetter = obj => ((DcsObject)obj).Attributes },
				new OLVColumn { Text = "Marker", Name = GridColumn.MapMarker, Width = GridWidth.Medium, AspectGetter = obj => ((DcsObject)obj).MapMarker },
				new OLVColumn { Text = "Information", Name = GridColumn.Information, Width = GridWidth.ExtraLarge, AspectGetter = obj => ((DcsObject)obj).Information },
				new OLVColumn { Text = "Main in group", Name = GridColumn.MainInGroup, Width = GridWidth.Small, CheckBoxes = true, AspectGetter = obj => ((DcsObject)obj).MainInGroup },
			});
			m_dgv.RebuildColumns();
		}
		#endregion
	}
}

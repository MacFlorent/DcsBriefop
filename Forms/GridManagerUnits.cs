using BrightIdeasSoftware;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.Forms
{
	internal class GridManagerUnits(FastObjectListView dgv, IEnumerable<BopUnit> units) : GridManagerBase<BopUnit>(dgv, units)
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Coalition = "Coalition";
			public static readonly string Country = "Country";
			public static readonly string Group = "Group";
			public static readonly string DisplayName = "DisplayName";
			public static readonly string ObjectClass = "ObjectClass";
			public static readonly string Type = "Type";
			public static readonly string Attributes = "Attributes";
			public static readonly string Playable = "Playable";
			public static readonly string Callsign = "Callsign";
			public static readonly string CallsignGroup = "CallsignGroup";
			public static readonly string CallsignElement = "CallsignElement";
			public static readonly string DatalinkType = "DatalinkType";
			public static readonly string DatalinkCallsign = "DatalinkCallsign";
			public static readonly string DatalinkId = "DatalinkId";
		}
		public static List<string> ColumnsDisplayedGroup { get; private set; } = [GridColumn.Id, GridColumn.DisplayName, GridColumn.ObjectClass, GridColumn.Type, GridColumn.Attributes, GridColumn.Playable];

		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_grid.AllColumns.AddRange(
			[
				new() { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopUnit)obj).Id },
				new() { Text = "Coalition", Name = GridColumn.Coalition, Width = GridWidth.Small, AspectGetter = obj => ((BopUnit)obj).BopGroup.CoalitionName },
				new() { Text = "Country", Name = GridColumn.Country, Width = GridWidth.Small, AspectGetter = obj => ((BopUnit)obj).BopGroup.CountryName },
				new() { Text = "Group", Name = GridColumn.Group, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).BopGroup.ToStringDisplayName() },
				new() { Text = "Display name", Name = GridColumn.DisplayName, Width = GridWidth.Large, AspectGetter = obj => ((BopUnit)obj).ToStringDisplayName() },
				new() { Text = "Class", Name = GridColumn.ObjectClass, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).GroupClass },
				new() { Text = "Type", Name = GridColumn.Type, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).Type },
				new() { Text = "Attributes", Name = GridColumn.Attributes, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).Attributes },
				new() { Text = "Playable", Name = GridColumn.Playable, Width = GridWidth.Small, CheckBoxes = true, AspectGetter = obj => ((BopUnit)obj).Playable },
				new() { Text = "Callsign", Name = GridColumn.Callsign, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.Callsign?.ToString() : null },
				new() { Text = "Callsign group", Name = GridColumn.CallsignGroup, Width = GridWidth.Small, AspectGetter = obj => (obj is BopUnitFlight f) ? f.Callsign?.Group : (object)null },
				new() { Text = "Callsign elem", Name = GridColumn.CallsignElement, Width = GridWidth.Small, AspectGetter = obj => (obj is BopUnitFlight f) ? f.Callsign?.Element : (object)null },
				new() { Text = "Datalink type", Name = GridColumn.DatalinkType, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.DatalinkId?.DatalinkType : null },
				new() { Text = "Datalink callsign", Name = GridColumn.DatalinkCallsign, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.DatalinkId?.ToStringCallsign() : null },
				new() { Text = "Datalink ID", Name = GridColumn.DatalinkId, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.DatalinkId?.Id : null },
			]);
			m_grid.RebuildColumns();
		}
		#endregion
	}
}

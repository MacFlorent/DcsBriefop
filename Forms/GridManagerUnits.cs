using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.Forms
{
	internal class GridManagerUnits : GridManagerBase<BopUnit>
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
		public static List<string> ColumnsDisplayedGroup { get; private set; } = new List<string>() { GridColumn.Id, GridColumn.DisplayName, GridColumn.ObjectClass, GridColumn.Type, GridColumn.Attributes, GridColumn.Playable };
		#endregion

		#region CTOR
		public GridManagerUnits(FastObjectListView dgv, IEnumerable<BopUnit> units) : base(dgv, units) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_dgv.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopUnit)obj).Id },
				new OLVColumn { Text = "Coalition", Name = GridColumn.Coalition, Width = GridWidth.Small, AspectGetter = obj => ((BopUnit)obj).BopGroup.CoalitionName },
				new OLVColumn { Text = "Country", Name = GridColumn.Country, Width = GridWidth.Small, AspectGetter = obj => ((BopUnit)obj).BopGroup.CountryName },
				new OLVColumn { Text = "Group", Name = GridColumn.Group, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).BopGroup.ToStringDisplayName() },
				new OLVColumn { Text = "Display name", Name = GridColumn.DisplayName, Width = GridWidth.Large, AspectGetter = obj => ((BopUnit)obj).ToStringDisplayName() },
				new OLVColumn { Text = "Class", Name = GridColumn.ObjectClass, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).GroupClass },
				new OLVColumn { Text = "Type", Name = GridColumn.Type, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).Type },
				new OLVColumn { Text = "Attributes", Name = GridColumn.Attributes, Width = GridWidth.Medium, AspectGetter = obj => ((BopUnit)obj).Attributes },
				new OLVColumn { Text = "Playable", Name = GridColumn.Playable, Width = GridWidth.Small, CheckBoxes = true, AspectGetter = obj => ((BopUnit)obj).Playable },
				new OLVColumn { Text = "Callsign", Name = GridColumn.Callsign, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.Callsign?.ToString() : null },
				new OLVColumn { Text = "Callsign group", Name = GridColumn.CallsignGroup, Width = GridWidth.Small, AspectGetter = obj => (obj is BopUnitFlight f) ? f.Callsign?.Group : (object)null },
				new OLVColumn { Text = "Callsign elem", Name = GridColumn.CallsignElement, Width = GridWidth.Small, AspectGetter = obj => (obj is BopUnitFlight f) ? f.Callsign?.Element : (object)null },
				new OLVColumn { Text = "Datalink type", Name = GridColumn.DatalinkType, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.DatalinkId?.DatalinkType : null },
				new OLVColumn { Text = "Datalink callsign", Name = GridColumn.DatalinkCallsign, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.DatalinkId?.ToStringCallsign() : null },
				new OLVColumn { Text = "Datalink ID", Name = GridColumn.DatalinkId, Width = GridWidth.Medium, AspectGetter = obj => (obj is BopUnitFlight f) ? f.DatalinkId?.Id : null },
			});
			m_dgv.RebuildColumns();
		}
		#endregion
	}
}

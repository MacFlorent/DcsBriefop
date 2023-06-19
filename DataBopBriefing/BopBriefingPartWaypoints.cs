using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using HtmlTags;
using System.Text;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartWaypoints : BaseBopBriefingPart
	{
		#region Fields
		private static class TableColumns
		{
			public static readonly string Number = "Number";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Track = "Track°M";
			public static readonly string Distance = "Dist.";
			public static readonly string Altitude = "Altitude";
			public static readonly string Speed = "Speed";
			public static readonly string Task = "Task";
			public static readonly string Notes = "Notes";
		}
		public static List<string> AvailableTableColumns = new()
		{
			TableColumns.Number,
			TableColumns.Name,
			TableColumns.Type,
			TableColumns.Track,
			TableColumns.Distance,
			TableColumns.Altitude,
			TableColumns.Speed,
			TableColumns.Task,
			TableColumns.Notes
		};
		#endregion

		#region Properties
		public string Header { get; set; }
		public int GroupId { get; set; }
		public bool DisplayGroupName { get; set; }
		public bool DisplayGraph { get; set; }
		public bool IncludeBullseye { get; set; }
		public List<string> SelectedTableColumns { get; set; } = new();
		#endregion

		#region CTOR
		public BopBriefingPartWaypoints() : base(ElementBriefingPartType.Waypoints, "table") { }
		#endregion

		#region Methods
		public override void InitializeDefault()
		{
			base.InitializeDefault();
			DisplayGroupName = true;
			IncludeBullseye = false;
			DisplayGraph = false;
			SelectedTableColumns.AddRange(new List<string> { TableColumns.Number, TableColumns.Altitude, TableColumns.Notes });
		}

		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendWithSeparator(Header, " ");
			sb.AppendWithSeparator($"group {GroupId}", " - ");
			return sb.ToString();
		}

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			BopGroup bopGroup = bopManager.BopMission.Groups.Where(_g => _g.Id == GroupId).FirstOrDefault();
			if (bopGroup is null)
				return tags;

			bopGroup.FinalizeFromMiz();
			IEnumerable<BopRoutePoint> bopRoutePoints = bopGroup.RoutePoints.Where(_brp => IncludeBullseye || _brp.Name != ElementGlobalData.BullseyeRoutePointName);

			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}
			if (bopGroup is not null && DisplayGroupName)
			{
				tags.Add(new HtmlTag("h2").AppendText($"Waypoints: {bopGroup.ToStringDisplayName()}"));
			}

			IEnumerable<string> columns = GetColumns();
			HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");
			HtmlTag tagThead = tagTable.Add("thead");
			foreach (string sColumn in columns)
			{
				string sTableHeader = sColumn;
				if (sTableHeader == TableColumns.Altitude)
					sTableHeader = $"{sColumn} {ToolsMeasurement.AltitudeUnit(bopBriefingFolder.MeasurementSystem)}";
				else if (sTableHeader == TableColumns.Distance)
					sTableHeader = $"{sColumn} {ToolsMeasurement.DistanceUnit(bopBriefingFolder.MeasurementSystem)}";
				else if (sTableHeader == TableColumns.Speed)
					sTableHeader = $"{sColumn} {ToolsMeasurement.SpeedUnit(bopBriefingFolder.MeasurementSystem)}";

				tagThead.Add("td").AddClass("header").AppendText(sTableHeader);
			}

			if (bopRoutePoints is not null)
			{
				foreach(BopRoutePoint bopRoutePoint in bopRoutePoints)
				{
					HtmlTag tagTr = tagTable.Add("tr");
					foreach (string sColumn in columns)
					{
						if (sColumn == TableColumns.Number)
							tagTr.Add("td").AppendText(bopRoutePoint.Number.ToString());
						else if (sColumn == TableColumns.Name)
							tagTr.Add("td").AppendText(bopRoutePoint.Name);
						else if (sColumn == TableColumns.Type)
							tagTr.Add("td").AppendText(bopRoutePoint.Type);
						else if (sColumn == TableColumns.Track)
							tagTr.Add("td").AppendText($"{bopRoutePoint.GetTrack(true):000}");
						else if (sColumn == TableColumns.Distance)
							tagTr.Add("td").AppendText($"{bopRoutePoint.GetDistance(bopBriefingFolder.MeasurementSystem):0}");
						else if (sColumn == TableColumns.Altitude)
							tagTr.Add("td").AppendText($"{bopRoutePoint.GetAltitude(bopBriefingFolder.MeasurementSystem):0}");
						else if (sColumn == TableColumns.Speed)
							tagTr.Add("td").AppendText($"{bopRoutePoint.GetSpeed(bopBriefingFolder.MeasurementSystem):0}");
						else if (sColumn == TableColumns.Task)
							tagTr.Add("td").AppendText(bopRoutePoint.Tasks.FirstOrDefault()?.ToStringDisplayName());
						else if (sColumn == TableColumns.Notes)
							tagTr.Add("td").AppendText(bopRoutePoint.Notes);
					}
				}
			}

			tags.Add(tagTable);

			if (DisplayGraph && bopRoutePoints is not null)
				tags.AddRange(BuildHtmlContentGraph(bopManager, bopBriefingFolder, bopRoutePoints));
			
			return tags;
		}

		private List<HtmlTag> BuildHtmlContentGraph(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder, IEnumerable<BopRoutePoint> bopRoutePoints)
		{
			List<HtmlTag> tags = new List<HtmlTag>();
			BopGroup bopGroup = bopManager.BopMission.Groups.Where(_g => _g.Id == GroupId).FirstOrDefault();
			//https://www.html5canvastutorials.com/tutorials/html5-canvas-paths/
			string sCanvasName = $"graphCanvas{Guid}";
			HtmlTag tagCanvas = new HtmlTag("canvas")
				.Attr("width", $"{bopBriefingFolder.ImageSize.Width}")
				.Attr("height", $"{bopBriefingFolder.ImageSize.Height / 5}")
				.Attr("style", $"border:1px solid #000000;")
				.Id(sCanvasName);
			HtmlTag tagScript = new HtmlTag("script");
			string sScript =
$$"""
	var canvas = document.getElementById("{{sCanvasName}}");
	var ctx = canvas.getContext("2d");
	ctx.fillStyle = "white";
	ctx.fillRect(0, 0, canvas.width, canvas.height);

	var myLineChart = new LineChart({
	canvasId: "{{sCanvasName}}",
	minX: 0,
	minY: 0,
	maxX: {{bopRoutePoints.Max(_brp => _brp.Number)}},
	maxY: {{bopRoutePoints.Max(_brp => _brp.GetAltitude(bopBriefingFolder.MeasurementSystem)) + 100}},
	unitsPerTickX: 1,
	unitsPerTickY: 10000
	});
	var data = [
	{{BuildHtmlContentGraphPoints(bopManager, bopBriefingFolder, bopRoutePoints)}}
	];
	myLineChart.drawLine(data, "red", 3);
""";

			tagScript.AppendHtml(sScript);
			tags.Add(tagCanvas);
			tags.Add(tagScript);
			return tags;
		}

		private string BuildHtmlContentGraphPoints(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder, IEnumerable<BopRoutePoint> bopRoutePoints)
		{
			StringBuilder sb = new StringBuilder();
			BopGroup bopGroup = bopManager.BopMission.Groups.Where(_g => _g.Id == GroupId).FirstOrDefault();
			
			foreach (BopRoutePoint brp in bopRoutePoints)
			{
				sb.AppendWithSeparator($$"""{ x: {{brp.Number}}, y: {{brp.GetAltitude(bopBriefingFolder.MeasurementSystem)}} }""", ",");
			}

			return sb.ToString();
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<GMapOverlay> partOverlays = new List<GMapOverlay>();
			BopGroup bopGroup = bopManager.BopMission.Groups.Where(_g => _g.Id == GroupId).FirstOrDefault();
			if (bopGroup is not null)
			{
			bopGroup.FinalizeFromMiz();
			partOverlays.Add(bopGroup.GetMapOverlayRoute(null, ElementMapOverlayRouteDisplay.PointLabelLight, bopBriefingFolder.MeasurementSystem));
			}

			return partOverlays;
		}

		private IEnumerable<string> GetColumns()
		{
			if (SelectedTableColumns is not null && SelectedTableColumns.Count > 0)
				return AvailableTableColumns.Where(_c => SelectedTableColumns.Contains(_c));
			else
				return AvailableTableColumns;
		}
		#endregion
	}


}

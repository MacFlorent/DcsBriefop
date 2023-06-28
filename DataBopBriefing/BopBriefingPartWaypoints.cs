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

		public override IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new();

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
			HtmlTag tagThead = tagTable.Add("tr");
			foreach (string sColumn in columns)
			{
				string sTableHeader = sColumn;
				if (sTableHeader == TableColumns.Altitude)
					sTableHeader = $"{sColumn} {ToolsMeasurement.AltitudeUnit(bopBriefingFolder.MeasurementSystem)}";
				else if (sTableHeader == TableColumns.Distance)
					sTableHeader = $"{sColumn} {ToolsMeasurement.DistanceUnit(bopBriefingFolder.MeasurementSystem)}";
				else if (sTableHeader == TableColumns.Speed)
					sTableHeader = $"{sColumn} {ToolsMeasurement.SpeedUnit(bopBriefingFolder.MeasurementSystem)}";

				tagThead.Add("th").AppendText(sTableHeader);
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
						{
							string sSpeeds = $"{bopRoutePoint.GetSpeedCalibrated(PreferencesManager.Preferences.Briefing.MeasurementSystem):0} CAS / {bopRoutePoint.GetSpeedMach():0.00} M";
							tagTr.Add("td").AppendText(sSpeeds);
						}
						else if (sColumn == TableColumns.Task)
							tagTr.Add("td").AppendText(bopRoutePoint.Tasks.FirstOrDefault()?.ToStringDisplayName());
						else if (sColumn == TableColumns.Notes)
							tagTr.Add("td").AppendText(bopRoutePoint.Notes);
					}
				}
			}
			tags.Add(tagTable);

			if (DisplayGraph && bopRoutePoints is not null)
				tags.AddRange(BuildHtmlContentChart(bopManager, bopBriefingFolder, bopRoutePoints));
			
			return tags;
		}

		private List<HtmlTag> BuildHtmlContentChart(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder, IEnumerable<BopRoutePoint> bopRoutePoints)
		{
			List<HtmlTag> tags = new();
			BopGroup bopGroup = bopManager.BopMission.Groups.Where(_g => _g.Id == GroupId).FirstOrDefault();
			string sChartName = $"chartWaypoints{Guid}";
			HtmlTag tagChart = new HtmlTag("div").Id(sChartName);

			int iWidth = bopBriefingFolder.ImageSize.Width - 10;
			int iHeight = bopBriefingFolder.ImageSize.Height / 4;

			string sLegend = $"Altitude {ToolsMeasurement.AltitudeUnit(bopBriefingFolder.MeasurementSystem)} - Speeds KIAS - Tracks mag°";
			string sColor = "null";
			if (!string.IsNullOrEmpty(bopBriefingFolder.CoalitionName))
			{
				Color color = ToolsBriefop.GetCoalitionColor(bopBriefingFolder.CoalitionName);
				sColor = $"\"#{color.R:X2}{color.G:X2}{color.B:X2}\"";
			}

			StringBuilder sbDataPoints = new();
			StringBuilder sbNames = new();
			StringBuilder sbTracks = new();
			StringBuilder sbSpeeds = new();

			foreach (BopRoutePoint brp in bopRoutePoints)
			{
				sbDataPoints.AppendWithSeparator($"[{brp.Number:0}, {brp.GetAltitude(bopBriefingFolder.MeasurementSystem):0}]", ",");
				
				if (string.IsNullOrEmpty(brp.Name))
					sbNames.AppendWithSeparator($"0", ",");
				else
					sbNames.AppendWithSeparator($"\"{brp.Name}\"", ",");

				double? dTrack = brp.GetTrack(true);
				if (dTrack is null || brp.Number == 0)
					sbTracks.AppendWithSeparator($"0", ",");
				else
					sbTracks.AppendWithSeparator($"\"{dTrack:000}°\"", ",");

				double? dSpeed = brp.GetSpeedCalibrated(bopBriefingFolder.MeasurementSystem);
				if (dSpeed is null || brp.Number == 0)
					sbSpeeds.AppendWithSeparator($"0", ",");
				else
					sbSpeeds.AppendWithSeparator($"\"{dSpeed:0}\"", ",");
			}


			HtmlTag tagScript = new("script");
			string sScript =
$$"""
	let chartDataPoints = [{{sbDataPoints}}];
	let chartWaypointNames = [{{sbNames}}];
	let chartWaypointTracks = [{{sbTracks}}];
	let chartWaypointSpeeds = [{{sbSpeeds}}];

	ApexChartCreate(
		"{{sLegend}}",
		chartDataPoints, chartWaypointNames, chartWaypointTracks, chartWaypointSpeeds, {{sColor}}, "{{iWidth}}px", "{{iHeight}}px", "{{sChartName}}"
	);
""";

			tagScript.AppendHtml(sScript);
			tags.Add(tagChart);
			tags.Add(tagScript);
			return tags;
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<GMapOverlay> partOverlays = new();
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

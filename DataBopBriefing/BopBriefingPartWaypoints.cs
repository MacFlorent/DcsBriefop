using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
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
			public static readonly string Altitude = "Altitude";
			public static readonly string Task = "Task";
			public static readonly string Notes = "Notes";
		}
		public static List<string> AvailableTableColumns = new()
		{
			TableColumns.Number,
			TableColumns.Name,
			TableColumns.Type,
			TableColumns.Altitude,
			TableColumns.Task,
			TableColumns.Notes
		};
		#endregion

		#region Properties
		public string Header { get; set; }
		public int GroupId { get; set; }
		public bool DisplayGroupName { get; set; }
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
			SelectedTableColumns.AddRange(new List<string> { TableColumns.Number, TableColumns.Altitude, TableColumns.Notes });
		}

		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendWithSeparator(Header, " ");
			sb.AppendWithSeparator($"group {GroupId}", " - ");
			return sb.ToString();
		}

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			BopGroup bopGroup = bopMission.Groups.Where(_g => _g.Id == GroupId).FirstOrDefault();

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
					sTableHeader = $"{sColumn} {ToolsBriefop.GetUnitAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem)}";

				tagThead.Add("td").AddClass("header").AppendText(sTableHeader);
			}

			if (bopGroup is not null)
			{
				bopGroup.FinalizeFromMiz();
				foreach(BopRoutePoint bopRoutePoint in bopGroup.RoutePoints)
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
						else if (sColumn == TableColumns.Altitude)
							tagTr.Add("td").AppendText($"{bopRoutePoint.GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}");
						else if (sColumn == TableColumns.Task)
							tagTr.Add("td").AppendText(bopRoutePoint.Tasks.FirstOrDefault()?.ToStringDisplayName());
						else if (sColumn == TableColumns.Notes)
							tagTr.Add("td").AppendText(bopRoutePoint.Notes);
					}
				}
			}

			//HtmlTag tagCanvas = new HtmlTag("canvas").Attr("width", "100%").Id("myCanvas");
			//HtmlTag tagScript = new HtmlTag("script");
			//tagScript.AppendHtml("var c = document.getElementById(\"myCanvas\");var ctx = c.getContext(\"2d\");ctx.moveTo(0,0);ctx.fillStyle = \"#FF0000\";ctx.fillRect(0, 0, 150, 75);");

			tags.Add(tagTable);

			//tags.Add(tagCanvas);
			//tags.Add(tagScript);
			return tags;
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<GMapOverlay> partOverlays = new List<GMapOverlay>();
			BopGroup bopGroup = bopMission.Groups.Where(_g => _g.Id == GroupId).FirstOrDefault();
			bopGroup.FinalizeFromMiz();

			partOverlays.Add(bopGroup.GetMapOverlayRoute(null, ElementMapOverlayRouteDisplay.PointLabelLight));
			
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

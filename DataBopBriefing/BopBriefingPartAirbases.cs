using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartAirbases : BopBriefingPartBase
	{
		#region Fields
		private static class TableColumns
		{
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Radio = "Radio";
			public static readonly string Localisation = "Localisation";
			public static readonly string Notes = "Notes";
		}
		public static List<string> AvailableColumns = new()
		{
			TableColumns.Name,
			TableColumns.Type,
			TableColumns.Radio,
			TableColumns.Localisation,
			TableColumns.Notes
		};
		#endregion

		#region Properties
		public string Header { get; set; }
		public List<Tuple<int, ElementAirbaseType>> Airbases { get; set; } = new();
		public List<string> SelectedColumns { get; set; } = new() { TableColumns.Name, TableColumns.Radio, TableColumns.Notes };
		#endregion

		#region CTOR
		public BopBriefingPartAirbases() : base(ElementBriefingPartType.Airbases, "table") { }
		#endregion

		#region Methods
		public override string ToStringAdditional()
		{
			return Header ?? "-no header-";
		}

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}

			IEnumerable<string> columns = GetColumns();
			HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");
			HtmlTag tagThead = tagTable.Add("thead");
			foreach (string sColumn in columns)
			{
				tagThead.Add("td").AddClass("header").AppendText(sColumn);
			}

			foreach (BopAirbase bopAirbase in GetOrderedBopElements(bopMission))
			{
				if (bopAirbase is object)
				{
					bopAirbase.FinalizeFromMiz();

					HtmlTag tagTr = tagTable.Add("tr");
					foreach (string sColumn in columns)
					{
						if (sColumn == TableColumns.Name)
							tagTr.Add("td").AppendText(bopAirbase.Name);
						else if (sColumn == TableColumns.Type)
							tagTr.Add("td").AppendText(bopAirbase.AirbaseType.ToString());
						else if (sColumn == TableColumns.Radio)
							tagTr.Add("td").AppendText(bopAirbase.ToStringRadios());
						else if (sColumn == TableColumns.Localisation)
							tagTr.Add("td").AppendText(bopAirbase.Coordinate.ToString(bopBriefingFolder.CoordinateDisplay));
						else if (sColumn == TableColumns.Notes)
							tagTr.Add("td").AppendText(bopAirbase.ToStringAdditional());

					}
				}
			}

			tags.Add(tagTable);
			return tags;
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BopMission bopMission)
		{
			List<GMapOverlay> partOverlays = new List<GMapOverlay>();
			foreach (BopAirbase bopAirbase in GetOrderedBopElements(bopMission))
			{
				bopAirbase.FinalizeFromMiz();
				partOverlays.Add(bopAirbase.GetMapOverlay());
			}
			return partOverlays;
		}

		private IEnumerable<string> GetColumns()
		{
			if (SelectedColumns is not null && SelectedColumns.Count > 0)
				return AvailableColumns.Where(_c => SelectedColumns.Contains(_c));
			else
				return AvailableColumns;
		}

		private IEnumerable<BopAirbase> GetOrderedBopElements(BopMission bopMission)
		{
			List<BopAirbase> orderedElements = new List<BopAirbase>();
			foreach (Tuple<int, ElementAirbaseType> airbase in Airbases.OrderBy(_a => _a.Item2).ThenBy(_a => _a.Item1))
			{
				BopAirbase bopAirbase = bopMission.Airbases.Where(_ba => _ba.Id == airbase.Item1 && _ba.AirbaseType == airbase.Item2).FirstOrDefault();
				if (bopAirbase is not null)
				{
					orderedElements.Add(bopAirbase);
				}
			}
			return orderedElements;
		}
		#endregion
	}
}

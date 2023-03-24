using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using HtmlTags;
using System.Text;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartAirbases : BaseBopBriefingPart
	{
		#region Fields
		private static class TableColumns
		{
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Radio = "Radio";
			public static readonly string Notes = "Notes";
		}
		public static List<string> AvailableColumns = new()
		{
			TableColumns.Name,
			TableColumns.Type,
			TableColumns.Radio,
			TableColumns.Notes
		};
		#endregion

		#region Properties
		public string Header { get; set; }
		public List<BopBriefingPartAirbase> Airbases { get; set; } = new();
		public List<string> SelectedColumns { get; set; } = new();
		#endregion

		#region CTOR
		public BopBriefingPartAirbases() : base(ElementBriefingPartType.Airbases, "table") { }
		#endregion

		#region Methods
		public override void InitializeDefault()
		{
			base.InitializeDefault();
			SelectedColumns.AddRange(new List<string> { TableColumns.Name, TableColumns.Radio, TableColumns.Notes });
		}

		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendWithSeparator(Header, " ");
			sb.AppendWithSeparator($"{Airbases.Count} airbases", " - ");
			return sb.ToString();
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

			foreach (BopAirbase bopAirbase in GetBopAirbases(bopMission))
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
						else if (sColumn == TableColumns.Notes)
							tagTr.Add("td").AppendText(bopAirbase.ToStringAdditional());

					}
				}
			}

			tags.Add(tagTable);
			return tags;
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<GMapOverlay> partOverlays = new List<GMapOverlay>();
			foreach (BopAirbase bopAirbase in GetBopAirbases(bopMission))
			{
				bopAirbase.FinalizeFromMiz();
				partOverlays.Add(bopAirbase.GetMapOverlay(ToolsBriefop.GetCoalitionColor(bopBriefingFolder.CoalitionName)));
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

		public List<BopAirbase> GetBopAirbases(BopMission bopMission)
		{
			List<BopAirbase> elements = new List<BopAirbase>();
			foreach (BopBriefingPartAirbase airbase in Airbases)
			{
				BopAirbase bopAirbase = bopMission.Airbases.Where(_ba => _ba.Id == airbase.Id && _ba.AirbaseType == airbase.AirbaseType).FirstOrDefault();
				if (bopAirbase is not null)
				{
					elements.Add(bopAirbase);
				}
			}
			return elements;
		}

		public void SetBopAirbases(IEnumerable<BopAirbase> bopAirbases)
		{
			Airbases.Clear();
			foreach (BopAirbase bopAirbase in bopAirbases)
				Airbases.Add(new BopBriefingPartAirbase() { Id = bopAirbase.Id, AirbaseType = bopAirbase.AirbaseType });
		}
		#endregion
	}

	internal class BopBriefingPartAirbase
	{
		public int Id { get; set; }
		public ElementAirbaseType AirbaseType { get; set; }
	}
}

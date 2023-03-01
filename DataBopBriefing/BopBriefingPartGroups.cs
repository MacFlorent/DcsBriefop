using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartGroups : BopBriefingPartBase
	{
		#region Properties
		public string Header { get; set; }
		public List<int> Groups { get; set; } = new();
		#endregion

		#region CTOR
		public BopBriefingPartGroups() : base(ElementBriefingPartType.Airbases, "table") { }
		#endregion

		#region Methods
		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}

			HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");

			HtmlTag tagThead = tagTable.Add("thead");
			tagThead.Add("td").AddClass("header").AppendText("Name");
			tagThead.Add("td").AddClass("header").AppendText("Type");
			tagThead.Add("td").AddClass("header").AppendText("Radio");
			tagThead.Add("td").AddClass("header").AppendText("Notes");

			foreach (BopAirbase bopAirbase in GetOrderedBopAirbases(bopMission))
			{
				if (bopAirbase is object)
				{
					HtmlTag tagTr = tagTable.Add("tr");
					tagTr.Add("td").AppendText(bopAirbase.Name);
					tagTr.Add("td").AppendText(bopAirbase.AirbaseType.ToString());
					tagTr.Add("td").AppendText(bopAirbase.ToStringRadios());
					tagTr.Add("td").AppendText(bopAirbase.ToStringAdditional());
				}
			}

			tags.Add(tagTable);
			return tags;
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BopMission bopMission)
		{
			List<GMapOverlay> partOverlays = new List<GMapOverlay>();
			//foreach (BopAirbase bopAirbase in GetOrderedBopAirbases(bopMission))
			//{
			//	bopAirbase.FinalizeFromMiz();
			//	partOverlays.Add(bopAirbase.GetMapOverlayPosition());
			//}
			return partOverlays;
		}

		private List<BopAirbase> GetOrderedBopAirbases(BopMission bopMission)
		{
			List<BopAirbase> orderedBopAirbases = new List<BopAirbase>();
			//foreach (Tuple<int, ElementAirbaseType> airbase in Groups.OrderBy(_a => _a.Item2).ThenBy(_a => _a.Item1))
			//{
			//	BopAirbase bopAirbase = bopMission.Airbases.Where(_ba => _ba.Id == airbase.Item1 && _ba.AirbaseType == airbase.Item2).FirstOrDefault();
			//	if (bopAirbase is not null)
			//	{
			//		orderedBopAirbases.Add(bopAirbase);
			//	}
			//}
			return orderedBopAirbases;
		}
		#endregion
	}
}

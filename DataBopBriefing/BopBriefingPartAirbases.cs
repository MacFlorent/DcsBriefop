using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartAirbases : BopBriefingPartBase
	{
		#region Properties
		public string Header { get; set; }
		public List<int> AirbasesIds { get; set; } = new List<int>();
		#endregion

		#region CTOR
		public BopBriefingPartAirbases() : base(ElementBriefingPartType.Airbases, "table") { }
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
			tagThead.Add("td").AddClass("header").AppendText("Radio");
			tagThead.Add("td").AddClass("header").AppendText("Tacan");
			tagThead.Add("td").AddClass("header").AppendText("Notes");

			foreach (int iId in AirbasesIds)
			{
				BopAirbase bopAirbase = bopMission.Airbases.Where(_ba => _ba.Id == iId).FirstOrDefault();
				if (bopAirbase is object)
				{
					HtmlTag tagTr = tagTable.Add("tr");
					tagTr.Add("td").AppendText(bopAirbase.Name);
					tagTr.Add("td").AppendText("");
					tagTr.Add("td").AppendText(bopAirbase.Tacan.ToString());
					tagTr.Add("td").AppendText(bopAirbase.ToStringAdditional());
				}
			}

			tags.Add(tagTable);
			return tags;
		}
		#endregion
	}
}

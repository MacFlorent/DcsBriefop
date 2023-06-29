using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartTableText : BaseBopBriefingPart
	{
		#region Properties
		public bool Weather { get; set; } = false;
		public bool Bullseye { get; set; } = true;
		public bool BullseyeWithDescription { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartTableText() : base(ElementBriefingPartType.TableText, "table") { }
		#endregion

		#region Methods
		public override IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new();
			HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");

			if (Bullseye)
			{
				if (bopManager.BopMission.Coalitions.TryGetValue(bopBriefingFolder.CoalitionName, out BopCoalition bopCoalition))
				{
					string sHeader = "Bullseye";
					if (bopCoalition.BullseyeWaypoint == ElementBullseyeWaypoint.One)
						sHeader += " [WP1]";
					else if (bopCoalition.BullseyeWaypoint == ElementBullseyeWaypoint.Last)
						sHeader += " [Last WP]";

					HtmlTag tagTr = tagTable.Add("tr");
					tagTr.Add("th").AppendText(sHeader);
					tagTr.Add("td").Append(bopCoalition.Bullseye.ToString(bopBriefingFolder.CoordinateDisplay).HtmlLineBreaks());

					if (BullseyeWithDescription && !string.IsNullOrEmpty(bopCoalition.BullseyeDescription))
					{
						tagTr = tagTable.Add("tr");
						tagTr.Add("th").AppendText("Notes");
						tagTr.Add("td").Append(bopCoalition.BullseyeDescription.HtmlLineBreaks());
					}
				}
			}

			if (Weather)
			{
				HtmlTag tagTr = tagTable.Add("tr");
				tagTr.Add("th").AppendText("Weather");
				tagTr.Add("td").Append(bopManager.BopMission.Weather.ToString(bopBriefingFolder.WeatherDisplay, bopBriefingFolder.MeasurementSystem).HtmlLineBreaks());
			}

			tags.Add(tagTable);
			return tags;

		}
		#endregion
	}
}

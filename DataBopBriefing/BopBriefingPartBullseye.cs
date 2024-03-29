﻿using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartBullseye : BaseBopBriefingPart
	{
		#region Properties
		public bool WithDescription { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartBullseye() : base(ElementBriefingPartType.Bullseye, "table") { }
		#endregion

		#region Methods
		public override string ToStringAdditional()
		{
			return WithDescription ? "With description" : "";
		}

		public override IEnumerable<HtmlTag> BuildHtmlContent(BriefopManager bopManager, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new();

			if (bopManager.BopMission.Coalitions.TryGetValue(bopBriefingFolder.CoalitionName, out BopCoalition bopCoalition))
			{
				string sHeader = "Bullseye";
				if (bopCoalition.BullseyeWaypoint == ElementBullseyeWaypoint.One)
					sHeader += " [WP1]";
				else if (bopCoalition.BullseyeWaypoint == ElementBullseyeWaypoint.Last)
					sHeader += " [Last WP]";

				HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");
				HtmlTag tagTr = tagTable.Add("tr");
				tagTr.Add("th").AppendText(sHeader);
				tagTr.Add("td").Append(bopCoalition.Bullseye.ToString(bopBriefingFolder.CoordinateDisplay).HtmlLineBreaks());

				if (WithDescription && !string.IsNullOrEmpty(bopCoalition.BullseyeDescription))
				{
					tagTr = tagTable.Add("tr");
					tagTr.Add("th").AppendText("Notes");
					tagTr.Add("td").Append(bopCoalition.BullseyeDescription.HtmlLineBreaks());
				}
				tags.Add(tagTable);
			}

			return tags;
		}
		#endregion
	}
}

﻿using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartBullseye : BopBriefingPartBase
	{
		#region Properties
		public bool WithDescription { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartBullseye() : base(ElementBriefingPartType.Bullseye, "table") { }
		#endregion

		#region Methods
		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			if (bopMission.Coalitions.TryGetValue(bopBriefingFolder.CoalitionName, out BopCoalition bopCoalition))
			{
				string sHeader = "Bullseye";
				if (bopCoalition.BullseyeWaypoint)
					sHeader += " [WP1]";

				HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");
				HtmlTag tagTr = tagTable.Add("tr");
				tagTr.Add("td").AddClass("header").AppendText(sHeader);
				tagTr.Add("td").Append(bopCoalition.Bullseye.ToString(bopBriefingFolder.CoordinateDisplay).HtmlLineBreaks());

				if (WithDescription && !string.IsNullOrEmpty(bopCoalition.BullseyeDescription))
				{
					tagTr = tagTable.Add("tr");
					tagTr.Add("td").AddClass("header").AppendText("Notes");
					tagTr.Add("td").Append(bopCoalition.BullseyeDescription.HtmlLineBreaks());
				}
				tags.Add(tagTable);
			}

			return tags;
		}
		#endregion
	}
}

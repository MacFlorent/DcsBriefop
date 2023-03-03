using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartParagraph : BopBriefingPartBase
	{
		#region Properties
		public string Header { get; set; }
		public string Text { get; set; }
		#endregion

		public BopBriefingPartParagraph() : base(ElementBriefingPartType.Paragraph, "content") { }

		public override string ToStringAdditional()
		{
			return Header ?? "";
		}

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();
			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}
			if (!string.IsNullOrEmpty(Text))
			{
				tags.Add(new HtmlTag("p").Append(Text.HtmlLineBreaks()));
			}

			return tags;
		}
	}

	internal class BopBriefingPartSortie : BopBriefingPartBase
	{
		public BopBriefingPartSortie() : base(ElementBriefingPartType.Sortie, "content") { }

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();
			if (!string.IsNullOrEmpty(bopMission.Sortie))
			{
				tags.Add(new HtmlTag("h2").Append(bopMission.Sortie.HtmlLineBreaks()));
			}

			return tags;
		}
	}

	internal class BopBriefingPartDescription : BopBriefingPartBase
	{
		public BopBriefingPartDescription() : base(ElementBriefingPartType.Description, "content") { }

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			if (!string.IsNullOrEmpty(bopMission.Description))
			{
				tags.Add(new HtmlTag("h2").AppendText("Description"));
				tags.Add(new HtmlTag("p").Append(bopMission.Description.HtmlLineBreaks()));
			}

			return tags;
		}
	}

	internal class BopBriefingPartTask : BopBriefingPartBase
	{
		public BopBriefingPartTask() : base(ElementBriefingPartType.Task, "content") { }

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			if (bopMission.Coalitions.TryGetValue(bopBriefingFolder.CoalitionName, out BopCoalition bopCoalition))
			{
				if (!string.IsNullOrEmpty(bopCoalition.Task))
				{
					tags.Add(new HtmlTag("h2").AppendText($"{bopCoalition.CoalitionName} task"));
					tags.Add(new HtmlTag("p").Append(bopCoalition.Task.HtmlLineBreaks()));
				}
			}

			return tags;
		}
	}
}

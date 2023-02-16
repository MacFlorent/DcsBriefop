using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using HtmlTags;

namespace DcsBriefop.DataBopBriefing
{
	internal abstract class BopBriefingPartParagraphBase : BopBriefingPart
	{
		#region Fields
		protected string m_sHeader;
		protected string m_sText;
		#endregion

		#region CTOR
		public BopBriefingPartParagraphBase(BopMission bopMission, BopBriefingFolder bopBriefingFolder, string sPartType) : base(bopMission, bopBriefingFolder, sPartType, "content") { }
		#endregion

		#region Methods
		protected override IEnumerable<HtmlTag> BuildHtmlContent()
		{
			List<HtmlTag> tags = new List<HtmlTag>();
			if (!string.IsNullOrEmpty(m_sHeader))
			{
				//tags.Add(new HtmlTag("h2").AppendText(m_sHeader));
				tags.Add(new HtmlTag("h2").Append(m_sHeader.HtmlLineBreaks()));
			}
			if (!string.IsNullOrEmpty(m_sText))
			{
				//tags.Add(new HtmlTag("p").AppendText(m_sText));
				tags.Add(new HtmlTag("p").Append(m_sText.HtmlLineBreaks()));
			}

			return tags;
		}
		#endregion
	}

	internal class BopBriefingPartParagraph : BopBriefingPartParagraphBase
	{
		#region Properties
		public string Header
		{ 
			get {  return m_sHeader; }
			set { m_sHeader = value; }
		}
		public string Text
		{
			get { return m_sText; }
			set { m_sText = value; }
		}
		#endregion

		public BopBriefingPartParagraph(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder, ElementBriefingPartType.Paragraph) { }
	}

	internal class BopBriefingPartSortie : BopBriefingPartParagraphBase
	{
		public BopBriefingPartSortie(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder, ElementBriefingPartType.Sortie)
		{
			m_sHeader = m_bopMission.Sortie;
		}
	}

	internal class BopBriefingPartDescription : BopBriefingPartParagraphBase
	{
		public BopBriefingPartDescription(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder, ElementBriefingPartType.Description)
		{
			m_sText = m_bopMission.Description;
		}
	}

	internal class BopBriefingPartTask : BopBriefingPartParagraphBase
	{
		public BopBriefingPartTask(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder, ElementBriefingPartType.Task)
		{
			if (m_bopMission.Coalitions.TryGetValue(m_bopBriefingFolder.CoalitionName, out BopCoalition bopCoalition))
			{
				m_sHeader = $"{bopCoalition.CoalitionName} task";
				m_sText = bopCoalition.Task;
			}
		}
	}

}

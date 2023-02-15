using DcsBriefop.DataBopMission;
using Maroontress.Html;

namespace DcsBriefop.DataBopBriefing
{
	internal abstract class BopBriefingPartParagraph : BopBriefingPart
	{
		#region Fields
		protected string m_sHeader;
		protected string m_sText;
		#endregion

		#region CTOR
		public BopBriefingPartParagraph(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder, "Paragraph", "content") { }
		#endregion

		#region Methods
		private static List<Tag> BuildHtmlContentParagraph(string sParagraph, NodeFactory nodeOf)
		{
			List<Tag> tags = new List<Tag>();

			string[] strings = sParagraph.Split(Environment.NewLine);
			foreach (string s in strings)
			{
				tags.Add(nodeOf.Span.AddAttributes(("style", "white-space: pre-line")).Add(s));
			}

			return tags;
		}

		protected override IEnumerable<Tag> BuildHtmlContent(NodeFactory nodeOf)
		{
			List<Tag> tags = new List<Tag>();
			if (!string.IsNullOrEmpty(m_sHeader))
			{
				tags.Add(nodeOf.H2.Add(m_sHeader.Replace(Environment.NewLine, nodeOf.Entity.NewLine.ToString())));
			}
			if (!string.IsNullOrEmpty(m_sText))
			{
				tags.Add(nodeOf.P.Add(m_sText.Replace(Environment.NewLine, nodeOf.Entity.NewLine.ToString())));
			}

			return tags;
		}
		#endregion
	}

	internal class BopBriefingPartParagraphFree : BopBriefingPartParagraph
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

		public BopBriefingPartParagraphFree(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder) { }
	}

	internal class BopBriefingPartParagraphSortie : BopBriefingPartParagraph
	{
		public BopBriefingPartParagraphSortie(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder)
		{
			PartName = "Sortie";
			m_sHeader = m_bopMission.Sortie;
		}
	}

	internal class BopBriefingPartParagraphDescription : BopBriefingPartParagraph
	{
		public BopBriefingPartParagraphDescription(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder)
		{
			PartName = "Description";
			m_sText = m_bopMission.Description;
		}
	}

	internal class BopBriefingPartParagraphTask : BopBriefingPartParagraph
	{
		public BopBriefingPartParagraphTask(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder)
		{
			PartName = "Task";

			if (m_bopMission.Coalitions.TryGetValue(m_bopBriefingFolder.CoalitionName, out BopCoalition bopCoalition))
			{
				m_sHeader = $"{bopCoalition.CoalitionName} task";
				m_sText = bopCoalition.Task;
			}
		}
	}

}

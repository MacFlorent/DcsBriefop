using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using Maroontress.Html;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPage
	{
		#region Fields
		protected BopMission m_bopMission;
		protected BopBriefingOptions m_options;
		#endregion

		#region Properties
		public string Name { get; set; }
		public ElementBriefingPageContent PageContent { get; set; }
		public List<BopBriefingPart> Parts { get; set; } = new List<BopBriefingPart>();
		#endregion

		#region CTOR
		public BopBriefingPage(BopMission bopMission, BopBriefingOptions options)
		{
			m_bopMission = bopMission;
			m_options = options;
			PageContent = (ElementBriefingPageContent.Map | ElementBriefingPageContent.Html);
		}
		#endregion

		#region Methods
		public Tag BuildHtmlContent()
		{
			NodeFactory nodeOf = Nodes.NewFactory();
			Tag tagHtml = nodeOf.Html;
			foreach (BopBriefingPart part in Parts)
			{
				Tag tagPart = part.BuildHtmlContent(nodeOf);
				if (tagPart is object)
				{
					tagHtml = tagHtml.Add(tagPart);
				}
			}
			return tagHtml;
		}

		public string BuildHtmlContentString()
		{
			Tag tagHtml = BuildHtmlContent();
			return tagHtml.ToString(FormatOptions.DefaultIndent);
		}
		#endregion

	}
}

using DcsBriefop.DataBopMission;
using Definux.HtmlBuilder;

namespace DcsBriefop.DataBopBriefing
{
	internal abstract class BopBriefingPart
	{
		#region Fields
		protected BopMission m_bopMission;
		protected BopBriefingFolder m_bopBriefingFolder;

		protected string m_sCssClass;
		#endregion

		#region Properties
		public string PartName { get; protected set; }
		#endregion

		#region CTOR
		public BopBriefingPart(BopMission bopMission, BopBriefingFolder bopBriefingFolder, string sPartType, string sCssClass)
		{
			m_bopMission = bopMission;
			m_bopBriefingFolder = bopBriefingFolder;
			PartName = sPartType;
			m_sCssClass = sCssClass;
		}
		#endregion

		#region Methods
		public Definux.HtmlBuilder.HtmlElement BuildHtml(HtmlBuilder builder)
		{
			Definux.HtmlBuilder.HtmlElement htmlElement =
				builder.StartElement(HtmlTags.Div).WithClasses(m_sCssClass).Append(BuildHtmlContent(builder));


			//return nodeOf.Div.WithClass(m_sCssClass).Add(BuildHtmlContent(nodeOf));
		}

		protected abstract IEnumerable<Definux.HtmlBuilder.HtmlElement> BuildHtmlContent(HtmlBuilder builder);
		#endregion
	}
}

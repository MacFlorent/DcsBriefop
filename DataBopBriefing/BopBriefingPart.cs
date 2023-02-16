using DcsBriefop.DataBopMission;
using HtmlTags;

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
		public string PartType { get; protected set; }
		#endregion

		#region CTOR
		public BopBriefingPart(BopMission bopMission, BopBriefingFolder bopBriefingFolder, string sPartType, string sCssClass)
		{
			m_bopMission = bopMission;
			m_bopBriefingFolder = bopBriefingFolder;
			PartType = sPartType;
			m_sCssClass = sCssClass;
		}
		#endregion

		#region Methods
		public HtmlTag BuildHtml()
		{
			return new HtmlTag("div").AddClass(m_sCssClass).Append(BuildHtmlContent());
		}

		protected abstract IEnumerable<HtmlTag> BuildHtmlContent();
		#endregion
	}
}
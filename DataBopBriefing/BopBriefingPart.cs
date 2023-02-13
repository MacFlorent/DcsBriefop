using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using Maroontress.Html;

namespace DcsBriefop.DataBopBriefing
{
	internal abstract class BopBriefingPart
	{
		#region Fields
		protected BopMission m_bopMission;
		protected BopBriefingOptions m_options;
		#endregion

		#region Properties
		public string Code { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPart(BopMission bopMission, BopBriefingOptions options)
		{
			m_bopMission = bopMission;
			m_options = options;
		}
		#endregion

		#region Methods
		public abstract Tag BuildHtmlContent(NodeFactory nodeOf);
		#endregion
	}

	internal class BopBriefingPartText : BopBriefingPart
	{
		#region Properties
		public string Text { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartText(BopMission bopMission, BopBriefingOptions options) : base(bopMission, options) { }
		#endregion

		#region Methods
		public override Tag BuildHtmlContent(NodeFactory nodeOf)
		{
			return null;
		}
		#endregion
	}

	//internal class BopBriefingPartHtml : BopBriefingPart
	//{
	//	string Html { get; set; }
	//}

	internal class BopBriefingPartBullseye : BopBriefingPart
	{
		#region Properties
		public bool WithDescription { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartBullseye(BopMission bopMission, BopBriefingOptions options) : base(bopMission, options) { }
		#endregion

		#region Methods
		public override Tag BuildHtmlContent(NodeFactory nodeOf)
		{
			Tag tagPart = null;

			if (m_bopMission.Coalitions.TryGetValue(m_options.CoalitionName, out BopCoalition bopCoalition))
			{
				tagPart = nodeOf.Table;
				Tag tagTrBullseye = nodeOf.Tr.Add
				(
					nodeOf.Td.Add(bopCoalition.Bullseye.ToString(m_options.CoordinateDisplay))
				);
				tagPart = tagPart.Add(tagTrBullseye);

				if (WithDescription)
				{
					Tag tagTrBullseyeDescription = nodeOf.Tr.Add
					(
						nodeOf.Td.Add(bopCoalition.BullseyeDescription)
					);
					tagPart = tagPart.Add(tagTrBullseyeDescription);
				}
			}

			return tagPart;
		}
		#endregion

	}
}

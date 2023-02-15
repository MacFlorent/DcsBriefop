using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using Maroontress.Html;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartBullseye : BopBriefingPart
	{
		#region Properties
		public bool WithDescription { get; set; }
		#endregion

		#region CTOR
		public BopBriefingPartBullseye(BopMission bopMission, BopBriefingFolder bopBriefingFolder) : base(bopMission, bopBriefingFolder, "Bullseye", "table") { }
		#endregion

		#region Methods
		protected override IEnumerable<Tag> BuildHtmlContent(NodeFactory nodeOf)
		{
			List<Tag> tags = new List<Tag>();

			if (m_bopMission.Coalitions.TryGetValue(m_bopBriefingFolder.CoalitionName, out BopCoalition bopCoalition))
			{
				string sHeader = "Bullseye";
				if (bopCoalition.BullseyeWaypoint)
					sHeader += " [WP1]";

				Tag tagTable = nodeOf.Table.AddAttributes(("width", "100%")).Add
				(
					nodeOf.Tr.Add
					(
						nodeOf.Td.WithClass("header").Add(sHeader),
						nodeOf.Td.Add(bopCoalition.Bullseye.ToString(m_bopBriefingFolder.CoordinateDisplay))
					)
				);

				if (WithDescription && !string.IsNullOrEmpty(bopCoalition.BullseyeDescription))
				{
					tagTable = tagTable.Add
					(
						nodeOf.Td.WithClass("header").Add("Notes"),
						nodeOf.Td.Add(bopCoalition.BullseyeDescription)
					);
				}
				tags.Add(tagTable);
			}

			return tags;
		}
		#endregion
	}
}

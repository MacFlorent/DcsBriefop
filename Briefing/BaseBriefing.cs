using DcsBriefop.Data;

namespace DcsBriefop.Briefing
{
	internal abstract class BaseBriefing
	{
		public LsonStructure.RootMission RootMission { get; private set; }
		public LsonStructure.RootDictionary RootDictionary { get; private set; }
		public CustomData RootCustom { get; private set; }
		public DcsTheatre Theatre { get; private set; }

	public BaseBriefing(MissionManager manager)
		{
			RootMission = manager.RootMission;
			RootDictionary = manager.RootDictionary;
			RootCustom = manager.RootCustom;
			Theatre = new DcsTheatre(RootMission.Theatre);
		}

		public BaseBriefing(BriefingPack bp)
		{
			RootMission = bp.RootMission;
			RootDictionary = bp.RootDictionary;
			RootCustom = bp.RootCustom;
			Theatre = bp.Theatre;
		}
	}
}

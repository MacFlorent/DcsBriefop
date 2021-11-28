using DcsBriefop.Data;

namespace DcsBriefop.Briefing
{
	internal abstract class BaseBriefing
	{
		public DataMiz.MizRootMission RootMission { get; private set; }
		public DataMiz.MizRootDictionary RootDictionary { get; private set; }
		public CustomData RootCustom { get; private set; }
		public Theatre Theatre { get; private set; }

	public BaseBriefing(MissionManager manager)
		{
			RootMission = manager.RootMission;
			RootDictionary = manager.RootDictionary;
			RootCustom = manager.RootCustom;
			Theatre = new Theatre(RootMission.Theatre);
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

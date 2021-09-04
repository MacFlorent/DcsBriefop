using System.Collections.Generic;

namespace DcsBriefop.Briefing
{
	internal class BriefingPack : BaseBriefing
	{
		public string Sortie
		{
			get { return m_manager.RootDictionary.Sortie; }
		}

		//public Dictionary<string, BriefingPackCoalition> BriefingPackCoalitions { get; private set; }
		public BriefingPackCoalition BriefingPackRed { get; private set; }
		public BriefingPackCoalition BriefingPackBlue { get; private set; }
		public BriefingPackCoalition BriefingPackNeutral { get; private set; }


		public BriefingPack(MissionManager manager) : base(manager)
		{
			Initialize();
		}

		private void Initialize()
		{
			BriefingPackRed = new BriefingPackCoalition(m_manager, MasterData.CoalitionName.Red);
			BriefingPackBlue = new BriefingPackCoalition(m_manager, MasterData.CoalitionName.Blue);
			BriefingPackNeutral = new BriefingPackCoalition(m_manager, MasterData.CoalitionName.Neutral);

			//BriefingPackCoalitions = new Dictionary<string, BriefingPackCoalition>();
			//BriefingPackCoalitions.Add(MasterData.CoalitionName.Red, new BriefingPackCoalition(m_manager, MasterData.CoalitionName.Red));
			//BriefingPackCoalitions.Add(MasterData.CoalitionName.Blue, new BriefingPackCoalition(m_manager, MasterData.CoalitionName.Blue));
			//BriefingPackCoalitions.Add(MasterData.CoalitionName.Neutral, new BriefingPackCoalition(m_manager, MasterData.CoalitionName.Neutral));
		}
	}
}

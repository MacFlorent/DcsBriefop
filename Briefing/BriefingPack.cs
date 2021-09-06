namespace DcsBriefop.Briefing
{
	internal class BriefingPack : BaseBriefing
	{
		public string Sortie
		{
			get { return m_manager.RootDictionary.Sortie; }
		}

		public bool DisplayRed
		{
			get { return m_manager.RootCustom.DisplayRed.GetValueOrDefault(); }
			set { m_manager.RootCustom.DisplayRed = value; }
		}
		public bool DisplayBlue
		{
			get { return m_manager.RootCustom.DisplayBlue.GetValueOrDefault(); }
			set { m_manager.RootCustom.DisplayBlue = value; }
		}
		public bool DisplayNeutral
		{
			get { return m_manager.RootCustom.DisplayNeutral.GetValueOrDefault(); }
			set { m_manager.RootCustom.DisplayNeutral = value; }
		}


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
		}
	}
}

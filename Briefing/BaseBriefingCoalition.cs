namespace DcsBriefop.Briefing
{
	internal abstract class BaseBriefingCoalition : BaseBriefing
	{
		public string Coalition{ get; private set; }
		public string Sortie
		{
			get { return m_manager.RootDictionary.Sortie; }
		}

		public BaseBriefingCoalition(MissionManager manager, string sCoalition) : base(manager)
		{
			Coalition = sCoalition;
		}
	}
}

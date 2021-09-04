namespace DcsBriefop.Briefing
{
	internal class BriefingPackCoalition : BaseBriefingCoalition
	{
		public bool Displayed { get; set; }

		public BriefingPageSituation BriefingPageSituation { get; private set; }

		public BriefingPackCoalition(MissionManager manager, string sCoalition) : base(manager, sCoalition)
		{
			Initialize();
		}

		private void Initialize()
		{
			BriefingPageSituation = new BriefingPageSituation(m_manager, Coalition);
			Displayed = !string.IsNullOrEmpty(BriefingPageSituation.Task);
		}
	}
}

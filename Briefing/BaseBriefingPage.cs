namespace DcsBriefop.Briefing
{
	internal abstract class BaseBriefingPage : BaseBriefingCoalition
	{
		public string Title { get; protected set; }
		public string Label
		{
			get { return $"{Title}_{Coalition}".ToUpper(); }
		}

		public BaseBriefingPage(MissionManager manager, string sCoalition) : base(manager, sCoalition) { }
	}
}

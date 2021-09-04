namespace DcsBriefop.Briefing
{
	internal abstract class BaseBriefing
	{
		protected MissionManager m_manager;

		public BaseBriefing(MissionManager manager)
		{
			m_manager = manager;
		}
	}
}

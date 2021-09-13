using DcsBriefop.MasterData;
using System;

namespace DcsBriefop.Briefing
{
	internal class BriefingPack : BaseBriefing
	{
		public string Sortie
		{
			get { return m_manager.RootDictionary.Sortie; }
		}

		public string Description
		{
			get { return m_manager.RootDictionary.Description; }
			set { m_manager.RootDictionary.Description = value; }
		}

		public DateTime Date
		{
			get { return new DateTime(m_manager.RootMission.Date.Year, m_manager.RootMission.Date.Month, m_manager.RootMission.Date.Day).AddSeconds(m_manager.RootMission.StartTime); }
			set
			{
				m_manager.RootMission.Date = new DateTime(value.Year, value.Month, value.Day);
				m_manager.RootMission.StartTime = Convert.ToInt32((value - m_manager.RootMission.Date).TotalSeconds);
			}
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


		public Theatre Theatre { get; private set; }
		public BriefingWeather Weather { get; private set; }
		public BriefingCoalition BriefingRed { get; private set; }
		public BriefingCoalition BriefingBlue { get; private set; }
		public BriefingCoalition BriefingNeutral { get; private set; }


		public BriefingPack(MissionManager manager) : base(manager)
		{
			Theatre = new Theatre(m_manager.RootMission.Theatre);
			Weather = new BriefingWeather(m_manager);
			BriefingRed = new BriefingCoalition(m_manager, CoalitionCode.Red);
			BriefingBlue = new BriefingCoalition(m_manager, CoalitionCode.Blue);
			BriefingNeutral = new BriefingCoalition(m_manager, CoalitionCode.Neutral);
		}
	}
}

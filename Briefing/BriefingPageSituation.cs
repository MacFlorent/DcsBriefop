using DcsBriefop.MasterData;
using System;

namespace DcsBriefop.Briefing
{
	internal class BriefingPageSituation : BaseBriefingPage
	{
		public DateTime Date
		{
			get { return new DateTime(m_manager.RootMission.Date.Year, m_manager.RootMission.Date.Month, m_manager.RootMission.Date.Day).AddSeconds(m_manager.RootMission.StartTime); }
			set
			{
				m_manager.RootMission.Date = new DateTime(value.Year, value.Month, value.Day);
				m_manager.RootMission.StartTime = Convert.ToInt32((value - m_manager.RootMission.Date).TotalSeconds);
			}
		}
		public string Description
		{
			get { return m_manager.RootDictionary.Description; }
			set { m_manager.RootDictionary.Description = value; }
		}

		public string Task
		{
			get
			{
				if (Coalition == CoalitionName.Red)
					return m_manager.RootDictionary.RedTask;
				else if (Coalition == CoalitionName.Blue)
					return m_manager.RootDictionary.BlueTask;
				else if (Coalition == CoalitionName.Neutral)
					return m_manager.RootDictionary.NeutralTask;
				else
					return null;
			}
			set
			{
				if (Coalition == CoalitionName.Red)
					m_manager.RootDictionary.RedTask = value;
				else if (Coalition == CoalitionName.Blue)
					m_manager.RootDictionary.BlueTask = value;
				else if (Coalition == CoalitionName.Neutral)
					m_manager.RootDictionary.NeutralTask = value;
			}
		}
		
		public BriefingWeather Weather { get; private set; }

		public BriefingPageSituation(MissionManager manager, string sCoalition) : base(manager, sCoalition)
		{
			Title = "SITUATION";
			Weather = new BriefingWeather(m_manager);
		}

		//public override void FromManagerRoot()
		//{
		//	Date = new DateTime(m_manager.RootMission.Date.Year, m_manager.RootMission.Date.Month, m_manager.RootMission.Date.Day).AddSeconds(m_manager.RootMission.StartTime);

		//	Description = m_manager.RootDictionary.Description;

		//	if (Coalition == CoalitionName.Red)
		//		Task = m_manager.RootDictionary.RedTask;
		//	else if (Coalition == CoalitionName.Blue)
		//		Task = m_manager.RootDictionary.BlueTask;
		//	else if (Coalition == CoalitionName.Neutral)
		//		Task = m_manager.RootDictionary.NeutralTask;

		//	Weather = new BriefingWeather(m_manager);
		//}

		//public override void ToManagerRoot()
		//{
		//	m_manager.RootMission.Date = new DateTime(Date.Year, Date.Month, Date.Day);
		//	m_manager.RootMission.StartTime = Convert.ToInt32((Date - m_manager.RootMission.Date).TotalSeconds);

		//	m_manager.RootDictionary.Description = Description;

		//	if (Coalition == CoalitionName.Red)
		//		m_manager.RootDictionary.RedTask = Task;
		//	else if (Coalition == CoalitionName.Blue)
		//		m_manager.RootDictionary.BlueTask = Task;
		//	else if (Coalition == CoalitionName.Neutral)
		//		m_manager.RootDictionary.NeutralTask = Task;
		//}
	}
}

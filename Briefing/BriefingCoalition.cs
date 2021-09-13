using CoordinateSharp;
using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingCoalition : BaseBriefing
	{
		private Coalition m_coalition;

		public Coordinate Bullseye
		{
			get { return m_manager.BriefingPack.Theatre.GetCoordinate(m_coalition.BullseyeY, m_coalition.BullseyeX); }
		}

		public string BullseyeCoordinates
		{
			get { return $"{Bullseye.ToStringDMS()}{Environment.NewLine}{Bullseye.ToStringDDM()}{Environment.NewLine}{Bullseye.ToStringMGRS()}"; }
		}

		public string BullseyeDescription
		{
			get { return m_manager.RootCustom.GetCoalition(m_coalition.Code)?.BullseyeDescription; }
			set { m_manager.RootCustom.GetCoalition(m_coalition.Code).BullseyeDescription = value; }
		}

		public string Name
		{
			get { return m_coalition.Code; }
		}

		public string Task
		{
			get
			{
				if (Name == CoalitionCode.Red)
					return m_manager.RootDictionary.RedTask;
				else if (Name == CoalitionCode.Blue)
					return m_manager.RootDictionary.BlueTask;
				else if (Name == CoalitionCode.Neutral)
					return m_manager.RootDictionary.NeutralTask;
				else
					return null;
			}
			set
			{
				if (Name == CoalitionCode.Red)
					m_manager.RootDictionary.RedTask = value;
				else if (Name == CoalitionCode.Blue)
					m_manager.RootDictionary.BlueTask = value;
				else if (Name == CoalitionCode.Neutral)
					m_manager.RootDictionary.NeutralTask = value;
			}
		}

		public List<BriefingFlight> Flights { get; private set; } = new List<BriefingFlight>();
				
				// ships
		// planes


		public BriefingCoalition(MissionManager manager, string sCoalitionName) : base(manager)
		{
			m_coalition = m_manager.RootMission.Coalitions.Where(c => c.Code == sCoalitionName).FirstOrDefault();

			foreach (Country c in m_coalition.Countries)
			foreach (GroupPlane gp in c.GroupPlanes)
			{
				Flights.Add(new BriefingFlight(m_manager, gp));
			}
		}
	}
}

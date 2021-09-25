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
				if (Name == ElementCoalition.Red)
					return m_manager.RootDictionary.RedTask;
				else if (Name == ElementCoalition.Blue)
					return m_manager.RootDictionary.BlueTask;
				else if (Name == ElementCoalition.Neutral)
					return m_manager.RootDictionary.NeutralTask;
				else
					return null;
			}
			set
			{
				if (Name == ElementCoalition.Red)
					m_manager.RootDictionary.RedTask = value;
				else if (Name == ElementCoalition.Blue)
					m_manager.RootDictionary.BlueTask = value;
				else if (Name == ElementCoalition.Neutral)
					m_manager.RootDictionary.NeutralTask = value;
			}
		}

		public List<BriefingFlight> GroupAirs { get; private set; } = new List<BriefingFlight>();
		public List<BriefingShip> GroupShips { get; private set; } = new List<BriefingShip>();

		// ships
		// planes


		public BriefingCoalition(MissionManager manager, string sCoalitionName) : base(manager)
		{
			m_coalition = m_manager.RootMission.Coalitions.Where(c => c.Code == sCoalitionName).FirstOrDefault();

			foreach (Country c in m_coalition.Countries)
			{
				foreach (GroupFlight ga in c.GroupAirs)
				{
					GroupAirs.Add(new BriefingFlight(m_manager, ga));
				}
				foreach (GroupShip gs in c.GroupShips)
				{
					GroupShips.Add(new BriefingShip(m_manager, gs));
				}

			}
		}
	}
}

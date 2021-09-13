using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingFlight : BaseBriefing
	{
		private GroupPlane m_group;

		public bool Included
		{
			get
			{
				CustomDataFlight cdf = m_manager.RootCustom.Flights?.Where(_f => _f.Id == Id).FirstOrDefault();
				if (cdf is null)
					return Playable;
				else
					return cdf.Included;
			}
			set
			{
				CustomDataFlight cdf = m_manager.RootCustom.Flights?.Where(_f => _f.Id == Id).FirstOrDefault();
				if (cdf is null && value != Playable)
					m_manager.RootCustom.Flights.Add(new CustomDataFlight { Id = Id, Included = value });
				else if (cdf is object)
					cdf.Included = value;
			}
		}

		public int Id
		{
			get { return m_group.Id; }
		}
		public string Name
		{
			get { return m_group.Name; }
		}
		public string Task
		{
			get { return m_group.Task; }
		}

		public decimal RadioFrequency
		{
			get { return m_group.RadioFrequency; }
			set { m_group.RadioFrequency = value; }
		}
		public int RadioModulation
		{
			get { return m_group.RadioModulation; }
			set { m_group.RadioModulation = value; }
		}

		public string RadioString
		{
			get { return ToolsMasterData.GetRadioString(RadioFrequency, RadioModulation); }
		}

		public string Callsign
		{
			get
			{
				string sCallsign = m_group.Units.OfType<UnitPlane>().FirstOrDefault()?.Callsign;
				if (!string.IsNullOrEmpty(sCallsign))
					return sCallsign.Substring(0, sCallsign.Length - 1);
				else
					return null;
			}
		}

		public bool Playable
		{
			get { return m_group.Units.Where(u => u.Skill == Skill.Player || u.Skill == Skill.Client).Any(); }
		}

		public string UnitTypes
		{
			get
			{
				IEnumerable<string> grouped = m_group.Units.GroupBy(u => u.Type).Select(g => g.Key);
				return string.Join(",", grouped);
			}
		}


		public BriefingFlight(MissionManager manager, LsonStructure.GroupPlane gp) : base(manager)
		{
			m_group = gp;
		}

	}
}

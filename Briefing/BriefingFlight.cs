using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingFlight : BriefingGroup
	{
		private GroupFlight GroupFlight { get { return m_group as GroupFlight; } }

		public string Type { get { return GroupFlight.Units.FirstOrDefault()?.Type; } }

		public string Task
		{
			get { return GroupFlight.Task; }
		}

		public decimal RadioFrequency
		{
			get { return GroupFlight.RadioFrequency; }
			set { GroupFlight.RadioFrequency = value; }
		}
		public int RadioModulation
		{
			get { return GroupFlight.RadioModulation; }
			set { GroupFlight.RadioModulation = value; }
		}

		public BriefingFlight(MissionManager manager, GroupFlight ga) : base(manager, ga) { }

		public string GetRadioString()
		{
			return ToolsMasterData.GetRadioString(RadioFrequency, RadioModulation);
		}

		public string GetCallsign()
		{
				string sCallsign = m_group.Units.OfType<UnitPlane>().FirstOrDefault()?.Callsign;
				if (!string.IsNullOrEmpty(sCallsign))
					return sCallsign.Substring(0, sCallsign.Length - 1);
				else
					return null;
		}

	}
}

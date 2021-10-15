using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingFlight : BriefingGroup
	{
		#region Fields
		private GroupFlight GroupFlight { get { return m_group as GroupFlight; } }
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.aircraft.ToString();
		public override string Category { get { return "Flight"; } }
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
		#endregion

		#region CTOR
		public BriefingFlight(BriefingPack bp, GroupFlight ga, BriefingCoalition bc) : base(bp, ga, bc)
		{
			if (BriefingStatus == ElementGroupStatusId.NotSet)
			{
				if (Playable)
					BriefingStatus = ElementGroupStatusId.FullRoute;
				else if (ElementTask.Supports.Contains(Task))
					BriefingStatus = ElementGroupStatusId.Orbit;
				else
					BriefingStatus = ElementGroupStatusId.Point;
			}

			InitializeMapData();
		}
		#endregion

		#region Methods
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
		#endregion
	}
}

using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using DcsBriefop.Tools;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetFlight : Asset
	{
		#region Fields
		private GroupFlight GroupFlight { get { return m_group as GroupFlight; } }
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.aircraft.ToString();
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
		public AssetFlight(BriefingPack bp, GroupFlight ga, BriefingCoalition bc) : base(bp, ga, bc) { }
		#endregion

		#region Methods
		protected override void InitializeCustomData()
		{
			if(Playable)
			{
				Category = ElementAssetCategory.Mission;
				MapDisplay = ElementAssetMapDisplay.None;
			}
			else if (Task == ElementTask.Awacs || Task == ElementTask.Refueling)
			{
				Category = ElementAssetCategory.Support;
				MapDisplay = ElementAssetMapDisplay.Orbit;
			}
			else
			{
				Category = ElementAssetCategory.Mission;
				MapDisplay = ElementAssetMapDisplay.None;
			}
		}

		public string GetRadioString()
		{
			return ToolsMisc.GetRadioString(RadioFrequency, RadioModulation);
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

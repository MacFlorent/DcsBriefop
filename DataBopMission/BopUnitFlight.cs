using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Text;

namespace DcsBriefop.DataBopMission
{
	internal class BopUnitFlight : BopUnit
	{
		#region Fields
		#endregion

		#region Properties
		public BopCallsign Callsign { get; set; }
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public BopUnitFlight(Miz miz, Theatre theatre, BopGroup bopGroup, MizUnit mizUnit) : base(miz, theatre, bopGroup, mizUnit)
		{
			if (m_mizUnit.Callsign is MizCallsign mizCallsign)
				Callsign = BopCallsign.NewFromMizCallsign (mizCallsign);
			else
				Callsign = BopCallsign.NewFromNumber(m_mizUnit.CallsignNumber);

			Tacan = bopGroup.GetTacanFromRouteTask(Id);
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			Callsign?.ToMiz();

		}
		#endregion

		#region Methods
		public override string ToStringDisplayName()
		{
			string sDisplayName = base.ToStringDisplayName();

			if (Callsign is object && (!Playable || !PreferencesManager.Preferences.Mission.NoCallsignForPlayableFlights))
				return $"{Callsign} | {sDisplayName}";
			else
				return sDisplayName;
		}

		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditional());

			if (Tacan is object)
				sb.AppendWithSeparator($"TACAN:{Tacan}", " ");

			return sb.ToString();
		}
		#endregion
	}
}

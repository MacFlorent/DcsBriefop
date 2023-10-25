using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopDatalinkId : BaseBop
	{
		#region Fields
		private MizAdditionalPropertiesAircraft m_mizAdditionalPropertiesAircraft;
		#endregion

		#region Properties
		public ElementDatalinkType DatalinkType { get; set; }
		public string Id { get; set; }
		public string Callsign { get; set; }
		public string CallsignNumber { get; set; }
		#endregion

		#region CTOR
		public BopDatalinkId(Miz miz, Theatre theatre, MizAdditionalPropertiesAircraft mizAdditionalPropertiesAircraft) : base(miz, theatre)
		{
			m_mizAdditionalPropertiesAircraft = mizAdditionalPropertiesAircraft;

			if (m_mizAdditionalPropertiesAircraft.StnL16 is not null)
			{ // F-16 and F-18
				DatalinkType = ElementDatalinkType.Link16;
				Id = m_mizAdditionalPropertiesAircraft.StnL16;
				Callsign = m_mizAdditionalPropertiesAircraft.VoiceCallsignLabel;
				CallsignNumber = m_mizAdditionalPropertiesAircraft.VoiceCallsignNumber;
			}
			else if (m_mizAdditionalPropertiesAircraft.SadlTn is not null)
			{// A-10C
				DatalinkType = ElementDatalinkType.Sadl;
				Id = m_mizAdditionalPropertiesAircraft.SadlTn;
				Callsign = m_mizAdditionalPropertiesAircraft.VoiceCallsignLabel;
				CallsignNumber = m_mizAdditionalPropertiesAircraft.VoiceCallsignNumber;
			}
			else if (m_mizAdditionalPropertiesAircraft.TnIdmLb is not null)
			{// AH-64
				DatalinkType = ElementDatalinkType.Idm;
				Id = m_mizAdditionalPropertiesAircraft.TnIdmLb;
				Callsign = m_mizAdditionalPropertiesAircraft.OwnshipCallSign;
			}
		}

		public static BopDatalinkId NewFromAircraftAdditionalProperties(Miz miz, Theatre theatre, MizAdditionalPropertiesAircraft mizAdditionalPropertiesAircraft)
		{
			BopDatalinkId bopDatalinkId = null;
			if (mizAdditionalPropertiesAircraft is not null &&
				(mizAdditionalPropertiesAircraft.StnL16 is not null || mizAdditionalPropertiesAircraft.SadlTn is not null || mizAdditionalPropertiesAircraft.TnIdmLb is not null)
				)
			{
				bopDatalinkId = new BopDatalinkId(miz, theatre, mizAdditionalPropertiesAircraft);
			}

			return bopDatalinkId;
		}

		#endregion

			#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			if (m_mizAdditionalPropertiesAircraft.StnL16 is not null)
			{ // F-16 and F-18
				m_mizAdditionalPropertiesAircraft.StnL16 = Id;
				m_mizAdditionalPropertiesAircraft.VoiceCallsignLabel = Callsign;
				m_mizAdditionalPropertiesAircraft.VoiceCallsignNumber = CallsignNumber;
			}
			else if (m_mizAdditionalPropertiesAircraft.SadlTn is not null)
			{// A-10C
				m_mizAdditionalPropertiesAircraft.SadlTn = Id;
				m_mizAdditionalPropertiesAircraft.VoiceCallsignLabel = Callsign;
				m_mizAdditionalPropertiesAircraft.VoiceCallsignNumber = CallsignNumber;
			}
			else if (m_mizAdditionalPropertiesAircraft.TnIdmLb is not null)
			{// AH-64
				m_mizAdditionalPropertiesAircraft.TnIdmLb = Id;
				m_mizAdditionalPropertiesAircraft.OwnshipCallSign = Callsign;
			}
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			return $"{ToStringCallsign()} [{Id}]";
		}

		public string ToStringCallsign()
		{
			string s = Callsign;
			if (!string.IsNullOrEmpty(CallsignNumber))
				s += $"-{CallsignNumber}";
			
			return s;
		}
		#endregion
	}
}

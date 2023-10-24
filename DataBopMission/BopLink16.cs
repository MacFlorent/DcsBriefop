using DcsBriefop.Data;
using DcsBriefop.DataMiz;

namespace DcsBriefop.DataBopMission
{
	internal class BopLink16 : BaseBop
	{
		#region Fields
		private MizAdditionalPropertiesAircraft m_mizAdditionalPropertiesAircraft;
		#endregion

		#region Properties
		public string StnL16 { get; set; }
		public string Label { get; set; }
		public string Number { get; set; }
		#endregion

		#region CTOR
		public BopLink16(Miz miz, Theatre theatre, MizAdditionalPropertiesAircraft mizAdditionalPropertiesAircraft) : base(miz, theatre)
		{
			m_mizAdditionalPropertiesAircraft = mizAdditionalPropertiesAircraft;

			StnL16 = m_mizAdditionalPropertiesAircraft.StnL16;
			Label = m_mizAdditionalPropertiesAircraft.VoiceCallsignLabel;
			Number = m_mizAdditionalPropertiesAircraft.VoiceCallsignNumber;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizAdditionalPropertiesAircraft.StnL16 = StnL16;
			m_mizAdditionalPropertiesAircraft.VoiceCallsignLabel = Label;
			m_mizAdditionalPropertiesAircraft.VoiceCallsignNumber = Number;
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			return $"{ToStringCallsign()} [{StnL16}]";
		}

		public string ToStringCallsign()
		{
			return $"{Label}-{Number}";
		}
		#endregion
	}
}

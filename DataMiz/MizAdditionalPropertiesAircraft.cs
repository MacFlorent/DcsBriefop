using DcsBriefop.Tools;
using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizAdditionalPropertiesAircraft : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string StnL16 = "STN_L16";
			public static readonly string VoiceCallsignLabel = "VoiceCallsignLabel";
			public static readonly string VoiceCallsignNumber = "VoiceCallsignNumber";
		}

		public string StnL16 { get; set; }
		public string VoiceCallsignLabel { get; set; }
		public string VoiceCallsignNumber { get; set; }

		public MizAdditionalPropertiesAircraft(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			StnL16 = Lsd.IfExistsString(LuaNode.StnL16);
			VoiceCallsignLabel = Lsd.IfExistsString(LuaNode.VoiceCallsignLabel);
			VoiceCallsignNumber = Lsd.IfExistsString(LuaNode.VoiceCallsignNumber);
		}

		public override void ToLua()
		{
			Lsd.SetIfExists(LuaNode.StnL16, StnL16);
		}
	}
}

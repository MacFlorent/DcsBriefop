using DcsBriefop.Tools;
using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizAdditionalPropertiesAircraft : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string StnL16 = "STN_L16";
			public static readonly string SadlTn = "SADL_TN";
			public static readonly string VoiceCallsignLabel = "VoiceCallsignLabel";
			public static readonly string VoiceCallsignNumber = "VoiceCallsignNumber";
			public static readonly string TnIdmLb = "TN_IDM_LB";
			public static readonly string OwnshipCallSign = "OwnshipCallSign";
		}

		public string StnL16 { get; set; }
		public string SadlTn { get; set; }
		public string VoiceCallsignLabel { get; set; }
		public string VoiceCallsignNumber { get; set; }
		public string TnIdmLb { get; set; }
		public string OwnshipCallSign { get; set; }

		public MizAdditionalPropertiesAircraft(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			StnL16 = Lsd.IfExistsString(LuaNode.StnL16);
			SadlTn = Lsd.IfExistsString(LuaNode.SadlTn);
			VoiceCallsignLabel = Lsd.IfExistsString(LuaNode.VoiceCallsignLabel);
			VoiceCallsignNumber = Lsd.IfExistsString(LuaNode.VoiceCallsignNumber);
			TnIdmLb = Lsd.IfExistsString(LuaNode.TnIdmLb);
			OwnshipCallSign = Lsd.IfExistsString(LuaNode.OwnshipCallSign);
		}

		public override void ToLua()
		{
			Lsd.SetIfExists(LuaNode.StnL16, StnL16);
			Lsd.SetIfExists(LuaNode.SadlTn, SadlTn);
			Lsd.SetIfExists(LuaNode.TnIdmLb, TnIdmLb);
		}
	}
}

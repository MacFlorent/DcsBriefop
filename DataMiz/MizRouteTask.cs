using DcsBriefop.Tools;
using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizRouteTask : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Id = "id";
			public static readonly string Number = "number";
			public static readonly string Enabled = "enabled";
			public static readonly string Params = "params";
			public static readonly string Action = "action";
		}

		public string Id { get; set; }
		public int Number { get; set; }
		public bool Enabled { get; set; }
		public MizRouteTaskAction Action { get; set; }

		public MizRouteTask(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = m_lsd[LuaNode.Id].GetString();
			Number = m_lsd[LuaNode.Number].GetInt();
			Enabled = m_lsd[LuaNode.Enabled].GetBool();

			LsonDict lsdAction = m_lsd[LuaNode.Params].GetDict().IfExists(LuaNode.Action)?.GetDict();
			if (lsdAction is object)
				Action = new MizRouteTaskAction(lsdAction);
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Id] = Id;
			m_lsd[LuaNode.Number] = Number;
			m_lsd[LuaNode.Enabled] = Enabled;

			Action?.ToLua();
		}

		//public string GetStringTacan()
		//{
		//	if (Action is object && Action.Id == "ActivateBeacon")
		//		return ToolsDcs.GetStringTacan (Action.ParamChannel.GetValueOrDefault(), Action.ParamModeChannel, Action.ParamCallsign);
		//	else
		//		return null;
		//}
	}

	internal class MizRouteTaskAction : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Id = "id";
			public static readonly string Params = "params";

			public static readonly string ParamType = "type";
			public static readonly string ParamUnitId = "unitId";
			public static readonly string ParamCallsign = "callsign";
			public static readonly string ParamModeChannel = "modeChannel";
			public static readonly string ParamChannel = "channel";
			public static readonly string ParamFrequency = "frequency";
		}

		public string Id { get; set; }
		public int? ParamType { get; set; }
		public int? ParamUnitId { get; set; }
		public string ParamCallsign { get; set; }
		public string ParamModeChannel { get; set; }
		public int? ParamChannel { get; set; }
		public int? ParamFrequency { get; set; }

		public MizRouteTaskAction(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = m_lsd[LuaNode.Id].GetString();
			ParamType = m_lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamType);
			ParamUnitId = m_lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamUnitId);
			ParamCallsign = m_lsd[LuaNode.Params].GetDict().IfExistsString(LuaNode.ParamCallsign);
			ParamModeChannel = m_lsd[LuaNode.Params].GetDict().IfExistsString(LuaNode.ParamModeChannel);
			ParamChannel = m_lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamChannel);
			ParamFrequency = m_lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamFrequency);
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Id] = Id;

			LsonDict lsdParams = m_lsd[LuaNode.Params].GetDict();
			lsdParams.SetOrAddInt(LuaNode.ParamType, ParamType);
			lsdParams.SetOrAddInt(LuaNode.ParamType, ParamType);
			lsdParams.SetOrAddInt(LuaNode.ParamUnitId, ParamUnitId);
			lsdParams.SetOrAddString(LuaNode.ParamCallsign, ParamCallsign);
			lsdParams.SetOrAddString(LuaNode.ParamModeChannel, ParamModeChannel);
			lsdParams.SetOrAddInt(LuaNode.ParamChannel, ParamChannel);
			lsdParams.SetOrAddInt(LuaNode.ParamFrequency, ParamFrequency);
		}
	}
}

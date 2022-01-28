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
			Id = Lsd[LuaNode.Id].GetString();
			Number = Lsd[LuaNode.Number].GetInt();
			Enabled = Lsd[LuaNode.Enabled].GetBool();

			LsonDict lsdAction = Lsd[LuaNode.Params].GetDict().IfExists(LuaNode.Action)?.GetDict();
			if (lsdAction is object)
				Action = new MizRouteTaskAction(lsdAction);
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Id] = Id;
			Lsd[LuaNode.Number] = Number;
			Lsd[LuaNode.Enabled] = Enabled;

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
			Id = Lsd[LuaNode.Id].GetString();
			ParamType = Lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamType);
			ParamUnitId = Lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamUnitId);
			ParamCallsign = Lsd[LuaNode.Params].GetDict().IfExistsString(LuaNode.ParamCallsign);
			ParamModeChannel = Lsd[LuaNode.Params].GetDict().IfExistsString(LuaNode.ParamModeChannel);
			ParamChannel = Lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamChannel);
			ParamFrequency = Lsd[LuaNode.Params].GetDict().IfExistsInt(LuaNode.ParamFrequency);
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Id] = Id;

			LsonDict lsdParams = Lsd[LuaNode.Params].GetDict();
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

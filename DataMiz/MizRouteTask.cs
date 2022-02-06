﻿using DcsBriefop.Tools;
using LsonLib;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizRouteTaskHolder : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Id = "id";
			public static readonly string Params = "params";
			public static readonly string Tasks = "tasks";
		}

		public string Id { get; set; }
		public List<MizRouteTask> Tasks { get; set; } = new List<MizRouteTask>();

		public MizRouteTaskHolder(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = Lsd[LuaNode.Id].GetString();

			LsonDict lsdTasks = Lsd[LuaNode.Params][LuaNode.Tasks].GetDict();
			foreach (LsonValue lsv in lsdTasks.Values)
			{
				Tasks.Add(new MizRouteTask(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Id] = Id;

			foreach (MizRouteTask tasks in Tasks)
			{
				tasks.ToLua();
			}
		}
	}

	internal class MizRouteTask : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Enabled = "enabled";
			public static readonly string Id = "id";
			public static readonly string Params = "params";
		}

		public string Id { get; set; }
		public bool Enabled { get; set; }
		public MizRouteTaskParams Params { get; set; }

		public MizRouteTask(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = Lsd[LuaNode.Id].GetString();
			if (Lsd.ContainsKey(LuaNode.Enabled))
				Enabled = Lsd[LuaNode.Enabled].GetBool();
			else
				Enabled = false;

			LsonDict lsdParams= Lsd[LuaNode.Params].GetDictSafe();
			if (lsdParams is object)
				Params = new MizRouteTaskParams(lsdParams);
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Id] = Id;
			Lsd.SetIfExists(LuaNode.Enabled, Enabled);

			Params?.ToLua();
		}
	}

	internal class MizRouteTaskParams : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Action = "action";
			public static readonly string Task = "task";
			public static readonly string Altitude = "altitude";
			public static readonly string Pattern = "pattern";
			public static readonly string Speed = "speed";
			public static readonly string SpeedEdited = "speedEdited";
	}

		public MizRouteTaskAction Action { get; set; }
		public MizRouteTask Task { get; set; }

		public decimal? Altitude { get; set; }
		public string Pattern { get; set; }
		public decimal? Speed { get; set; }
		public bool? SpeedEdited { get; set; }

		public MizRouteTaskParams(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			LsonDict lsdTask = Lsd.IfExists(LuaNode.Task)?.GetDictSafe();
			if (lsdTask is object)
				Task = new MizRouteTask(lsdTask);

			LsonDict lsdAction = Lsd.IfExists(LuaNode.Action)?.GetDictSafe();
			if (lsdAction is object)
				Action = new MizRouteTaskAction(lsdAction);

			Altitude = Lsd.IfExistsDecimal(LuaNode.Altitude);
			Pattern = Lsd.IfExistsString(LuaNode.Pattern);
			Speed = Lsd.IfExistsDecimal(LuaNode.Speed);
			SpeedEdited = Lsd.IfExistsBool(LuaNode.SpeedEdited);
		}

		public override void ToLua()
		{
			Task?.ToLua();
			Action?.ToLua();

			Lsd.SetIfExists(LuaNode.Altitude, Altitude);
			Lsd.SetIfExists(LuaNode.Pattern, Pattern);
			Lsd.SetIfExists(LuaNode.Speed, Speed);
			Lsd.SetIfExists(LuaNode.SpeedEdited, SpeedEdited);
		}
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
			lsdParams.SetIfExists(LuaNode.ParamType, ParamType);
			lsdParams.SetIfExists(LuaNode.ParamType, ParamType);
			lsdParams.SetIfExists(LuaNode.ParamUnitId, ParamUnitId);
			lsdParams.SetIfExists(LuaNode.ParamCallsign, ParamCallsign);
			lsdParams.SetIfExists(LuaNode.ParamModeChannel, ParamModeChannel);
			lsdParams.SetIfExists(LuaNode.ParamChannel, ParamChannel);
			lsdParams.SetIfExists(LuaNode.ParamFrequency, ParamFrequency);
		}
	}
}

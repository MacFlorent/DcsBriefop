using DcsBriefop.Tools;
using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizRoutePoint : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Name = "name";
			public static readonly string Type = "type";
			public static readonly string Action = "action";
			public static readonly string Altitude = "alt";
			public static readonly string AltitudeType = "alt_type";
			public static readonly string Speed = "speed";
			public static readonly string Y = "y";
			public static readonly string X = "x";
			public static readonly string AirdromeId = "airdromeId";
			public static readonly string HelipadId = "helipadId";
			public static readonly string LinkUnit = "linkUnit";
			public static readonly string Task = "task";
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public double Altitude { get; set; }
		public string AltitudeType { get; set; }
		public double Speed { get; set; }
		public double Y { get; set; }
		public double X { get; set; }
		public int? AirdromeId { get; set; }
		public int? HelipadId { get; set; }
		public int? LinkUnitId { get; set; }
		public MizRouteTaskHolder RouteTaskHolder { get; set; }

		public MizRoutePoint(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Name = Lsd.IfExistsString(LuaNode.Name);
			Type = Lsd[LuaNode.Type].GetString();
			Action = Lsd[LuaNode.Action].GetString();
			Altitude = Lsd[LuaNode.Altitude].GetDouble();
			AltitudeType = Lsd.IfExistsString(LuaNode.AltitudeType);
			Speed = Lsd[LuaNode.Speed].GetDouble();
			Y = Lsd[LuaNode.Y].GetDouble();
			X = Lsd[LuaNode.X].GetDouble();
			AirdromeId = Lsd.IfExistsInt(LuaNode.AirdromeId);
			HelipadId = Lsd.IfExistsInt(LuaNode.HelipadId);
			LinkUnitId = Lsd.IfExistsInt(LuaNode.LinkUnit);

			if (Lsd.ContainsKey(LuaNode.Task))
				RouteTaskHolder = new MizRouteTaskHolder(Lsd[LuaNode.Task].GetDict());
		}

		public override void ToLua()
		{
			Lsd.SetOrAddString(LuaNode.Name, Name);
			Lsd[LuaNode.Type] = Type;
			Lsd[LuaNode.Action] = Action;
			Lsd[LuaNode.Altitude] = Altitude;
			Lsd[LuaNode.AltitudeType] = AltitudeType;
			Lsd[LuaNode.Speed] = Speed;
			Lsd[LuaNode.Y] = Y;
			Lsd[LuaNode.X] = X;
			Lsd.SetOrAddInt(LuaNode.AirdromeId, AirdromeId);
			Lsd.SetOrAddInt(LuaNode.HelipadId, HelipadId);

			RouteTaskHolder?.ToLua();
		}

		public static string GetLuaTemplate()
		{
			return ToolsResources.GetTextResourceContent("MizRoutePoint", "lua", null);
		}

		public static MizRoutePoint NewFromLuaTemplate()
		{
			string sLuaTemplate = GetLuaTemplate();
			Dictionary<string, LsonValue> l = LsonVars.Parse(sLuaTemplate);
			return new MizRoutePoint(l["template"].GetDict());
		}

	}
}

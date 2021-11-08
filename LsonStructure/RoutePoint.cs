using DcsBriefop.Tools;
using LsonLib;
using System.Collections.Generic;

namespace DcsBriefop.LsonStructure
{
	internal class RoutePoint : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string Name = "name";
			public static readonly string Type = "type";
			public static readonly string Action = "action";
			public static readonly string Altitude = "alt";
			public static readonly string AltitudeType = "alt_type";
			public static readonly string Y = "y";
			public static readonly string X = "x";
			public static readonly string AirdromeId = "airdromeId";
			public static readonly string HelipadId = "helipadId";
			public static readonly string LinkUnit = "linkUnit";
			public static readonly string Task = "task";
			public static readonly string Params = "params";
			public static readonly string Tasks = "tasks";
		}

		public string Name { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public decimal Altitude { get; set; }
		public string AltitudeType { get; set; }
		public decimal Y { get; set; }
		public decimal X { get; set; }
		public int? AirdromeId { get; set; }
		public int? HelipadId { get; set; }
		public int? LinkUnitId { get; set; }
		public List<RouteTask> RouteTasks { get; set; } = new List<RouteTask>();

		public RoutePoint(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Name = m_lsd.IfExistsString(LuaNode.Name);
			Type = m_lsd[LuaNode.Type].GetString();
			Action = m_lsd[LuaNode.Action].GetString();
			Altitude = m_lsd[LuaNode.Altitude].GetDecimal();
			AltitudeType = m_lsd.IfExistsString(LuaNode.AltitudeType);
			Y = m_lsd[LuaNode.Y].GetDecimal();
			X = m_lsd[LuaNode.X].GetDecimal();
			AirdromeId = m_lsd.IfExistsInt(LuaNode.AirdromeId);
			HelipadId = m_lsd.IfExistsInt(LuaNode.HelipadId);
			LinkUnitId = m_lsd.IfExistsInt(LuaNode.LinkUnit);

			if (m_lsd.ContainsKey(LuaNode.Task))
			{
				LsonDict lsdRouteTasks = m_lsd[LuaNode.Task][LuaNode.Params][LuaNode.Tasks].GetDict();
				foreach (LsonValue lsv in lsdRouteTasks.Values)
				{
					RouteTasks.Add(new RouteTask(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			m_lsd.SetOrAddString(LuaNode.Name, Name);
			m_lsd[LuaNode.Type] = Type;
			m_lsd[LuaNode.Action] = Action;
			m_lsd[LuaNode.Altitude] = Altitude;
			m_lsd[LuaNode.AltitudeType] = AltitudeType;
			m_lsd[LuaNode.Y] = Y;
			m_lsd[LuaNode.X] = X;
			m_lsd.SetOrAddInt(LuaNode.AirdromeId, AirdromeId);
			m_lsd.SetOrAddInt(LuaNode.HelipadId, HelipadId);

			LsonDict lsdRouteTasks = m_lsd[LuaNode.Task][LuaNode.Params][LuaNode.Tasks].GetDict();
			foreach (RouteTask rt in RouteTasks)
			{
				rt.ToLua();
			}
		}
	}
}

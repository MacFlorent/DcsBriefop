using DcsBriefop.Tools;
using LsonLib;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataMiz
{
	internal class MizGroup : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Id = "groupId";
			public static readonly string Name = "name";
			public static readonly string LateActivation = "lateActivation";
			public static readonly string Y = "y";
			public static readonly string X = "x";
			public static readonly string Task = "task";
			public static readonly string RadioModulation = "modulation";
			public static readonly string RadioFrequency = "frequency";
			public static readonly string Units = "units";
			public static readonly string Route = "route";
			public static readonly string Points = "points";
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public bool LateActivation { get; set; }
		public decimal Y { get; set; }
		public decimal X { get; set; }
		public string Task { get; set; }
		public decimal? RadioFrequency { get; set; }
		public int? RadioModulation { get; set; }
		public virtual List<MizUnit> Units { get; set; } = new List<MizUnit>();
		public List<MizRoutePoint> RoutePoints { get; set; } = new List<MizRoutePoint>();

		public MizGroup(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = Lsd[LuaNode.Id].GetInt();
			Name = Lsd[LuaNode.Name].GetString();
			LateActivation = Lsd.IfExistsBool(LuaNode.LateActivation).GetValueOrDefault();
			Y = Lsd[LuaNode.Y].GetDecimal();
			X = Lsd[LuaNode.X].GetDecimal();
			Task = Lsd.IfExistsString(LuaNode.Task);
			RadioFrequency = Lsd.IfExistsDecimal(LuaNode.RadioFrequency);
			RadioModulation = Lsd.IfExistsInt(LuaNode.RadioModulation);

			if (Lsd.ContainsKey(LuaNode.Units))
			{
				LsonDict lsdUnits = Lsd[LuaNode.Units].GetDict();
				foreach (LsonValue lsv in lsdUnits.Values)
				{
					Units.Add(new MizUnit(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Route))
			{
				LsonDict lsdRoutePoints = Lsd[LuaNode.Route][LuaNode.Points].GetDict();
				foreach (LsonValue lsv in ToolsLson.GetOrderedValueList(lsdRoutePoints))
				{
					RoutePoints.Add(new MizRoutePoint(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Id] = Id;
			Lsd[LuaNode.Name] = Name;
			Lsd.SetIfExists(LuaNode.LateActivation, LateActivation);
			Lsd.SetIfExists(LuaNode.Task, Task);
			Lsd.SetIfExists(LuaNode.RadioFrequency, RadioFrequency);
			Lsd.SetIfExists(LuaNode.RadioModulation, RadioModulation);

			foreach (MizUnit unit in Units)
			{
				unit.ToLua();
			}

			if (Lsd.ContainsKey(LuaNode.Route))
			{
				LsonDict lsdRoutePoints = Lsd[LuaNode.Route][LuaNode.Points].GetDict();
				lsdRoutePoints.Clear();

				int i = 1;
				foreach (MizRoutePoint rp in RoutePoints)
				{
					rp.ToLua();
					lsdRoutePoints.Add(i, rp.Lsd);
					i++;
				}
			}
		}
	}
}

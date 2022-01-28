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
			public static readonly string Route = "route";
			public static readonly string Points = "points";
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public bool LateActivation { get; set; }
		public decimal Y { get; set; }
		public decimal X { get; set; }

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

			Lsd.SetOrAddBool(LuaNode.LateActivation, LateActivation);

			if (Lsd.ContainsKey(LuaNode.Route))
			{
				LsonDict lsdRoutePoints = Lsd[LuaNode.Route][LuaNode.Points].GetDict();
				lsdRoutePoints.Clear();

				int i = 0;
				foreach (MizRoutePoint rp in RoutePoints)
				{
					rp.ToLua();
					lsdRoutePoints.Add(i, rp.Lsd);
					i++;
				}
			}
		}
	}

	internal class MizGroupFlight : MizGroup
	{
		private class LuaNode
		{
			public static readonly string Task = "task";
			public static readonly string RadioModulation = "modulation";
			public static readonly string RadioFrequency = "frequency";
			public static readonly string Units = "units";
		}

		public string Task { get; set; }
		public decimal RadioFrequency { get; set; }
		public int RadioModulation { get; set; }

		public MizGroupFlight(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();

			Task = Lsd[LuaNode.Task].GetString();
			RadioFrequency = Lsd[LuaNode.RadioFrequency].GetDecimal();
			RadioModulation = Lsd[LuaNode.RadioModulation].GetInt();

			LsonDict lsdUnits = Lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new MizUnitFlight(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			Lsd[LuaNode.Task] = Task;
			Lsd[LuaNode.RadioFrequency] = RadioFrequency;
			Lsd[LuaNode.RadioModulation] = RadioModulation;

			foreach (MizUnitFlight unit in Units.OfType<MizUnitFlight>())
			{
				unit.ToLua();
			}
		}
	}

	internal class MizGroupShip : MizGroup
	{
		private class LuaNode
		{
			public static readonly string Units = "units";
		}

		public MizGroupShip(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
			LsonDict lsdUnits = Lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new MizUnitShip(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			foreach (MizUnitShip unit in Units.OfType<MizUnitShip>())
			{
				unit.ToLua();
			}
		}
	}

	internal class MizGroupVehicle : MizGroup
	{
		private class LuaNode
		{
			public static readonly string Units = "units";
		}

		public MizGroupVehicle(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
			LsonDict lsdUnits = Lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new MizUnitVehicle(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			foreach (MizUnitVehicle unit in Units.OfType<MizUnitVehicle>())
			{
				unit.ToLua();
			}
		}
	}

	internal class MizGroupStatic : MizGroup
	{
		private class LuaNode
		{
			public static readonly string Units = "units";
		}

		public MizGroupStatic(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
			LsonDict lsdUnits = Lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new MizUnitStatic(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			foreach (MizUnitStatic unit in Units.OfType<MizUnitStatic>())
			{
				unit.ToLua();
			}
		}
	}
}

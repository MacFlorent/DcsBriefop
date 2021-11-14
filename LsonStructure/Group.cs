using DcsBriefop.Tools;
using LsonLib;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.LsonStructure
{
	internal class Group : BaseLsonStructure
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

		public virtual List<Unit> Units { get; set; } = new List<Unit>();
		public List<RoutePoint> RoutePoints { get; set; } = new List<RoutePoint>();

		public Group(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = m_lsd[LuaNode.Id].GetInt();
			Name = m_lsd[LuaNode.Name].GetString();
			LateActivation = m_lsd.IfExistsBool(LuaNode.LateActivation).GetValueOrDefault();
			Y = m_lsd[LuaNode.Y].GetDecimal();
			X = m_lsd[LuaNode.X].GetDecimal();

			if (m_lsd.ContainsKey(LuaNode.Route))
			{
				LsonDict lsdRoutePoints = m_lsd[LuaNode.Route][LuaNode.Points].GetDict();
				foreach (LsonValue lsv in lsdRoutePoints.Values)
				{
					RoutePoints.Add(new RoutePoint(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Id] = Id;
			m_lsd[LuaNode.Name] = Name;

			m_lsd.SetOrAddBool(LuaNode.LateActivation, LateActivation);

			foreach (RoutePoint rp in RoutePoints)
			{
				rp.ToLua();
			}
		}
	}

	internal class GroupFlight : Group
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

		public GroupFlight(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();

			Task = m_lsd[LuaNode.Task].GetString();
			RadioFrequency = m_lsd[LuaNode.RadioFrequency].GetDecimal();
			RadioModulation = m_lsd[LuaNode.RadioModulation].GetInt();

			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new UnitFlight(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			m_lsd[LuaNode.Task] = Task;
			m_lsd[LuaNode.RadioFrequency] = RadioFrequency;
			m_lsd[LuaNode.RadioModulation] = RadioModulation;

			foreach (UnitFlight unit in Units.OfType<UnitFlight>())
			{
				unit.ToLua();
			}
		}
	}

	internal class GroupShip : Group
	{
		private class LuaNode
		{
			public static readonly string Units = "units";
		}

		public GroupShip(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new UnitShip(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			foreach (UnitShip unit in Units.OfType<UnitShip>())
			{
				unit.ToLua();
			}
		}
	}

	internal class GroupVehicle : Group
	{
		private class LuaNode
		{
			public static readonly string Units = "units";
		}

		public GroupVehicle(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new UnitVehicle(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			foreach (UnitVehicle unit in Units.OfType<UnitVehicle>())
			{
				unit.ToLua();
			}
		}
	}

	internal class GroupStatic : Group
	{
		private class LuaNode
		{
			public static readonly string Units = "units";
		}

		public GroupStatic(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new UnitStatic(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			foreach (UnitStatic unit in Units.OfType<UnitStatic>())
			{
				unit.ToLua();
			}
		}
	}
	}

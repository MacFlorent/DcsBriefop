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
					RoutePoints.Add(new MizRoutePoint(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Id] = Id;
			m_lsd[LuaNode.Name] = Name;

			m_lsd.SetOrAddBool(LuaNode.LateActivation, LateActivation);

			foreach (MizRoutePoint rp in RoutePoints)
			{
				rp.ToLua();
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

			Task = m_lsd[LuaNode.Task].GetString();
			RadioFrequency = m_lsd[LuaNode.RadioFrequency].GetDecimal();
			RadioModulation = m_lsd[LuaNode.RadioModulation].GetInt();

			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
			foreach (LsonValue lsv in lsdUnits.Values)
			{
				Units.Add(new MizUnitFlight(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			base.ToLua();

			m_lsd[LuaNode.Task] = Task;
			m_lsd[LuaNode.RadioFrequency] = RadioFrequency;
			m_lsd[LuaNode.RadioModulation] = RadioModulation;

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
			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
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
			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
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
			LsonDict lsdUnits = m_lsd[LuaNode.Units].GetDict();
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

using LsonLib;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizCountry : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Name = "name";
			public static readonly string Plane = "plane";
			public static readonly string Helicopter = "helicopter";
			public static readonly string Ship = "ship";
			public static readonly string Vehicle = "vehicle";
			public static readonly string Static = "static";
			public static readonly string Group = "group";
		}

		public string Name { get; set; }
		public List<MizGroupFlight> GroupFlights { get; set; } = new List<MizGroupFlight>();
		public List<MizGroupShip> GroupShips { get; set; } = new List<MizGroupShip>();
		public List<MizGroupVehicle> GroupVehicles { get; set; } = new List<MizGroupVehicle>();
		public List<MizGroupStatic> GroupStatics { get; set; } = new List<MizGroupStatic>();

		public MizCountry(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Name = Lsd[LuaNode.Name].GetString();

			if (Lsd.ContainsKey(LuaNode.Plane))
			{
				LsonDict lsdGroupPlanes = Lsd[LuaNode.Plane][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupPlanes.Values)
				{
					GroupFlights.Add(new MizGroupFlight(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Helicopter))
			{
				LsonDict lsdGroupHelicopters = Lsd[LuaNode.Helicopter][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupHelicopters.Values)
				{
					GroupFlights.Add(new MizGroupFlight(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Ship))
			{
				LsonDict lsdGroupShips = Lsd[LuaNode.Ship][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupShips.Values)
				{
					GroupShips.Add(new MizGroupShip(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Vehicle))
			{
				LsonDict lsdGroupVehicles = Lsd[LuaNode.Vehicle][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupVehicles.Values)
				{
					GroupVehicles.Add(new MizGroupVehicle(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Static))
			{
				LsonDict lsdGroupStatics = Lsd[LuaNode.Static][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupStatics.Values)
				{
					GroupStatics.Add(new MizGroupStatic(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Name] = Name;

			foreach (MizGroupFlight gp in GroupFlights)
			{
				gp.ToLua();
			}
			foreach (MizGroupShip gs in GroupShips)
			{
				gs.ToLua();
			}
		}

		//public List<int> GetUsedAirdromeIds()
		//{
		//	return GroupPlanes.Select(_gp => _gp.GetAirdromeId()).Where(_id => _id is object).OfType<int>().ToList();
		//}

		//public Unit FindUnit(int iUnitId)
		//{
		//	Unit u = GroupPlanes.Select(_gp => _gp.FindUnit(iUnitId)).Where(_u => _u is object).FirstOrDefault();
		//	if (u is null)
		//		u = GroupShips.Select(_gp => _gp.FindUnit(iUnitId)).Where(_u => _u is object).FirstOrDefault();

		//	return u;
		//}
	}
}
using LsonLib;
using System.Collections.Generic;

namespace DcsBriefop.LsonStructure
{
	internal class Country : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string Name = "name";
			public static readonly string Plane = "plane";
			public static readonly string Helicopter = "helicopter";
			public static readonly string Ship = "ship";
			public static readonly string Group = "group";
		}

		public string Name { get; set; }
		public List<GroupFlight> GroupFlights { get; set; } = new List<GroupFlight>();
		public List<GroupShip> GroupShips { get; set; } = new List<GroupShip>();

		public Country(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Name = m_lsd[LuaNode.Name].GetString();

			if (m_lsd.ContainsKey(LuaNode.Plane))
			{
				LsonDict lsdGroupPlanes = m_lsd[LuaNode.Plane][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupPlanes.Values)
				{
					GroupFlights.Add(new GroupFlight(lsv.GetDict()));
				}
			}

			if (m_lsd.ContainsKey(LuaNode.Helicopter))
			{
				LsonDict lsdGroupHelicopters = m_lsd[LuaNode.Helicopter][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupHelicopters.Values)
				{
					GroupFlights.Add(new GroupFlight(lsv.GetDict()));
				}
			}

			if (m_lsd.ContainsKey(LuaNode.Ship))
			{
				LsonDict lsdGroupShips = m_lsd[LuaNode.Ship][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupShips.Values)
				{
					GroupShips.Add(new GroupShip(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Name] = Name;

			foreach (GroupFlight gp in GroupFlights)
			{
				gp.ToLua();
			}
			foreach (GroupShip gs in GroupShips)
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
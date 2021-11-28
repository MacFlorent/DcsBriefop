using LsonLib;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizCoalition : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Name = "name";
			public static readonly string Bullseye = "bullseye";
			public static readonly string BullseyeY = "y";
			public static readonly string BullseyeX = "x";
			public static readonly string NavPoints = "nav_points";
			public static readonly string Country = "country";
		}

		public string Name { get; private set; }
		public decimal BullseyeY { get; set; }
		public decimal BullseyeX { get; set; }

		public List<MizInitialPoint> InitialPoints { get; private set; } = new List<MizInitialPoint>();
		public List<MizCountry> Countries { get; private set; } = new List<MizCountry>();

		public MizCoalition(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Name = m_lsd[LuaNode.Name].GetString();

			BullseyeY = m_lsd[LuaNode.Bullseye][LuaNode.BullseyeY].GetDecimal();
			BullseyeX = m_lsd[LuaNode.Bullseye][LuaNode.BullseyeX].GetDecimal();

			LsonDict lsdInitialPoints = m_lsd[LuaNode.NavPoints].GetDict();
			foreach (LsonValue lsv in lsdInitialPoints.Values)
			{
				InitialPoints.Add(new MizInitialPoint(lsv.GetDict()));
			}

			LsonDict lsdCountries = m_lsd[LuaNode.Country].GetDict();
			foreach (LsonValue lsv in lsdCountries.Values)
			{
				Countries.Add(new MizCountry(lsv.GetDict()));
			}
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Name] = Name;

			m_lsd[LuaNode.Bullseye][LuaNode.BullseyeY] = BullseyeY;
			m_lsd[LuaNode.Bullseye][LuaNode.BullseyeX] = BullseyeX;

			foreach (MizInitialPoint ip in InitialPoints)
			{
				ip.ToLua();
			}
			foreach (MizCountry c in Countries)
			{
				c.ToLua();
			}
		}

		//public List<GroupPlane> GetGroupPlanes()
		//{
		//	List<GroupPlane> l = new List<GroupPlane>();
		//	foreach (Country country in Countries)
		//	{
		//		l.AddRange(country.GroupPlanes);
		//	}

		//	return l;
		//}

		//public List<GroupShip> GetGroupShips()
		//{
		//	List<GroupShip> l = new List<GroupShip>();
		//	foreach (Country country in Countries)
		//	{
		//		l.AddRange(country.GroupShips);
		//	}

		//	return l;
		//}

		//public List<int> GetUsedAirdromeIds()
		//{
		//	List<int> l = new List<int>();
		//	foreach (Country country in Countries)
		//	{
		//		l.AddRange(country.GetUsedAirdromeIds());
		//	}

		//	return l.Distinct().ToList();
		//}

		//public Unit FindUnit(int iUnitId)
		//{
		//	return Countries.Select(_c => _c.FindUnit(iUnitId)).Where(_u => _u is object).FirstOrDefault();
		//}
	}
}

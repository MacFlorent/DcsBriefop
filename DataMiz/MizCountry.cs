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
		public List<MizGroup> GroupFlights { get; set; } = new List<MizGroup>();
		public List<MizGroup> GroupShips { get; set; } = new List<MizGroup>();
		public List<MizGroup> GroupVehicles { get; set; } = new List<MizGroup>();
		public List<MizGroup> GroupStatics { get; set; } = new List<MizGroup>();

		public MizCountry(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Name = Lsd[LuaNode.Name].GetString();

			if (Lsd.ContainsKey(LuaNode.Plane))
			{
				LsonDict lsdGroupPlanes = Lsd[LuaNode.Plane][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupPlanes.Values)
				{
					GroupFlights.Add(new MizGroup(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Helicopter))
			{
				LsonDict lsdGroupHelicopters = Lsd[LuaNode.Helicopter][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupHelicopters.Values)
				{
					GroupFlights.Add(new MizGroup(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Ship))
			{
				LsonDict lsdGroupShips = Lsd[LuaNode.Ship][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupShips.Values)
				{
					GroupShips.Add(new MizGroup(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Vehicle))
			{
				LsonDict lsdGroupVehicles = Lsd[LuaNode.Vehicle][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupVehicles.Values)
				{
					GroupVehicles.Add(new MizGroup(lsv.GetDict()));
				}
			}

			if (Lsd.ContainsKey(LuaNode.Static))
			{
				LsonDict lsdGroupStatics = Lsd[LuaNode.Static][LuaNode.Group].GetDict();
				foreach (LsonValue lsv in lsdGroupStatics.Values)
				{
					GroupStatics.Add(new MizGroup(lsv.GetDict()));
				}
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Name] = Name;

			foreach (MizGroup mizGroup in GroupFlights)
			{
				mizGroup.ToLua();
			}
			foreach (MizGroup mizGroup in GroupShips)
			{
				mizGroup.ToLua();
			}
			foreach (MizGroup mizGroup in GroupVehicles)
			{
				mizGroup.ToLua();
			}
			foreach (MizGroup mizGroup in GroupStatics)
			{
				mizGroup.ToLua();
			}
		}
	}
}
using LsonLib;

namespace DcsBriefop.LsonStructure
{
	internal class Unit : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string Id = "unitId";
			public static readonly string Name = "name";
			public static readonly string Type = "type";
			public static readonly string Skill = "skill";
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Skill { get; set; }

		public Unit(LsonDict lsd) : base(lsd) { }
		
		public override void FromLua()
		{
			Id = m_lsd[LuaNode.Id].GetInt();
			Name = m_lsd[LuaNode.Name].GetString();
			Type = m_lsd[LuaNode.Type].GetString();
			Skill = m_lsd[LuaNode.Skill].GetString();
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Id] = Id;
			m_lsd[LuaNode.Name] = Name;
			m_lsd[LuaNode.Type] = Type;
			m_lsd[LuaNode.Skill] = Skill;
		}
	}

	internal class UnitPlane : Unit
	{
		private class LuaNode
		{
			public static readonly string Callsign = "callsign";
			public static readonly string Name = "name";
			public static readonly string Modex = "onboard_num";
		}

		public string Callsign { get; set; }
		public string Modex { get; set; }

		public UnitPlane(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();

			Callsign = m_lsd[LuaNode.Callsign][LuaNode.Name].GetString();
			Modex = m_lsd[LuaNode.Modex].GetString();
		}

		public override void ToLua()
		{
			base.ToLua();

			m_lsd[LuaNode.Callsign][LuaNode.Name] = Callsign;
			m_lsd[LuaNode.Modex] = Modex;
		}
	}

	internal class UnitShip : Unit
	{
		private class LuaNode
		{
			public static readonly string RadioFrequency = "frequency";
			public static readonly string RadioModulation = "modulation";
		}

		public decimal RadioFrequency { get; set; }
		public int RadioModulation { get; set; }

		public UnitShip(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();

			RadioFrequency = m_lsd[LuaNode.RadioFrequency].GetDecimal();
			RadioModulation = m_lsd[LuaNode.RadioModulation].GetInt();
		}

		public override void ToLua()
		{
			base.ToLua();

			m_lsd[LuaNode.RadioFrequency] = RadioFrequency;
			m_lsd[LuaNode.RadioModulation] = RadioModulation;
		}
	}
}

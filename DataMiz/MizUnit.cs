using DcsBriefop.Tools;
using LsonLib;
using System.Collections;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizUnit : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Id = "unitId";
			public static readonly string Name = "name";
			public static readonly string Type = "type";
			public static readonly string Skill = "skill";
			public static readonly string Y = "y";
			public static readonly string X = "x";
			public static readonly string Radio = "Radio";
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Skill { get; set; }
		public decimal Y { get; set; }
		public decimal X { get; set; }
		public MizRadio[] Radios { get; set; }

		public MizUnit(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = m_lsd[LuaNode.Id].GetInt();
			Name = m_lsd[LuaNode.Name].GetString();
			Type = m_lsd[LuaNode.Type].GetString();
			Skill = m_lsd.IfExistsString(LuaNode.Skill);
			Y = m_lsd[LuaNode.Y].GetDecimal();
			X = m_lsd[LuaNode.X].GetDecimal();

			if (m_lsd.ContainsKey(LuaNode.Radio))
			{
				LsonDict lsdRadios = m_lsd[LuaNode.Radio].GetDict();
				Radios = new MizRadio[lsdRadios.Count + 1];
				foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdRadios)
				{
					Radios[kvp.Key.GetInt()] = new MizRadio(kvp.Value.GetDict());
				}
			}
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Id] = Id;
			m_lsd[LuaNode.Name] = Name;
			m_lsd[LuaNode.Type] = Type;
			m_lsd[LuaNode.Skill] = Skill;

			if (Radios is object)
			{
				foreach (MizRadio radio in Radios)
				{
					radio?.ToLua();
				}
			}
		}
	}

	internal class MizUnitFlight : MizUnit
	{
		private class LuaNode
		{
			public static readonly string Callsign = "callsign";
			public static readonly string Name = "name";
			public static readonly string Modex = "onboard_num";
		}

		public string Callsign { get; set; }
		public string Modex { get; set; }

		public MizUnitFlight(LsonDict lsd) : base(lsd) { }

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

	internal class MizUnitShip : MizUnit
	{
		private class LuaNode
		{
			public static readonly string RadioFrequency = "frequency";
			public static readonly string RadioModulation = "modulation";
		}

		public decimal RadioFrequency { get; set; }
		public int RadioModulation { get; set; }

		public MizUnitShip(LsonDict lsd) : base(lsd) { }

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

	internal class MizUnitVehicle : MizUnit
	{
		private class LuaNode
		{
		}

		public MizUnitVehicle(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
		}

		public override void ToLua()
		{
			base.ToLua();
		}
	}

	internal class MizUnitStatic : MizUnit
	{
		private class LuaNode
		{
		}

		public MizUnitStatic(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			base.FromLua();
		}

		public override void ToLua()
		{
			base.ToLua();
		}
	}

}

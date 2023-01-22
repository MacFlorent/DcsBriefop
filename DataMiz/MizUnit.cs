using DcsBriefop.Tools;
using LsonLib;
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
			public static readonly string Alt = "alt";
			public static readonly string Radio = "Radio";
			public static readonly string RadioFrequency = "frequency";
			public static readonly string RadioModulation = "modulation";
			public static readonly string Callsign = "callsign";
			public static readonly string OnboardNum = "onboard_num";
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Skill { get; set; }
		public decimal Y { get; set; }
		public decimal X { get; set; }
		public decimal? Altitude { get; set; }
		public decimal? RadioFrequency { get; set; }
		public int? RadioModulation { get; set; }
		public MizRadio[] Radios { get; set; }
		public int? CallsignNumber { get; set; }
		public MizCallsign Callsign { get; set; }
		public string OnboardNum { get; set; }

		public MizUnit(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Id = Lsd[LuaNode.Id].GetInt();
			Name = Lsd[LuaNode.Name].GetString();
			Type = Lsd[LuaNode.Type].GetString();
			Skill = Lsd.IfExistsString(LuaNode.Skill);
			Y = Lsd[LuaNode.Y].GetDecimal();
			X = Lsd[LuaNode.X].GetDecimal();
			Altitude = Lsd.IfExistsDecimal(LuaNode.Alt);

			if (Lsd.ContainsKey(LuaNode.Callsign))
			{
				LsonValue lsvCallsign = Lsd[LuaNode.Callsign];
				if (lsvCallsign is LsonDict lsdCallsign)
					Callsign = new MizCallsign(lsdCallsign);
				else
					CallsignNumber = lsvCallsign.GetIntLenientSafe();
			}

			OnboardNum = Lsd.IfExistsString(LuaNode.OnboardNum);

			RadioFrequency = Lsd.IfExistsDecimal(LuaNode.RadioFrequency);
			RadioModulation = Lsd.IfExistsInt(LuaNode.RadioModulation);
			if (Lsd.ContainsKey(LuaNode.Radio))
			{
				LsonDict lsdRadios = Lsd[LuaNode.Radio].GetDict();
				Radios = new MizRadio[lsdRadios.Count + 1];
				foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdRadios)
				{
					Radios[kvp.Key.GetInt()] = new MizRadio(kvp.Value.GetDict());
				}
			}
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Id] = Id;
			Lsd[LuaNode.Name] = Name;
			Lsd[LuaNode.Type] = Type;
			Lsd[LuaNode.Skill] = Skill;
			
			if (Callsign is object)
				Callsign.ToLua();
			if (CallsignNumber is object)
				Lsd.SetIfExists(LuaNode.Callsign, CallsignNumber);

			Lsd.SetIfExists (LuaNode.OnboardNum, OnboardNum);

			Lsd.SetIfExists(LuaNode.RadioFrequency, RadioFrequency);
			Lsd.SetIfExists(LuaNode.RadioModulation, RadioModulation);
			if (Radios is object)
			{
				foreach (MizRadio radio in Radios)
				{
					radio?.ToLua();
				}
			}
		}
	}
}

using LsonLib;
using System;
using System.Collections.Generic;

namespace DcsBriefop.LsonStructure
{
	internal class RootDictionary : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string Sortie = "DictKey_sortie_5";
			public static readonly string Description = "DictKey_descriptionText_1";
			public static readonly string RedTask = "DictKey_descriptionRedTask_2";
			public static readonly string BlueTask = "DictKey_descriptionBlueTask_3";
			public static readonly string NeutralTask = "DictKey_descriptionNeutralsTask_4";
		}

		public Dictionary<string, LsonValue> RootLua { get; private set; }

		public string Sortie { get; set; }
		public string Description { get; set; }
		public string RedTask { get; set; }
		public string BlueTask { get; set; }
		public string NeutralTask { get; set; }

		public RootDictionary(Dictionary<string, LsonValue> rootLua) : base(rootLua["dictionary"].GetDict())
		{
			RootLua = rootLua;
		}

		public override void FromLua()
		{
			Sortie = FromLuaString(m_lsd[LuaNode.Sortie].GetString());
			Description = FromLuaString(m_lsd[LuaNode.Description].GetString());
			RedTask = FromLuaString(m_lsd[LuaNode.RedTask].GetString());
			BlueTask = FromLuaString(m_lsd[LuaNode.BlueTask].GetString());
			NeutralTask = FromLuaString(m_lsd[LuaNode.NeutralTask].GetString());
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Sortie] = ToLuaString(Sortie);
			m_lsd[LuaNode.Description] = ToLuaString(Description);
			m_lsd[LuaNode.RedTask] = ToLuaString(RedTask);
			m_lsd[LuaNode.BlueTask] = ToLuaString(BlueTask);
			m_lsd[LuaNode.NeutralTask] = ToLuaString(NeutralTask);
		}

		private string FromLuaString(string sString)
		{
			return sString.Replace("\\\n", Environment.NewLine);
		}
		private string ToLuaString(string sString)
		{
			string s = FromLuaString(sString);
			return s.Replace(Environment.NewLine, "\\\n");
		}
	}
}

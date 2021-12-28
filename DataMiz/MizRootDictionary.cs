﻿using LsonLib;
using System;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizRootDictionary : BaseMiz
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

		public MizRootDictionary(Dictionary<string, LsonValue> rootLua) : base(rootLua["dictionary"].GetDict())
		{
			RootLua = rootLua;
		}

		public override void FromLua()
		{
			Sortie = m_lsd[LuaNode.Sortie].GetString();
			Description = m_lsd[LuaNode.Description].GetString();
			RedTask = m_lsd[LuaNode.RedTask].GetString();
			BlueTask = m_lsd[LuaNode.BlueTask].GetString();
			NeutralTask = m_lsd[LuaNode.NeutralTask].GetString();
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Sortie] = Sortie;
			m_lsd[LuaNode.Description] = Description;
			m_lsd[LuaNode.RedTask] = RedTask;
			m_lsd[LuaNode.BlueTask] = BlueTask;
			m_lsd[LuaNode.NeutralTask] = NeutralTask;
		}
	}
}
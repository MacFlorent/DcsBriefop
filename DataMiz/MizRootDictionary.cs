using DcsBriefop.Tools;
using LsonLib;
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
			Sortie = ToolsLson.IfExistsString(Lsd, LuaNode.Sortie);
			Description = ToolsLson.IfExistsString(Lsd, LuaNode.Description);
			RedTask = ToolsLson.IfExistsString(Lsd, LuaNode.RedTask);
			BlueTask = ToolsLson.IfExistsString(Lsd, LuaNode.BlueTask);
			NeutralTask = ToolsLson.IfExistsString(Lsd, LuaNode.NeutralTask);
		}

		public override void ToLua()
		{
			Lsd.SetOrAddString(LuaNode.Sortie, Sortie);
			Lsd.SetOrAddString(LuaNode.Description, Description);
			Lsd.SetOrAddString(LuaNode.RedTask, RedTask);
			Lsd.SetOrAddString(LuaNode.BlueTask, BlueTask);
			Lsd.SetOrAddString(LuaNode.NeutralTask, NeutralTask);
		}
	}
}

using DcsBriefop.Tools;
using LsonLib;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizRadio : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Modulations = "modulations";
			public static readonly string Channels = "channels";
		}

		public int[] Modulations { get; set; }
		public decimal[] Channels { get; set; }

		public MizRadio(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			LsonDict lsdModulations = Lsd.IfExists(LuaNode.Modulations)?.GetDict();
			if (lsdModulations is null)
				Modulations = new int[0];
			else
			{
				Modulations = new int[lsdModulations.Count + 1];
				foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdModulations)
				{
					Modulations[kvp.Key.GetInt()] = kvp.Value.GetIntLenientSafe().GetValueOrDefault(0);
				}
			}

			LsonDict lsdChannels = Lsd[LuaNode.Channels].GetDict();
			Channels = new decimal[lsdChannels.Count + 1];
			foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdChannels)
			{
				Channels[kvp.Key.GetInt()] = kvp.Value.GetDecimal();
			}
		}

		public override void ToLua()
		{
			LsonDict lsdModulations = Lsd.IfExists(LuaNode.Modulations)?.GetDict();
			if (lsdModulations is object)
			{
				for (int i = 0; i < Modulations.Length; i++)
				{
					if (lsdModulations.ContainsKey(i))
					{
						lsdModulations[i] = Modulations[i];
					}
				}
			}

			LsonDict lsdChannels = Lsd[LuaNode.Channels].GetDict();
			for (int i = 0; i < Channels.Length; i++)
			{
				if (lsdChannels.ContainsKey(i))
				{
					lsdChannels[i] = Channels[i];
				}
			}
		}
	}
}

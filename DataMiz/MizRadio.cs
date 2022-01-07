using LsonLib;
using System.Collections;
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
			LsonDict lsdModulations = m_lsd[LuaNode.Modulations].GetDict();
			Modulations = new int[lsdModulations.Count + 1];
			foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdModulations)
			{
				Modulations[kvp.Key.GetInt()] = kvp.Value.GetInt();
			}

			LsonDict lsdChannels = m_lsd[LuaNode.Channels].GetDict();
			Channels = new decimal[lsdChannels.Count + 1];
			foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdChannels)
			{
				Channels[kvp.Key.GetInt()] = kvp.Value.GetDecimal();
			}
		}

		public override void ToLua()
		{
			LsonDict lsdModulations = m_lsd[LuaNode.Modulations].GetDict();
			for (int i = 0; i < Modulations.Length; i++)
			{
				if (lsdModulations.ContainsKey(i))
				{
					lsdModulations[i] = Modulations[i];
				}
			}

			LsonDict lsdChannels = m_lsd[LuaNode.Channels].GetDict();
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

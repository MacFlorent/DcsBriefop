using DcsBriefop.Data;
using DcsBriefop.Tools;
using LsonLib;
using System.Collections.Generic;
using System.Linq;

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
			LsonDict lsdChannels = Lsd.IfExists(LuaNode.Channels)?.GetDict();
			LsonDict lsdModulations = Lsd.IfExists(LuaNode.Modulations)?.GetDict();

			int iLastChannel = lsdChannels?.Select(_l => _l.Key.GetInt())?.DefaultIfEmpty(0).Max() ?? 0;
			int iLastModulation = lsdModulations?.Select(_l => _l.Key.GetInt())?.DefaultIfEmpty(0).Max() ?? 0;
			int iLast = iLastChannel;
			if (iLastModulation > iLast)
				iLast = iLastModulation;

			int iCount = iLast + 1; // add 1 to account for the lua 1 starting index. First slot in the arrays will remain empty.
			Channels = new decimal[iCount];
			Modulations = new int[iCount];

			for (int i = 0; i < iCount; i++)
			{
				Channels[i] = 0;
				Modulations[i] = ElementRadioModulation.AM;
			}

			if (lsdChannels is object)
			{
				foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdChannels)
				{
					Channels[kvp.Key.GetInt()] = kvp.Value.GetDecimal();
				}
			}

			if (lsdModulations is object)
			{
				foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdModulations)
				{
					Modulations[kvp.Key.GetInt()] = kvp.Value.GetIntLenientSafe().GetValueOrDefault(ElementRadioModulation.AM);
				}
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

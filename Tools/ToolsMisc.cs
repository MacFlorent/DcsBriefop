using DcsBriefop.MasterData;
using System.Text;

namespace DcsBriefop.Tools
{
	internal static class ToolsMisc
	{
		public static void AppendWithSeparator(this StringBuilder sb, string value, string sSeparator)
		{
			if (sb.Length > 0)
				sb.Append(sSeparator);
			sb.Append(value);
		}

		public static string GetTacanString(int iChannel, string sMode, string sCallsign)
		{
			return $"{iChannel}{sMode} [{sCallsign}]";
		}

		public static string GetRadioString(decimal dFrequency, int iModulation)
		{
			return $"{dFrequency:###.00} {GetRadioModulationString(iModulation)}";
		}

		public static string GetRadioModulationString(int iModulation)
		{
			if (iModulation == ElementRadioModulation.AM)
				return "AM";
			if (iModulation == ElementRadioModulation.FM)
				return "FM";
			else
				return null;
		}
	}
}

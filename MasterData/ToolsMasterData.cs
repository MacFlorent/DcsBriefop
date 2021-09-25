namespace DcsBriefop.MasterData
{
	internal static class ToolsMasterData
	{
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

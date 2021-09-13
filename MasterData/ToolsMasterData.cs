namespace DcsBriefop.MasterData
{
	internal static class ToolsMasterData
	{
		public static string GetRadioString(decimal dFrequency, int iModulation)
		{
			return $"{dFrequency:###.00} {GetRadioModulationString(iModulation)}";
		}

		public static string GetRadioModulationString(int iModulation)
		{
			if (iModulation == RadioModulation.AM)
				return "AM";
			if (iModulation == RadioModulation.FM)
				return "FM";
			else
				return null;
		}
	}
}

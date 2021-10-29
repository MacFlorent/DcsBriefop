using System.Collections.Generic;

namespace DcsBriefop.Data
{
	internal class WeatherPreset
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int Density { get; set; }
		public bool Precipitation { get; set; }
		public int? Visibility { get; set; } // Visibility in meters

		public WeatherPreset(string sName, string sDescription, int iDensity, bool bPrecipitation, int? iVisibilityMeters)
		{
			Name = sName;
			Description = sDescription;
			Density = iDensity;
			Precipitation = bPrecipitation;
			Visibility = iVisibilityMeters;
		}


		public static Dictionary<string, WeatherPreset> WeatherPresets { get; set; }
		
		private static void AddWeatherPreset(string sName, string sDescription, int iDensity, bool bPrecipitation, int? iVisibilityMeters)
		{
			WeatherPresets.Add(sName, new WeatherPreset(sName, sDescription, iDensity, bPrecipitation, iVisibilityMeters));
		}

		static WeatherPreset()
		{
			WeatherPresets = new Dictionary<string, WeatherPreset>();
			AddWeatherPreset("Preset1", "Light Scattered 1 - FEW/SCT", 2, false, null);
			AddWeatherPreset("Preset2", "Light Scattered 2 - FEW/SCT", 2, false, null);
			AddWeatherPreset("Preset3", "High Scattered 1 - SCT", 3, false, null);
			AddWeatherPreset("Preset4", "High Scattered 2 - SCT", 3, false, null);
			AddWeatherPreset("Preset5", "Scattered 1 - SCT", 3, false, null);
			AddWeatherPreset("Preset6", "Scattered 2 - SCT/BKN", 4, false, null);
			AddWeatherPreset("Preset7", "Scattered 3 - BKN", 3, false, null);
			AddWeatherPreset("Preset8", "High Scattered 3 - SCT/BKN", 4, false, null);
			AddWeatherPreset("Preset9", "Scattered 4 - BKN", 5, false, null);
			AddWeatherPreset("Preset10", "Scattered 5// SCT / BKN", 4, false, null);
			AddWeatherPreset("Preset11", "Scattered 6// BKN", 6, false, null);
			AddWeatherPreset("Preset12", "Scattered 7// BKN", 6, false, null);
			AddWeatherPreset("Preset13", "Broken 1 - BKN", 6, false, null);
			AddWeatherPreset("Preset14", "Broken 2 - BKN", 6, false, null);
			AddWeatherPreset("Preset15", "Broken 3 - SCT/BKN", 4, false, null);
			AddWeatherPreset("Preset16", "Broken 4 - BKN", 6, false, null);
			AddWeatherPreset("Preset17", "Broken 5 - BKN/OVC", 7, false, null);
			AddWeatherPreset("Preset18", "Broken 6 - BKN/OVC", 7, false, null);
			AddWeatherPreset("Preset19", "Broken 7 - OVC", 8, false, null);
			AddWeatherPreset("Preset20", "Broken 8 - BKN/OVC", 7, false, null);
			AddWeatherPreset("Preset21", "Overcast 1 - BKN/OVC", 7, false, null);
			AddWeatherPreset("Preset22", "Overcast 2 - BKN", 6, false, null);
			AddWeatherPreset("Preset23", "Overcast 3 - BKN", 6, false, null);
			AddWeatherPreset("Preset24", "Overcast 4 - BKN/OVC", 7, false, null);
			AddWeatherPreset("Preset25", "Overcast 5 - OVC", 8, false, null);
			AddWeatherPreset("Preset26", "Overcast 6 - OVC", 8, false, null);
			AddWeatherPreset("Preset27", "Overcast 7 - OVC", 8, false, null);
			AddWeatherPreset("RainyPreset1", "Rainy 1 - OVC", 8, true, 4000);
			AddWeatherPreset("RainyPreset2", "Rainy 2 - BKN/OVC", 7, true, 5000);
			AddWeatherPreset("RainyPreset3", "Rainy 3 - OVC", 8, true, 4000);
		}
	}
}

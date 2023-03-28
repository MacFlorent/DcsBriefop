using DcsBriefop.Tools;

namespace DcsBriefop.Data
{
	internal class Preferences
	{
		public PreferencesApplication Application { get; set; } = new PreferencesApplication();
		public PreferencesMission Mission { get; set; } = new PreferencesMission();
		public PreferencesMap Map { get; set; } = new PreferencesMap();
		public PreferencesBriefing Briefing { get; set; } = new PreferencesBriefing();

		public void InitializeDefault()
		{
			Application.InitializeDefault();
			Mission.InitializeDefault();
			Map.InitializeDefault();
			Briefing.InitializeDefault();
		}
	}

	internal class PreferencesApplication
	{
		public string WorkingDirectory { get; set; }
		public List<string> RecentMiz { get; set; } = new List<string>();
		public bool BackupBeforeOverwrite { get; set; } = true;
		public bool GenerateBatchCommandOnSave { get; set; } = true;

		public void InitializeDefault()
		{
			WorkingDirectory = Path.Combine(ToolsMisc.GetDirectoryDcsBetaSave(), "Missions");
			if (!Directory.Exists(WorkingDirectory))
				WorkingDirectory = Path.Combine(ToolsMisc.GetDirectoryDcsSave(), "Missions");
			if (!Directory.Exists(WorkingDirectory))
				WorkingDirectory = ".";
		}

		public void AddRecentMiz(string sRecentMizPath)
		{
			foreach (string s in RecentMiz.Where(s => string.Equals(s, sRecentMizPath, StringComparison.OrdinalIgnoreCase)).ToList())
				RecentMiz.Remove(s);

			RecentMiz.Remove(sRecentMizPath);
			if (RecentMiz.Count >= 10)
				RecentMiz.RemoveAt(RecentMiz.Count - 1);

			RecentMiz.Insert(0, sRecentMizPath);
		}
	}

	internal class PreferencesMission
	{
		public bool NoCallsignForPlayableFlights { get; set; } = true;
		public ElementBullseyeWaypoint BullseyeWaypoint { get; set; } = ElementBullseyeWaypoint.One;
		public void InitializeDefault() { }
	}

	internal class PreferencesMap
	{
		public string ProviderName { get; set; } = GMap.NET.MapProviders.BingMapProvider.Instance.Name;
		public double Zoom { get; set; } = 9;

		public void InitializeDefault() { }
	}

	internal class PreferencesBriefing
	{
		public ElementWeatherDisplay WeatherDisplay { get; set; } = ElementWeatherDisplay.Plain;
		public ElementMeasurementSystem MeasurementSystem { get; set; } = ElementMeasurementSystem.Imperial;
		public ElementCoordinateDisplay CoordinateDisplay { get; set; } = ElementCoordinateDisplay.Mgrs;
		public Size ImageSize { get; set; } = new Size(720, 1085);
		public bool GenerateOnSave { get; set; } = true;
		//public bool GenerateMiz { get; set; } = true;
		//public bool GenerateDirectory { get; set; } = false;
		public bool GenerateDirectoryHtml { get; set; } = false;
		//public string GenerateDirectoryName { get; set; } = "BriefopGenerated";

		public void InitializeDefault() { }
	}


}


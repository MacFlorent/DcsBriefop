using DcsBriefop.Data;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingFolder
	{
		#region Fields
		#endregion

		#region Properties
		public string Name { get; set; }
		public List<string> AircraftTypes { get; set; }
		public string CoalitionName { get; set; }
		public ElementWeatherDisplay WeatherDisplay { get; set; }
		public ElementMeasurementSystem MeasurementSystem { get; set; }
		public ElementCoordinateDisplay CoordinateDisplay { get; set; }
		public Size ImageSize { get; set; }

		public List<BopBriefingPage> Pages { get; set; } = new List<BopBriefingPage>();
		#endregion

		#region CTOR
		public BopBriefingFolder()
		{
			WeatherDisplay = PreferencesManager.Preferences.Briefing.WeatherDisplay;
			MeasurementSystem = PreferencesManager.Preferences.Briefing.MeasurementSystem;
			CoordinateDisplay = PreferencesManager.Preferences.Briefing.CoordinateDisplay;
			ImageSize = PreferencesManager.Preferences.Briefing.ImageSize;
		}
		#endregion

		#region Methods
		public BopBriefingPage AddPage()
		{
			BopBriefingPage bopBriefingPage = new BopBriefingPage();
			bopBriefingPage.Title = $"{Name}_{Pages.Count:000}";
			Pages.Add(bopBriefingPage);
			return bopBriefingPage;
		}
		#endregion

	}
}

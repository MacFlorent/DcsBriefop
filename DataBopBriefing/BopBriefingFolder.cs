using DcsBriefop.Data;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingFolder
	{
		#region Fields
		protected BopMission m_bopMission;
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
		public BopBriefingFolder(BopMission bopMission)
		{
			m_bopMission = bopMission;

			WeatherDisplay = PreferencesManager.Preferences.Briefing.WeatherDisplay;
			MeasurementSystem = PreferencesManager.Preferences.Briefing.MeasurementSystem;
			CoordinateDisplay = PreferencesManager.Preferences.Briefing.CoordinateDisplay;
			ImageSize = PreferencesManager.Preferences.Briefing.ImageSize;
		}
		#endregion

		#region Methods

		#endregion

	}
}

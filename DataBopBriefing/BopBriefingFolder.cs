using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using GMap.NET.WindowsForms;

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
		public BopBriefingPage AddPage(BopMission bopMission)
		{
			BopBriefingPage bopBriefingPage = new BopBriefingPage();
			bopBriefingPage.Title = $"{Name}_{Pages.Count:000}";

			bopBriefingPage.MapData.Zoom = PreferencesManager.Preferences.Map.Zoom;
			if (bopMission.Coalitions.TryGetValue(CoalitionName, out BopCoalition bopCoalition))
			{
				bopBriefingPage.MapData.CenterLatitude = bopCoalition.Bullseye.Latitude.DecimalDegree;
				bopBriefingPage.MapData.CenterLongitude = bopCoalition.Bullseye.Longitude.DecimalDegree;
			}
			else
			{
				Airdrome firstAirdrome = bopMission.Theatre.Airdromes.FirstOrDefault();
				if (firstAirdrome is object)
				{
					bopBriefingPage.MapData.CenterLatitude = firstAirdrome.Latitude;
					bopBriefingPage.MapData.CenterLongitude = firstAirdrome.Longitude;
				}
			}

			Pages.Add(bopBriefingPage);
			return bopBriefingPage;
		}
		#endregion

	}
}

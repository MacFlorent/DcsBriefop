using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Collections.Generic;

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

		public void OrderPage(BopBriefingPage bopBriefingPage, int iWay)
		{
			if (iWay < 0)
				iWay = -1;
			else
				iWay = 1;

			int iCurrentIndex = Pages.IndexOf(bopBriefingPage);
			if (iCurrentIndex < 0)
				return;

			int iNewIndex = iCurrentIndex + iWay;
			if (iNewIndex >= 0 && iNewIndex < Pages.Count)
			{
				Pages.Remove(bopBriefingPage);
				Pages.Insert(iNewIndex, bopBriefingPage);
			}
		}
		#endregion

		public async Task<List<BopBriefingGeneratedFile>> GenerateFiles(BopMission bopMission)
		{
			List<BopBriefingGeneratedFile> files = new();
			string sFolderName = Name;
			if (string.IsNullOrEmpty(sFolderName))
				sFolderName = $"{bopMission.BopBriefingFolders.IndexOf(this):000}";

			int iPage = 0;
			foreach(BopBriefingPage page in Pages)
			{
				string sPageFileName = $"{sFolderName}_{iPage:000}{page.Title}";

				if (page.Render.HasFlag(ElementBriefingPageRender.Html))
				{
					BopBriefingGeneratedFile file = new BopBriefingGeneratedFile();
					file.FileName = sPageFileName;
					//file.FileDirectories.AddRange(AircraftTypes);
					file.Image = await page.BuildHtmlImage(bopMission, this);
					file.Html = page.BuildHtmlString(bopMission, this);

					files.Add(file);
				}
				if (page.Render.HasFlag(ElementBriefingPageRender.Map))
				{
					BopBriefingGeneratedFile file = new BopBriefingGeneratedFile();
					file.FileName = $"{sPageFileName}_map";
					//file.FileDirectories.AddRange(AircraftTypes);
					file.Image = page.BuildMapImage(bopMission, this);

					files.Add(file);
				}

				iPage++;
			}

			return files;
		}

	}
}

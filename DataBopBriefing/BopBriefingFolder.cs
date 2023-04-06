using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingFolder : IEquatable<BopBriefingFolder>
	{
		#region Fields
		#endregion

		#region Properties
		public Guid Guid { get; set; }
		public string Name { get; set; }
		public List<string> Kneeboards { get; set; } = new List<string>();
		public string CoalitionName { get; set; }
		public ElementWeatherDisplay WeatherDisplay { get; set; }
		public ElementMeasurementSystem MeasurementSystem { get; set; }
		public ElementCoordinateDisplay CoordinateDisplay { get; set; }
		public Size ImageSize { get; set; }
		public bool Inactive { get; set; }
		public List<BopBriefingPage> Pages { get; set; } = new List<BopBriefingPage>();
		#endregion

		#region CTOR
		public BopBriefingFolder()
		{
			Guid = Guid.NewGuid();
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
			if (bopMission.Coalitions.TryGetValue(CoalitionName ?? "", out BopCoalition bopCoalition))
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

		#region Generation
		private IEnumerable<string> GetDirectories()
		{
			List<string> directories = new List<string>();
			if (Kneeboards is null || Kneeboards.Count <= 0)
				directories.Add("");
			else
				directories = Kneeboards;

			return directories;
		}

		public async Task<ListBopBriefingGeneratedFile> GenerateFiles(BriefopManager bopManager)
		{
			ListBopBriefingGeneratedFile files = new();
			string sFolderName = Name;
			if (string.IsNullOrEmpty(sFolderName))
				sFolderName = $"{bopManager.BopMission.BopBriefingFolders.IndexOf(this):000}";

			IEnumerable<string> kneeboards = GetDirectories();

			int iPage = 0;
			foreach(BopBriefingPage page in Pages)
			{
				string sPageFileName = $"{sFolderName}_{iPage:000}{page.Title}";
				sPageFileName = ToolsMisc.SanitizeFileName(sPageFileName);

				if (page.Render.HasFlag(ElementBriefingPageRender.Html))
				{
					BopBriefingGeneratedFile file = new();
					file.FileName = sPageFileName;
					file.Kneeboards.AddRange(kneeboards);
					file.Image = await page.BuildHtmlImage(bopManager, this);
					file.Html = page.BuildHtmlString(bopManager, this);

					files.Add(file);
				}
				if (page.Render.HasFlag(ElementBriefingPageRender.Map))
				{
					BopBriefingGeneratedFile file = new BopBriefingGeneratedFile();
					file.FileName = $"{sPageFileName}_map";
					file.Kneeboards.AddRange(kneeboards);
					file.Image = page.BuildMapImage(bopManager, this);

					files.Add(file);
				}

				iPage++;
			}

			return files;
		}
		#endregion

		#region IEquatable
		public bool Equals(BopBriefingFolder other)
		{
			if (other is null)
				return false;

			return (Guid == other.Guid);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BopBriefingFolder);
		}

		public override int GetHashCode() => Guid.GetHashCode();
		#endregion
	}
}

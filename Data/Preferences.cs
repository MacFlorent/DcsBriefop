using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace DcsBriefop.Data
{
	internal class Preferences
	{
		public PreferencesApplication Application { get; set; } = new PreferencesApplication();
		public PreferencesMission Mission { get; set; } = new PreferencesMission();
		//public PreferencesBriefing Briefing { get; set; } = new PreferencesBriefing();
		public PreferencesMap Map { get; set; } = new PreferencesMap();
		public void InitializeDefault()
		{
			Application.InitializeDefault();
			Mission.InitializeDefault();
			Map.InitializeDefault();
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
		public ElementWeatherDisplay WeatherDisplay { get; set; } = ElementWeatherDisplay.Plain;
		public ElementCoordinateDisplay CoordinateDisplay { get; set; } = ElementCoordinateDisplay.Mgrs;

		public void InitializeDefault() { }
	}

	//internal class PreferencesBriefing
	//{
	//	public bool ExportOnSave { get; set; } = true;
	//	public bool ExportMiz { get; set; } = true;
	//	public bool ExportLocalDirectory { get; set; } = true;
	//	public bool ExportLocalDirectoryHtml { get; set; } = false;
	//	public string ExportLocalDirectoryPath { get; set; }
	//	public Size ExportImageSize { get; set; } = new Size(720, 1085);
	//	public string ExportImageBackgroundColor { get; set; }
	//	public List<ElementExportFileType> ExportFileTypes { get; set; } = new List<ElementExportFileType>();

	//	public void InitializeDefault()
	//	{
	//		ExportImageBackgroundColor = ColorTranslator.ToHtml(Color.Black);

	//		ExportFileTypes.Clear();
	//		ExportFileTypes.Add(ElementExportFileType.Operations);
	//		ExportFileTypes.Add(ElementExportFileType.Opposition);
	//		ExportFileTypes.Add(ElementExportFileType.Missions);
	//	}
	//}

	internal class PreferencesMap
	{
		public string DefaultProvider { get; set; } = GMap.NET.MapProviders.BingMapProvider.Instance.Name;
		public double DefaultZoom { get; set; } = 9;

		public void InitializeDefault() { }
	}
}


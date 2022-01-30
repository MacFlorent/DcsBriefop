using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DcsBriefop.Tools
{
	internal static class ToolsSettings
	{
		public static class ElementSetting
		{
			public static readonly string WorkingDirectory = "workingDirectory";
		}


		public static string ReadSetting(string sKey)
		{
			return ConfigurationManager.AppSettings[sKey];
		}

		public static void AddUpdateAppSettings(string sKey, string sValue)
		{
			var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			KeyValueConfigurationCollection settings = configFile.AppSettings.Settings;

			if (settings[sKey] is null)
			{
				settings.Add(sKey, sValue);
			}
			else
			{
				settings[sKey].Value = sValue;
			}

			configFile.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}

		public static void SetWorkingDirectory(string sDirectory)
		{
			AddUpdateAppSettings (ElementSetting.WorkingDirectory, sDirectory);
		}

		public static string GetWorkingDirectory()
		{
			string sDirectory = ReadSetting(ElementSetting.WorkingDirectory);

			if (string.IsNullOrEmpty(sDirectory))
			{
				sDirectory = Path.Combine(ToolsMisc.GetDirectoryDcsBetaSave(), "Missions");
				if (!Directory.Exists(sDirectory))
					sDirectory = Path.Combine(ToolsMisc.GetDirectoryDcsSave(), "Missions");
				if (!Directory.Exists(sDirectory))
					sDirectory = ".";
			}

			return ToolsMisc.GetDirectoryFullPath(sDirectory);
		}
	}
}

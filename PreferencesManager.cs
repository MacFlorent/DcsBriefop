﻿using DcsBriefop.Data;
using DcsBriefop.Tools;
using Newtonsoft.Json;
using System.IO;

namespace DcsBriefop
{
	internal class PreferencesManager
	{
		private static readonly string m_sPreferencesResourceName = "preferences";
		private static Preferences m_preferences;

		public static Preferences Preferences
		{
			get
			{
				if (m_preferences is null)
					Load();

				return m_preferences;
			}
			set { m_preferences = value; }
		}

		public static void Load()
		{
			string sJsonStream = ToolsResources.GetJsonResourceContent(m_sPreferencesResourceName);
			if (string.IsNullOrEmpty(sJsonStream))
			{
				m_preferences = new Preferences();
				m_preferences.InitializeDefault();
			}
			else
			{
				m_preferences = JsonConvert.DeserializeObject<Preferences>(sJsonStream);
			}
		}

		public static void Save()
		{
			string sResourceFilePath = ToolsResources.GetResourceFileFullPath(m_sPreferencesResourceName, "json");
			File.WriteAllText(sResourceFilePath, JsonConvert.SerializeObject(Preferences, Formatting.Indented));
		}
	}
}

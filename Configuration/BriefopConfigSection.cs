using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopConfigSection : ConfigurationSection
	{
		public BriefopConfigSection() { }

		[ConfigurationProperty("baseDirectoryMarkers", DefaultValue = @".\markers")]
		public string BaseDirectoryMarkers
		{
			get { return this["baseDirectoryMarkers"] as string; }
			set { this["baseDirectoryMarkers"] = value; }
		}

		[ConfigurationProperty("baseDirectoryRoutes", DefaultValue = @".\routes")]
		public string BaseDirectoryRoutes
		{
			get { return this["baseDirectoryRoutes"] as string; }
			set { this["baseDirectoryRoutes"] = value; }
		}

		[ConfigurationProperty("briefopMarkers")]
		public BriefopMarkersElement BriefopMarkers
		{
			get { return this["briefopMarkers"] as BriefopMarkersElement; }
			set { this["briefopMarkers"] = value; }
		}

		public static BriefopConfigSection GetConfigSection()
		{
			return ConfigurationManager.GetSection("briefopConfig") as BriefopConfigSection;
		}
	}
}

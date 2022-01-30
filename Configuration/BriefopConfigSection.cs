using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopConfigSection : ConfigurationSection
	{
		public BriefopConfigSection() { }

		[ConfigurationProperty("workingDirectory", DefaultValue = "")]
		public string WorkingDirectory
		{
			get { return this["workingDirectory"] as string; }
			set { this["workingDirectory"] = value; }
		}

		public static BriefopConfigSection GetConfigSection()
		{
			return ConfigurationManager.GetSection("briefopConfig") as BriefopConfigSection;
		}
	}
}

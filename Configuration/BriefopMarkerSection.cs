using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopMarkerSection : ConfigurationSection
	{
		public BriefopMarkerSection() { }

		[ConfigurationProperty("baseDirectory", DefaultValue = @".\markers")]
		public string BaseDirectory
		{
			get { return this["baseDirectory"] as string; }
			set { this["baseDirectory"] = value; }
		}

		[ConfigurationProperty("defaultWidth", DefaultValue = 32)]
		public int DefaultWidth
		{
			get { return (int)this["defaultWidth"]; }
			set { this["defaultWidth"] = value; }
		}

		[ConfigurationProperty("defaultHeight", DefaultValue = 32)]
		public int DefaultHeight
		{
			get { return (int)this["defaultHeight"]; }
			set { this["defaultHeight"] = value; }
		}

		[ConfigurationProperty("markerConfigs", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(BriefopMarkerCollection), AddItemName = "add")]
		public BriefopMarkerCollection MarkerConfigs
		{
			get { return this["markerConfigs"] as BriefopMarkerCollection; }
		}

		public static BriefopMarkerSection GetMarkerSection()
		{
			return ConfigurationManager.GetSection("briefopMarkers") as BriefopMarkerSection;
		}
	}
}

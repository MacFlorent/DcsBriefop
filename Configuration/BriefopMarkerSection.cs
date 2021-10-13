using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopMarkerSection : ConfigurationSection
	{
		public BriefopMarkerSection() { }

		[ConfigurationProperty("baseDirectory", DefaultValue = @".\markers", IsRequired = true)]
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

		[ConfigurationCollection(typeof(BriefopMarkerCollection), AddItemName = "add")]
		public BriefopMarkerCollection Markers
		{
			get { return this["MarkerConfigs"] as BriefopMarkerCollection; }
		}

		public static BriefopMarkerSection GetMarkerSection()
		{
			return ConfigurationManager.GetSection("BriefopMarkers") as BriefopMarkerSection;
		}
	}
}

using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopMarkersElement : ConfigurationElement
	{
		public BriefopMarkersElement() { }

		[ConfigurationProperty("defaultWidth", DefaultValue = 24)]
		public int DefaultWidth
		{
			get { return (int)this["defaultWidth"]; }
			set { this["defaultWidth"] = value; }
		}

		[ConfigurationProperty("defaultHeight", DefaultValue = 24)]
		public int DefaultHeight
		{
			get { return (int)this["defaultHeight"]; }
			set { this["defaultHeight"] = value; }
		}

		[ConfigurationProperty("markerConfigsList", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(BriefopMarkerCollection), AddItemName = "add")]
		public BriefopMarkerCollection MarkerConfigsList
		{
			get { return this["markerConfigsList"] as BriefopMarkerCollection; }
		}
	}
}

using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopMarkerElement : ConfigurationElement
	{
		public BriefopMarkerElement() { }

		[ConfigurationProperty("fileName", IsRequired = true, IsKey = true)]
		public string FileName
		{
			get { return (string)this["fileName"]; }
			set { this["fileName"] = value; }
		}

		[ConfigurationProperty("width")]
		public int? Width
		{
			get { return this["width"] as int?; }
			set { this["width"] = value; }
		}

		[ConfigurationProperty("height")]
		public int? Height
		{
			get { return this["height"] as int?; }
			set { this["height"] = value; }
		}

		[ConfigurationProperty("offsetWidth")]
		public double? OffsetWidth
		{
			get { return this["offsetWidth"] as double?; }
			set { this["offsetWidth"] = value; }
		}

		[ConfigurationProperty("offsetHeight")]
		public double? OffsetHeight
		{
			get { return this["offsetHeight"] as double?; }
			set { this["offsetHeight"] = value; }
		}

	}
}

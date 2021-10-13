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

		public int Width
		{
			get { return (int)this["width"]; }
			set { this["width"] = value; }
		}

		public int Height
		{
			get { return (int)this["height"]; }
			set { this["height"] = value; }
		}

		public double OffsetWidth
		{
			get { return (double)this["offsetWidth"]; }
			set { this["offsetWidth"] = value; }
		}

		public double OffsetHeight
		{
			get { return (double)this["offsetHeight"]; }
			set { this["offsetHeight"] = value; }
		}

	}
}

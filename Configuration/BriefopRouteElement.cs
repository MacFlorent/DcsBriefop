using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopRouteElement : ConfigurationElement
	{
		public BriefopRouteElement() { }

		[ConfigurationProperty("fileName", IsRequired = true, IsKey = true)]
		public string FileName
		{
			get { return (string)this["fileName"]; }
			set { this["fileName"] = value; }
		}

		[ConfigurationProperty("thicknessCorrection")]
		public decimal? ThicknessCorrection
		{
			get { return this["thicknessCorrection"] as decimal?; }
			set { this["thicknessCorrection"] = value; }
		}

		[ConfigurationProperty("dcsMizStyle")]
		public string DcsMizStyle
		{
			get { return (string)this["dcsMizStyle"]; }
			set { this["dcsMizStyle"] = value; }
		}
	}
}

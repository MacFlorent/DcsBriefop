using System.Configuration;

namespace DcsBriefop.Configuration
{
	internal class BriefopRoutesElement : ConfigurationElement
	{
		public BriefopRoutesElement() { }

		[ConfigurationProperty("defaultThicknessCorrection", DefaultValue = "1")]
		public decimal DefaultThicknessCorrection
		{
			get { return (decimal)this["defaultThicknessCorrection"]; }
			set { this["defaultThicknessCorrection"] = value; }
		}

		[ConfigurationProperty("routeConfigsList", IsDefaultCollection = false)]
		[ConfigurationCollection(typeof(BriefopRouteCollection), AddItemName = "add")]
		public BriefopRouteCollection MarkerConfigsList
		{
			get { return this["routeConfigsList"] as BriefopRouteCollection; }
		}
	}
}

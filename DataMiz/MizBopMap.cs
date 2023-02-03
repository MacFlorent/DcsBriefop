using GMap.NET.WindowsForms;

namespace DcsBriefop.DataMiz
{
	internal class MizBopMap
	{
		public string DisplayName { get; set; }
		public string Provider { get; set; }
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }
		public GMapOverlay MapOverlay { get; set; }
	}
}

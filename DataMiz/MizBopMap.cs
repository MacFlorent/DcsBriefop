using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace DcsBriefop.DataMiz
{
	internal class MizBopMap
	{
		public string Provider { get; set; }
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }
		public GMapOverlay MapOverlay { get; set; } = new GMapOverlay();

		public GMapProvider GetProvider()
		{
			string sMapProvider = Provider;
			if (string.IsNullOrEmpty(sMapProvider))
				sMapProvider = PreferencesManager.Preferences.Map.DefaultProvider;

			return GMapProviders.TryGetProvider(sMapProvider);
		}
	}
}

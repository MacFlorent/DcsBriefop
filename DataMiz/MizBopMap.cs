using GMap.NET.WindowsForms;

namespace DcsBriefop.DataMiz
{
	internal class MizBopMap
	{
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }
		public List<GMapMarker> CustomMarkers { get; set; } = new();

		public GMapOverlay BuildCustomMapOverlay()
		{
			GMapOverlay customMapOverlay = new GMapOverlay();
			foreach(GMapMarker marker in CustomMarkers)
			{
				customMapOverlay.Markers.Add(marker);
			}

			return customMapOverlay;
		}

		public void FromCustomMapOverlay (GMapOverlay customMapOverlay)
		{
			CustomMarkers.Clear();

			if (customMapOverlay is not null)
			{
				foreach (GMapMarker marker in customMapOverlay.Markers)
				{
					GMapMarker 
					CustomMarkers.Add(marker);
				}
			}
		}
	}
}

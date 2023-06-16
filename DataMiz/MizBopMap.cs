using DcsBriefop.Map;
using GMap.NET.WindowsForms;

namespace DcsBriefop.DataMiz
{
	internal class MizBopMap
	{
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }
		public List<GMarkerBriefop> CustomMarkers { get; set; } = new();

		public GMapOverlay BuildCustomMapOverlay()
		{
			GMapOverlay customMapOverlay = new GMapOverlay();
			foreach(GMarkerBriefop marker in CustomMarkers)
			{
				customMapOverlay.Markers.Add(marker.NewCleanCopy());
			}

			return customMapOverlay;
		}

		public void FromCustomMapOverlay (GMapOverlay customMapOverlay)
		{
			CustomMarkers.Clear();

			if (customMapOverlay is not null)
			{
				foreach (GMarkerBriefop marker in customMapOverlay.Markers.OfType<GMarkerBriefop>())
				{
					CustomMarkers.Add(marker.NewCleanCopy());
				}
			}
		}
	}
}

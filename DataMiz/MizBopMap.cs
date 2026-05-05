using DcsBriefop.Map;
using GMap.NET;
using GMap.NET.WindowsForms;
using Mapsui;
using Mapsui.Layers;

namespace DcsBriefop.DataMiz
{
	internal class MizBopMap
	{
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }
		public List<BriefopMarker> CustomMarkers { get; set; } = new();

		public MemoryLayer BuildCustomLayer()
		{
			MemoryLayer layer = new("CustomMarkers");
			List<IFeature> features = new();
			foreach (BriefopMarker marker in CustomMarkers)
			{
				PointFeature feature = new(MapProjection.ToMPoint(marker.Position));
				feature.Styles.Add(new BriefopMarkerStyle(marker));
				features.Add(feature);
			}
			layer.Features = features;
			return layer;
		}

		// TODO Phase 4: replace with Mapsui-based image generation and remove GMapOverlay dependency
		public GMapOverlay BuildCustomMapOverlay()
		{
			GMapOverlay overlay = new();
			foreach (BriefopMarker marker in CustomMarkers)
			{
				PointLatLng pos = new(marker.Position.Latitude, marker.Position.Longitude);
				overlay.Markers.Add(GMarkerBriefop.NewFromTemplateName(pos, marker.TemplateName, marker.TintColor, marker.Label, marker.Scale, marker.Angle));
			}
			return overlay;
		}
	}
}

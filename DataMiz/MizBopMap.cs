using DcsBriefop.Map;
using Mapsui;
using Mapsui.Layers;

namespace DcsBriefop.DataMiz
{
	internal class MizBopMap
	{
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }
		public List<BriefopMarker> CustomMarkers { get; set; } = [];

		public MemoryLayer BuildCustomMapLayer()
		{
			MemoryLayer layer = new("CustomMarkers") { Style = null };
			List<IFeature> features = new();
			foreach (BriefopMarker marker in CustomMarkers)
			{
				PointFeature mapFeature = new(MapProjection.ToMPoint(marker.Position));
				mapFeature.Styles.Add(new BriefopMarkerStyle(marker));
				features.Add(mapFeature);
			}
			layer.Features = features;
			return layer;
		}

	}
}

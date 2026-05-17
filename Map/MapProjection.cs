using DcsBriefop.Data;
using Mapsui;
using Mapsui.Projections;

namespace DcsBriefop.Map
{
	internal static class MapProjection
	{
		#region Fields
		// Web Mercator (EPSG:3857): resolution = world-width-in-metres / (256 * 2^zoom)
		private static readonly double[] s_zoomResolutions = [.. Enumerable.Range(0, 21).Select(z => 156543.033928 / Math.Pow(2, z))];
		#endregion

		#region Methods
		public static MPoint ToMPoint(GeoPoint point)
		{
			(double x, double y) = SphericalMercator.FromLonLat(point.Longitude, point.Latitude);
			return new MPoint(x, y);
		}

		public static MPoint ToMPoint(double dLat, double dLng)
		{
			(double x, double y) = SphericalMercator.FromLonLat(dLng, dLat);
			return new MPoint(x, y);
		}

		public static GeoPoint ToGeoPoint(MPoint point)
		{
			(double dLng, double dLat) = SphericalMercator.ToLonLat(point.X, point.Y);
			return new GeoPoint(dLat, dLng);
		}

		public static double ZoomToResolution(int iZoom)
		{
			return s_zoomResolutions[Math.Clamp(iZoom, 0, 20)];
		}

		public static int ResolutionToZoom(double dResolution)
		{
			for (int i = 0; i < s_zoomResolutions.Length - 1; i++)
			{
				double midpoint = (s_zoomResolutions[i] + s_zoomResolutions[i + 1]) / 2.0;
				if (dResolution > midpoint)
					return i;
			}
			return 20;
		}

		public static GeoPoint ScreenToGeoPoint(Viewport viewport, double dScreenX, double dScreenY)
		{
			// Manual viewport transform (rotation not considered — always 0 in this app)
			double dWorldX = viewport.CenterX + (dScreenX - viewport.Width / 2.0) * viewport.Resolution;
			double dWorldY = viewport.CenterY - (dScreenY - viewport.Height / 2.0) * viewport.Resolution;
			return ToGeoPoint(new MPoint(dWorldX, dWorldY));
		}
		#endregion
	}
}
